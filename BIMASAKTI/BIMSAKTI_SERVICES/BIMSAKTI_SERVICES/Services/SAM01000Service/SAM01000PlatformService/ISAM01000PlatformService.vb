Imports System.ServiceModel
Imports R_BackEnd
Imports SAM01000Back

' NOTE: You can use the "Rename" command on the context menu to change the interface name "ISAM01000PlatformService" in both code and config file together.
<ServiceContract()>
Public Interface ISAM01000PlatformService

    Inherits R_IServicebase(Of PlatformDTO)
End Interface
