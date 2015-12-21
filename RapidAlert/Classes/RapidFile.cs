 

namespace RapidAlert.Classes
{
    public enum DetectionStatus
    {
       Detection=1,
       Status =0
    }
    public class RapidFile
    {
        public DetectionStatus status { get; set; }
        
        public string location { get; set; }
        public string computer_name { get; set; }
        
    }
}
