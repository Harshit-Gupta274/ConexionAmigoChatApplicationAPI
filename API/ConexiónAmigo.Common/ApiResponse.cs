using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexiónAmigo.Common
{
    public class BaseApiResponse
    {
        public BaseApiResponse()
        {

        }

        public bool Success { get; set; }
        public long UserId { get; set; }
        public string Message { get; set; }
        public long dataCount { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string ProfilePic { get; set; }

        public string CorrelationId { get; set; }
        public int Count { get; set; }

    }
    public class ApiResponse<T> : BaseApiResponse       //Here T represents it can take any type of datatype
    {
        public virtual IList<T> Data { get; set; }      //We have to make List<T> i.e new List<Datatype>();
        public virtual T AnyData { get; set; }

    }

    public class ApiPostResponse<T> : BaseApiResponse
    {
        public virtual T Data { get; set; }
    }
}
