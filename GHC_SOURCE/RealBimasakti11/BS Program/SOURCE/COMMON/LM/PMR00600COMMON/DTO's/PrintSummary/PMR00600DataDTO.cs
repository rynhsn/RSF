using PMR00600COMMON.DTO_s;
using System;

namespace PMR00600COMMON
{
    public class PMR00600DataDTO : PMR00600SPResultDTO
    {
        public decimal NSUM_AMOUNT
        {
            get
            {
                return NJAN_AMOUNT + NFEB_AMOUNT + NMAR_AMOUNT + NAPR_AMOUNT + NMAY_AMOUNT + NJUN_AMOUNT +
                       NJUL_AMOUNT + NAUG_AMOUNT + NSEP_AMOUNT + NOCT_AMOUNT + NNOV_AMOUNT + NDEC_AMOUNT;
            }
        }
    }
}
