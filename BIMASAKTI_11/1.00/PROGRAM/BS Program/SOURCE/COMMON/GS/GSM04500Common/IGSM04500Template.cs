using System;
using System.Collections.Generic;
using System.Text;

namespace GSM04500Common
{
    public interface IGSM04500Template
    {
        GSM04500UploadFileDTO DownloadTemplateFile();
    }
}
