using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace PMT01300COMMON
{
    public class PMT01300SingleResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
    public class PMT01300ListResult<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

    public class PMT01300SaveDTO<T>
    {
        public T Data { get; set; }
        public eCRUDMode CRUDMode { get; set; }
    }

    public class PMT01300UploadFileDTO : R_APIResultBaseDTO
    {
        public byte[] FileBytes { get; set; }
    }
}
