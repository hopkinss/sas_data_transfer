using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SasDataTransfer.Api.Models;
using SasDataTransfer.Api.Service;
using SasDataTransfer.Domain;
using SasDataTransfer.Domain.Models;

namespace SasDataTransfer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetTransferController : ControllerBase
    {
        private IConfiguration _cfg;
        private SasDataTransferContext _context;
        private IHttpContextAccessor _ca;
        public GetTransferController(IConfiguration cfg, SasDataTransferContext context, IHttpContextAccessor httpContextAccessor)
        {
            NetworkService.MapDrive(cfg);
            _cfg = cfg;
            _context = context;
            _ca = httpContextAccessor;
        }


        [HttpPost]
        public async Task<IActionResult> Post(object value)
        {
            try
            {
                var args = JsonConvert.DeserializeObject<GetTransferArgs>(value?.ToString());
                if (args != null)
                {
                    args.User = args.User ?? GetCurrentUser(_ca);

                    var ds = new DataService(_context, args);
                    var response = await ds.GetTransfer();
                    if (response != null)
                    {
                        return Ok(response);
                    }

                    return NoContent();
                }
               return BadRequest("Invalid arguments to GetTransfer, See SPI for assistance");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string? GetCurrentUser(IHttpContextAccessor ca)
        {
            try
            {
                var name = ca.HttpContext?.User.Identity?.Name?.Split('\\')[1];
                return !string.IsNullOrWhiteSpace(name) ? name : "anonymous";
            }
            catch
            {
                return "anonymous";
            }
        }
    }
}
