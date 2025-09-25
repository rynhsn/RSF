namespace PMT01100Common.DTO._1._Unit_List
{
    public class PMT01100UnitList_BuildingDTO
    {
        public string? CBUILDING_ID { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public string? CBUILDING_ID_AND_NAME { get; set; }

        public void SetBuildingIdAndName()
        {
            CBUILDING_ID_AND_NAME = $"{CBUILDING_NAME} ({CBUILDING_ID})";
        }
    }
}
