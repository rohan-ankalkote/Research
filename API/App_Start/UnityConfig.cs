using API.Common;
using Unity;
using Unity.Lifetime;

namespace API
{
    public static class UnityConfig
    {
        public static IUnityContainer Container { get; private set; }

        public static void RegisterComponents()
        {
			Container = new UnityContainer();
            
            Container.RegisterType<Cache>(new HierarchicalLifetimeManager());
        }
    }
}