Imports System.ServiceModel
Imports R_Common
Imports SAM01000Back
Imports R_BackEnd
Imports System.ServiceModel.Channels

' NOTE: You can use the "Rename" command on the context menu to change the interface name "ISAM01000StreamingService" in both code and config file together.
<ServiceContract()>
Public Interface ISAM01000StreamingService

    <OperationContract(Action:="getCompList", ReplyAction:="getCompList")> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getCompList() As Message

    <OperationContract(Action:="getSMTPList", ReplyAction:="getSMTPList")> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getSMTPList() As Message

    <OperationContract()>
    <FaultContract(GetType(R_ServiceExceptions))>
    Sub Dummy(ByVal poPar As List(Of SAM01000GridDTO), ByVal poPar1 As List(Of SMTPDTO), ByVal poPar2 As List(Of PlatformDTOnon))

    <OperationContract(Action:="GetPlatformList", ReplyAction:="GetPlatformList")>
    <FaultContract(GetType(R_ServiceExceptions))>
    Function GetPlatformList() As Message
End Interface
