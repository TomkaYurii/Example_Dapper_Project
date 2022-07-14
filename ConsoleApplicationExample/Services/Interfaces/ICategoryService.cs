using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationExample.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task GetAllInfoAboutCategory(int id);
    }
}
