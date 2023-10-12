using ConexiónAmigo.Common;
using ConexiónAmigo.Data.DbRepository;
using ConexiónAmigo.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexiónAmigo.Services.Services
{
    public class MessageHandlerService : IMessageHandlerService
    {
        private readonly IMessageHandler _messageHandler;

        public MessageHandlerService(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        public Task<List<UserListModel>> GetAllUsers(int SenderId)
        {
            return _messageHandler.GetAllUsers(SenderId);
        }

        public async Task<List<MessagesListModel>> GetChats(int SenderId, int ReceiverId)
        {
            return await _messageHandler.GetChats(SenderId, ReceiverId);
        }

        public async Task<List<InboxParticipantsList>> GetInboxParticipants(int SenderId)
        {
            return await _messageHandler.GetInboxParticipants(SenderId);
        }

        public async Task<int> SendMessage(int SenderId, int ReceiverID, string TextMessage, DateTime CreatedDate)
        {
            return await _messageHandler.SendMessage(SenderId , ReceiverID , TextMessage, CreatedDate);
        }
    }
}
