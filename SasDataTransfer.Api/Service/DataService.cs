using Microsoft.EntityFrameworkCore;
using SasDataTransfer.Api.Controllers;
using SasDataTransfer.Api.Models;
using SasDataTransfer.Domain;
using SasDataTransfer.Domain.Models;

namespace SasDataTransfer.Api.Service
{
    public class DataService
    {
        private SasDataTransferContext _context;
        private SetTransferArgs? _setArgs;
        private GetTransferArgs? _getArgs;
        public DataService(SasDataTransferContext context, SetTransferArgs args)
        {
            _context = context;
            _setArgs = args;
        }
        public DataService(SasDataTransferContext context, GetTransferArgs args)
        {
            _context = context;
            _getArgs = args;
        }

        public async Task<Transfer?> GetTransfer()
        {
            try
            {
                int protocolId = await _context.Protocol.Where(x => x.ProtocolName == _getArgs.Protocol).Select(x=>x.Id).FirstOrDefaultAsync();
                if(protocolId>0)
                {
                    int analysisId= await _context.Analysis.Where(x=>x.ProtocolId==protocolId && x.AnalysisName==_getArgs.Analysis)
                        .Select(x=>x.Id).FirstOrDefaultAsync();

                    if (analysisId > 0)
                    {
                        var transfer = await _context.Transfer.Where(x => x.AnalysisId == analysisId && x.IsTransferSuccessful == null)
                            .OrderByDescending(x => x.RequestDate).FirstOrDefaultAsync();

                        return transfer;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public async Task<Response> CreateRequest()
        {
            var protocol = await SetProtocol(_setArgs.Protocol.ToLower());
            if (protocol.Key == 0)
                return new Response(false, protocol.Value, MessageType.Error);

            var analysis = await SetAnalysis(protocol.Key, _setArgs.Analysis.ToLower());
            if (analysis.Key == 0)
                return new Response(false, analysis.Value, MessageType.Error);

            var transfer = await SetTransfer(analysis.Key);
            if (transfer.Key == 0)
                return new Response(false, transfer.Value, MessageType.Error);

            var response = new Response(true, "success", MessageType.Info);
            response.TransferId = transfer.Key;
            return response;
        }

        private async Task<KeyValuePair<int, string>> SetTransfer(int fk)
        {
            try
            {
                var transfer = new Transfer()
                {
                    AnalysisId = fk,
                    OutputLoc = _setArgs.OutDir,
                    InputLoc = _setArgs.InDir,
                    UserAccount = _setArgs.User ?? string.Empty,
                    RequestDate = DateTime.Now,
                    SjmJobName = _setArgs.SjmJob,
                    IsTransferSuccessful = null
                };
                _context.Transfer.Add(transfer);
                await _context.SaveChangesAsync();
                return new KeyValuePair<int, string>(transfer.Id, $"Transfer request '{transfer.Id}' created");

            }
            catch (Exception ex)
            {
                return new KeyValuePair<int, string>(0, ex.Message);
            }
        }
        private async Task<KeyValuePair<int, string>> SetAnalysis(int fk, string analysisName)
        {
            try
            {
                var a = await _context.Analysis.FirstOrDefaultAsync(x => x.AnalysisName == analysisName &&
                                                                         x.ProtocolId == fk);
                if (a != null)
                {
                    return new KeyValuePair<int, string>(a.Id, string.Empty);
                }

                var analysis = new Analysis()
                {
                    AnalysisName = analysisName,
                    ProtocolId = fk
                };
                _context.Analysis.Add(analysis);

                await _context.SaveChangesAsync();
                return new KeyValuePair<int, string>(analysis.Id, string.Empty);
            }
            catch (Exception ex)
            {
                return new KeyValuePair<int, string>(0, ex.Message);
            }
        }

        private async Task<KeyValuePair<int, string>> SetProtocol(string protocolName)
        {
            try
            {
                var p = await _context.Protocol.FirstOrDefaultAsync(x => x.ProtocolName.ToLower() == protocolName);
                if (p != null)
                {
                    return new KeyValuePair<int, string>(p.Id, string.Empty);
                }

                var protocol = new Protocol()
                {
                    ProtocolName = protocolName
                };
                _context.Protocol.Add(protocol);
                await _context.SaveChangesAsync();
                return new KeyValuePair<int, string>(protocol.Id, string.Empty);
            }
            catch (Exception ex)
            {
                return new KeyValuePair<int, string>(0, ex.Message);
            }
        }
    }
}
