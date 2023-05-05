Imports System.ServiceModel
Imports R_Common
Imports TelerikMenuBackCls

' NOTE: You can use the "Rename" command on the context menu to change the interface name "ILoginService" in both code and config file together.
<ServiceContract()>
Public Interface ILoginService
    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Sub Svc_R_UserLocking(ByVal poParameter As LoginDTO)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Sub Svc_R_UserLockingCompany(ByVal pcCurrentCompanyId As String, ByVal pcNewCompanyId As String, ByVal pcUserId As String)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Sub Svc_R_UserLockingFlush(ByVal pcCurrentCompanyId As String, ByVal pcUserId As String)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function Logon(ByVal poParameter As LoginDTO) As LoginDTO

    <OperationContract()>
    <FaultContract(GetType(R_ServiceExceptions))>
    Sub SetKey(ByVal pcKey As String)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function svc_getUserCompanyBroadcast(ByVal poParameter As SAM_USER_COMPANYDTO) As SAM_USER_COMPANYDTO

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function svc_getCompanyAndUserName(ByVal poParameter As LoginDTO) As LoginDTO

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getLastUpdate(ByVal poParameter As LoginDTO) As Nullable(Of Date)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Sub doFlushData(ByVal pcUserId As String, ByVal pcCompanyId As String)
End Interface
