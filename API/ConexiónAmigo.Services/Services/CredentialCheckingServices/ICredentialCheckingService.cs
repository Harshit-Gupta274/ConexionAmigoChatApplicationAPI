using ConexiónAmigo.Common;
using ConexiónAmigo.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexiónAmigo.Services.Services.CredentialCheckingServices
{
    public interface ICredentialCheckingService
    {
        Task<SignInDataGetModel> SignIn(SignInModel credential);

        Task<SignUpResponseModel> SignUp(SignUpModel credential);
    }
}
