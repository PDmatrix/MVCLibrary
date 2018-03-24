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
    public class UserHandler
    {
        public static LibContext db = new LibContext();

        public static IEnumerable<UserViewModel> IndexUser()
        {
            var list = db.Users.Select(x => new UserViewModel
            {
                Users = x,
                Age = (DateTime.Now.Month < x.Birthday.Month || (DateTime.Now.Month == x.Birthday.Month && DateTime.Now.Day < x.Birthday.Day)) ? DateTime.Now.Year - x.Birthday.Year - 1 : DateTime.Now.Year - x.Birthday.Year,
                Birthday = x.Birthday,
            }).ToArray();
            return list;
        }

        public static bool CreateUser(User user, bool isValid)
        {
            if (isValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public static User FindUser(int? id)
        {
            return db.Users.Find(id);
        }

        public static bool EditUser(User user, bool isValid)
        {
            if (isValid)
            {
                var local = db.Set<User>()
                         .Local
                         .FirstOrDefault(f => f.Id == user.Id);
                if (local != null)
                {
                    db.Entry(local).State = EntityState.Detached;
                }
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;
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