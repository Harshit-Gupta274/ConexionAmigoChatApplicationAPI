using ConexiónAmigo.ChatHub;
using ConexiónAmigo.Common;
using ConexiónAmigo.Model.Models;
using ConexiónAmigo.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ConexiónAmigo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServerController : ControllerBase
    {
        private readonly IHubContext<MainHub> _hubContext;
        private readonly IMessageHandlerService _messageHandlerService;
        public ServerController(IHubContext<MainHub> hubContext , IMessageHandlerService messageHandlerService)
        {
            _hubContext = hubContext;
            _messageHandlerService = messageHandlerService;
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost("SendMessage")]
        public async Task<BaseApiResponse> SendMessage(int SenderId ,int ReceiverID ,  string TextMessage, DateTime CreatedDate)
        {
            BaseApiResponse apiResponse = new BaseApiResponse();
            var result = await _messageHandlerService.SendMessage(SenderId, ReceiverID, TextMessage, CreatedDate);
            if(result == 1)
                apiResponse.Success = true;
            else
                apiResponse.Success = false;

            return apiResponse;

        }

        [HttpGet("/GetChats")]
        public async Task<ApiResponse<MessagesListModel>> GetChats(int SenderId , int ReceiverId)
        {
            ApiResponse<MessagesListModel> apiResponse = new ApiResponse<MessagesListModel>();
            apiResponse.Data = new List<MessagesListModel>();
            var result = await _messageHandlerService.GetChats(SenderId , ReceiverId);
            if(result != null)
            {
                apiResponse.Success = true;
                apiResponse.Message = "Chats Received";
                apiResponse.Data = result.ToList();
            }
            else
            {
                apiResponse.Success = false;
                apiResponse.Message = "No Chats";

            }
            return apiResponse;
        }

        [HttpGet("/GetInboxParticipants")]
        public async Task<ApiResponse<InboxParticipantsList>> GetInboxParticipants(int SenderId)
        {
            ApiResponse<InboxParticipantsList> apiResponse = new ApiResponse<InboxParticipantsList>();
            apiResponse.Data = new List<InboxParticipantsList>();
            var result = await _messageHandlerService.GetInboxParticipants(SenderId);

            if(result.Count > 0) {
                apiResponse.Success = true;
                apiResponse.Message = "List received";
                apiResponse.Data = result.ToList();
            }
            else
            {
                apiResponse.Success= false;
                apiResponse.Message = "No List";
            }
            return apiResponse;
        }

        [HttpGet("/GetAllUsers")]
        public async Task<ApiResponse<UserListModel>> GetAllUsers(int SenderId)
        {
            ApiResponse<UserListModel> apiResponse = new ApiResponse<UserListModel>();
            apiResponse.Data = new List<UserListModel>();
            var result = await _messageHandlerService.GetAllUsers(SenderId);

            if (result.Count > 0)
            {
                apiResponse.Success = true;
                apiResponse.Message = "List received";
                apiResponse.Data = result.ToList();
            }
            else
            {
                apiResponse.Success = false;
                apiResponse.Message = "No List";
            }
            return apiResponse;
        }


    }
}
