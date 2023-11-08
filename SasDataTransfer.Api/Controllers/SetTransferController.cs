using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SasDataTransfer.Api.Models;
using SasDataTransfer.Api.Service;
using SasDataTransfer.Domain;


namespace SasDataTransfer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetTransferController : ControllerBase
    {
        private IConfiguration _cfg;
        private SasDataTransferContext _context;
        private IHttpContextAccessor _ca;
        public SetTransferController(IConfiguration cfg,SasDataTransferContext context,IHttpContextAccessor httpContextAccessor)
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
                var args = JsonConvert.DeserializeObject<SetTransferArgs>(value?.ToString());
                if (args != null)
                {
                    args.User = args.User ?? GetCurrentUser(_ca);
                    var checkArgs = new ValidationService(args,_cfg);
                    var checkStatus = await checkArgs.Validate();

                    if (!checkStatus.IsSuccess)
                        return checkStatus;

                    var ds = new DataService(_context, args);                   
                    var response = await ds.CreateRequest();
                    return response;
                }
                return new Response(false, $"Unable to parse arguments", MessageType.Error);
            }
            catch ( Exception ex)
            {
                return new Response(false,ex.Message,MessageType.Error);
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
