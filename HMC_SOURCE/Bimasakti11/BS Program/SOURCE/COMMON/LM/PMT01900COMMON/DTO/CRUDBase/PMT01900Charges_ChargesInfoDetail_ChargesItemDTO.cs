namespace PMT01900Common.DTO.CRUDBase
{
    public class PMT01900Charges_ChargesInfoDetail_ChargesItemDTO
    {
        public string? CITEM_NAME { get; set; }
        public int IQTY { get; set; }
        public decimal NUNIT_PRICE { get; set; }
        public decimal NDISCOUNT { get; set; }
        public decimal NTOTAL_PRICE { get; set; }

        //CR 24/07/2024 penambahan ISEQ sesuai db
        public int ISEQ { get; set; }
    }
}
