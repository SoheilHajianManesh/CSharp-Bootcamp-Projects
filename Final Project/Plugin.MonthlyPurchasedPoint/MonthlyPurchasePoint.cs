using FinalProject.Common;
using FinalProject.Common.Entities;
using Plugin.Common;

namespace MonthlyPurchasedPoint;

public class MonthlyPurchasedPoint : ILoyaltyClubPoint
{
    public Dictionary<int, int> CalculateLoyaltyClubPoints(IDbContext dbContext)
    {
        var invoiceDetails = dbContext.Invoices
            .Select(invoice => new
            {
                Cid = invoice.CustomerId,
                Year = invoice.InvoiceDate.Year,
                Month = invoice.InvoiceDate.Month,
                Quantity = invoice.InvoiceLines.Sum(line => line.Quantity)
            });
        var customersTotalPurchasedEachYearAndMonth = invoiceDetails
            .GroupBy(details => new
            {
                Cid = details.Cid, details.Year, details.Month,
            })
            .Select(group => new
            {
                Cid = group.Key.Cid,
                Year = group.Key.Year,
                Month = group.Key.Month,
                TotalQuantity = group.Sum(details => details.Quantity)
            });
        var customersEarnedPointEachYearAndMonth = customersTotalPurchasedEachYearAndMonth
            .Select(arg => new
            {
                Cid = arg.Cid,
                Year = arg.Year,
                Month = arg.Month,
                TotalPoint = (arg.TotalQuantity == 1) ? 5 :
                    (arg.TotalQuantity == 2) ? 15 :
                    (arg.TotalQuantity >= 3) ? 30 : 0
            });
        var customersTotalPoint = customersEarnedPointEachYearAndMonth
            .GroupBy(arg => arg.Cid)
            .Select(group => new
            {
                group.Key,
                TotalPoint = group.Sum(arg => arg.TotalPoint)
            });
        return customersTotalPoint.ToDictionary(customer => customer.Key, customer => customer.TotalPoint);
    }
}
