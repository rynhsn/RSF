using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMR02400BACK
{
    public class PrintBaseHeaderResultDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CCOMPANY_NAME { get; set; }
        public string CDATETIME_NOW { get; set; }
        public byte[] CLOGO { get; set; }
    }
}
