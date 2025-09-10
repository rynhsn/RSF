using APR00300Common.DTOs;

namespace APR00300Common
{
    // Interface ini digunakan untuk menampung method-method yang digunakan di APR00300
    public interface IAPR00300
    {
        APR00300ListDTO<APR00300PropertyDTO> APR00300GetPropertyList();
        APR00300SingleDTO<APR00300PeriodYearRangeDTO> APR00300GetYearRange();
        APR00300SingleDTO<APR00300TodayDTO> APR00300GetToday();
    }
}