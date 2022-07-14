using Dapper_Example.DAL.Repositories;
using Dapper_Example.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Data.SqlClient;

namespace WinFormsApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var host = Host.CreateDefaultBuilder()
             .ConfigureServices((hostContext, services) =>
             {
                // Connection/Transaction for database
                 services.AddScoped((s) => new SqlConnection(hostContext.Configuration.GetConnectionString("MSSQLConnection")));
                 services.AddScoped<IDbTransaction>(s =>
                 {
                     SqlConnection conn = s.GetRequiredService<SqlConnection>();
                     conn.Open();
                     return conn.BeginTransaction();
                 });

                // Dependendency Injection for Repositories/UOF from DAL
                 services.AddScoped<IProductRepository, ProductRepository>();
                 services.AddScoped<ICategoryRepository, CategoryRepository>();
                 services.AddScoped<IUnitOfWork, UnitOfWork>();


                 services.AddScoped<Form1>();
             })
             .ConfigureAppConfiguration((hostingContext, config) =>
             {
                 var env = hostingContext.HostingEnvironment;
                 config.AddEnvironmentVariables();
                 config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                 config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);
             })
             .Build();

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            var formSVC = provider.GetRequiredService<Form1>();
            // To customize application configuration such as set high DPI settings or default font,
            //// see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            Application.Run(formSVC);
        }
    }
}