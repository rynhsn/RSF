Imports System.ServiceModel
Imports R_Common
Imports SAM01200Back
Imports R_BackEnd
Imports System.ServiceModel.Channels

' NOTE: You can use the "Rename" command on the context menu to change the interface name "ISAM01200StreamingService" in both code and config file together.
<ServiceContract()>
Public Interface ISAM01200StreamingService

    <OperationContract(Action:="getUserList", ReplyAction:="getUserList")> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getUserList() As Message

    <OperationContract(Action:="getUserCompanyList", ReplyAction:="getUserCompanyList")> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getUserCompanyList() As Message

    <OperationContract(Action:="getUserMenuList", ReplyAction:="getUserMenuList")> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getUserMenuList() As Message

    <OperationContract(Action:="getCompanyMultiple", ReplyAction:="getCompanyMultiple")> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getCompanyMultiple() As Message

    <OperationContract(Action:="getMenuMultiple", ReplyAction:="getMenuMultiple")> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getMenuMultiple() As Message

    <OperationContract(Action:="getCompanyCopyList", ReplyAction:="getCompanyCopyList")> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getCompanyCopyList() As Message

    <OperationContract(Action:="getMenuCopyList", ReplyAction:="getMenuCopyList")> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getMenuCopyList() As Message

    <OperationContract(Action:="GetAssignmentUserList", ReplyAction:="GetAssignmentUserList")> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function GetAssignmentUserList() As Message
End Interface
