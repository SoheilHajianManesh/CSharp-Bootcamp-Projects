using System.Text;

namespace FinalProject.Common.Entities;

public partial class MediaType
{
    public int MediaTypeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
    
    public override string ToString()
    {
        var mediaTypeProperties = GetType().GetProperties();
        var result = new StringBuilder();
        foreach (var property in mediaTypeProperties)
        {
            if (property == null)
                continue;
            if (property.GetType().GetInterfaces().Contains(typeof(ICollection<Track>)))
            {
                var tracks = (ICollection<Track>) property.GetValue(this);
                foreach (var track in tracks)
                {
                    result.Append(track.ToString());
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
