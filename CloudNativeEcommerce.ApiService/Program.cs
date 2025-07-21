using CloudNativeEcommerce.ApiService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();
builder.AddSqlServerDbContext<EcommerceDbContext>("ecommerce");

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    var context = scopedProvider.GetRequiredService<EcommerceDbContext>();
    await context.Database.EnsureCreatedAsync();
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// API endpoint to retrieve all products
app.MapGet("/products", async (EcommerceDbContext context) =>
{
    return await context.Products.ToListAsync();
})
.WithName("GetAllProducts");

app.MapDefaultEndpoints();

app.Run();
