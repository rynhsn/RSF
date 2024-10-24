using System;

namespace APR00300Common.DTOs
{
    //DTO ini digunakan untuk menampung data tanggal hari ini
    public class APR00300TodayDTO
    {
        public int IYEAR { get; set; }
        public string CMONTH { get; set; }
        public DateTime? DTODAY { get; set; }
    }
}