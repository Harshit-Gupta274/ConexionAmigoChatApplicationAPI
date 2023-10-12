using ConexiónAmigo.Common;
using ConexiónAmigo.Common.EncryptionDecryption;
using ConexiónAmigo.Model.Models;
using ConexiónAmigo.Services.Services.CredentialCheckingServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConexiónAmigo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Credentials : ControllerBase
    {
        private readonly ICredentialCheckingService _credentialCheckingService;

        public Credentials(ICredentialCheckingService credentialCheckingService)
        {
            _credentialCheckingService = credentialCheckingService;
        }

        [HttpPost("/SignIn")]
        public async Task<ApiResponse<SignInResponseModel>> SignIn(SignInModel credential)
        {
            ApiResponse<SignInResponseModel> response = new ApiResponse<SignInResponseModel>();
            response.AnyData = new SignInResponseModel();
            if(ModelState.IsValid) {
                credential.SessionToken = EncryptionDecryption.CreateRandomToken(credential.Password);
                SignInDataGetModel result = await _credentialCheckingService.SignIn(credential);
                SignInResponseModel responseModel = new SignInResponseModel();
                if (result.PasswordHash != null && result.PasswordSalt != null)
                {
                    if (EncryptionDecryption.VerifyPasswordHash(credential.Password, result.PasswordHash, result.PasswordSalt))
                    {

                        responseModel.Email = result.Email;
                        responseModel.UserName = result.UserName;
                        responseModel.SessionToken = result.SessionToken;
                        responseModel.UserId = result.UserId;
                        response.AnyData = responseModel;
                        response.Success = true;
                        response.Message = "Welcome";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Invalid Credentials";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "No Such User Exists";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Please Enter Valid Credentials";
            }
            
            return response;
        }


        [HttpPost("/SignUp")]

        public async Task<BaseApiResponse> SignUp()
        {
            BaseApiResponse response = new BaseApiResponse();
            var Email = Request.Form["Email"].ToString();
            var UserName = Request.Form["UserName"].ToString();
            var Password = Request.Form["Password"].ToString();
            var AvatarImageName = Request.Form["AvatarImageName"].ToString();

            
            EncryptionDecryption.CreatePasswordHash(Password, out byte[] PasswordHash, out byte[] PasswordSalt);
            SignUpModel credential = new SignUpModel();
            credential.Password = Password;
            credential.Email = Email;   
            credential.UserName = UserName;
            credential.AvatarImageName = AvatarImageName;
            credential.CreatedDate = DateTime.Now;
            credential.PasswordHash = PasswordHash;
            credential.PasswordSalt = PasswordSalt;
            string VerificationToken = EncryptionDecryption.CreateRandomToken(credential.Password);
            credential.VerificationToken = VerificationToken;
            credential.VerificationExpire = DateTime.UtcNow.AddMinutes(2);
            if (Request.Form.Files.Count > 0)
            {

                var file = Request.Form.Files[0];
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Asset/Images");
                string extension = Path.GetExtension(AvatarImageName);
                string FileNameWithoutExtension = Path.GetFileNameWithoutExtension(AvatarImageName).Replace(" ","");
                string NewFileName = FileNameWithoutExtension + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                string destinationPath = Path.Combine(path, NewFileName);
                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                credential.AvatarImageName = NewFileName;
            }
            SignUpResponseModel result = await _credentialCheckingService.SignUp(credential);
            if(result.Success)
            {
                response.Success = true;
                response.Message = "SignUp Succesfully";
            }
            else
            {
                response.Success = false;
                response.Message = "User Already Exist";
            }
            
            return response;
        }
    }
}
