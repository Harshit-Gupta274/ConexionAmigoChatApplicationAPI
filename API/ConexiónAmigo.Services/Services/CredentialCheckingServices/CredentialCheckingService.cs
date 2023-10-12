using ConexiónAmigo.Common;
using ConexiónAmigo.Data.DbRepository.CredentialsChecking;
using ConexiónAmigo.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexiónAmigo.Services.Services.CredentialCheckingServices
{
    public class CredentialCheckingService : ICredentialCheckingService
    {
        private readonly ICredentialChecking _credentialChecking;

        public CredentialCheckingService(ICredentialChecking credentialChecking)
        {
            _credentialChecking = credentialChecking;
        }

        public Task<SignInDataGetModel> SignIn(SignInModel credential)
        {
            return _credentialChecking.SignIn(credential);
        }

        public Task<SignUpResponseModel> SignUp(SignUpModel credential)
        {
            return _credentialChecking.SignUp(credential);
        }
    }
}
