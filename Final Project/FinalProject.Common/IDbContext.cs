using FinalProject.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Common;

public interface IDbContext : IDisposable
{
    public DbSet<Album> Albums { get; set; }

    public DbSet<Artist> Artists { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<Invoice> Invoices { get; set; }

    public DbSet<InvoiceLine> InvoiceLines { get; set; }

    public DbSet<MediaType> MediaTypes { get; set; }

    public DbSet<Playlist> Playlists { get; set; }

    public DbSet<Track> Tracks { get; set; }
}