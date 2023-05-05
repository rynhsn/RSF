Imports System.ServiceModel
Imports R_Common
Imports GSM00100Back
Imports R_BackEnd

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IGST00300Service" in both code and config file together.
<ServiceContract()>
Public Interface IGSM00100Service

    Inherits R_IServicebase(Of GSM00100DTO)

    <OperationContract()> _
    <FaultContract(GetType(R_ServiceExceptions))> _
    Function CheckDelete(poParam As GSM00100DTO) As Boolean

    <OperationContract()>
    <FaultContract(GetType(R_ServiceExceptions))>
    Sub TestSendEmail(poParam As EmailDTO)

    '<OperationContract()>
    '<FaultContract(GetType(R_ServiceExceptions))>
    'Sub TestErrorHandlingSP(plWithError As Boolean)
End Interface
