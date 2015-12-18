 

namespace RapidAlert.Classes
{
    public enum Status
    {
       Detection=1,
       Status =0
    }
    public class RapidFile
    {
        public string FileName { get; set; }
        public string Location { get; set; }
        public string ComputerName { get; set; }
        public Status DetectionStatus { get; set; }
    }
}
