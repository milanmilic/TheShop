using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TheShop.BL.IRepositories;
using TheShop.BL.Repositories;

namespace TheShop.WEB.App_Start
{
    public class IocConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType(typeof(ArticleRepository)).As(typeof(IArticleRepository));
            builder.RegisterType(typeof(BuyerRepository)).As(typeof(IBuyerRepository));
            builder.RegisterType(typeof(SupplierRepository)).As(typeof(ISupplierRepository));
            builder.RegisterType(typeof(OrderSellRepository)).As(typeof(IOrderSellRepository));

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}