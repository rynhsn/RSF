using System;
using System.Collections.Generic;

namespace PMT01300COMMON
{
    public interface IPMT01300Init
    {
        PMT01300SingleResult<PMT01300TransCodeInfoGSDTO> GetTransCodeInfo();
        IAsyncEnumerable<PMT01300AgreementChargeCalUnitDTO> GetAllAgreementChargeCallUnitList();
        IAsyncEnumerable<PMT01300AgreementBuildingUtilitiesDTO> GetAllBuildingUtilitiesList();
        IAsyncEnumerable<PMT01300PropertyDTO> GetAllPropertyList();
        IAsyncEnumerable<PMT01300UniversalDTO> GetAllUniversalList();
    }
}
