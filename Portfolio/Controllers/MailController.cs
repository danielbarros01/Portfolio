using Microsoft.AspNetCore.Mvc;
using Portfolio.Entities;
using Portfolio.Services.Email;

namespace Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public MailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(MailRequest mailRequest)
        {
            try
            {
                await _emailService.SendEmail(mailRequest);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}