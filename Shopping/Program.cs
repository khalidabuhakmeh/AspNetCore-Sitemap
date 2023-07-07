using Microsoft.EntityFrameworkCore;
using Shopping.Models;
using Shopping.Sitemap;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

// my unique extension method for sitemap information
builder.Services.AddSitemap();
builder.Services.AddOutputCache(options => {
    options.AddPolicy("sitemap", b => b.Expire(TimeSpan.FromSeconds(1)));
});

builder.Services.AddDbContext<Database>(
    ob => ob.UseSqlite("Data Source = database.db")
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseOutputCache();

app.MapSitemap().CacheOutput("sitemap");

app.MapGet("cool/{id}", () => "cool beans")
   .WithSitemap("cool", new { id = 3 });

app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.Run();