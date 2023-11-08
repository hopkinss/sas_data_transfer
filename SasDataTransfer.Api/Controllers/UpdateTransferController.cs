using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SasDataTransfer.Api.Models;
using SasDataTransfer.Api.Service;
using SasDataTransfer.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SasDataTransfer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateTransferController : ControllerBase
    {
        private IConfiguration _cfg;
        private SasDataTransferContext _context;
        private IHttpContextAccessor _ca;
        public UpdateTransferController(IConfiguration cfg, SasDataTransferContext context, IHttpContextAccessor httpContextAccessor)
        {
            NetworkService.MapDrive(cfg);
            _cfg = cfg;
            _context = context;
            _ca = httpContextAccessor;
        }



        [HttpPost]
        public async Task<Response> Post(object value)
        {
            try
            {
                var args = JsonConvert.DeserializeObject<UpdateTransferArgs>(value?.ToString());
                if (args != null)
                {
                    var ds = new UpdateDataService(_context, args);
                    var response = await ds.UpdateTransfer();
                    return response;
                }
                return new Response(false, $"Unable to parse arguments", MessageType.Error);
            }
            catch (Exception ex)
            {
                
                return new Response(false, ex.Message, MessageType.Error);
            }
        }
    }
}
