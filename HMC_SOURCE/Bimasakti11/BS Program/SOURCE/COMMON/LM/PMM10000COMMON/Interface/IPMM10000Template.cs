using PMM10000COMMON.Upload;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM10000COMMON.Interface
{
    public interface IPMM10000Template
    {
        PMM10000TemplateDTO DownloadTemplateFile();
    }
}
