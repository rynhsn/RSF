Imports System.ServiceModel
Imports R_Common
Imports TelerikMenuBackCls
Imports R_BackEnd
Imports System.ServiceModel.Channels

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IMenuStreamingService" in both code and config file together.
<ServiceContract()>
Public Interface IMenuStreamingService

    <OperationContract(Action:="getMenu", ReplyAction:="getMenu")> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getMenu() As Message

    <OperationContract(Action:="getMenuAccess", ReplyAction:="getMenuAccess")> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getMenuAccess() As Message

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Sub Dummy(ByVal poPar As List(Of MenuDTOnon))
End Interface
