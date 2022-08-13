using System;
using System.Collections.Generic; 
using System.Text;
using System.Text.Json;
using TflRoadStatusApp.Helpers.Interfaces;
using TflRoadStatusApp.Modal;
using TflRoadStatusApp.Modal.enums;

namespace TflRoadStatusApp.Helpers
{
    public class TflRoadMessageHelper : ITflRoadMessageHelper
    {
        public TflRoadMessageHelper()
        {

        }

        public void Print(string messageToPrint)
        {
            Console.WriteLine(messageToPrint);
        }

        public string Read()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Parses the response string passed as param and display the details of Invalid Road
        /// Returns the Invlaid Status Code
        /// </summary>
        /// <param name="strResponse"></param>
        /// <returns>int</returns>
        public int PrintInvalidRoadMessage(string strResponse)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var errData = JsonSerializer.Deserialize<TflRoadStatusErrorResponse>(strResponse);
            if(errData.Message != null)
                stringBuilder.AppendLine($"{errData.Message.Split(":")[1].Trim()} is not a valid road");
            stringBuilder.Append("Please try again...");
            this.Print(stringBuilder.ToString());
            return (int)ExitCode.InvalidRoad;
        }
        /// <summary>
        /// Parses the response string passed as param and display the details of Road to Console
        /// Return the Success Exit Code
        /// </summary>
        /// <param name="strResponse"></param>
        /// <returns>int</returns>
        public int PrintSuccessMessage(string strResponse)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var data = JsonSerializer.Deserialize<List<TflRoadStatusSuccessResponse>>(strResponse);
            foreach (TflRoadStatusSuccessResponse item in data)
            {
                stringBuilder.AppendLine($"The status of the {item.DisplayName} is as follows");
                stringBuilder.AppendLine($"Road Status is {item.StatusSeverity}");
                stringBuilder.AppendLine($"Road Status Description is {item.StatusSeverityDescription}");
                stringBuilder.AppendLine("");
            }
            this.Print(stringBuilder.ToString());
            return (int)ExitCode.Success;
        }
    }
}
