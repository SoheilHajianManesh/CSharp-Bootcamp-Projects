using System.Text;

namespace FinalProject.Common.Entities;

public partial class Genre
{
    public int GenreId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
    
    public override string ToString()
    {
        var genreProperties = GetType().GetProperties();
        var result = new StringBuilder();
        foreach (var property in genreProperties)
        {
            if (property == null)
                continue;
            if(property.GetType().GetInterfaces().Contains(typeof(ICollection<Track>)))
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
