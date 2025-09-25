
using System.Security;

namespace PMT01100Common.Utilities.Front
{
    public class PMT01100ParameterFrontChangePageDTO
    {
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; } = "";
        //For Unit Info, Charges, and Deposit
        public string? CBUILDING_ID { get; set; } = "";
        public string? CBUILDING_NAME { get; set; } = "";
        //Updated : For call from Page PMT01200
        public string? CALLER_ACTION { get; set; } = "";

        //Updated 13 June 2024 : For Data From OfferList
        public string? ODataUnitList { get; set; }

    }
}
