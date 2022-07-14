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
                .ConfigureServices((context, services) =>
                {
                    
                    services.AddScoped<IProductRepository, ProductRepository>();
                    services.AddScoped<ICategoryRepository, CategoryRepository>();
                    services.AddScoped<IUnitOfWork, UnitOfWork>();

                    services.AddScoped((s) => new SqlConnection(context.Configuration.GetConnectionString("MSSQLConnection")));
                    services.AddScoped<IDbTransaction>(s =>
                    {
                        SqlConnection conn = s.GetRequiredService<SqlConnection>();
                        conn.Open();
                        return conn.BeginTransaction();
                    });
                })
                .Build();


            // To customize application configuration such as set high DPI settings or default font,
            //// see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
        }
    }
}