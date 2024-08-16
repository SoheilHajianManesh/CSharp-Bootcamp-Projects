using FinalProject.Service;

namespace FinalProject;

public class Program
{
    public static async Task Main()
    {
        LoyaltyClubHandler.CalculateLoyaltyClubPoints();
        await LoyaltyClubHandler.CalculateLoyaltyClubPointsAsync();
    }
}