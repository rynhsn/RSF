using System;
using System.Collections.Generic;
using R_APICommonDTO;

namespace PMT04200Common.DTOs;

public class PropertyListDTO
{
    public string CCOMPANY_ID { get; set; }
    public string CPROPERTY_ID { get; set; }
    public string CPROPERTY_NAME { get; set; }
    public bool LACTIVE { get; set; }
    public string CCREATE_BY { get; set; }
    public DateTime DCREATE_DATE { get; set; }
    public string CUPDATE_BY { get; set; }
    public DateTime DUPDATE_DATE { get; set; }
}
public class PropertyListDataDTO :R_APIResultBaseDTO
{
    public List<PropertyListDTO> Data { get; set; }
}

public class DropDownDTO
{
    public string Id { get; set; }
    public string Text { get; set; }
}

public class CheckBoxDTO
{
    public string Id { get; set; }
    public bool Value{ get; set; }
}

public class DropDownDataDTO : R_APIResultBaseDTO
{
    public List<DropDownDTO> Data { get; set; }
}