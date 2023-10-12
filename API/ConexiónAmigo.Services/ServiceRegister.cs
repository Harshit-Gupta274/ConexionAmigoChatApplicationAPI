using ConexiónAmigo.Services.Services;
using ConexiónAmigo.Services.Services.CredentialCheckingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexiónAmigo.Services
{
    public static class ServiceRegister
    {
        public static Dictionary<Type ,Type> GetAllService()
        {
            Dictionary<Type , Type> service=  new Dictionary<Type, Type>() {
                {typeof(IMessageHandlerService)  , typeof(MessageHandlerService)},
                {typeof(ICredentialCheckingService) , typeof(CredentialCheckingService)},
            };

            return service;
        }
    }
}
