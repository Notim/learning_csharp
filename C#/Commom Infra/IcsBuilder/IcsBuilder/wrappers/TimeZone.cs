namespace root.wrappers {

    public class TimeZone {
        public string timeZoneId { get; set; }
        
        public TimeZoneSet Standard { get; set; }
        public TimeZoneSet DayLight { get; set; }
    }

}