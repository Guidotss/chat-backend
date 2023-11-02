using Chat_backend.Interfaces;
using Chat_backend.Interfaces.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat_backend.Frameworks___Drivers.Controllers
{
    [Route("api/chats")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IChatRepository _chatRepository;

        public ChatsController(IChatRepository chatRepository) 
        {
            _chatRepository = chatRepository;
        }
        private IActionResult HandleErrors(Exception ex)
        {
            return ex switch
            {
                _ => StatusCode(500, new { ok = false, message = "Internal server errror", error = ex.Message })
            }; 
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
        
    }
}
