using ConsoleApplicationExample.Services.Interfaces;
using Dapper_Example.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationExample.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
        public async Task GetAllInfoAboutProduct(int id)
        {
            var product = await _unitOfWork._productRepository.GetAsync(id);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-->> Зведена інформація про продукт");
            Console.WriteLine("-->> Назва продукту - \t" +  { product.Name, 10});
            Console.WriteLine("-->> Характеристики продукту - \t" + product.Properties);
            Console.WriteLine("-->> Ціна продукту - \t" + product.Price);
            Console.WriteLine("-->> Продавець продукту - \t" + product.Seller);
            Console.WriteLine("-->> Бренд продукту продукту - \t" + product.Brand);

            var category_of_product = await _unitOfWork._categoryRepository.GetAsync(product.Id);

            Console.WriteLine("-->> Категорія продукту - \t" + category_of_product.Name);
            Console.WriteLine("" + Environment.NewLine);
            Console.ResetColor();
        }
    }
}
