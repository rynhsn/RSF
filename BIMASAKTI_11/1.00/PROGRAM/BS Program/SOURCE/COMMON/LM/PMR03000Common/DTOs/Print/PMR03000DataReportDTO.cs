using System;
using System.Collections.Generic;

namespace PMR03000Common.DTOs.Print
{
    public class PMR03000ResultDataDTO
    {
        public PMR03000ReportLabelDTO Label { get; set; }
        public PMR03000BaseHeaderDTO? Header { get; set; }

        public List<PMR03000DataReportDTO> Datas { get; set; }
        // public List<PMR03000VADTO> VirtualAccountData { get; set; }
        // public List<PMR03000DetailUnitDTO> DataUnitList { get; set; }
        // public List<PMR03000DetailUtilityDTO> DataUtility1 { get; set; }
        // public List<PMR03000DetailUtilityDTO> DataUtility2 { get; set; }
        // public List<PMR03000DetailUtilityDTO> DataUtility3 { get; set; }
        // public List<PMR03000DetailUtilityDTO> DataUtility4 { get; set; }
    }

    public class PMR03000MessageInfoDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CMESSAGE_TYPE { get; set; }
        public string CMESSAGE_TYPE_DESCR { get; set; }
        public string CMESSAGE_NO { get; set; }
        public string TMESSAGE_DESCRIPTION { get; set; }
        public string CADDITIONAL_DESCRIPTION { get; set; }
        public bool LACTIVE { get; set; }
        public string CACTIVE_BY { get; set; }
        public DateTime DACTIVE_DATE { get; set; }
        public string CINACTIVE_BY { get; set; }
        public DateTime DINACTIVE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }

    public class PMR03000VADTO
    {
        public string CBANK_CODE { get; set; }
        public string CBANK_NAME { get; set; }
        public string CVA_CODE { get; set; }
    }

    public class PMR03000BaseHeaderDTO
    {
        public byte[]? CLOGO { get; set; }
        public string PROPERTY_ID { get; set; } = "PROPERTY_ID";
        public string PROPERTY_NAME { get; set; } = "PROPERTY_NAME";
        public string CSTORAGE_ID { get; set; } = "";
    }

    public class PMR03000DataReportDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CSTATEMENT_DATE { get; set; }
        public DateTime DSTATEMENT_DATE { get; set; }
        public string CDUE_DATE { get; set; }
        public DateTime DDUE_DATE { get; set; }
        public string CLOI_AGRMT_REC_ID { get; set; } = "";
        public string CSTORAGE_ID { get; set; }

        public string CUSER_ID { get; set; }

        //SUBHEADER
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CBILLING_ADDRESS { get; set; }
        public string CREF_NO { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CUNIT_ID_LIST { get; set; }
        public string CUNIT_DESCRIPTION { get; set; }
        public string CCURRENCY { get; set; }
        public string CCURRENCY_CODE { get; set; }

        public decimal NPREVIOUS_BALANCE { get; set; }
        public decimal NPREVIOUS_PAYMENT { get; set; }
        public decimal NCURRENT_PENALTY { get; set; }
        public decimal NNEW_BILLING { get; set; }

        public decimal NNEW_BALANCE { get; set; }

        //SUPRESS if value 0
        public decimal NSALES { get; set; }
        public decimal NRENT { get; set; }
        public decimal NDEPOSIT { get; set; }
        public decimal NREVENUE_SHARING { get; set; }
        public decimal NSERVICE_CHARGE { get; set; }
        public decimal NSINKING_FUND { get; set; }
        public decimal NPROMO_LEVY { get; set; }
        public decimal NGENERAL_CHARGE { get; set; }
        public decimal NELECTRICITY { get; set; }
        public decimal NCHILLER { get; set; }
        public decimal NWATER { get; set; }
        public decimal NGAS { get; set; }
        public decimal NPARKING { get; set; }
        public decimal NOVERTIME { get; set; }
        public decimal NGENERAL_UTILITY { get; set; }
        public string CMESSAGE_NO { get; set; }
        public string CMESSAGE_NAME { get; set; }
        public string TMESSAGE_DESCR_RTF { get; set; }
        public string TADDITIONAL_DESCR_RTF { get; set; }

        public PMR03000MessageInfoDTO MessageInfo { get; set; }

        public List<PMR03000VADTO> VirtualAccountData { get; set; }
        public List<PMR03000DetailUnitDTO> DataUnitList { get; set; }
        public bool DataUnitListIsEmpty { get; set; } = false;
        public List<PMR03000DetailUtilityDTO> DataUtility1 { get; set; }
        public bool DataUtility1IsEmpty { get; set; } = false;
        public List<PMR03000DetailUtilityDTO> DataUtility2 { get; set; }
        public bool DataUtility2IsEmpty { get; set; } = false;
        public List<PMR03000DetailUtilityDTO> DataUtility3 { get; set; }
        public bool DataUtility3IsEmpty { get; set; } = false;
        public List<PMR03000DetailUtilityDTO> DataUtility4 { get; set; }
        public bool DataUtility4IsEmpty { get; set; } = false;
    }

    public class PMR03000DetailUnitDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CREF_NO { get; set; }
        public string CCUSTOMER_ID { get; set; }
        public string CCUSTOMER_NAME { get; set; }
        public string CTRANS_DESC { get; set; }
        public decimal NOCCUPIABLE_AREA { get; set; }
        public string CINV_PERIOD_DESC { get; set; }
        public decimal NFEE_AMT { get; set; }
        public decimal NCHARGE_AMOUNT { get; set; }
        public decimal NTOTAL_AMOUNT { get; set; }

        // --- 25/08/2025
        public string CINVOICE_NO { get; set; }
        public string? CCURRENCY_CODE { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime DREF_DATE { get; set; }
        public bool LTAXABLE { get; set; } //filter tax & Sub Total
        public decimal NTAX_AMT { get; set; } //TAX Amount
        public decimal NSUB_TOTAL_AMT { get; set; } //Sub Total
        public string CUNIT_DESCRIPTION { get; set; }
    }

    public class PMR03000DetailUtilityDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCUSTOMER_ID { get; set; }
        public string CREF_NO { get; set; }
        public string CCHARGES_TYPE { get; set; }

        public string CMETER_NO { get; set; }

        //public decimal NBLOCK1_CHARGE { get; set; } //Tarif listrik dan air
        //public decimal NBLOCK2_CHARGE { get; set; }
        //public decimal NBLOCK1_START { get; set; } //Meter awal listrik
        //public decimal NBLOCK2_START { get; set; }
        //public decimal NBLOCK1_END { get; set; } // meter akhir listrik
        //public decimal NBLOCK2_END { get; set; }
        //public decimal NBLOCK1_USAGE { get; set; } // penggunaan listrik 
        //public decimal NBLOCK2_USAGE { get; set; }
        public decimal NMETER_START { get; set; } //Meter awal listrik dan water
        public decimal NMETER_END { get; set; } // meter akhir listrik dan water
        public decimal NMETER_USAGE { get; set; } // penggunaan listrik dan air
        public decimal NMETER_CHARGE { get; set; } //Tarif listrik dan air
        public decimal NBEBAN_BERSAMA { get; set; }
        public decimal NCAPACITY { get; set; } //daya terpasang  listrik
        public decimal NCF { get; set; } // calculation factor listrik
        public decimal NUSAGE_MIN_CHARGE { get; set; } // rekening minimum  listrik
        public decimal NMIN_CHARGE_AMT { get; set; }
        public decimal NADDITIONAL_AMT { get; set; } // biaya tambahan listrik
        public decimal NTOTAL_USAGE_KVA { get; set; }
        public decimal NSTANDING_CONSUMP_AMT { get; set; } //Pemakaian listrik // biaya tetap untuk water
        public decimal NSUB_TOTAL_AMT { get; set; } //sub total listrik
        public decimal NADMIN_FEE_AMT { get; set; }
        public decimal NVAT_AMT { get; set; } // PPJU listrik
        public decimal NADMIN_FEE_TAX_AMT { get; set; }
        public decimal NTOTAL_AMT { get; set; } //sub total biaya air / listrik
        public decimal NMAINTENANCE_FEE { get; set; } //Biaya operasional
        public decimal CFROM_SEQ_NO { get; set; }

        public string CTRANS_DESC { get; set; }

        // --- 25/08/2025
        public string CINVOICE_NO { get; set; } = "";
        public string? CCURRENCY_CODE { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime DREF_DATE { get; set; }
        public decimal NUSAGE_CF { get; set; } //Penggunaan air dan Gas
        public decimal NBLOCK1_USAGE_CF { get; set; } //Penggunaan 
        public decimal NBLOCK2_START { get; set; } //Penggunaan Awal 
        public decimal NBLOCK2_END { get; set; } //Penggunaan Akhir 
        public decimal NBLOCK2_USAGE { get; set; } //Penggunaan 
        public decimal NBLOCK2_CHARGE { get; set; } //Tarif 
        public decimal NBLOCK2_USAGE_CF { get; set; } //Penggunaan 
        public bool LTAXABLE { get; set; } //filter tax & Sub Total
        public decimal NTAX_AMT { get; set; } //TAX Amount
        public decimal NUSAGE_CHARGE { get; set; } //Tarif air dan gas
        public decimal NSTANDING_AMT { get; set; } //Standing Amount
        public decimal NRETRIBUTION_AMT { get; set; } //PPJU
        public decimal NTRANSFORMATOR_AMT { get; set; } //Biaya Transformator
        public decimal NBLOCK1_BLOCK2_AMT { get; set; } //Pemakaian
        public decimal NUSAGE_AMT { get; set; } //Pemakaian Water dan Gas
        public string CCHARGES_ID { get; set; } //Untuk Parameter Rate
        public string CSTART_DATE { get; set; } //Untuk Parameter Rate

        public string CUNIT_DESCRIPTION { get; set; }
        public string CUSAGE_RATE_MODE { get; set; }

        public List<PMR03000RateWGListDTO> RateWGList { get; set; } = new List<PMR03000RateWGListDTO>();
    }

    public class PMR03000RateWGListDTO
    {
        public int IUP_TO_USAGE { get; set; }
        public decimal NUSAGE_CHARGE { get; set; }

        public int IMIN_USAGE { get; set; }
        public decimal NFROM_TO { get; set; }
        public decimal NSUB_TOTAL_ROW { get; set; }
    }

    public class PMR03000ReportLabelDTO
    {
        public string Label_RingkasanPenagihan { get; set; } = "Ringkasan Penagihan";
        public string Label_RingkasanPenagihan_En { get; set; } = "(Billing Statement Summary)";
        public string Label_TanggalInvoice { get; set; } = "Tanggal Penagihan";
        public string Label_TanggalInvoice_En { get; set; } = "(Invoice Date)";
        public string Label_TanggalJatuhTempo { get; set; } = "Tanggal Jatuh Tempo";
        public string Label_TanggalJatuhTempo_En { get; set; } = "(Due Date)";
        public string Label_RingkasanInvoice { get; set; } = "Ringkasan Invoice";
        public string Label_RingkasanInvoice_En { get; set; } = "(Summary Invoice)";
        public string Label_TagihanLalu { get; set; } = "Tagihan Lalu";
        public string Label_TagihanLalu_En { get; set; } = "(Previous Balance)";
        public string Label_PembayaranLalu { get; set; } = "Pembayaran Lalu";
        public string Label_PembayaranLalu_En { get; set; } = "(Previous Payment)";
        public string Label_PenaltySaatIni { get; set; } = "Penalty Saat Ini";
        public string Label_PenaltySaatIni_En { get; set; } = "(Current Penalty)";
        public string Label_TotalTagihanSaatIni { get; set; } = "Total Tagihan Saat Ini";
        public string Label_TotalTagihanSaatIni_En { get; set; } = "(Total Due This Statement)";
        public string Label_JumlahYangHarusDibayar { get; set; } = "Jumlah yang harus dibayar";
        public string Label_JumlahYangHarusDibayar_En { get; set; } = "(Total must be paid)";
        public string Label_VirtualAccount { get; set; } = "Untuk pembayaran gunakan Virtual Account di bawah ini";
        public string Label_VirtualAccount_En { get; set; } = "(For payment please use Virtual Account below)";
        public string Label_Sales { get; set; } = "Sales";
        public string Label_Rent { get; set; } = "Rent";
        public string Label_Deposit { get; set; } = "Deposit";
        public string Label_RevenueSharing { get; set; } = "Revenue Sharing";
        public string Label_ServiceCharge { get; set; } = "Service Charge";
        public string Label_SingkingFund { get; set; } = "Singking Fund";
        public string Label_PromoLevy { get; set; } = "Promo Levy";
        public string Label_Other { get; set; } = "Other";
        public string Label_Electricity { get; set; } = "Electricity";
        public string Label_Chiller { get; set; } = "Chiller";
        public string Label_Water { get; set; } = "Water";
        public string Label_Gas { get; set; } = "Gas";
        public string Label_Parking { get; set; } = "Parking";
        public string Label_Overtime { get; set; } = "Overtime";
        public string Label_GeneralUtility { get; set; } = "General Utility";
        public string Label_Currency { get; set; } = "Rp";
        public string Label_IPLHeader { get; set; } = "Iuran Pemeliharaan Lingkungan, Singking Fund Nomor Transaksi";
        public string Label_IPL { get; set; } = "Iuran Pemeliharaan Lingkungan";
        public string Label_OccupiableArea { get; set; } = "Occupiable Area";
        public string Label_InvoicedPeriod { get; set; } = "Invoicing Period";
        public string Label_Fee { get; set; } = "Fee";
        public string Label_ChargeAmount { get; set; } = "Charge Amount";
        public string Label_Total { get; set; } = "Total";
        public string Label_TotalBiayaListrik { get; set; } = "Total Biaya Listrik";
        public string Label_TotalBiayaChiller { get; set; } = "Total Biaya Chiller";
        public string Label_AirListrik { get; set; } = "Air, Listrik Nomor Transaksi";
        public string Label_MeterNo { get; set; } = "Meter No";
        public string Label_CalculationFactor { get; set; } = "Calculation Factor";
        public string Label_CatatanMeter { get; set; } = "Catatan Meter";
        public string Label_MeterAwal { get; set; } = "Meter Awal";
        public string Label_MeterAkhir { get; set; } = "Meter Akhir";
        public string Label_PenggunaanListrik { get; set; } = "Penggunaan Listrik";
        public string Label_PenggunaanChiller { get; set; } = "Penggunaan Chiller";
        public string Label_RekeningMinimum { get; set; } = "Rekening Minimum";
        public string Label_TotalPemakaian { get; set; } = "Total Pemakaian";
        public string Label_SubTotal { get; set; } = "Sub Total";
        public string Label_PPJU { get; set; } = "PPJU 2.40%";
        public string Label_BiayaTambahan { get; set; } = "Biaya Tambahan";
        public string Label_DayaTerpasang { get; set; } = "Daya Terpasang (Kva)";
        public string Label_KwhYangDiperhitungkan { get; set; } = "Kwh Yang Diperhitungkan";
        public string Label_BatasKWHMinimum { get; set; } = "Batas kwh minimum";
        public string Label_SubTotalBiayaListrik { get; set; } = "Sub Total Biaya Listrik";
        public string Label_SubTotalBiayaChiller { get; set; } = "Sub Total Biaya Chiller";
        public string Label_PenggunaanAir { get; set; } = "Penggunaan Air";
        public string Label_PenggunaanGas { get; set; } = "Penggunaan Gas";
        public string Label_BiayaTetap { get; set; } = "Biaya Tetap";
        public string Label_BiayaOperasional { get; set; } = "Biaya Operasional";
        public string Label_Tarif { get; set; } = "Tarif";
        public string Label_TarifPemakaian { get; set; } = "Tarif Pemakaian";
        public string Label_TarifBlock1 { get; set; } = "Tarif Block 1";
        public string Label_TarifBlock2 { get; set; } = "Tarif Block 2";
        public string Label_SubTotalBiayaAir { get; set; } = "Sub Total Biaya Air";
        public string Label_SubTotalBiayaGas { get; set; } = "Sub Total Biaya Gas";
        public string Label_TotalBiayaAir { get; set; } = "Total Biaya Air";
        public string Label_TotalBiayaGas { get; set; } = "Total Biaya Gas";

        public string Label_InvoiceNo { get; set; } = "Invoice No.";
        public string Label_Tax { get; set; } = "Tax";
        public string Label_BebanBersama { get; set; } = "Beban Bersama";
        public string Label_BiayaTransformator { get; set; } = "Biaya Transformator";
        public string Label_BiayaAdmin { get; set; } = "Biaya Admin";
        public string Label_MaintenanceFee { get; set; } = "Maintenance Fee";
        public string Label_StandingAmount { get; set; } = "Standing Amount";
        public string Label_Block1 { get; set; } = "Block 1";
        public string Label_Block2 { get; set; } = "Block 2";
    }

    public class PMR03000ReportClientParameterDTO
    {
        public string CTENANT_CUSTOMER_ID { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CREPORT_CULTURE { get; set; }
        public int IDECIMAL_PLACES { get; set; }
        public string CNUMBER_FORMAT { get; set; }
        public string CDATE_LONG_FORMAT { get; set; }
        public string CDATE_SHORT_FORMAT { get; set; }
        public string CTIME_LONG_FORMAT { get; set; }
        public string CTIME_SHORT_FORMAT { get; set; }
    }

    public class PMR03000ParamSaveStorageDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CLANG_ID { get; set; } = "";
        public string CTENANT_ID { get; set; } = "";
        public string CFILE_NAME { get; set; } = "";
        public string CLOI_AGRMT_REC_ID { get; set; } = "";
        public string CREF_PRD { get; set; } = "";
        public string CSTORAGE_ID { get; set; } = "";
        public string FileExtension { get; set; }
        public byte[] REPORT { get; set; }
    }

    public class PMR03000StorageType
    {
        public string? CSTORAGE_TYPE { get; set; }
        public string? CSTORAGE_PROVIDER_ID { get; set; }
    }

    public class PMR03000ParamSaveBillingStatement
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CTENANT_ID { get; set; }
        public string CLOI_AGRMT_REC_ID { get; set; }
        public string CREF_PRD { get; set; }
        public string CREF_DATE { get; set; }
        public string CDUE_DATE { get; set; }
        public string CSTORAGE_ID { get; set; }
        public string CUSER_ID { get; set; }
    }

    public class PMR03000BillingStatementDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CADDRESS { get; set; }
        public string CBILLING_EMAIL { get; set; }
        public string CREF_NO { get; set; }
        public string CPERIOD { get; set; }
        public string CPERIOD_YEAR_DISPLAY { get; set; }
        public string CPERIOD_MONTH_DISPLAY { get; set; }
        public string CPERIOD_DISPLAY { get; set; }
        public string CDUE_DATE { get; set; }
        public DateTime DDUE_DATE { get; set; }
        public string CDUE_DATE_DISPLAY { get; set; }
        public string CBILL_DATE { get; set; }
        public string CSTORAGE_ID { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CUNIT_ID { get; set; }
        public string CUNIT_NAME { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public decimal NTOTAL_AMT { get; set; }
        public string CTOTAL_AMT_DISPLAY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }

    public class PMR03000DistributeReportDataDTO
    {
        //INI BUAT EMAIL
        public string? CSTORAGE_ID { get; set; }
        public string? CFILE_NAME { get; set; }
        public string? CFILE_ID { get; set; }
        public byte[]? OFILE_DATA_REPORT { get; set; }
        public bool LDATA_READY { get; set; } // Misalnya ini untuk mengecek apakah file siap dikirim
    }

    public class PMR03000GetEmailTemplateDTO
    {
        public string CTEMPLATE_DESC { get; set; }
        public string CTEMPLATE_BODY { get; set; }
        public string CSMTP_SERVER { get; set; }
        public string CSMTP_PORT { get; set; }
        public string CGENERAL_EMAIL_ADDRESS { get; set; }
    }
}