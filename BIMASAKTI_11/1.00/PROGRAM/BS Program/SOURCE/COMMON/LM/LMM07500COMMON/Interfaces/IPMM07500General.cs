using PMM07500COMMON.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM07500COMMON.Interfaces
{
    public interface IPMM07500General
    {
        IAsyncEnumerable<PropertyDTO> GetPropertyList();
        IAsyncEnumerable<CurrencyDTO> GetCurrencyList();

    }
}
