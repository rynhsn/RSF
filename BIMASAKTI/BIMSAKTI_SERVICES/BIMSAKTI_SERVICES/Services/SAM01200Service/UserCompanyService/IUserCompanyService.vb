Imports System.ServiceModel
Imports R_Common
Imports SAM01200Back
Imports R_BackEnd
Imports System.ServiceModel.Channels

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IUserCompanyService" in both code and config file together.
<ServiceContract()>
Public Interface IUserCompanyService

    Inherits R_IServicebase(Of UserCompanyDTO)

End Interface
