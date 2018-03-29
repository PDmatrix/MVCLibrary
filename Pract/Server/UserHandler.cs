using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pract.Models;

namespace Pract.Server
{
    public static class UserHandler
    {
        private static readonly LibContext db = new LibContext();
        private static readonly int PageSize = Convert.ToInt32(Properties.Resources.PageSize);

        private static UserPagingViewModel PagingIndex(IQueryable<UserIndexViewModel> users, int page)
        {
            IEnumerable<UserIndexViewModel> usersPerPages= users.OrderBy(r => r.Users.Id).Skip((page - 1) * PageSize).Take(PageSize);
            PageInfo pageInfo = new PageInfo { PageNumber=page, TotalItems= users.Count()};
            return new UserPagingViewModel { PageInfo = pageInfo, UserViewModel = usersPerPages };
        }

        public static UserPagingViewModel IndexUser(int page)
        {
            return PagingIndex(db.Users.Select(x => new UserIndexViewModel
            {
                Users = x,
                Birthday = x.Birthday
            }), page);
        }

        public static void CreateUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public static User FindUser(int? id)
        {
            return db.Users.Find(id);
        }

        public static void EditUser(User user)
        {
            var local = db.Set<User>().Local.FirstOrDefault(f => f.Id == user.Id);
            if (local != null)
            {
                db.Entry(local).State = EntityState.Detached;
            }

            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void DeleteUser(int id)
        {
            User user = FindUser(id);
            db.Users.Remove(user);
            db.SaveChanges();
        }

        public static UserIndexViewModel UserView(User user)
        {
            return new UserIndexViewModel
            {
                Users = user,
                Birthday = user.Birthday
            };
        }
    }
}