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

        public static IEnumerable<UserViewModel> IndexUser()
        {
            var list = db.Users.Select(x => new UserViewModel
            {
                Users = x,
                Birthday = x.Birthday
            }).ToArray();
            return list;
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

        public static UserViewModel UserView(User user)
        {
            UserViewModel viewModel = new UserViewModel
            {
                Users = user,
                Birthday = user.Birthday
            };
            return viewModel;
        }

    }
}