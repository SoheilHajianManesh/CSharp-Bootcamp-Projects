using System.Text;

namespace FinalProject.Common.Entities;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Company { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? PostalCode { get; set; }

    public string? Phone { get; set; }

    public string? Fax { get; set; }

    public string Email { get; set; } = null!;

    public int? SupportRepId { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual Employee? SupportRep { get; set; }

    public override string ToString()
    {
        var customerProperties = GetType().GetProperties();
        var result = new StringBuilder();
        foreach (var property in customerProperties)
        {
            if (property == null)
                continue;
            if (property.GetType().GetInterfaces().Contains(typeof(ICollection<Invoice>)))
            {
                var invoices = (ICollection<Invoice>)property.GetValue(this);
                foreach (var invoice in invoices)
                {
                    result.Append(invoice.ToString());
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
