using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static Pract.Server.ResourceClass;
using Pract.Models;

namespace Pract.Server
{
    public class UserRepository : GenericRepository<User>
    {
        private static UserPagingViewModel PagingIndex(IQueryable<UserIndexViewModel> users, int page)
        {
            IEnumerable<UserIndexViewModel> usersPerPages= users.OrderBy(r => r.Users.Id).Skip((page - 1) * PageSize).Take(PageSize);
            PageInfo pageInfo = new PageInfo { PageNumber=page, TotalItems= users.Count()};
            return new UserPagingViewModel { PageInfo = pageInfo, UserViewModel = usersPerPages };
        }

        private readonly LibContext _db;
 
        public UserRepository(LibContext db) : base(db)
        {
            _db = new LibContext();
        }

        public UserPagingViewModel PageUser(int page)
        {
            return PagingIndex(_db.Users.Select(x => new UserIndexViewModel
            {
                Users = x,
                Birthday = x.Birthday
            }), page);
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