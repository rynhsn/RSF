Public Class MenuListDTO
    Public Property CCOMPANY_ID As String
    Public Property CUSER_ID As String
    Public Property CMENU_ID As String
    Public Property CMENU_NAME As String
    Public Property CSUB_MENU_TYPE As String
    Public Property CSUB_MENU_ID As String
    Public Property CSUB_MENU_NAME As String
    Public Property CSUB_MENU_TOOL_TIP As String
    Public Property CSUB_MENU_IMAGE As String
    Public Property CPARENT_SUB_MENU_ID As String
    Public Property IGROUP_INDEX As Nullable(Of Integer)
    Public Property IROW_INDEX As Nullable(Of Integer)
    Public Property ICOLUMN_INDEX As Nullable(Of Integer)
    Public Property LFAVORITE As Boolean
    Public Property IFAVORITE_INDEX As Nullable(Of Integer)
    Public Property ILEVEL As Integer
    Public Property CSUB_MENU_ACCESS As String = "A,U,D,P,V" 'tambahin di service
End Class
