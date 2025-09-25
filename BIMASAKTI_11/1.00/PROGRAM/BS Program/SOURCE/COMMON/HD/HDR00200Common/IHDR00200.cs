using HDR00200Common.DTOs;

namespace HDR00200Common
{
    public interface IHDR00200
    {
        HDR00200ListDTO<HDR00200PropertyDTO> HDR00200GetPropertyList();
        HDR00200SingleDTO<HDR00200DefaultParamDTO> HDR00200GetDefaultParam(HDR00200PropertyDTO poProperty);
        HDR00200ListDTO<HDR00200CodeDTO> HDR00200GetCodeList();
    }
}