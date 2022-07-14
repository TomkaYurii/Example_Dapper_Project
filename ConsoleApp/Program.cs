// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Dapper_Example.DAL.Repositories.Interfaces;
using ConsoleApp;
using Dapper_Example.DAL.Repositories;
using System.Data;
using System.Data.SqlClient;

static class Program
{
    static void Main()
    {
        var builder = new ConfigurationBuilder();
        BuildConfig(builder);

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddScoped<IProductRepository, ProductRepository>();
                services.AddScoped<ICategoryRepository, CategoryRepository>();
                services.AddScoped<IUnitOfWork, UnitOfWork>();
                services.AddSingleton<IProductService, ProductService>();

                services.AddScoped((s) => new SqlConnection(context.Configuration.GetConnectionString("MSSQLConnection")));
                services.AddScoped<IDbTransaction>(s =>
                {
                    SqlConnection conn = s.GetRequiredService<SqlConnection>();
                    conn.Open();
                    return conn.BeginTransaction();
                });
            })
            .Build();

        //// Створюємо екземпляри сервісів
        var ProductSVC = ActivatorUtilities.CreateInstance<ProductService>(host.Services);
        //var CategorySVC = ActivatorUtilities.CreateInstance<CategoryService>(host.Services);

        //// Зі створених сервісів викликаємо необхідні use cases 
        //ProductSVC.GetAllInfoAboutProduct();
        //CategorySVC.GetAllInfoAboutCategory();
    }


    static void BuildConfig(IConfigurationBuilder builder)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables();
    }

}