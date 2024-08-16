using System.Text;

namespace FinalProject.Common.Entities;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? Title { get; set; }

    public int? ReportsTo { get; set; }

    public DateTime? BirthDate { get; set; }

    public DateTime? HireDate { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? PostalCode { get; set; }

    public string? Phone { get; set; }

    public string? Fax { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Employee> InverseReportsToNavigation { get; set; } = new List<Employee>();

    public virtual Employee? ReportsToNavigation { get; set; }


    public override string ToString()
    {
        var employeeProperties = GetType().GetProperties();
        var result = new StringBuilder();
        foreach (var property in employeeProperties)
        {
            if (property == null)
                continue;
            if (property.GetType().GetInterfaces().Contains(typeof(ICollection<Customer>)))
            {
                var customers = (ICollection<Customer>)property.GetValue(this);
                foreach (var customer in customers)
                {
                    result.Append(customer.ToString());
                }
            }
            if (property.GetType().GetInterfaces().Contains(typeof(ICollection<Employee>)))
            {
                var employees = (ICollection<Employee>)property.GetValue(this);
                foreach (var employee in employees)
                {
                    result.Append(employee.ToString());
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
