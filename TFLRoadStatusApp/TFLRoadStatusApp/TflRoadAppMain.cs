using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TflRoadStatusApp.Helpers;
using TflRoadStatusApp.Helpers.Interfaces;
using TflRoadStatusApp.Modal.enums;

namespace TflRoadStatusApp
{
    public class TflRoadAppMain: ITflRoadAppMain
    {
        private readonly ITflRoadService _service;
        private readonly ILogger<TflRoadAppMain> _logger;
        private readonly ITflRoadMessageHelper _messageHelper;

        public TflRoadAppMain(ITflRoadService service, ILogger<TflRoadAppMain> logger, ITflRoadMessageHelper messageHelper)
        {
            _service = service;
            _logger = logger;
            _messageHelper = messageHelper;
        }
        /// <summary>
        /// Takes the Road Names as arguments and returns with the Exitcode
        /// </summary>
        /// <param name="args"></param>
        /// <returns>int</returns>
        public async Task<int> RunAsync(string[] args)
        {
            _messageHelper.Print("Welcome To TFL Road Status App:");
            string roadName = "";

            if (args.Length == 0) // Check if application has run without any params. If yes, then ask for user to provide Road Name.
            {
                _messageHelper.Print("Please Provide the Road Names To Search For: (for example: A1 or A1,A2)");
                roadName = _messageHelper.Read();
            }

            string roadIds = args.Length > 0 ? string.Join(",", args) : roadName;

            try
            {
                _messageHelper.Print("");
                _logger.LogInformation($"Getting Status Info For Road Ids {roadIds}");
                var response = await _service.Get($"Road/{roadIds}"); //Calls TflRoadService to get the data from API {baseurl}/Road/{ids}
                var strResponse = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Response Message For Road Ids {roadIds} From API: {strResponse}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return _messageHelper.PrintSuccessMessage(strResponse); //Display the details of Road and return with exit code

                }
                else
                {
                    return _messageHelper.PrintInvalidRoadMessage(strResponse); //Display the details of Invalid Road and return with exit code

                }
            }
            catch (Exception ex)
            {
                _messageHelper.Print("Something went wrong. Please check app logs for more details.");
                _logger.LogError($"Error Occured in the app while calling API for Road {roadIds}: " + ex.Message);
                _logger.LogError($"Exception Details: " + ex);
                return (int)ExitCode.UnknownError;
            }
        }
    }
}
