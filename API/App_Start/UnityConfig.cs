using API.Common;
using API.Common.Models;
using API.Repositories;
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
            Container.RegisterType<IMaintenanceRepository<CountryModel>, MaintenanceRepository<CountryModel>>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMaintenanceRepository<UserModel>, MaintenanceRepository<UserModel>>(new ContainerControlledLifetimeManager());
        }
    }
}