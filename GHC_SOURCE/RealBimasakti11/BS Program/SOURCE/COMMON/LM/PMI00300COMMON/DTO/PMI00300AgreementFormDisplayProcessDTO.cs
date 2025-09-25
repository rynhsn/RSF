using System;
using System.Collections.Generic;
using System.Text;

namespace PMI00300COMMON.DTO
{
    public class PMI00300AgreementFormDisplayProcessDTO : PMI00300AgreementFormDetailParameterDTO
    {
        public int IPERIOD_YEAR { get; set; } = DateTime.Now.Year;
    }
}