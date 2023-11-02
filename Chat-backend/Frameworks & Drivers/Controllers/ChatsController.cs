using Chat_backend.Interfaces;
using Chat_backend.Interfaces.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            return ex switch
            {
                _ => StatusCode(500, new { ok = false, message = "Internal server errror", error = ex.Message })
            }; 
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
        
    }
}
