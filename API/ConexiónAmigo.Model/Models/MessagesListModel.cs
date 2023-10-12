using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexiónAmigo.Model.Models
{
    public class MessagesListModel
    {
        public int MessageId { get; set; }  
        public string TextMessage { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; } 
    }
}
