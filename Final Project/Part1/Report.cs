using FinalProject.Common;
using FinalProject.Data;
using Microsoft.EntityFrameworkCore;

namespace Part1;

public static class Report
{
    private static readonly string ProjectRootDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName;

    private static readonly string BasePath = ProjectRootDirectory + "/Part1/Reports/";
    
    public static void GetAllReports()
    {
        CalculateInvoiceTotalSummery();
        SelectRandom100Tracks();
        SelectTop5TracksWithHighestSales();
        GetCustomersWithInvoiceItemsUnderTwoDollars();
        GetTop5LoyalCustomersBasedOnTotalPurchaseAmount();
        GetEmployeesWithTheirsRespectiveManagers();
        CalculateAverageSalesPerMonth();
        GetTop100GenresForEachCountry();
        GetTop100TracksBasedOnCustomerPreferencesAndPurchases();
        GetCutomersWithSummeryOfTheirLastInvoice();
    }

    private static void CalculateInvoiceTotalSummery()
    {
        using IDbContext dbContext = new AppDbContext();
        var summery = new
        {
            Count = dbContext.Invoices.Count(),
            TotalSum = dbContext.Invoices.Sum(i => i.Total),
            TotalAverage = dbContext.Invoices.Average(i => i.Total)
        };

        using var writer = new StreamWriter($"{BasePath}query1.txt");
        writer.WriteLine(
            "Retrieve the count, sum, and average of the total amounts from invoices using a single query.");
        writer.WriteLine($"Count = {summery.Count} \nSum = {summery.TotalSum} \nAverage = summery.TotalAverage");
    }

    private static void SelectRandom100Tracks() 
    {
        using IDbContext dbContext = new AppDbContext();
        var random100Track = dbContext.Tracks
            .OrderBy(t => EF.Functions.Random())
            .Take(100);

        using var writer = new StreamWriter($"{BasePath}query2.txt");
        writer.WriteLine("Fetch a random selection of the top 100 tracks.");
        foreach (var track in random100Track)
        {
            writer.WriteLine($"Track Id = {track.TrackId} - Track Name = {track.Name}");
        }
    }

    private static void SelectTop5TracksWithHighestSales()
    {
        using IDbContext dbContext = new AppDbContext();
        var top5TrackWithHighestSales = dbContext.Tracks
            .OrderByDescending(track => track.InvoiceLines.Sum(line => line.UnitPrice * line.Quantity))
            .Take(5);

        using var writer = new StreamWriter($"{BasePath}query3.txt");
        writer.WriteLine("Retrieve the top 5 tracks that have the highest sales.");
        foreach (var track in top5TrackWithHighestSales)
        {
            writer.WriteLine($"Track Id = {track.TrackId} - Track Name = {track.Name}");
        }
    }

    private static void GetCustomersWithInvoiceItemsUnderTwoDollars() 
    {
        using IDbContext dbContext = new AppDbContext();
        var customersWithInvoiceItemsUnderTwoDollars = dbContext.Customers
            .Join(dbContext.Invoices,
                customer => customer.CustomerId,
                invoice => invoice.CustomerId,
                ((customer, invoice) => new {customer, invoice.InvoiceId}))
            .Join(dbContext.InvoiceLines,
                combined => combined.InvoiceId,
                line => line.InvoiceId,
                (combined, line) => new {combined.customer, line.Quantity, line.UnitPrice})
            .Where(customersWithInvoiceItemsInfo =>
                (customersWithInvoiceItemsInfo.Quantity * customersWithInvoiceItemsInfo.UnitPrice < 2))
            .Distinct()
            .Select(customersWithInvoiceItemsInfo => customersWithInvoiceItemsInfo.customer);

        using var writer = new StreamWriter($"{BasePath}query4.txt");
        writer.WriteLine(
            "Obtain all customers with invoices containing at least one line item priced less than $2.");
        foreach (var customer in customersWithInvoiceItemsUnderTwoDollars)
        {
            writer.WriteLine(
                $"Customer Id = {customer.CustomerId} - First Name = {customer.FirstName} - Last Name = {customer.LastName}");
        }
    }

    private static void GetTop5LoyalCustomersBasedOnTotalPurchaseAmount() 
    {
        using IDbContext dbContext = new AppDbContext();
        var top5LoyalCustomers = dbContext.Customers
            .OrderByDescending(customer => customer.Invoices.Sum(invoice => invoice.Total))
            .Take(5);

        using var writer = new StreamWriter($"{BasePath}query5.txt");
        writer.WriteLine(
            "Retrieve the top 5 loyal customers based on the total purchase amount across all their invoices.");
        foreach (var customer in top5LoyalCustomers)
        {
            writer.WriteLine(
                $"Customer Id = {customer.CustomerId} - First Name = {customer.FirstName} - Last Name = {customer.LastName}");
        }
    }

    private static void GetEmployeesWithTheirsRespectiveManagers()
    {
        using IDbContext dbContext = new AppDbContext();
        var employeesWithTheirManagers = dbContext.Employees
            .Select(employee => new
                {
                    employee,
                    manager = employee.ReportsToNavigation
                }
            );

        using var writer = new StreamWriter($"{BasePath}query6.txt");
        writer.WriteLine("Retrieve a list of employees along with their respective managers.");
        foreach (var employee in employeesWithTheirManagers)
        {
            writer.WriteLine(
                $"Employee Id = {employee.employee.EmployeeId} - Name = {employee.employee.FirstName} : "
                + ((employee.manager != null)
                    ? $"Manager Id = {employee.manager.EmployeeId} - Name = {employee.manager.FirstName}"
                    : "null"));
        }
    }

    private static void CalculateAverageSalesPerMonth()
    {
        using IDbContext dbContext = new AppDbContext();
        var monthWithTheirAverageSales = dbContext.Invoices
            .GroupBy(invoice => invoice.InvoiceDate.Month)
            .Select(group => new
            {
                month = group.Key,
                averageSales = group.Average(i => i.Total)
            });

        using var writer = new StreamWriter($"{BasePath}query7.txt");
        writer.WriteLine("Calculate the average sales per month.");
        foreach (var month in monthWithTheirAverageSales)
        {
            writer.WriteLine($"Month {month.month} : Average Sales{month.averageSales}");
        }
    }

    private static void GetTop100GenresForEachCountry() 
    {
        using IDbContext context = new AppDbContext();
        var invoicesGroupedByCountry = context.Invoices
            .Join(context.InvoiceLines, invoice => invoice.InvoiceId, line => line.InvoiceId,
                ((invoice, line) => new {invoice.BillingCountry, line}))
            .Join(context.Tracks, combined => combined.line.TrackId, track => track.TrackId,
                (combined, track) => new {combined.BillingCountry, combined.line, track.GenreId})
            .Join(context.Genres, combined => combined.GenreId, genre => genre.GenreId,
                (combined, genre) => new {combined.BillingCountry, combined.line, genre})
            .GroupBy(combined => new {combined.BillingCountry});
        
        // Use AsEnumerable and in-memory processing to group invoices of each country by genre 
        var countriesWithTheirTop100Genres = invoicesGroupedByCountry
            .AsEnumerable()
            .Select(group => new
            {
                Country = group.Key,
                Top100Genre = group.GroupBy(g => g.genre)
                    .Select(i => new
                    {
                        Genre = i.Key,
                        totalSale = i.Sum(a => a.line.Quantity)
                    })
                    .OrderByDescending(genre => genre.totalSale)
                    .Take(100)
            });

        using var writer = new StreamWriter($"{BasePath}query8.txt");
        writer.WriteLine("Fetch the top 100 most popular genres for each country.");
        foreach (var country in countriesWithTheirTop100Genres)
        {
            writer.WriteLine($"{country.Country} : ");
            foreach (var item in country.Top100Genre)
                writer.WriteLine($"     {item.Genre.Name} - Total Sales = {item.totalSale}");
        }
    }

    private static void GetTop100TracksBasedOnCustomerPreferencesAndPurchases()
    {
        using var dbContext = new AppDbContext();
        var tracksWithCustomersIdAndPurchasesDetails = dbContext.Tracks
                .Join(dbContext.InvoiceLines, track => track.TrackId, line => line.TrackId,
                    (track, line) => new {track, line.InvoiceId, line.Quantity, line.UnitPrice})
                .Join(dbContext.Invoices, combined => combined.InvoiceId, invoice => invoice.InvoiceId,
                    (combined, invoice) => new {combined.track, invoice.CustomerId, combined.UnitPrice, combined.Quantity})
            ;
        var top100Tracks = tracksWithCustomersIdAndPurchasesDetails
            .GroupBy(line => line.track)
            .Select(group => new
            {
                Track = group.Key,
                DistinctCustomerBuyed = group.Select(x => x.CustomerId).Distinct().Count(),
                TotalPurchased = group.Select(x => x.Quantity * x.UnitPrice).Sum()
            })
            .OrderByDescending(trackPurchasedDetails => trackPurchasedDetails.DistinctCustomerBuyed)
            .ThenByDescending(trackPurchasedDetails => trackPurchasedDetails.TotalPurchased)
            .Take(100);

        using var writer = new StreamWriter($"{BasePath}query9.txt");
        writer.WriteLine("Retrieve the top 100 most popular tracks based on customer preferences and purchases.");
        foreach (var track in top100Tracks)
        {
            writer.WriteLine(
                $" Track Id = {track.Track.TrackId} - Track Name = {track.Track.Name} : Distinct Customer Buy  = {track.DistinctCustomerBuyed} , Total Purchased = {track.TotalPurchased}");
        }
    }

    private static void GetCutomersWithSummeryOfTheirLastInvoice()
    {
        using var dbContext = new AppDbContext();
        var customersWithTheirLastInvoice = dbContext.Customers
            .Select(customer => new
            {
                customer,
                lastInvoice = customer.Invoices.OrderByDescending(invoice => invoice.InvoiceDate).FirstOrDefault()
            });

        using var writer = new StreamWriter($"{BasePath}query10.txt");
        writer.WriteLine(
            "Obtain a list of all customers and details of their last invoice,displaying null when a customer has not made any purchases.");
        foreach (var customer in customersWithTheirLastInvoice)
        {
            writer.WriteLine(
                $"Customer Id = {customer.customer.CustomerId} - First Name = {customer.customer.FirstName} - " +
                $"Last Name = {customer.customer.LastName} : Last Invoice Id = {customer.lastInvoice?.InvoiceId} - " +
                $"Last Invoice Date = {customer.lastInvoice?.InvoiceDate}");
        }
    }
}