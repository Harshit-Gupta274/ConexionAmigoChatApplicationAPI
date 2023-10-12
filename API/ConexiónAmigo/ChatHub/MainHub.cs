using ConexiónAmigo.Common;
using ConexiónAmigo.Model.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace ConexiónAmigo.ChatHub
{
    
    public class MainHub : Hub
    {
        private static  Dictionary<string , string> UserList = new Dictionary<string , string>();

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient httpClient;
        public MainHub(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            httpClient = _httpClientFactory.CreateClient("Api");
        }
        public override async Task OnConnectedAsync()
        {
            string UserId = Context.GetHttpContext().Request.Query["senderId"];
            if(UserId == null)
            {
                UserId = "";
            }

            if (!UserList.ContainsKey(UserId))
            {
                UserList.Add(UserId, Context.ConnectionId);
            }
            else
            {
                UserList[UserId] = Context.ConnectionId;
            }

            var response = await httpClient.GetAsync($"GetInboxParticipants?SenderId={Convert.ToInt32(UserId)}");

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync();
                Console.WriteLine(content.Result);
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<InboxParticipantsList>>(content.Result);
                Console.WriteLine(apiResponse);
                await Clients.Client(UserList[UserId]).SendAsync("Data", apiResponse , Context.ConnectionId);
            }
            else
            {
                var apiRsponse = new ApiResponse<InboxParticipantsList>();
                apiRsponse.Success = false;
                apiRsponse.Data = new List<InboxParticipantsList>();
                await Clients.Client(UserList[UserId]).SendAsync("Data", apiRsponse, Context.ConnectionId);
            }

            //await Clients.All.SendAsync("UniqueId" , UserList[UserId]);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                UserList.Remove(Context.ConnectionId);
                Console.WriteLine($"{Context.ConnectionId} Disconnected");
                await Clients.All.SendAsync("UniqueIdDestroy", Context.ConnectionId, "Offline");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send {ex.Message}");  
                throw ex;
            }
        }



        //public async Task SendMessage(int SenderId, int ReceiverID, string TextMessage, DateTime CreatedDate)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", TextMessage , CreatedDate);
        //}

        public async Task SendMessage(string SenderId, string ReceiverID, string TextMessage)
        {
            bool isOnline = false;
            if (UserList.ContainsKey(ReceiverID))
            {
                isOnline = true;
            }
            try
            {
                
                //MessagingModel Message = new MessagingModel() 
                //{
                //    SenderId = SenderId,
                //    ReceiverId = ReceiverID,
                //    TextMessage = TextMessage,
                //    CreatedDate = DateTime.Now
                //};

                //var parameters = new Dictionary<string, string>
                //{
                //    { "SenderId", SenderId },
                //    { "ReceiverID", ReceiverID },
                //    { "TextMessage", TextMessage },
                //    {"CreatedDate" , DateTime.Now.ToString() }
                //};

                //var serializedData = JsonConvert.SerializeObject(parameters);
                //var Data = new StringContent(serializedData, Encoding.UTF8, "application/json");

                


                //var serializedData = JsonConvert.SerializeObject(Message);
                //var Data = new StringContent(serializedData , Encoding.UTF8 , "application/json");

                //var response = await httpClient.PostAsync($"api/Server/SendMessage", Data);

                string apiUrl = $"api/Server/SendMessage?SenderId={SenderId}&ReceiverId={ReceiverID}&TextMessage={TextMessage}&CreatedDate={DateTime.Now}";

                var response = await httpClient.PostAsync(apiUrl, null);

                if (response.IsSuccessStatusCode) {
                    if (isOnline)
                    {
                        await Clients.Client(UserList[ReceiverID]).SendAsync("ReceiveMessage", TextMessage, SenderId);
                    }
                    else
                    {
                        await Clients.Client(UserList[SenderId]).SendAsync("MessageSent", TextMessage, SenderId);
                    }
                    
                    var content = response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                }
                else
                {
                    await Clients.Client(UserList[SenderId]).SendAsync("ReceiveMessageError", "400", SenderId);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public async Task GetChats(string SenderId, string ReceiverId)
        {

            var apiUrl = $"http://localhost:5214/GetChats?SenderId={Convert.ToInt16(SenderId)}&ReceiverId={Convert.ToInt16(ReceiverId)}";
            var response = await httpClient.GetAsync(apiUrl);
            if(response.IsSuccessStatusCode)
            {
                var Content = response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<GetMessagesListModel>>(Content.Result);
                await Clients.Client(UserList[SenderId.ToString()]).SendAsync("MessageList", apiResponse, Context.ConnectionId);
            }
            else
            {
                var apiResponse = new ApiResponse<GetMessagesListModel>();
                apiResponse.Success = false;
                apiResponse.Message = "Unable to get list";
                await Clients.Client(UserList[SenderId.ToString()]).SendAsync("MessageListError", apiResponse, Context.ConnectionId);
            }

        }

        public async Task GetInboxParticipants(string SenderId)
        {
            var response = await httpClient.GetAsync($"GetInboxParticipants?SenderId={Convert.ToInt16(SenderId)}");
            if(response.IsSuccessStatusCode)
            {
                var Content = response.Content.ReadAsStringAsync();
                ApiResponse<InboxParticipantsList> apiResponse = JsonConvert.DeserializeObject<ApiResponse<InboxParticipantsList>>(Content.Result);
                apiResponse.Success = true;
                apiResponse.Message = "Succesfully retrived participants list";
                await Clients.Client(UserList[SenderId.ToString()]).SendAsync("InboxParticipants", apiResponse);
            }
            else
            {
                ApiResponse<InboxParticipantsList> apiResponse = new ApiResponse<InboxParticipantsList>();
                apiResponse.Success = false;
                apiResponse.Message = "Unable to get Participants";
                await Clients.Client(UserList[SenderId.ToString()]).SendAsync("InboxParticipantsError", apiResponse);
            }

        }

        public async Task GetAllUsers(string SenderId)
        {
            var response = await httpClient.GetAsync($"GetAllUsers?SenderId={Convert.ToInt16(SenderId)}");
            if (response.IsSuccessStatusCode)
            {
                var Content = response.Content.ReadAsStringAsync();
                ApiResponse<UserListModel> apiResponse = JsonConvert.DeserializeObject<ApiResponse<UserListModel>>(Content.Result);
                apiResponse.Success = true;
                apiResponse.Message = "Succesfully retrived all users";
                await Clients.Client(UserList[SenderId.ToString()]).SendAsync("AllUserReceived", apiResponse);
            }
            else
            {
                ApiResponse<UserListModel> apiResponse = new ApiResponse<UserListModel>();
                apiResponse.Success = false;
                apiResponse.Message = "Unable to get users";
                await Clients.Client(UserList[SenderId.ToString()]).SendAsync("AllUserReceivedError", apiResponse);
            }

        }



    }
}
