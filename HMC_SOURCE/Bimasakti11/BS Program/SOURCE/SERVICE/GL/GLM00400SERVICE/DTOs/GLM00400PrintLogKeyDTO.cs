using R_CommonFrontBackAPI.Log;

namespace GLM00400SERVICE
{
    public class GLM00400PrintLogKeyDTO<T>
    {
        public T poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
    }
}
