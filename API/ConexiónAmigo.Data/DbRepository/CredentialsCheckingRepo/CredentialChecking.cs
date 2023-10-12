using ConexiónAmigo.Common;
using ConexiónAmigo.Model.Config;
using ConexiónAmigo.Model.Models;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexiónAmigo.Data.DbRepository.CredentialsChecking
{
    public class CredentialChecking : BaseRepository,ICredentialChecking
    {
        public CredentialChecking(IOptions<DataConfig> connectionString) : base(connectionString)
        {
        }

        public async Task<SignInDataGetModel> SignIn(SignInModel credential)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Email" , credential.Email);
            parameters.Add("@SessionToken", credential.SessionToken);
            
            var result = await QueryFirstOrDefaultAsync<SignInDataGetModel>("ConexionAmigo_SignInChecking", parameters);

            return result;
        }
 
        public async Task<SignUpResponseModel> SignUp(SignUpModel credential)
        {
            SignUpResponseModel response = new SignUpResponseModel();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserName" , credential.UserName);
            parameters.Add("@Email", credential.Email);
            parameters.Add("@PasswordHash", credential.PasswordHash);
            parameters.Add("@PasswordSalt", credential.PasswordSalt);
            parameters.Add("@Avatar", credential.AvatarImageName);
            parameters.Add("@VerificationToken", credential.VerificationToken);
            parameters.Add("@VerificationExpire", credential.VerificationExpire);
            parameters.Add("@CreatedDate" , credential.CreatedDate);
            var result = await QueryFirstOrDefaultAsync<int>("ConexionAmigo_SignUpUser", parameters);
            if(result == 0)
            {
                response.Success = false;
            }
            else
            {
                response.Success = true;
            }

            return response;

        }
    }
}
