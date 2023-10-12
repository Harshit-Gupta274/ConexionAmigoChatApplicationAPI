using ConexiónAmigo.Data.DbRepository;
using ConexiónAmigo.Data.DbRepository.CredentialsChecking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexiónAmigo.Data
{
    public static class DataRegister
    {
        public static Dictionary<Type, Type> GetAllData()
        {
            Dictionary<Type, Type> Data = new Dictionary<Type, Type>() {

                {typeof(IMessageHandler) , typeof(MessageHandlerRepository) },
                {typeof(ICredentialChecking) , typeof(CredentialChecking) }

            };
            return Data;
        }

    }
}
