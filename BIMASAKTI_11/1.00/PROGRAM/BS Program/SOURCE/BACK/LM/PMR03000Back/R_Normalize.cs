using System.Text.RegularExpressions;

namespace FastReportDesigner.Library
{
    public static class R_Normalize
    {
        public static string NormalizeRTF(string rtf)
        {
            if (string.IsNullOrWhiteSpace(rtf))
                return rtf;

            rtf = Regex.Replace(rtf, @"ansicpg\d+", "ansicpg65001");

            var defaultLang = 1033;
            rtf = Regex.Replace(rtf, @"\\deflang\d+", $"\\deflang{defaultLang}");
            rtf = Regex.Replace(rtf, @"\\deflangfe\d+", $"\\deflangfe{defaultLang}");
            rtf = Regex.Replace(rtf, @"\\themelang\d+", $"\\themelang{defaultLang}");
            rtf = Regex.Replace(rtf, @"\\themelangfe\d+", $"\\themelangfe{defaultLang}");
            rtf = Regex.Replace(rtf, @"\\lang\d+", $"\\lang{defaultLang}");
            rtf = Regex.Replace(rtf, @"\\langfe\d+", $"\\langfe{defaultLang}");

            //// Remove LibreOffice generator info
            //rtf = Regex.Replace(rtf, @"\{\\\*\\generator.*?\}", string.Empty, RegexOptions.Singleline);

            //// Remove \info block
            //rtf = Regex.Replace(rtf, @"\{\\info.*?\}", string.Empty, RegexOptions.Singleline);

            //// Remove \userprops block
            //rtf = Regex.Replace(rtf, @"\{\\\*\\userprops.*?\}", string.Empty, RegexOptions.Singleline);




            return rtf;
        }
    }
}
