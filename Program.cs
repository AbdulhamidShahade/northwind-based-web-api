using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NorthwindBasedWebAPI.Data;
using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Repositories.IRepository;
using NorthwindBasedWebAPI.Repositories.Repository;
using NorthwindBasedWebApplication.API.Repositories.Repository;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var key = builder.Configuration.GetValue<string>("ApiSettings:Key");



builder.Services.AddControllers();





// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
            "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
            "Example: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLServer"));
});

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICustomerDemographicRepository, CustomerDemographicRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IShipperRepository, ShipperRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ITerritoryRepository, TerritoryRepository>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());




builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();




builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x => {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var columnOptions = new ColumnOptions
{
    AdditionalColumns = new List<SqlColumn>
    {
        new SqlColumn { ColumnName = "MethodType", DataType = SqlDbType.NVarChar, DataLength = 50 },
        new SqlColumn { ColumnName = "User", DataType = SqlDbType.NVarChar, DataLength = 100 },
        new SqlColumn { ColumnName = "Role", DataType = SqlDbType.NVarChar, DataLength = 50 },
        new SqlColumn { ColumnName = "Details", DataType = SqlDbType.NVarChar, DataLength = 500 },
        new SqlColumn { ColumnName = "StatusCode", DataType = SqlDbType.NVarChar, DataLength = 30 },
        new SqlColumn { ColumnName = "Success", DataType = SqlDbType.Bit },
        new SqlColumn { ColumnName = "Failed", DataType = SqlDbType.Bit },
        new SqlColumn { ColumnName = "ErrorMessage", DataType = SqlDbType.NVarChar, DataLength = 500 },
        new SqlColumn { ColumnName = "CreatedAt", DataType = SqlDbType.Date }
    }
};

columnOptions.Store.Remove(StandardColumn.Exception);
columnOptions.Store.Remove(StandardColumn.Properties);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Fatal)
    .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Fatal)
    .WriteTo.MSSqlServer(
        connectionString: "Data Source=ABDULHAMIT;Initial Catalog=northwind-based-web-api;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False",
        tableName: "SystemLogs",
        autoCreateSqlTable: true,
        columnOptions: columnOptions
    )
    .CreateLogger();




builder.Host.UseSerilog();


var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
