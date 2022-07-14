using Dapper_Example.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_Example.DAL.Repositories
{
    public class ProductRepositoryEF : IProductRepository
    {
        public Task<int> AddAsync(Product t)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddRangeAsync(IEnumerable<Product> list)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> ProductByCategoryASync(int CategoryId)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceAsync(Product t)
        {
            throw new NotImplementedException();
        }
    }
}
