using ConexiónAmigo.Data;
using ConexiónAmigo.Services;

namespace ConexiónAmigo
{
    public static class Register
    {
        public static void RegisterService(this IServiceCollection services)
        {
            Configure(services, DataRegister.GetAllData());
            Configure(services, ServiceRegister.GetAllService());
        }

        public static void Configure(IServiceCollection services, Dictionary<Type, Type> types)
        {
            foreach (KeyValuePair<Type, Type> type in types)
                services.AddSingleton(type.Key, type.Value);
        }
    }
}
