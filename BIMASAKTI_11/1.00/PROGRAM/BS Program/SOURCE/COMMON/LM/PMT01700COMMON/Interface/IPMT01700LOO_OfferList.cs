using PMT01700COMMON.DTO._2._LOO._1._LOO___Offer_List;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.Interface
{
    public interface IPMT01700LOO_OfferList
    {
        IAsyncEnumerable<PMT01700LOO_OfferList_OfferListDTO> GetOfferList();
        IAsyncEnumerable<PMT01700LOO_OfferList_UnitListDTO> GetUnitList();
        PMT01700LOO_OfferList_OfferListDTO UpdateAgreement(PMT01700LOO_ProcessOffer_DTO poEntity);
    }
}
