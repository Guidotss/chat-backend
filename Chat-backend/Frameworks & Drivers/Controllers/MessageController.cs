using Chat_backend.Interfaces;
using Chat_backend.Interfaces.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat_backend.Frameworks___Drivers.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository; 

        public MessageController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        private IActionResult HandleErrors(Expression<Func<IActionResult>> expression)
        {
            try
            {
                return expression.Compile().Invoke();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, message = "Internal Server error", error = ex.Message });
            }
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var messages = await _messageRepository.GetAllMessages(); 
                return Ok(new { ok = true, messages }); 
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { ok = false, message = "Internal Server error", error = ex.Message }); 
            }
        }


        public async Task<IActionResult> CreateMessage([FromBody] NewMessageDto message)
        {
            try
            {
                var newMessage = await _messageRepository.CreateMessage(message);
                return Ok(new { ok = true, message = newMessage }); 
            }catch(Exception ex)
            {
                return HandleErrors(() => StatusCode(500, new { ok = false, message = "Internal Server error", error = ex.Message }));
            }
        }

        
    }
}
