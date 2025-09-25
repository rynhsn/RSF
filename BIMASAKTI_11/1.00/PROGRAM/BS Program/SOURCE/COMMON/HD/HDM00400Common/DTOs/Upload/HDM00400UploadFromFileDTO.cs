using System;

namespace HDM00400Common.DTOs.Upload
{
    public class HDM00400UploadFromFileDTO
    {
        public string PublicLocationId { get; set; } ="";
        public string PublicLocationName { get; set; } ="";
        public string BuildingId { get; set; } ="";
        public string FloorId { get; set; } ="";
        public string Description { get; set; } ="";
        public bool Active { get; set; }
        public string NonActiveDate { get; set; } ="";
    }
    
    public class HDM00400UploadRequestDTO
    {
        public int NO { get; set; }
        public string PublicLocId { get; set; }
        public string PublicLocName { get; set; }
        public string BuildingId { get; set; } ="";
        public string FloorId { get; set; } ="";
        public string Description { get; set; }
        public bool Active { get; set; }
        public string NonActiveDate { get; set; }
    }
    
    public class HDM00400UploadForSystemDTO
    {
        public int NO { get; set; }
        
        public string PublicLocId { get; set; }
        public string PublicLocName { get; set; }
        public string BuildingId { get; set; } ="";
        public string FloorId { get; set; } ="";
        public string Description { get; set; }
        public bool Active { get; set; }
        public string NonActiveDate { get; set; }
        public DateTime? DNonActiveDate { get; set; }

        public string ErrorMessage { get; set; }
        public string ValidFlag { get; set; }
    }

    public class HDM00400UploadExcelDTO
    {
        public string No { get; set; }
        public string PublicLocationId { get; set; }
        public string PublicLocationName { get; set; }
        public string BuildingId { get; set; } ="";
        public string FloorId { get; set; } ="";
        public string Description { get; set; }
        public bool Active { get; set; }
        public string NonActiveDate { get; set; }
        public string Valid { get; set; }
        public string Notes { get; set; }
    }
}