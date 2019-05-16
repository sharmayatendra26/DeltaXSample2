using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace DependencyResolver
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            System.Web.Mvc.DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            // register dependency resolver for WebAPI RC  
            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(container);

            //return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            //container.RegisterType<IProductServices, ProductServices>().RegisterType<UnitOfWork>(new HierarchicalLifetimeManager());
            //IProductServices myServiceInstance = container.Resolve<IProductServices>();
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            //Component initialization via MEF  
            ComponentLoader.LoadContainer(container, ".\\bin", "DeltaXSetup.dll");
            ComponentLoader.LoadContainer(container, ".\\bin", "BusinessServices.dll");
        }
    }

    public class UnityResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        protected IUnityContainer container;
        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            this.container = container;
        }
        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            container.Dispose();
        }
    }
}