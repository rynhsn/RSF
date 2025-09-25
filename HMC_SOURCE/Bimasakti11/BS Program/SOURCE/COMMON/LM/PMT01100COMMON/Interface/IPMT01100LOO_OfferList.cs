using PMT01100Common.DTO._2._LOO._1._LOO___Offer_List;
using System.Collections.Generic;

namespace PMT01100Common.Interface
{
    public interface IPMT01100LOO_OfferList
    {
        IAsyncEnumerable<PMT01100LOO_OfferList_OfferListDTO> GetOfferList();
        IAsyncEnumerable<PMT01100LOO_OfferList_UnitListDTO> GetUnitList();
    }
}
