using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100REPORTCOMMON.DTOs.PMT02100PDF
{
    public class PMT02100InvitationValueReportDataDTO
    {
        //INI BUAT STORAGE
        //public string? CSTORAGE_ID { get; set; }
        //public string? CINVOICE_CODE { get; set; }
        //Buat SUBJECT
        //public string? CTENANT_NAME { get; set; }

        //INI BUAT EMAIL
        public string? CFILE_NAME { get; set; }
        public string? CFILE_ID { get; set; }
        public byte[]? OFILE_DATA_REPORT { get; set; }
        public bool LDATA_READY { get; set; }  // Misalnya ini untuk mengecek apakah file siap dikirim
    }
}
