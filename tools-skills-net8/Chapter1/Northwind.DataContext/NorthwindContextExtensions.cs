using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Northwind.DataContext;

public static class NorthwindContextExtensions
{
    /// <summary>
    /// Adds NorthwindContext to the specified IServicessCollection.
    /// Uses the SqlServer database provider.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="connectionString">Set to override the default.</param>
    /// <returns></returns>
    public static IServiceCollection AddNorthwindContext(this IServiceCollection services, 
    string? connectionString = null)
    {
        if (connectionString == null)
        {
            SqlConnectionStringBuilder builder = new();
            
            builder.DataSource = "tcp:127.0.0.1,1433";
            builder.InitialCatalog = "Northwind";
            builder.TrustServerCertificate = true;
            builder.MultipleActiveResultSets = true;
            builder.ConnectTimeout = 3;
            builder.UserID = Environment.GetEnvironmentVariable("MY_SQL_USER");
            builder.Password = Environment.GetEnvironmentVariable("MY_SQL_PWD");

            connectionString = builder.ConnectionString;
        }

        services.AddDbContext<NorthwindContext>(options =>
        {
            options.UseSqlServer(connectionString);

            // Log to console when executing EF Core commands
            options.LogTo(Console.WriteLine,
            new [] { Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting });
        },
        
        // Register with a transient lifetime to avoid concurrency issues with Blazor Server projects
        contextLifetime: ServiceLifetime.Transient,
        optionsLifetime: ServiceLifetime.Transient);

        return services;
    }
}