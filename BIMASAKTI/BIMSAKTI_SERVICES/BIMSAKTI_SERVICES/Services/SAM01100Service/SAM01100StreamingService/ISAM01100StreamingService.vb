Imports System.ServiceModel
Imports R_Common
Imports SAM01100Back
Imports R_BackEnd
Imports System.ServiceModel.Channels

' NOTE: You can use the "Rename" command on the context menu to change the interface name "ISAM01100StreamingService" in both code and config file together.
<ServiceContract()>
Public Interface ISAM01100StreamingService

    <OperationContract(Action:="getMenuList", ReplyAction:="getMenuList")> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getMenuList() As Message

    <OperationContract(Action:="getMenuProgramList", ReplyAction:="getMenuProgramList")> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getMenuProgramList() As Message

    <OperationContract(Action:="getProgramList", ReplyAction:="getProgramList")> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getProgramList() As Message

    <OperationContract(Action:="getProgramButton", ReplyAction:="getProgramButton")> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getProgramButton() As Message
End Interface
