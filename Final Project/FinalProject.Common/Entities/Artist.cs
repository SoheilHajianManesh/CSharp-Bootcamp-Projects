using System.Text;

namespace FinalProject.Common.Entities;

public partial class Artist
{
    public int ArtistId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public override string ToString()
    {
        var artistProperties = GetType().GetProperties();
        var result = new StringBuilder();
        foreach (var property in artistProperties)
        {
            if (property == null)
                continue;
            if(property.GetType().GetInterfaces().Contains(typeof(ICollection<Album>)))
            {
                var albums = (ICollection<Album>) property.GetValue(this);
                foreach (var album in albums)
                {
                    result.Append(album.ToString());
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
