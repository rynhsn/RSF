Imports System.ServiceModel
Imports R_Common
Imports TelerikMenuBackCls

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IMenuService" in both code and config file together.
<ServiceContract()>
Public Interface IMenuService

    <OperationContract()>
    <FaultContract(GetType(R_ServiceExceptions))>
    Function getLastId(ByVal pcCompanyId As String, ByVal pcUserId As String, ByVal pcMENU_ID As String, ByVal pcSUB_MENU_TYPE As String) As MenuDTO

    <OperationContract()>
    <FaultContract(GetType(R_ServiceExceptions))>
    Sub insertMenuToDB(ByVal poMenuDTO As MenuDTO)

    <OperationContract()>
    <FaultContract(GetType(R_ServiceExceptions))>
    Sub deleteMenuFromDB(ByVal poMenuDTO As MenuDTO)

    <OperationContract()>
    <FaultContract(GetType(R_ServiceExceptions))>
    Sub updateMenuNameToDB(ByVal poMenuDTO As MenuDTO)

    <OperationContract()>
    <FaultContract(GetType(R_ServiceExceptions))>
    Sub updateGroupIndexToDB(ByVal poMenuDTO As MenuDTO, ByVal pcOperation As String)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Sub updateMenuPositionToDB(ByVal poMenuDTO As MenuDTO)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function _getProgramImage(ByVal poMenuDTO As MenuDTO) As String

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Sub saveMenuFavourite(poMenuDTO As MenuDTO)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Sub deleteMenuFavourite(poMenuDTO As MenuDTO)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function svc_getCompanyList(ByVal pcUserId As String, ByVal pcCompanyId As String) As List(Of SAM_USER_COMPANYDTO)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function checkMenuFavourite(poMenuDTO As MenuDTO) As Boolean

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getSecurity() As Boolean

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getAccessButton(pcCompId As String, pcProgId As String) As List(Of MenuProgramAccessDTO)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Sub SaveHistory(pcCompId As String, pcUserId As String, pcProgId As String, pcAction As String)

    <OperationContract()>
    <FaultContract(GetType(R_ServiceExceptions))>
    Function GetInfo(pcAppId As String) As List(Of AboutDTO)
End Interface
