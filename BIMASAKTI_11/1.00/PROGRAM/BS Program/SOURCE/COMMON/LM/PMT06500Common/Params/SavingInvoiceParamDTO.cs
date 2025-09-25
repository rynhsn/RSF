using PMT06500Common.DTOs;
using R_CommonFrontBackAPI;

namespace PMT06500Common.Params
{
    public class SavingInvoiceParamDTO<T>
    {
        public PMT06500InvoiceDTO Entity { get; set; }
        public eCRUDMode CRUDMode { get; set; }
    }
}