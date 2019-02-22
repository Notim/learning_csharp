using System;
using System.Text;

namespace root {
    
    
    
    public class IcsEvent {
        public DateTime StartDate { get; set; }
        public DateTime EndDate   { get; set; }
            
        public string summary     { get; set; }
        public string location    { get; set; }
        public string description { get; set; }
        public string fileName    { get; set; }
    }
    
    public class IcsBuilderController {
        
        public string getIcs() {
            DateTime DateStart = DateTime.Now;
            DateTime DateEnd   = DateStart.AddMinutes(105);

            string Summary     = "asdasdasdasdasdasdasdasdasdasdasda adsas dasd asd assd asda sdasd asd asd asas as dasd asdsa dsasd aswd a sda sa ds";
            string Location    = "Local do evento";
            string Description = "Descrição do evento";
            string FileName    = "item-de-calendário";

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("PRODID:sinctec-sistemas");
            sb.AppendLine("CALSCALE:GREGORIAN");
            sb.AppendLine("METHOD:PUBLISH");

            sb.AppendLine("BEGIN:VTIMEZONE");
            sb.AppendLine("TZID:America/Sao_Paulo");
            sb.AppendLine("BEGIN:STANDARD");
            sb.AppendLine("TZOFFSETTO:+0100");
            sb.AppendLine("TZOFFSETFROM:+0100");
            sb.AppendLine("END:STANDARD");
            sb.AppendLine("END:VTIMEZONE");

            sb.AppendLine("BEGIN:VEVENT");

            sb.AppendLine("DTSTART;TZID=America/Sao_Paulo:" + DateStart.ToString("yyyyMMddTHHmm00"));
            sb.AppendLine("DTEND;TZID=America/Sao_Paulo:"   + DateEnd.ToString("yyyyMMddTHHmm00"));

            sb.AppendLine("SUMMARY:" + Summary         + "");
            sb.AppendLine("LOCATION:" + Location       + "");
            sb.AppendLine("DESCRIPTION:" + Description + "");
            sb.AppendLine("PRIORITY:3");

            sb.AppendLine("END:VEVENT");

            sb.AppendLine("END:VCALENDAR");

            string CalendarItem = sb.ToString();

            return CalendarItem;
        }
    }

}