using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02580
{
    public class GSM02580DTO
    {
        public bool LSHOW_IN_MOBILE { get; set; }

        //Monday
        public bool LMONDAY { get; set; }
        public string CMONDAY_OPEN_H { get; set; }
        public int IMONDAY_OPEN_H { get; set; } = 0;
        public string CMONDAY_OPEN_M { get; set; }
        public int IMONDAY_OPEN_M { get; set; } = 0;
        public string CMONDAY_CLOSE_H { get; set; } 
        public int IMONDAY_CLOSE_H { get; set; } = 0;
        public string CMONDAY_CLOSE_M { get; set; }
        public int IMONDAY_CLOSE_M { get; set; } = 0;
        //public string CMONDAY_OPEN_TIME { get; set; }
        //public string CMONDAY_CLOSE_TIME { get; set; }

        //Tuesday
        public bool LTUESDAY { get; set; }
        public string CTUESDAY_OPEN_H { get; set; }
        public int ITUESDAY_OPEN_H { get; set; } = 0;
        public string CTUESDAY_OPEN_M { get; set; }
        public int ITUESDAY_OPEN_M { get; set; } = 0;
        public string CTUESDAY_CLOSE_H { get; set; }
        public int ITUESDAY_CLOSE_H { get; set; } = 0;
        public string CTUESDAY_CLOSE_M { get; set; }
        public int ITUESDAY_CLOSE_M { get; set; } = 0;
        //public string CTUESDAY_OPEN_TIME { get; set; }
        //public string CTUESDAY_CLOSE_TIME { get; set; }

        //Wednesday
        public bool LWEDNESDAY { get; set; }
        public string CWEDNESDAY_OPEN_H { get; set; }
        public int IWEDNESDAY_OPEN_H { get; set; } = 0;
        public string CWEDNESDAY_OPEN_M { get; set; }
        public int IWEDNESDAY_OPEN_M { get; set; } = 0;
        public string CWEDNESDAY_CLOSE_H { get; set; }
        public int IWEDNESDAY_CLOSE_H { get; set; } = 0;
        public string CWEDNESDAY_CLOSE_M { get; set; }
        public int IWEDNESDAY_CLOSE_M { get; set; } = 0;
        //public string CWEDNESDAY_OPEN_TIME { get; set; }
        //public string CWEDNESDAY_CLOSE_TIME { get; set; }

        //Thursday
        public bool LTHURSDAY { get; set; }
        public string CTHURSDAY_OPEN_H { get; set; }
        public int ITHURSDAY_OPEN_H { get; set; } = 0;
        public string CTHURSDAY_OPEN_M { get; set; }
        public int ITHURSDAY_OPEN_M { get; set; } = 0;
        public string CTHURSDAY_CLOSE_H { get; set; }
        public int ITHURSDAY_CLOSE_H { get; set; } = 0;
        public string CTHURSDAY_CLOSE_M { get; set; }
        public int ITHURSDAY_CLOSE_M { get; set; } = 0;
        //public string CTHURSDAY_OPEN_TIME { get; set; }
        //public string CTHURSDAY_CLOSE_TIME { get; set; }

        //Friday
        public bool LFRIDAY { get; set; }
        public string CFRIDAY_OPEN_H { get; set; }
        public int IFRIDAY_OPEN_H { get; set; } = 0;
        public string CFRIDAY_OPEN_M { get; set; }
        public int IFRIDAY_OPEN_M { get; set; } = 0;
        public string CFRIDAY_CLOSE_H { get; set; }
        public int IFRIDAY_CLOSE_H { get; set; } = 0;
        public string CFRIDAY_CLOSE_M { get; set; }
        public int IFRIDAY_CLOSE_M { get; set; } = 0;
        //public string CFRIDAY_OPEN_TIME { get; set; }
        //public string CFRIDAY_CLOSE_TIME { get; set; }

        //Saturday
        public bool LSATURDAY { get; set; }
        public string CSATURDAY_OPEN_H { get; set; }
        public int ISATURDAY_OPEN_H { get; set; } = 0;
        public string CSATURDAY_OPEN_M { get; set; }
        public int ISATURDAY_OPEN_M { get; set; } = 0;
        public string CSATURDAY_CLOSE_H { get; set; }
        public int ISATURDAY_CLOSE_H { get; set; } = 0;
        public string CSATURDAY_CLOSE_M { get; set; }
        public int ISATURDAY_CLOSE_M { get; set; } = 0;
        //public string CSATURDAY_OPEN_TIME { get; set; }
        //public string CSATURDAY_CLOSE_TIME { get; set; }

        //Sunday
        public bool LSUNDAY { get; set; }
        public string CSUNDAY_OPEN_H { get; set; }
        public int ISUNDAY_OPEN_H { get; set; } = 0;
        public string CSUNDAY_OPEN_M { get; set; }
        public int ISUNDAY_OPEN_M { get; set; } = 0;
        public string CSUNDAY_CLOSE_H { get; set; }
        public int ISUNDAY_CLOSE_H { get; set; } = 0;
        public string CSUNDAY_CLOSE_M { get; set; }
        public int ISUNDAY_CLOSE_M { get; set; } = 0;
        //public string CSUNDAY_OPEN_TIME { get; set; }
        //public string CSUNDAY_CLOSE_TIME { get; set; }

        public string CNOTES { get; set; }
    }
}
