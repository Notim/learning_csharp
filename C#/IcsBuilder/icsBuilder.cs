using System;
using System.Text;
using System.Web.Mvc;

using WEB.App_Infrastructure;

namespace WEB.Areas.Agendamentos.Controllers {
    
    public class Ics {
        
        public string prodId  { get; set; }
        public string version { get; set; }
        public string method { get; set; }
        public string tzid { get; set; }
            
        public string summary     { get; set; }
        public string location    { get; set; }
        public string description { get; set; }
        public string fileName    { get; set; }
    }
    
    public class IcsEvent {
        public DateTime StartDate { get; set; }
        public DateTime EndDate   { get; set; }
            
        public string summary     { get; set; }
        public string location    { get; set; }
        public string description { get; set; }
        public string fileName    { get; set; }
    }
    
    public class IcsBuilderController : BaseSistemaController {
        
        [HttpGet]
        public void getIcs() {
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

            Response.ClearContent();
            Response.ClearHeaders();
            Response.Clear();
            Response.Buffer      = true;
            Response.ContentType = "text/calendar";
            Response.AddHeader("content-length",      CalendarItem.Length.ToString());
            Response.AddHeader("content-disposition", "attachment; filename=\"" + FileName + ".ics\"");
            Response.Charset         = "UTF-8";
            Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            Response.Write(CalendarItem);
            Response.Flush();
            Response.End();
        }
    }

}