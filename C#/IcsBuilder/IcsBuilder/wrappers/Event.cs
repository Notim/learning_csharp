namespace root.wrappers {

    public class Event {
        public string classification { get; set; }
        public string description    { get; set; }
        public string dtStamp        { get; set; }
        public string dateEnd        { get; set; }
        public string dateStart      { get; set; }
        public string lastModified   { get; set; }
        public string location       { get; set; }
        public string priority       { get; set; }
        public string sequence       { get; set; }
        public string summary        { get; set; }
        public string transparency    { get; set; }
        public string uid    { get; set; }

    }

}

/*
 BEGIN:VEVENT
		CLASS:PUBLIC
		CREATED:20190219T143720Z
		DESCRIPTION: \n
		DTEND;TZID="E. South America Standard Time":20190219T123000
		DTSTAMP:20190219T143720Z
		DTSTART;TZID="E. South America Standard Time":20190219T120000
		LAST-MODIFIED:20190219T143720Z
		LOCATION:ADW ADWADWA
		PRIORITY:5
		SEQUENCE:0
		SUMMARY;LANGUAGE=pt-br:ADWWA DAD
		TRANSP:OPAQUE

		UID:040000008200E00074C5B7101A82E00800000000C097E76B47C8D40100000000000000001000000069228D3627028349A83D369930382229

		X-MICROSOFT-CDO-BUSYSTATUS:BUSY
		X-MICROSOFT-CDO-IMPORTANCE:1
		X-MICROSOFT-DISALLOW-COUNTER:FALSE
		X-MS-OLK-AUTOFILLLOCATION:FALSE
		X-MS-OLK-CONFTYPE:0

		BEGIN:VALARM
			TRIGGER:-PT15M
			ACTION:DISPLAY
			DESCRIPTION:Reminder
		END:VALARM
	END:VEVENT
 */