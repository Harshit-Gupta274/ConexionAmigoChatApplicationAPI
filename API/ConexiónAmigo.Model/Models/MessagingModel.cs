using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexiónAmigo.Model.Models
{
    public class MessagingModel
    {
        public string SenderId { get; set; }    
        public string ReceiverId { get; set; }
        public string TextMessage { get; set; } 

        public DateTime CreatedDate { get;set; }
    }
}
