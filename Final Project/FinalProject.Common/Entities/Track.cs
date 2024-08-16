using System.Text;

namespace FinalProject.Common.Entities;

public partial class Track
{
    public int TrackId { get; set; }

    public string Name { get; set; } = null!;

    public int? AlbumId { get; set; }

    public int MediaTypeId { get; set; }

    public int? GenreId { get; set; }

    public string? Composer { get; set; }

    public int Milliseconds { get; set; }

    public int? Bytes { get; set; }

    public double UnitPrice { get; set; }

    public virtual Album? Album { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();

    public virtual MediaType MediaType { get; set; } = null!;

    public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();

    public override string ToString()
    {
        var trackProperties = GetType().GetProperties();
        var result = new StringBuilder();
        foreach (var property in trackProperties)
        {
            if (property == null)
                continue;
            if (property.GetType().GetInterfaces().Contains(typeof(ICollection<Playlist>)))
            {
                var playlists = (ICollection<Playlist>) property.GetValue(this);
                foreach (var playlist in playlists)
                {
                    result.Append(playlist.ToString());
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
