using R_CommonFrontBackAPI.Log;

namespace GLTR00100SERVICE
{
    public class GLTR00100PrintLogKeyDTO<T>
    {
        public T poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
    }
}
