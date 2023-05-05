Imports System.ServiceModel
Imports R_Common
Imports SAM01200Back
Imports R_BackEnd
Imports System.ServiceModel.Channels

' NOTE: You can use the "Rename" command on the context menu to change the interface name "ISAM01200Service" in both code and config file together.
<ServiceContract()>
Public Interface ISAM01200Service

    Inherits R_IServicebase(Of SAM01200UserDTO)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getCmbCompany() As List(Of cmbDTO)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getCmbMenu(pcCompId As String) As List(Of cmbDTO)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getCmbCompanyCopy(pcUserId As String) As List(Of cmbDTO)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getCmbMenuCopy(pcCompId As String, pcUserId As String) As List(Of cmbDTO)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function resetPass(poParam As SAM01200UserDTO) As String

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Sub SliceFiles(pcUpdateBy As String, poSlice As SliceDTO)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Sub SaveImage(pcUpdateBy As String, poSlice As SliceDTO)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function GetImage(pcUserId As String) As List(Of SliceDTO)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getSMTP(pcCompId As String) As String

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function CheckAssignCompany(pcCompId As String, pcUserId As String, piLicenseNumber As Integer) As Boolean

    <OperationContract()>
    <FaultContract(GetType(R_ServiceExceptions))>
    Function GetUserCompanyCount(pcCompId As String) As Integer

    <OperationContract()>
    <FaultContract(GetType(R_ServiceExceptions))>
    Function GetEmailTemplateId(pcCompanyId As String, pcTemplateId As String) As String
End Interface
