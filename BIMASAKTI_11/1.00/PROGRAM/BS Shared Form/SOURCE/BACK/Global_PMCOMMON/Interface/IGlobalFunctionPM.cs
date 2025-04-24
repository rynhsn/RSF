using Global_PMCOMMON.DTOs.Generic__DTO;
using Global_PMCOMMON.DTOs.Response.Invoice_Type;
using Global_PMCOMMON.DTOs.Response.Property;
using Global_PMCOMMON.DTOs.User_Param_Detail;
using System;
using System.Collections.Generic;
using System.Text;

namespace Global_PMCOMMON.Interface
{
    public interface IGlobalFunctionPM
    {
        GenericRecord<GetUserParamDetailDTO> UserParamDetail(GetUserParamDetailParameterDTO poParam);
        IAsyncEnumerable<PropertyDTO> PropertyList();
        IAsyncEnumerable<InvoiceTypeDTO> InvoiceType(InvoiceTypeParameterDTO poParam);
    }
}
