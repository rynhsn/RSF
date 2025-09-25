using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.DTO.Utilities.Front
{
    public class PMT01700ParameterFrontChangePageDTO
    {
        public string CPROPERTY_ID { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string? CREF_NO { get; set; } = "";
        //For Unit Info, Charges, and Deposit
        public string CBUILDING_ID { get; set; } = "";
        public string CBUILDING_NAME { get; set; } = "";
        //Updated : For call from Page PMT01200
        public string? CALLER_ACTION { get; set; } = "";

        //Updated 13 June 2024 : For Data From OfferList
        public string? ODataUnitList { get; set; }

        //Updated 21 June 2024 : For Data OFFER AND OFFER LIST
        public string? COTHER_UNIT_ID { get; set; }
        //For utilities tab
        public string? CCHARGE_MODE { get; set; }
        public string? CFLOOR_ID { get; set; }
        //05/07/2024 for btn Revis
        public bool LREVISE_BUTTON { get; set; }
        public string? CTRANS_STATUS { get; set; }
        //21/10/24
        public string? CCURRENCY_CODE { get; set; }

    }
}
