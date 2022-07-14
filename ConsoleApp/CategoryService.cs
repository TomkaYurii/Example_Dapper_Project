using Dapper_Example.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public void GetAllInfoAboutCategory()
        {
            int id = 1;
            var category = _unitOfWork._categoryRepository.GetAsync(id).Result;
            Console.WriteLine("Інфорпмація про категорію - {0}", category.Name);

            Console.WriteLine("Job is Done! from the Console Product Service");
        }
    }
}
