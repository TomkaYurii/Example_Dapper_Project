using Dapper_Example.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
        public void GetAllInfoAboutProduct()
        {
            int id = 1;
            var product = _unitOfWork._productRepository.GetAsync(id).Result;
            Console.WriteLine("Інфорпмація про продукт - {0}", product.Name);

            Console.WriteLine("Job is Done! from the Console Product Service");
        }
    }
}
