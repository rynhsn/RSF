Imports System.ServiceModel
Imports R_Common
Imports SAM01100Back
Imports R_BackEnd
Imports System.ServiceModel.Channels

' NOTE: You can use the "Rename" command on the context menu to change the interface name "ISAM01100Service" in both code and config file together.
<ServiceContract()>
Public Interface ISAM01100Service

    Inherits R_IServicebase(Of SAM01100DTO)

    <OperationContract()> _
 <FaultContract(GetType(R_ServiceExceptions))> _
    Sub saveGeneralAccess(poNewEntity As Dictionary(Of String, String))

End Interface
