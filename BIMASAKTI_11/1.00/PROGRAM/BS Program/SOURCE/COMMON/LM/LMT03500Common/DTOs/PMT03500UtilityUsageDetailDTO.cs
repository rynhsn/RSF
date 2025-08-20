using System;

namespace PMT03500Common.DTOs
{
    public class PMT03500UtilityUsageDetailDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CUNIT_ID { get; set; }
        public string CUNIT_NAME { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CUSAGE_RATE_MODE { get; set; }
        public string CUSAGE_RATE_MODE_NAME { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CFLOOR_NAME { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_TYPE_DESCRS { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public string CSEQ_NO { get; set; }
        public string CINV_PRD { get; set; }
        public string CINV_YEAR { get; set; }
        public string CINV_MONTH { get; set; }
        public string CUTILITY_PRD { get; set; }
        public string CUTILITY_YEAR { get; set; }
        public string CUTILITY_MONTH { get; set; }
        public string CSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
        public DateTime DSTART_DATE { get; set; }
        public DateTime DEND_DATE { get; set; }
        public string CMETER_NO { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }

        public decimal NBLOCK1_START { get; set; }
        public decimal NBLOCK1_END { get; set; }
        public decimal NBLOCK1_USAGE { get; set; }
        public decimal NBLOCK2_START { get; set; }
        public decimal NBLOCK2_END { get; set; }
        public decimal NBLOCK2_USAGE { get; set; }
        public decimal NBEBAN_BERSAMA { get; set; }
        public decimal NAVG_BLOCK1_USAGE { get; set; }
        public decimal NAVG_BLOCK2_USAGE { get; set; }
        public decimal NKVA { get; set; }
        public decimal NCF { get; set; }
        public decimal NADMIN_FEE_PCT { get; set; }

        public decimal NSTANDING_CHARGE { get; set; }
        public decimal NLWBP_CHARGE { get; set; }
        public decimal NWBP_CHARGE { get; set; }
        public decimal NTRANSFORMATOR_FEE { get; set; }
        public decimal NRETRIBUTION_PCT { get; set; }
        public decimal NTTLB_PCT { get; set; }
        public decimal CADMIN_FEE { get; set; }


        public decimal NMETER_START { get; set; }
        public decimal NMETER_END { get; set; }

        public decimal NMETER_USAGE { get; set; }
        public decimal NUSAGE_CF { get; set; }

        public decimal NMAINTENANCE_FEE { get; set; }
        public decimal NUSAGE_MIN_CHARGE_AMT { get; set; }

        public decimal NTOTAL_BLOCK1_USAGE_PF => NBLOCK1_USAGE * NTRANSFORMATOR_FEE;
        public decimal NTOTAL_BLOCK2_USAGE_PF => NBLOCK2_USAGE * NTRANSFORMATOR_FEE;

        public decimal NTOTAL_STANDING_CHARGE => Math.Round(NSTANDING_CHARGE * NKVA, 2);
        public decimal NTOTAL_BLOCK1_CHARGE => Math.Round(NLWBP_CHARGE * NBLOCK1_USAGE, 2);
        public decimal NTOTAL_BLOCK2_CHARGE => Math.Round(NWBP_CHARGE * NBLOCK2_USAGE + 0, 2);
        public decimal NTOTAL_ADDITIONAL => 0 * 0;

        public decimal NTOTAL_RETRIBUTION => Math.Round(NRETRIBUTION_PCT / 100 * NTOTAL_STANDING_CONSUMPTION, 2);
        public decimal NTOTAL_TTLB => Math.Round(NTOTAL_RETRIBUTION / 100 * NTOTAL_STANDING_CONSUMPTION, 2);
        public decimal NTOTAL_TRANSFORMATOR_FEE => Math.Round(NTRANSFORMATOR_FEE * NKVA, 2);

        // public decimal NTOTAL =>
        //     Math.Round(
        //         NTOTAL_STANDING_CONSUMPTION + NTOTAL_RETRIBUTION + NTOTAL_TTLB + NTOTAL_TRANSFORMATOR_FEE + NBEBAN_BERSAMA,
        //         2);

        public decimal NTOTAL_VAT_NON_ADM_TAX => 0;
        public decimal NTOTAL_VAT_ADM_TAX => 0;
        public decimal NTOTAL_ADMIN_FEE => Math.Round(NADMIN_FEE_PCT / 100 * NTOTAL + 0, 2);

        // public decimal NTOTAL_ADM_VAT =>
        //     Math.Round(NTOTAL + NTOTAL_VAT_NON_ADM_TAX + NTOTAL_ADMIN_FEE + NTOTAL_VAT_ADM_TAX, 2);


        public decimal NBLOCK1_CHARGE { get; set; }
        public decimal NBLOCK2_CHARGE { get; set; }
        public decimal NUSAGE_MIN_CHARGE { get; set; }
        public decimal NRETRIBUTION_PCT_2 { get; set; }
        public string CADMIN_FEE_2 { get; set; }
        public decimal NADMIN_FEE { get; set; }
        public decimal NBLOCK1_USAGE_CF { get; set; }
        public decimal NBLOCK2_USAGE_CF { get; set; }
        public decimal NTOTAL_USAGE_KVA { get; set; }
        
        
        public decimal NBLOCK1_AMT { get; set; }
        public decimal NBLOCK2_AMT { get; set; }
        public decimal NSTANDING_AMT { get; set; }
        public decimal NSTANDING_CONSUMP_AMT { get; set; }
        
        public string CRATE_TYPE { get; set; }
        public decimal NMIN_CHARGE_AMT { get; set; }
        public decimal NADDITIONAL_AMT { get; set; }
        public decimal NRETRIBUTION_AMT { get; set; }
        public decimal NADMIN_FEE_AMT { get; set; }
        public decimal NSUB_TOTAL_AMT { get; set; }
        public decimal NTOTAL_AMT { get; set; }

        public decimal NKWH_USED_MAX { get; set; }
        public decimal NKWH_USED_RATE { get; set; }
        public string CTAX_ID { get; set; }
        public decimal NTAX_PCT { get; set; }
        public decimal NADDITIONAL_USAGE { get; set; }
        public decimal NVAT_AMT { get; set; }
        public decimal NADMIN_FEE_TAX_AMT { get; set; }

        public decimal NTRANSFORMATOR_AMT { get; set; }

        public decimal NTOTAL_ADM_VAT => Math.Round(NTOTAL_AMT + NVAT_AMT + NADMIN_FEE_AMT + NADMIN_FEE_TAX_AMT, 2);
        public decimal NTOTAL_STANDING_CONSUMPTION =>
            Math.Round(NSTANDING_AMT + NBLOCK1_AMT + NBLOCK2_AMT + NADDITIONAL_AMT, 2);

        public decimal NTOTAL =>
            Math.Round(NTOTAL_STANDING_CONSUMPTION + NRETRIBUTION_AMT + NTRANSFORMATOR_AMT + NBEBAN_BERSAMA, 2);


        #region STORAGE_ID

        public string CSTART_PHOTO1_STORAGE_ID { get; set; }
        public string CSTART_PHOTO2_STORAGE_ID { get; set; }
        public string CSTART_PHOTO3_STORAGE_ID { get; set; }

        public string CEND_PHOTO1_STORAGE_ID { get; set; }
        public string CEND_PHOTO2_STORAGE_ID { get; set; }
        public string CEND_PHOTO3_STORAGE_ID { get; set; }

        public byte[] ODATA_START_PHOTO1 { get; set; }
        public byte[] ODATA_START_PHOTO2 { get; set; }
        public byte[] ODATA_START_PHOTO3 { get; set; }
        public byte[] ODATA_END_PHOTO1 { get; set; }
        public byte[] ODATA_END_PHOTO2 { get; set; }
        public byte[] ODATA_END_PHOTO3 { get; set; }

        public string CFILE_NAME_START_PHOTO1 { get; set; }
        public string CFILE_NAME_START_PHOTO2 { get; set; }
        public string CFILE_NAME_START_PHOTO3 { get; set; }
        public string CFILE_NAME_END_PHOTO1 { get; set; }
        public string CFILE_NAME_END_PHOTO2 { get; set; }
        public string CFILE_NAME_END_PHOTO3 { get; set; }
        public string CFILE_EXTENSION_START_PHOTO1 { get; set; }
        public string CFILE_EXTENSION_START_PHOTO2 { get; set; }
        public string CFILE_EXTENSION_START_PHOTO3 { get; set; }
        public string CFILE_EXTENSION_END_PHOTO1 { get; set; }
        public string CFILE_EXTENSION_END_PHOTO2 { get; set; }
        public string CFILE_EXTENSION_END_PHOTO3 { get; set; }

        #endregion
    }
}