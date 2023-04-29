
namespace Scripts.Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices _instance;

        public static AllServices Container => _instance ?? (_instance = new AllServices());

        public void RegisterSingle<TService>(TService implementetion) where TService : IService => 
            Implementetion<TService>.ServiceInstance = implementetion;

        public TService Single<TService>() where TService : IService => 
            Implementetion<TService>.ServiceInstance;


        private static class Implementetion<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}
