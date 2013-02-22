using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using NHibernate;
using Repositories;
using Student.Data;
using Student.Data.Mappings;
using Student.Domain;

namespace Student
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var configurator = new StudentNHibernateConfigurator();
            var builder = new ContainerBuilder();
            builder.Register(x => configurator.BuildSessionFactory("DefaultConnection", typeof(StudentMap))).SingleInstance();
            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession()).InstancePerApiRequest();
            builder.RegisterAssemblyTypes(typeof(IStudentRepository).Assembly).Where(type => type.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerApiRequest();


            // override default dependency resolver to use Autofac
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // this override is needed because WebAPI is not using DependencyResolver to build controllers 
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }
    }
}