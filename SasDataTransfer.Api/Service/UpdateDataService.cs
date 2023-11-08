using Microsoft.EntityFrameworkCore;
using SasDataTransfer.Api.Controllers;
using SasDataTransfer.Api.Models;
using SasDataTransfer.Domain;
using SasDataTransfer.Domain.Models;

namespace SasDataTransfer.Api.Service
{
    public class UpdateDataService
    {
        private SasDataTransferContext _context;
        private UpdateTransferArgs? _args;

        public UpdateDataService(SasDataTransferContext context, UpdateTransferArgs args)
        {
            _context = context;
            _args = args;
        }


        public async Task<Response> UpdateTransfer()
        {

            var transfer = await _context.Transfer.FirstOrDefaultAsync(x =>x.Id==_args.TransferId);
            if (transfer != null)
            {
                transfer.IsTransferSuccessful = _args.IsSuccess;
                _context.SaveChanges();

                var response = new Response(true, "Status Updated. ", MessageType.Info);
                response.TransferId = transfer.Id;


                if (_args.IsSuccess)
                {                   
                    try
                    {
                        foreach (var (k, v) in _args.Data)
                        {
                            var ds = new SasDataSet()
                            {
                                DataSetName = k,
                                TransferId = transfer.Id,
                                Variables = v.Select(x => new Variable()
                                {
                                    VariableName = x,

                                }).ToList()
                            };
                            _context.SasDataSet.Add(ds);
                            
                        }
                        await _context.SaveChangesAsync();
                        response.Message += $"{_args.Data.Count} datasets recorded";
                                           
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Message = ex.Message; 
                        response.MessageType = MessageType.Error;
                    }
                }
                return response;
            }
            return new Response(false,$"Could not location transfer with ID={_args.TransferId}",MessageType.Error);            
        }      
    }
}
