using ConexiónAmigo.Common;
using ConexiónAmigo.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexiónAmigo.Data.DbRepository
{
    public interface IMessageHandler
    {
        Task<int> SendMessage(int SenderId, int ReceiverID, string TextMessage, DateTime CreatedDate);
        Task<List<MessagesListModel>> GetChats(int SenderId, int ReceiverId);
        Task<List<InboxParticipantsList>> GetInboxParticipants(int SenderId);

        Task<List<UserListModel>> GetAllUsers(int SenderId);
    }
}
