using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pract.Models
{
    public class UserPagingViewModel
    {
        public IEnumerable<UserIndexViewModel> UserViewModel { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}