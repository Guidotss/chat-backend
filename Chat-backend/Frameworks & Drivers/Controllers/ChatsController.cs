using Chat_backend.Adapters.Errors;
using Chat_backend.Interfaces;
using Chat_backend.Interfaces.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Chat_backend.Frameworks___Drivers.Controllers
{
    [Route("api/chats")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository; 

        public ChatsController(IChatRepository chatRepository, IMessageRepository messageRepository) 
        {
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
        }
        private IActionResult HandleErrors(Exception ex)
        {
            if(ex is HttpError httpError)
            {
                return StatusCode(httpError.StatusCode, new { ok = false, message = httpError.Message });
            }

            return ex switch
            {
                _ => StatusCode(500, new { ok = false, message = "Internal server errror", error = ex.Message })
            }; 
        }

        private Guid CheckUUID(string id)
        {
            bool isValid = Guid.TryParse(id, out Guid result); 
            if(!isValid)
            {
                throw new Exception("Invalid UUID");
            }
            return result;
        }

        private async Task HandleWebSocketConnection(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while(!result.CloseStatus.HasValue)
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);   
                var newMessage = JsonSerializer.Deserialize<NewMessageDto>(message);
                if(newMessage != null)
                {
                    await _messageRepository.CreateMessage(newMessage);
                    await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                }
                
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);    
        } 
        
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateNewChat([FromBody] NewChatDto chat)
        {
            try
            {
                var newChat = await _chatRepository.CreateChat(chat);
                return Ok(new { ok = true, chat = newChat });
            }
            catch (Exception ex)
            {
                return HandleErrors(ex); 
            }
        }

        [HttpGet]
        [Route("messages")] 
        public async Task<IActionResult> CreateNewMessage()
        {
            try
            {
                if (HttpContext.WebSockets.IsWebSocketRequest)
                {
                    WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                    await HandleWebSocketConnection(webSocket);
                }
                else
                {
                    return BadRequest(new { ok = false, message = "Websocket connection is required" });
                }

                return Ok();  
            }catch(Exception ex)
            {
                return HandleErrors(ex);
            }
        }

        [HttpPost]
        [Route("add-user")]
        public async Task<IActionResult> AddUserToChat([FromBody] AddUserToChatDto newUser) 
        {
            try
            {
                await _chatRepository.AddUserToChat(newUser.ChatId, newUser.UserId);
                return Ok(new { ok = true, message = "User added to chat" });
            }catch(Exception ex)
            {
                return HandleErrors(ex); 
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetChatById()
        {
            string id = HttpContext.Request.RouteValues["id"]?.ToString()!;
            try
            {
                Guid idParsed = CheckUUID(id);
                var chat = await _chatRepository.GetChatById(idParsed);
                if(chat == null)
                {
                    return BadRequest(new { ok = false, message = "Chat not found" });
                }


                var chatUsersIds = chat.ChatUsers.Select(chatUser => chatUser.UserId);
                var chatMessages = chat.Messages.Select(message => new MessageDto{ Id = message.Id, Content = message.Content, UserId = message.UserId });
                var response = new GetChatByIdResponseDto
                {
                    Name = chat.Name,
                    Messages = chatMessages.ToArray(),
                    UsersId = chatUsersIds.ToArray(),
                };

                return Ok(new { ok = true, chat = response }); 
   

            }catch(Exception ex)
            {
                return HandleErrors(ex); 
            }
        }
        
    }
}
