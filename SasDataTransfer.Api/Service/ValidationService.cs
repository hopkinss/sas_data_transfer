using SasDataTransfer.Api.Models;
using RestSharp;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace SasDataTransfer.Api.Service
{

    public class ValidationService
    {
        private SetTransferArgs _request;
        private IConfiguration _cfg;
        public ValidationService(SetTransferArgs request, IConfiguration cfg)
        {
            _request = request;
            _cfg = cfg;
        }

        public async Task<Response> Validate()
        {
            if (!Directory.Exists(_request.InDir))
                return new Response(false, $"The folder {_request.InDir} is not valid", MessageType.Error);
            if (!Directory.Exists(_request.OutDir))
                return new Response(false, $"The folder {_request.OutDir} is not valid", MessageType.Error);
            if (await CheckIsLocked(_request.OutDir))
                return new Response(false, $"The output folder {_request.OutDir} is locked by IT", MessageType.Error);
            if (!await SasJobExists(_request.SjmJob))
                return new Response(false, $"The SJM job '{_request.SjmJob}' was not located in the database", MessageType.Error);

            return new Response(true, string.Empty, MessageType.Info);
        }

        private async Task<bool> SasJobExists(string sjmJob)
        {
            int sjmCount = 0;
            using (SqlConnection conn = new SqlConnection(_cfg.GetConnectionString("sjm")))
            {
                var cmd = new SqlCommand($"select count (*) from [SJM].[dbo].[Jobs] " +
                                         $"where LOWER(description)='{_request.SjmJob.ToLower()}'",conn);
                try
                {
                    conn.Open();
                    sjmCount = (int)await cmd.ExecuteScalarAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("Cant access the SJM database");
                }
            }
            return sjmCount>0;
        }

        private async Task<bool> CheckIsLocked(string outDir)
        {
            try
            {
                var client = new RestClient(_cfg["sjm_base"]);
                var request = new RestRequest($"api/islocked?folder={outDir}", Method.Get);
                var response = await client.ExecuteAsync(request);
                var parseResponse = JsonConvert.DeserializeObject<Tuple<bool, string>>(response.Content);
                return parseResponse.Item1;
            }
            catch (Exception ex)
            {

            }

            return false;
        }
    }
}
