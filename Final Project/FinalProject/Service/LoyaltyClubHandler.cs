using FinalProject.Common;
using FinalProject.Common.Entities;
using FinalProject.Data;
using Plugin.Common;

namespace FinalProject.Service;

public static class LoyaltyClubHandler
{
    private static Dictionary<Customer, int> _loyaltyClubPoints = new() { };

    private static readonly string ProjectRootDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName;

    private static readonly string MonthlyPurchasedPointPluginPath =
        ProjectRootDirectory + "/Plugin.MonthlyPurchasedPoint/bin/Debug/net8.0/Plugin.MonthlyPurchasedPoint.dll";

    private static readonly string SeasonalBonusPluginPath =
        ProjectRootDirectory + "/Plugin.SeasonalBonus/bin/Debug/net8.0/Plugin.SeasonalBonus.dll";
    
    private static readonly string ReportFilePathForSequentialAlgorithm = ProjectRootDirectory + "/FinalProject/Reports/LoyaltyClub.txt";

    private static readonly string ReportFilePathForAsyncAlgorithm = ProjectRootDirectory + "/FinalProject/Reports/LoyaltyClubAsync.txt";

    private static readonly object _lock = new();


    public static void CalculateLoyaltyClubPoints()
    {
        using var dbContext = new AppDbContext();
        _loyaltyClubPoints = dbContext.Customers.ToDictionary(customer => customer, customer => 0);
        LoadAndExecutePlugin(MonthlyPurchasedPointPluginPath, true, dbContext);
        LoadAndExecutePlugin(SeasonalBonusPluginPath, true, dbContext);
        WritePointsInFile(ReportFilePathForSequentialAlgorithm);
    }
    public static async Task CalculateLoyaltyClubPointsAsync()
    {
        var dbContext = new AppDbContext();
        _loyaltyClubPoints = dbContext.Customers.ToDictionary(customer => customer, customer => 0);
        var task1 = Task.Run(() => LoadAndExecutePlugin(MonthlyPurchasedPointPluginPath, true, dbContext));
        var task2 = Task.Run(() => LoadAndExecutePlugin(SeasonalBonusPluginPath, true, new AppDbContext()));
        await Task.WhenAll(task1, task2);
        WritePointsInFile(ReportFilePathForAsyncAlgorithm);
    }
    private static void LoadAndExecutePlugin(string pluginPath, bool isCollectible, IDbContext dbContext)
    {
        var alc = new PluginLoadContext(pluginPath, isCollectible);
        try
        {
            var assembly = alc.LoadFromAssemblyPath(pluginPath);

            var pluginType = assembly.ExportedTypes.Single(type => typeof(ILoyaltyClubPoint).IsAssignableFrom(type));
            var plugin = (ILoyaltyClubPoint) Activator.CreateInstance(pluginType)!;
            var points = plugin.CalculateLoyaltyClubPoints(dbContext);
            AddPointsToClub(points);
        }
        finally
        {
            if (isCollectible)
                alc.Unload();
        }
    }

    private static void AddPointsToClub(Dictionary<int, int> points)
    {
        foreach (var (customerId, point) in points)
        {
            var customer = _loyaltyClubPoints.Keys.Single(customer1 => customer1.CustomerId == customerId);
            lock (_lock)
            {
                _loyaltyClubPoints[customer] += point;
            }
        }
    }

    private static void WritePointsInFile(string filePath)
    {
        using var writer = new StreamWriter(filePath);
        writer.WriteLine("LOYALTY CLUB POINTS");
        foreach (var (customer, point) in _loyaltyClubPoints)
        {
            writer.WriteLine(
                $"Id = {customer.CustomerId}, First Name = {customer.FirstName}, Last Name = {customer.LastName} : Point = {point}");
        }
    }
}