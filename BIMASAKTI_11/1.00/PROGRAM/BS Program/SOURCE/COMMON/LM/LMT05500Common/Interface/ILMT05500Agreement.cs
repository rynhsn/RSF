using System;
using System.Collections.Generic;
using System.Text;
using PMT05500COMMON.DTO;

namespace PMT05500COMMON.Interface
{
    public interface ILMT05500Agreement
    {
        IAsyncEnumerable<LMT05500AgreementDTO> AgreementListStream();
        IAsyncEnumerable<LMT05500PropertyDTO> GetPropertyListStream();
        IAsyncEnumerable<LMT05500UnitDTO> GetDepositUnitStream();
    }
}
