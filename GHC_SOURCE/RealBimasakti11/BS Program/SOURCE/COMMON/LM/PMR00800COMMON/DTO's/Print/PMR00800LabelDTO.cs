using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00800COMMON.DTO_s.Print
{
    public class PMR00800LabelDTO
    {
        public string CCOLUMN_NO { get; set; } = "No";
        public string CCOLUMN_UNIT_ID { get; set; } = "Unit Id";
        public string CCOLUMN_UNIT_TYPE { get; set; } = "Unit Type";
        public string CCOLUMN_AGREEMENT_NO { get; set; } = "Agreement No";
        public string CCOLUMN_TENANT_ID { get; set; } = "Tenant Id";
        public string CCOLUMN_TENANT_NAME { get; set; } = "Tenant Name";
        public string CCOLUMN_OCCUPIED_AREA { get; set; } = "Occupied Area";
        public string CCOLUMN_RENT_HEADER { get; set; } = "RENT";
        public string CCOLUMN_RENT_RENTED { get; set; } = "Rented";
        public string CCOLUMN_RENT_AVAILABLE { get; set; } = "Available";
        public string CCOLUMN_RENT_PERCENT { get; set; } = "Rent%";
        public string CCOLUMN_SC_HEADER { get; set; } = "SERVICE CHARGE";
        public string CCOLUMN_SC_RENTED { get; set; } = "SC-Rented";
        public string CCOLUMN_SC_AVAILABLE { get; set; } = "SC-Available";
        public string CCOLUMN_SC_PERCENT { get; set; } = "SC%";
        public string CCOLUMN_PL_HEADER { get; set; } = "PROMO LEVY";
        public string CCOLUMN_PL_RENTED { get; set; } = "PL-Rented";
        public string CCOLUMN_PL_AVAILABLE { get; set; } = "PL-Available";
        public string CCOLUMN_PL_PERCENT { get; set; } = "PL%";
        public string CCOLUMN_SPACE_HEADER { get; set; } = "SPACE";
        public string CCOLUMN_AREA_RENTED { get; set; } = "Area Rented";
        public string CCOLUMN_AREA_ALL { get; set; } = "Area All";
        public string CCOLUMN_OCC_RATE { get; set; } = "OCC Rate%";
        public string CLABEL_PROPERTY { get; set; } = "Property";
        public string CLABEL_BUILDING { get; set; } = "Building";
        public string CLABEL_PERIOD { get; set; } = "Period";
        public string CLABEL_SUBTOTAL { get; set; } = "Sub Total";
        public string CLABEL_GRANDTOTAL { get; set; } = "Grand Total";
    }
}
