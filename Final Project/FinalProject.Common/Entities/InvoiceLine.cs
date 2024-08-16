using System.Text;

namespace FinalProject.Common.Entities;

public partial class InvoiceLine
{
    public int InvoiceLineId { get; set; }

    public int InvoiceId { get; set; }

    public int TrackId { get; set; }

    public double UnitPrice { get; set; }

    public int Quantity { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;

    public virtual Track Track { get; set; } = null!;
    
    public override string ToString()
    {
        var invoiceLineProperties = GetType().GetProperties();
        var result = new StringBuilder();
        foreach (var property in invoiceLineProperties)
        {
            if (property == null)
                continue;
            result.Append(property.Name);
            result.Append(": ");
            result.Append(property.GetValue(this));
            result.Append("\n");
        }

        return result.ToString();
    }
}
