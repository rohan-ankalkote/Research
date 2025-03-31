using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Unity;

namespace API
{
    /// <summary>
    /// Resolves the dependencies if provided as parameter.
    /// </summary>
    public class CustomUnityDependencyResolver : IDependencyResolver
    {
        private readonly IUnityContainer _container;

        public CustomUnityDependencyResolver(IUnityContainer container)
        {
            _container = container;
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.ResolveAll(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            var childContainer = _container.CreateChildContainer();
            return new CustomUnityDependencyResolver(childContainer);
        }
    }
}