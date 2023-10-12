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

namespace ConexiónAmigo.Data.DbRepository
{
    public class MessageHandlerRepository : BaseRepository , IMessageHandler
    {
        public MessageHandlerRepository(IOptions<DataConfig> connectionString) : base(connectionString)
        {
        }

        public async Task<List<MessagesListModel>> GetChats(int SenderId, int ReceiverId)
        {
            
            var parameters = new DynamicParameters();
            
            parameters.Add("@SenderId", SenderId);
            parameters.Add("@ReceiverID", ReceiverId);

            var result = await QueryAsync<MessagesListModel>("ConexionAmigo_SelectChat", parameters);
            if(result != null)
            {
                return result.ToList();
            }
            
            return result.ToList();

        }

        public async Task<int> SendMessage(int SenderId, int ReceiverID, string TextMessage, DateTime CreatedDate)
        {
            
            var parameters = new DynamicParameters();
            parameters.Add("@SenderId", SenderId);
            parameters.Add("@ReceiverID", ReceiverID);
            parameters.Add("@TextMessage", TextMessage);
            parameters.Add("@CreatedDate", CreatedDate);

            var result = await QueryFirstOrDefaultAsync<int>("ConexionAmigo_Chat_With_OneUser", parameters);
            if(result == 1)
            {
                return result;
            }
            return result;
        }


        public async Task<List<InboxParticipantsList>> GetInboxParticipants(int SenderId)
        {
            
            var parameters = new DynamicParameters();
            parameters.Add("@SenderId" , SenderId);

            var result = await QueryAsync<InboxParticipantsList>("ConexionAmigo_SelectInboxParticipants", parameters);
            
            if(result != null)
            {
                return result.ToList();
            }
            return result.ToList();
        }

        public async Task<List<UserListModel>> GetAllUsers(int SenderId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SenderId", SenderId);

            var result = await QueryAsync<UserListModel>("ConexionAmigo_SelectAllParticipants", parameters);

            if (result != null)
            {
                return result.ToList();
            }
            return result.ToList();
        }
    }
}
