using System.Collections.Generic;

namespace PMM01000COMMON
{
    public class PMM01000TopPrintDTO
    {
        public string CCHARGES_TYPE_DESCR { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public List<PMM01000HeaderPrintDTO> DataCharges { get; set; }
    }
}
