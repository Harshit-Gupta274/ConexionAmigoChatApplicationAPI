using ConexiónAmigo.Common;
using ConexiónAmigo.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexiónAmigo.Data.DbRepository.CredentialsChecking
{
    public interface ICredentialChecking
    {
        Task<SignInDataGetModel> SignIn(SignInModel credential);

        Task<SignUpResponseModel> SignUp(SignUpModel credential);
    }
}
