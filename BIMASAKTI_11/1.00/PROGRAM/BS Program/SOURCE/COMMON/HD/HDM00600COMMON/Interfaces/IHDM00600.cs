using HDM00600COMMON.DTO;
using HDM00600COMMON.DTO.General;
using HDM00600COMMON.DTO.Helper;
using HDM00600COMMON.DTO_s.Helper;
using R_APICommonDTO;
using System;
using System.Collections.Generic;

namespace HDM00600COMMON.Interfaces
{
    public interface IHDM00600
    {
        IAsyncEnumerable<PropertyDTO> GetPropertyList();
        IAsyncEnumerable<CurrencyDTO> GetCurrencyList();
        GeneralAPIResultBaseDTO<ActiveInactivePricelistParam> ActiveInactive_Pricelist(ActiveInactivePricelistParam poParam);
        IAsyncEnumerable<PricelistDTO> GetList_Pricelist();
        GeneralAPIResultBaseDTO<GeneralFileByteDTO> DownloadFile_TemplateExcelUpload();
    }
}
