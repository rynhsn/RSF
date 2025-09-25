using System;
using System.Collections.Generic;
using System.Text;

namespace GLR00300Common.GLR00300Print
{
    public class GLR00300LabelDTO
    {
        public string Label_Period { get; set; } = "Period";
        public string Label_AccountNo { get; set; } = "Account No.";
        public string Label_Center { get; set; } = "Center";
        public string Label_To { get; set; } = "to";
        public string Label_TrialBalanceType { get; set; } = "Trial Balance Type";
        public string Label_Currency { get; set; } = "Currency";
        public string Label_JournalAdjMode { get; set; } = "Journal Adj. Mode";
        public string Label_PrintMethod { get; set; } = "Print Method";
        public string Label_BudgetNo { get; set; } = "Budget No.";
        public string Label_GrandTotal { get; set; } = "Grand Total";
        public string Label_Difference { get; set; } = "Difference";
        public string Label_Note { get; set; } = "Note: *Inactive Account";
    }
}
