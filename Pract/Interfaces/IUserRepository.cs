using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pract.Models;

namespace Pract.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        PagingViewModel<UserIndexViewModel> PageUser(int page);
        UserIndexViewModel FindViewModel(int? id);
    }
}
