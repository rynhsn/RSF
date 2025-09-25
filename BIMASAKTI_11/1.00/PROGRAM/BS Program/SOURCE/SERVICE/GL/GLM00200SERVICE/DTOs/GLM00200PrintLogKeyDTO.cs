using R_BackEnd;
using R_CommonFrontBackAPI.Log;

namespace GLM00200SERVICE
{
    public class GLM00200PrintLogKeyDTO<T>
    {
        public T poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
        public R_ReportGlobalDTO poGlobalVar { get; set; }
    }
}
