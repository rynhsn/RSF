Imports System.ServiceModel
Imports R_Common
Imports SAM01000Back
Imports R_BackEnd
Imports System.ServiceModel.Channels

' NOTE: You can use the "Rename" command on the context menu to change the interface name "ISAM01000Service" in both code and config file together.
<ServiceContract()>
Public Interface ISAM01000Service

    Inherits R_IServicebase(Of SAM01000DTO)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getCmbLOB() As List(Of SAM01000CmbDTO)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function GetImage(pcCompId As String) As SliceDTO

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Sub SliceFiles(pcUpdateBy As String, poSlice As SliceDTO)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Sub SaveImage(pcUpdateBy As String, poSlice As SliceDTO, pcCompId As String)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Sub SaveDateTime(poNewEntity As SAM01000DTO)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function getLicenseActivation(pcCompId As String) As SAM01000Back.LicenseDTO

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Sub SaveTimeout(poNewEntity As SAM01000DTO)
End Interface
