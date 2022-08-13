namespace TflRoadStatusApp.Helpers.Interfaces
{
    public interface ITflRoadMessageHelper
    {
        void Print(string messageToPrint);
        string Read();
        int PrintInvalidRoadMessage(string strResponse);
        int PrintSuccessMessage(string strResponse);
    }
}