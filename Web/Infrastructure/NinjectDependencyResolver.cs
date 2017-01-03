using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Domen.Abstract;
using Domen.Repos;
using Ninject;
using Ninject.Syntax;
using Ninject.Web.Common;

namespace Web.Infrastructure
{
    public class NinjectDependencyResolver: System.Web.Mvc.IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver
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
            kernel.Bind<IMember>().To<DbMember>().InRequestScope();
        }
    }
}