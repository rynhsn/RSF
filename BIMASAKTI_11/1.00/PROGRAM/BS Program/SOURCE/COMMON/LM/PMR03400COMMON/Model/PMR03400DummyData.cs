using BaseHeaderReportCOMMON;
using PMR03400COMMON.DTO_s;
using PMR03400COMMON.Print_DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PMR03400COMMON.Model
{
    public class PMR03400DummyData
    {
        public static PMR03400PrintDisplayDTO PMR03400PrintDislpayWithBaseHeader()
        {
            PMR03400PrintDisplayDTO loRtn = new PMR03400PrintDisplayDTO();
            loRtn.BaseHeaderData = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "PMR03400",
                CPRINT_NAME = "Customer Ledger",
                CUSER_ID = "RCM",
            };
            loRtn.ReportData = new PMR03400ReportDataDTO();
            loRtn.ReportData.Header = "PM";
            loRtn.ReportData.Title = "Customer Ledger";
            loRtn.ReportData.Column = new PMR03400LabelDTO();
            loRtn.ReportData.HeaderParam = new PMR03400ParamDTO()
            {
                CCOMPANY_ID = "C001",
                CPROPERTY_ID = "P-JKT-A",

                // Periode laporan: dari Januari 2025 hingga Desember 2025
                CFR_PERIOD = "202501",
                CTO_PERIOD = "202512",

                CCURRENCY_TYPE = "IDR",

                // Kode (filter) dari tenant A sampai tenant Z
                CFR_CODE = "T001",
                CFR_CODE_NAME = "Tenant Pertama",
                CTO_CODE = "T999",
                CTO_CODE_NAME = "Tenant Terakhir",

                // Opsi Laporan (boolean)
                LDESC = true,      // Tampilkan deskripsi (Detailed)
                LPROFORMA = false, // Jangan sertakan Proforma (Non-Proforma)

                CLANGUAGE_ID = "EN"
            };
            loRtn.ReportData.HeaderParam.CPERIOD_DISPLAY = $"{loRtn.ReportData.HeaderParam.CFR_PERIOD.Insert(4, "-")} - {loRtn.ReportData.HeaderParam.CTO_PERIOD.Insert(4, "-")}";

            loRtn.ReportData.Data = GenerateDummyData(5, 3);

            return loRtn;
        }

        public static List<PMR03400SPResultDTO> GenerateDummyData(int totalRecords = 10, int totalTenants = 5)
        {
            var dummyList = new List<PMR03400SPResultDTO>();
            // 1. Inisialisasi saldo awal untuk 5 tenant unik
            var tenantBalances = new Dictionary<string, decimal>();
            var tenantIds = new List<string>();

            for (int j = 1; j <= totalTenants; j++)
            {
                string id = $"TID-{j:D3}";
                tenantIds.Add(id);
                // Setiap tenant dimulai dengan saldo awal yang berbeda
                tenantBalances.Add(id, 10000000.00m + (j * 1000000.00m));
            }

            DateTime baseDate = new DateTime(2025, 10, 1);

            for (int i = 0; i < totalRecords; i++)
            {
                // 2. Rotasi melalui 5 tenant ID (0, 1, 2, 3, 4, 0, 1, 2, 3, 4)
                string currentTenantId = tenantIds[i % totalTenants];
                decimal currentBalance = tenantBalances[currentTenantId];

                // Tentukan apakah transaksi adalah Tagihan (Debit) atau Pembayaran (Kredit)
                // Transaksi pertama per tenant: Tagihan (Debit). Transaksi kedua: Pembayaran (Kredit).
                bool isFirstTransaction = (i / totalTenants) == 0;

                decimal debitAmount = 0.00m;
                decimal creditAmount = 0.00m;
                string transCode;
                string transName;
                string transDesc;

                if (isFirstTransaction)
                {
                    // Transaksi #1 (index 0-4): Tagihan Sewa (Debit)
                    debitAmount = 5000000.00m;
                    transCode = "INV-SEW";
                    transName = "Invoice Sewa";
                    transDesc = $"Tagihan Sewa Bulan Okt {2025}";
                }
                else
                {
                    // Transaksi #2 (index 5-9): Penerimaan Pembayaran (Kredit)
                    creditAmount = 4800000.00m + (i * 10000.00m); // Sedikit variasi jumlah bayar
                    transCode = "RCP-PYM";
                    transName = "Penerimaan Pembayaran";
                    transDesc = $"Pembayaran Tagihan Ref-{i + 1:D3}";
                }

                // Hitung saldo akhir dan perbarui Dictionary untuk saldo berikutnya
                decimal endBalance = currentBalance - debitAmount + creditAmount;
                tenantBalances[currentTenantId] = endBalance;

                // Buat objek DTO
                var dto = new PMR03400SPResultDTO
                {
                    CCOMPANY_ID = "BSI",
                    CPROPERTY_ID = "ASHMD" + (i % 2 + 1),
                    CTENANT_ID = currentTenantId,
                    CTENANT_NAME = $"Tenant Nama {currentTenantId.Substring(4)}",

                    CREF_NO = $"REF{i + 1:D4}",
                    CREF_DATE = baseDate.AddDays(i * 3).ToString("yyyyMMdd"),
                    DREF_DATE = baseDate.AddDays(i * 3),

                    CTRANS_CODE = transCode,
                    CTRANS_NAME = transName,
                    CTRANS_DESC = transDesc,

                    CCURRENCY = "IDR",

                    NBEG_BALANCE = currentBalance,
                    NDEBIT = debitAmount,
                    NCREDIT = creditAmount,
                    NEND_BALANCE = endBalance,

                    CFROM_PERIOD = "202510",
                    CTO_PERIOD = "202511",
                    CFILTER_VALUE = "AK001-Angel to TntRPT01-Tenant Khusus Report"
                };

                dummyList.Add(dto);
            }

            // 3. Urutkan berdasarkan Tenant ID dan Tanggal Referensi untuk visualisasi grouping
            return dummyList.OrderBy(x => x.CTENANT_ID).ThenBy(x => x.DREF_DATE).ToList();
        }

    }
}
