using Books.Models;
using Books.Services;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
var builder = WebApplication.CreateBuilder(args);

Env.Load();
// Add services to the container.
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
builder.Services.AddDbContext<BooksContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddControllers();
/*AddScoped: Creates a new instance of the service once per HTTP request. 
  The same instance is used for all components that need it within that request. 
    The instance is disposed of at the end of the request.*/
builder.Services.AddScoped<BooksService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<PurchaseService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

/*Remove-Migration
   /// Drop-Database
   /// Add-Migration UpdateSchema
   /// Update-Database
   /// */
