Imports System.ServiceModel
Imports R_Common
Imports GSM00100Back
Imports R_BackEnd
Imports System.ServiceModel.Channels

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IGST00300StreamingService" in both code and config file together.
<ServiceContract()>
Public Interface IGSM00100StreamingService

    <OperationContract(Action:="getSMTPList", ReplyAction:="getSMTPList")> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getSMTPList() As Message
End Interface
