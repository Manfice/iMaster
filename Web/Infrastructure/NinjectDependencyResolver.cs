using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Domen.Abstract;
using Domen.Repos;
using Ninject;
using Ninject.Syntax;
using Ninject.Web.Common;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace Web.Infrastructure
{
    public class NinjectDependencyResolver:System.Web.Http.Dependencies.IDependencyResolver, IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver() : this(new StandardKernel())
        {
        }

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings(_kernel);    
        }
        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
        private static void AddBindings(IBindingRoot kernel)
        {
            kernel.Bind<ICustomer>().To<DbCustomer>().InRequestScope();
        }
    }
}