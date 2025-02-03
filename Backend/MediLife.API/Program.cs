using System.Data;
using System.Data.SqlClient;
using MediLife.BusinessProvider.IProviders;
using MediLife.BusinessProvider.Providers;
using MediLife.DataAccess.IRepository;
using MediLife.DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:4300") // Allow Angular app
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // Allow cookies/authentication
        });
});

// Add services to the container
builder.Services.AddControllers();

// Register interfaces and implementations
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Configure database connection string
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("MediLifeConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Database connection string 'MediLifeConnection' is not configured.");
    }
    return new SqlConnection(connectionString);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Apply CORS Before HTTPS Redirection
app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
