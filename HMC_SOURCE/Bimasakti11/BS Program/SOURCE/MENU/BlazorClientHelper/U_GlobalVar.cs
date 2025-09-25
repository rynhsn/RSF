using R_BlazorFrontEnd.Helpers;
using R_ContextFrontEnd;

namespace BlazorClientHelper
{
    public class U_GlobalVar : R_GlobalVar, IClientHelper
    {
        public U_GlobalVar(R_ContextHeader contextHeader) : base(contextHeader)
        {
        }
    }
}
