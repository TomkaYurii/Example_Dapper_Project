using ConsoleApplicationExample.Services.Interfaces;
using Dapper_Example.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationExample.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task GetAllInfoAboutCategory(int id)
        {
            var category = _unitOfWork._categoryRepository.GetAsync(id).Result;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-->> Зведена інформація про категорію");
            Console.WriteLine("-->> Назва категорії - \t" + category.Name);
            Console.WriteLine("-->> Характеристики категорії - \t" + category.Properties);

            var category_of_product = await _unitOfWork._productRepository.ProductByCategoryAsync(category.Id);

            Console.WriteLine("-->> Всі товари, що відносяться до відповідної категорії");
            foreach (var product in category_of_product)
            {
                Console.WriteLine("-->> Товар  - " + product.Name + "\t ID товару - " + product.Id);
            }
            Console.WriteLine("" + Environment.NewLine);
            Console.ResetColor();

        }
    }
}
