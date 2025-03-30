using Unity;

namespace API
{
    public static class UnityConfig
    {
        public static IUnityContainer Container { get; private set; }

        public static void RegisterComponents()
        {
			Container = new UnityContainer();
            
            // e.g. container.RegisterType<ITestService, TestService>();
        }
    }
}