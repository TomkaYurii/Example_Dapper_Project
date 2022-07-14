using ConsoleApplicationExample.Services;
using ConsoleApplicationExample.Services.Interfaces;
using Dapper_Example.DAL.Repositories;
using Dapper_Example.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Data.SqlClient;
using System.Text;

//======================================
//  КОНФІГУРУВАННЯ ДЖЕНЕРІК ХОСТА
//======================================

var host = Host.CreateDefaultBuilder(args)
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

            // Services from Console Application
            services.AddScoped<ProductService>();
            services.AddScoped<CategoryService>();
        })
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            var env = hostingContext.HostingEnvironment;
            config.AddEnvironmentVariables();
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);
        })
        .Build();

//======================================
//          БАЗОВИЙ ФУНКЦІОНАЛ
//======================================

static async Task ExecuteScopeProduct(IServiceProvider services, int Id)
{
    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    var ProductSVC = provider.GetRequiredService<ProductService>();
    await ProductSVC.GetAllInfoAboutProduct(Id);
}

static async Task ExecuteScopeCategory(IServiceProvider services, int Id)
{
    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    var CategorytSVC = provider.GetRequiredService<CategoryService>();
    await CategorytSVC.GetAllInfoAboutCategory(Id);
}

//======================================
//              МЕНЮШКА
//======================================

Console.OutputEncoding = UTF8Encoding.UTF8;
bool showMenu = true;
while (showMenu)
{
    showMenu = MainMenuAsync().Result;
}

async Task<bool> MainMenuAsync()
{
    Console.Clear();
    Console.WriteLine("Вибери опцію:");
    Console.WriteLine("[1] Отримати інформацію про продукт");
    Console.WriteLine("[2] Отримати інформацію про каталог");
    Console.WriteLine("[0] Exit");
    Console.Write("\r\nSelect an option: ");

    switch (Console.ReadLine())
    {
        case "1":
            try
            {
                Console.WriteLine("==========================================================");
                Console.WriteLine("Введи Id товару іфнормацію про який ти би хотів отримати: ");
                int product_Id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("-------------------------- ");
                Console.WriteLine("Шукаю в базі... Секунду... ");
                Console.WriteLine("-------------------------- ");
                Console.WriteLine("" + Environment.NewLine);
                    await ExecuteScopeProduct(host.Services, product_Id);
                Console.WriteLine("==========================================================");
                Console.WriteLine("Натисни любу кнопку, щоб продовжити...");
                Console.ReadKey();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("-------------------------- ");
                Console.WriteLine("Ти шось там наплужив.");
                Console.WriteLine("ОСЬ В ЧОМУ ПРИЧИНА...");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                Console.WriteLine("Тицькай кнопку щоб продовжити");
                Console.ReadKey();
                return true;
            }

        case "2":
            try
            {
                Console.WriteLine("==========================================================");
                Console.WriteLine("Введи Id каталогу іфнормацію про який ти би хотів отримати: ");
                int catalog_Id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("-------------------------- ");
                Console.WriteLine("Шукаю в базі... Секунду... ");
                Console.WriteLine("-------------------------- ");
                Console.WriteLine("" + Environment.NewLine);
                    await ExecuteScopeCategory(host.Services, catalog_Id);
                Console.WriteLine("==========================================================");
                Console.WriteLine("Натисни любу кнопку, щоб продовжити...");
                Console.ReadKey();
                return true;
                Console.ReadKey();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("-------------------------- ");
                Console.WriteLine("Ти шось там наплужив.");
                Console.WriteLine("ОСЬ В ЧОМУ ПРИЧИНА...");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                Console.WriteLine("Тицькай кнопку щоб продовжити");
                Console.ReadKey();
                return true;
            }

        case "0":
            return false;
        default:
            return true;
    }
}
