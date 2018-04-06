using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Pract.App_LocalResources;
using Pract.Interfaces;
using Pract.Models;

namespace Pract.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private static PagingViewModel<UserIndexViewModel> PagingIndex(IQueryable<UserIndexViewModel> items, int page)
        {
            IEnumerable<UserIndexViewModel> itemsPerPages= items.Skip((page - 1) * ResourceClass.PageSize).Take(ResourceClass.PageSize);
            PageInfo pageInfo = new PageInfo { PageNumber=page, TotalItems= items.Count()};
            return new PagingViewModel<UserIndexViewModel> { PageInfo = pageInfo, Elems = itemsPerPages };
        }

        private readonly LibContext _db;
 
        public UserRepository(DbContext db) : base(db)
        {
            _db = new LibContext();
        }

        public PagingViewModel<UserIndexViewModel> PageUser(int page)
        {
            return PagingIndex(_db.Users.Select(x => new UserIndexViewModel
            {
                Users = x,
                Birthday = x.Birthday
            }).OrderBy(r => r.Users.Id), page);
        }

        public UserIndexViewModel FindViewModel(int? id)
        {
            var user =  _db.Users.Find(id);
            return new UserIndexViewModel
            {
                Users = user,
                Birthday = user.Birthday
            };
        }
    }
}