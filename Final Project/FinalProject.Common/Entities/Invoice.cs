using System.Text;

namespace FinalProject.Common.Entities;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int CustomerId { get; set; }

    public DateTime InvoiceDate { get; set; }

    public string? BillingAddress { get; set; }

    public string? BillingCity { get; set; }

    public string? BillingState { get; set; }

    public string? BillingCountry { get; set; }

    public string? BillingPostalCode { get; set; }

    public double Total { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();
    
    public override string ToString()
    {
        var invoiceProperties = GetType().GetProperties();
        var result = new StringBuilder();
        foreach (var property in invoiceProperties)
        {
            if (property == null)
                continue;
            if (property.GetType().GetInterfaces().Contains(typeof(ICollection<InvoiceLine>)))
            {
                var invoiceLines = (ICollection<InvoiceLine>) property.GetValue(this);
                foreach (var invoiceLine in invoiceLines)
                {
                    result.Append(invoiceLine.ToString());
                }
            }
            result.Append(property.Name);
            result.Append(": ");
            result.Append(property.GetValue(this));
            result.Append("\n");
        }

        return result.ToString();
    }
}
