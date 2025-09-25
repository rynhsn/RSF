using R_APICommonDTO;

namespace GLM00400COMMON
{
    public class GLM00400Result<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }

}
