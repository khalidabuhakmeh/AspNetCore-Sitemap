using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Slugify;

namespace Shopping.Models;

public class Database : DbContext
{
    public Database(DbContextOptions<Database> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasData(new Product[]
            {
                new() { Id = 1, Name = "Express Galaxy Tumbler", Price = 19.95m },
                new() { Id = 2, Name = "Aero Life Air Purifier", Price = 89.99m },
                new() { Id = 3, Name = "Ocean Wave Projector", Price = 29.95m },
                new() { Id = 4, Name = "Illuminated Globe Decor", Price = 42.50m },
                new() { Id = 5, Name = "Moonlight Cushion", Price = 24.99m },
                new() { Id = 6, Name = "Sunrise Alarm Clock", Price = 35.00m },
                new() { Id = 7, Name = "Frosty Mini Fridge", Price = 120.00m },
                new() { Id = 8, Name = "Breeze Tower Fan", Price = 75.89m },
                new() { Id = 9, Name = "Comet Electric Scooter", Price = 299.99m },
                new() { Id = 10, Name = "Starlight Projector", Price = 27.50m }
            });
    }
}

public class Product
{
    private static SlugHelper _slugHelper = new(new()
    {
        CollapseDashes = true,
        TrimWhitespace = true,
        ForceLowerCase = true
    });

    public int Id { get; set; }
    public string Name { get; set; } = "";

    public decimal Price { get; set; }

    [NotMapped] public string Slug => _slugHelper.GenerateSlug(Name);
}