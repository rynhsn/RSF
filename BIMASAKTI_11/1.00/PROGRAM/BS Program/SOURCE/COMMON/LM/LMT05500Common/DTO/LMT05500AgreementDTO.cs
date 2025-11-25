using System;
using System.Collections.Generic;
using System.Text;

namespace PMT05500COMMON.DTO
{
    public class LMT05500AgreementDTO : LMT05500Header
    {
        public string? CDEPT_CODE { get; set; }
        public string? CDEPT_NAME { get; set; }
        public string? CREF_NO { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public string? CTRANS_STATUS_DESCR { get; set; }
        public string? CAGREEMENT_TYPE { get; set; }
        public string? CDOC_NO { get; set; }
        public string? CDOC_DATE { get; set; }

        public string? CCHARGE_MODE { get; set; }
        public string? CBUILDING_ID { get; set; }

        public string? CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string? CUNIT_DESCRIPTION { get; set; }

        // Properti baru dengan tipe data DateTime
        public DateTime? DSTART_DATE { get; set; }
        public DateTime? DEND_DATE { get; set; }
        public DateTime? DDOC_DATE { get; set; }
        //CR 21-8-2024
        public string? CCATEGORY { get; set; }
    }
}
