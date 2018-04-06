using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using Pract.Interfaces;
using Pract.Repositories;

namespace Pract.Util
{
    public class NinjectRegistrations : NinjectModule
    {

        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}