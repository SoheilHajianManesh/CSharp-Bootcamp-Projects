using FinalProject.Common;
using FinalProject.Common.Entities;
using Plugin.Common;

namespace Plugin.SeasonalBonus;

public class SeasonalBonus : ILoyaltyClubPoint
{
    public Dictionary<int, int> CalculateLoyaltyClubPoints(IDbContext dbContext)
    {
        var invoiceInSpring = dbContext.Invoices
            .Where(invoice => invoice.InvoiceDate.Month >= 3 && invoice.InvoiceDate.Month <= 5);
        var customersWithTotalPoints = invoiceInSpring
            .GroupBy(invoice => invoice.CustomerId)
            .Select(group => new
            {
                CustomerId = group.Key,
                TotalPoint = group.Select(invoice => invoice.InvoiceLines.Sum(line => line.Quantity)).Sum() * 50
            }).ToDictionary(arg => arg.CustomerId, arg => arg.TotalPoint);
        return customersWithTotalPoints;
    }
}
