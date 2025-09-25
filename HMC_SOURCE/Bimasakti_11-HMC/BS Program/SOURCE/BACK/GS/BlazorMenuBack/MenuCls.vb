Imports System.Data
Imports System.Data.Common
Imports BlazorMenuCommon
Imports R_BackEnd
Imports R_Common

Public Class MenuCls

    Public Function GetMenuAccess(poParam As ParamDTO) As List(Of MenuProgramAccessDTO)
        Dim lcCmd As String = ""
        Dim loList As List(Of MenuProgramAccessDTO) = Nothing
        Dim loEx As New R_Exception()
        Dim loDb As New R_Db

        Try
            lcCmd = "SELECT A.CCOMPANY_ID, A.CMENU_ID, B.CPROGRAM_ID, C.CPROGRAM_ACCESS AS CACCESS_ID, D.CMENU_NAME, "
            lcCmd += "CPROGRAM_NAME = CASE WHEN CLANG_ID <> 'EN' THEN ISNULL(F.CPROGRAM_NAME, E.CPROGRAM_NAME) ELSE E.CPROGRAM_NAME END, "
            lcCmd += "CTOOL_TIP = CASE WHEN CLANG_ID <> 'EN' THEN ISNULL(F.CTOOL_TIP, E.CTOOL_TIP) ELSE E.CTOOL_TIP END "
            lcCmd += "FROM SAM_USER_PROGRAM A (NOLOCK) "
            lcCmd += "INNER JOIN SAM_PROGRAM_ACCESS B (NOLOCK) "
            lcCmd += "ON B.CPROGRAM_ID = A.CSUB_MENU_ID "
            lcCmd += "INNER JOIN SAM_MENU_PROGRAM C (NOLOCK) "
            lcCmd += "ON C.CCOMPANY_ID = A.CCOMPANY_ID "
            lcCmd += "AND C.CMENU_ID = A.CMENU_ID "
            lcCmd += "AND C.CPROGRAM_ID = A.CSUB_MENU_ID "
            lcCmd += "INNER JOIN SAM_MENU D (NOLOCK) "
            lcCmd += "ON D.CCOMPANY_ID = A.CCOMPANY_ID "
            lcCmd += "AND D.CMENU_ID = A.CMENU_ID "
            lcCmd += "INNER JOIN SAM_PROGRAM E (NOLOCK) ON A.CSUB_MENU_ID = E.CPROGRAM_ID "
            lcCmd += "LEFT JOIN SAM_PROGRAM_LANG F (NOLOCK) ON E.CPROGRAM_ID = F.CPROGRAM_ID AND CLANG_ID = '{2}' "
            lcCmd += "WHERE A.CCOMPANY_ID = '{0}' AND A.CUSER_ID = '{1}' "
            lcCmd = String.Format(lcCmd, poParam.CCOMPANY_ID, poParam.CUSER_ID, poParam.CLANGUAGE_ID)

            loList = loDb.SqlExecObjectQuery(Of MenuProgramAccessDTO)(lcCmd)
        Catch ex As Exception
            loEx.Add(ex)
        End Try
        loEx.ThrowExceptionIfErrors()

        Return loList
    End Function

    Public Function GetMenu(poParam As ParamDTO) As List(Of MenuListDTO)
        Dim lcCmd As String = ""
        Dim loList As List(Of MenuListDTO) = Nothing
        Dim loEx As New R_Exception()
        Dim loDb As New R_Db

        Try
            With poParam
                If .ILEVEL = 1 Then
                    lcCmd = "SELECT A.CCOMPANY_ID, A.CUSER_ID, A.CMENU_ID, A.CSUB_MENU_TYPE, A.CSUB_MENU_ID, "
                    lcCmd += "CSUB_MENU_NAME = CASE A.CSUB_MENU_TYPE WHEN 'P' THEN ISNULL(D.CPROGRAM_NAME, B.CPROGRAM_NAME) ELSE A.CSUB_MENU_NAME END, "
                    lcCmd += "CSUB_MENU_TOOL_TIP = CASE A.CSUB_MENU_TYPE WHEN 'P' THEN ISNULL(D.CTOOL_TIP, B.CTOOL_TIP) ELSE A.CSUB_MENU_NAME END, "
                    lcCmd += "CSUB_MENU_IMAGE = CASE A.CSUB_MENU_TYPE WHEN 'P' THEN B.CPROGRAM_BUTTON ELSE '' END, "
                    lcCmd += "A.CPARENT_SUB_MENU_ID, A.IGROUP_INDEX, A.IROW_INDEX, A.ICOLUMN_INDEX, A.LFAVORITE, A.IFAVORITE_INDEX, A.ILEVEL, "
                    lcCmd += "Modul = B.CMODULE_ID, C.CMENU_NAME INTO #TempMenu FROM SAM_USER_PROGRAM A (NOLOCK) "
                    lcCmd += "LEFT OUTER JOIN SAM_PROGRAM B (NOLOCK) ON B.CPROGRAM_ID = A.CSUB_MENU_ID "
                    lcCmd += "LEFT OUTER JOIN SAM_PROGRAM_LANG D (NOLOCK) ON D.CPROGRAM_ID = B.CPROGRAM_ID AND D.CLANG_ID = '" + .CLANGUAGE_ID.Trim + "' "
                    lcCmd += "INNER JOIN SAM_MENU C (NOLOCK) ON C.CMENU_ID = A.CMENU_ID AND C.CCOMPANY_ID = A.CCOMPANY_ID "
                    lcCmd += "WHERE A.CCOMPANY_ID = '" + .CCOMPANY_ID.Trim + "' AND A.CUSER_ID = '" + .CUSER_ID.Trim + "' AND "

                    If Not .CMENU_ID = "" Then
                        lcCmd += "A.CMENU_ID = '" + .CMENU_ID.Trim + "' "
                        lcCmd += "AND "
                    End If
                    lcCmd += "A.ILEVEL = " + .ILEVEL.ToString.Trim

                Else '' pnLEVEL <> 1
                    lcCmd = "SELECT A.CCOMPANY_ID, A.CUSER_ID, A.CMENU_ID, A.CSUB_MENU_TYPE, A.CSUB_MENU_ID, A.CSUB_MENU_NAME, CSUB_MENU_TOOL_TIP = A.CSUB_MENU_NAME, "
                    lcCmd += "CSUB_MENU_IMAGE = '', A.CPARENT_SUB_MENU_ID, A.IGROUP_INDEX, A.IROW_INDEX, A.ICOLUMN_INDEX, A.LFAVORITE, A.IFAVORITE_INDEX, "
                    lcCmd += "A.ILEVEL, Modul = B.CMODULE_ID INTO #TempMenu FROM SAM_USER_PROGRAM A (NOLOCK) "
                    lcCmd += "WHERE A.CCOMPANY_ID = '{0}' AND A.CUSER_ID = '{1}' AND A.CMENU_ID = '{2}' AND A.CSUB_MENU_TYPE = 'G' AND A.CPARENT_SUB_MENU_ID = '{3}' "
                    lcCmd += "AND A.ILEVEL = {4} "
                    lcCmd = String.Format(lcCmd, .CCOMPANY_ID.Trim, .CUSER_ID.Trim, .CMENU_ID.Trim, .CSUB_MENU_ID.Trim, .ILEVEL)

                    lcCmd += "INSERT INTO #TempMenu "
                    lcCmd += "SELECT A.CCOMPANY_ID, A.CUSER_ID, A.CMENU_ID, A.CSUB_MENU_TYPE, A.CSUB_MENU_ID, CSUB_MENU_NAME = B.CPROGRAM_NAME, "
                    lcCmd += "CSUB_MENU_TOOL_TIP = B.CTOOL_TIP, CSUB_MENU_IMAGE = B.CPROGRAM_BUTTON, A.CPARENT_SUB_MENU_ID, A.IGROUP_INDEX, A.IROW_INDEX, "
                    lcCmd += "A.ICOLUMN_INDEX, A.LFAVORITE, A.IFAVORITE_INDEX, A.ILEVEL, Modul = B.CMODULE_ID "
                    lcCmd += "FROM SAM_USER_PROGRAM A (NOLOCK) "
                    lcCmd += "LEFT OUTER JOIN SAM_PROGRAM B (NOLOCK) "
                    lcCmd += "ON B.CPROGRAM_ID = A.CSUB_MENU_ID "
                    lcCmd += "WHERE A.CCOMPANY_ID = '{0}' AND A.CUSER_ID = '{1}' AND A.CMENU_ID = '{2}' AND A.CSUB_MENU_TYPE = 'P' "
                    lcCmd += "AND A.CPARENT_SUB_MENU_ID IN "
                    lcCmd += "(SELECT C.CSUB_MENU_ID FROM SAM_USER_PROGRAM C (NOLOCK) "
                    lcCmd += "WHERE C.CCOMPANY_ID = A.CCOMPANY_ID AND C.CUSER_ID = A.CUSER_ID AND C.CMENU_ID = A.CMENU_ID AND C.CSUB_MENU_TYPE = 'G' "
                    lcCmd += "AND C.CPARENT_SUB_MENU_ID = '{3}' AND C.ILEVEL = A.ILEVEL) "
                    lcCmd += "AND A.ILEVEL = {4} "
                    lcCmd = String.Format(lcCmd, .CCOMPANY_ID.Trim, .CUSER_ID.Trim, .CMENU_ID.Trim, .CSUB_MENU_ID.Trim, .ILEVEL)
                End If

                lcCmd += "SELECT * FROM #TempMenu "

                If Not .CMODUL_ID = "" Then
                    lcCmd += "WHERE Modul IN(" + .CMODUL_ID.Trim + ") OR Modul IS NULL "
                End If
            End With

            loList = loDb.SqlExecObjectQuery(Of MenuListDTO)(lcCmd)
        Catch ex As Exception
            loEx.Add(ex)
        End Try
        loEx.ThrowExceptionIfErrors()

        Return loList
    End Function

    Public Function GetProgramImage(ByVal poMenuDTO As MenuDTO) As String
        Dim lcCmd As String = ""
        Dim lcResult As String = ""
        Dim loDTO As MenuDTO = Nothing
        Dim loDb As New R_Db
        Dim loEx As New R_Exception()

        Try
            lcCmd = "SELECT CPROGRAM_BUTTON as CSUB_MENU_IMAGE "
            lcCmd += "FROM SAM_PROGRAM (NOLOCK) "
            lcCmd += "WHERE CPROGRAM_ID = '{0}' "
            lcCmd = String.Format(lcCmd, poMenuDTO.CSUB_MENU_ID.Trim)
            loDTO = loDb.SqlExecObjectQuery(Of MenuDTO)(lcCmd).FirstOrDefault

            lcResult = loDTO.CSUB_MENU_IMAGE
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()
        Return lcResult
    End Function

    Public Function GetCompanyList(ByVal pcUserId As String, ByVal pcCompanyId As String) As List(Of UserCompanyDTO)
        Dim loEx As New R_Exception
        Dim lcCmd As String = ""
        Dim loList As List(Of UserCompanyDTO) = Nothing
        Dim loDb As New R_Db

        Try
            lcCmd = "SELECT CCOMPANY_ID "
            lcCmd += "FROM SAM_USER_COMPANY A (NOLOCK) "
            lcCmd += "WHERE A.CUSER_ID = '{0}' "
            lcCmd += "AND A.CCOMPANY_ID  <> '{1}' "
            lcCmd = String.Format(lcCmd, pcUserId.Trim, pcCompanyId.Trim)

            loList = loDb.SqlExecObjectQuery(Of UserCompanyDTO)(lcCmd)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()

        Return loList
    End Function

    'Public Function GetSecurity() As Boolean
    '    Dim llRtn As Boolean = False
    '    Dim lcCmd As String
    '    Dim loDTO As New List(Of SecurityDTO)
    '    Dim loDb As New R_Db
    '    Dim loEx As New R_Exception()
    '    Dim loResult As New SecurityDTO

    '    Try
    '        lcCmd = "SELECT * "
    '        lcCmd += "FROM GST_PARAMETER (NOLOCK) "
    '        lcCmd = String.Format(lcCmd)
    '        loDTO = loDb.SqlExecObjectQuery(Of SecurityDTO)(lcCmd)

    '        loResult = loDTO.Where(Function(x) x.cParameterId = "SecurityAndAccountPolicy").Select(Function(x) x).FirstOrDefault

    '        If loResult.cParameterValue = "bycompany" Then
    '            llRtn = True
    '        End If

    '    Catch ex As Exception
    '        loEx.Add(ex)
    '    End Try

    '    loEx.ThrowExceptionIfErrors()
    '    Return llRtn
    'End Function

    'Public Function getAccessButton(pcCompId As String, pcProgId As String) As List(Of MenuProgramAccessDTO)
    '    Dim loResult As New List(Of MenuProgramAccessDTO)
    '    Dim lcCmd As String
    '    Dim loDb As New R_Db()
    '    Dim loEx As New R_Exception()

    '    Try
    '        lcCmd = "SELECT CPROGRAM_ACCESS_BUTTON AS CACCESS_ID, CMENU_ID "
    '        lcCmd += "FROM SAM_MENU_PROGRAM (NOLOCK) "
    '        lcCmd += "WHERE CCOMPANY_ID = '{0}' AND CPROGRAM_ID = '{1}' "
    '        lcCmd = String.Format(lcCmd, pcCompId, pcProgId)
    '        loResult = loDb.SqlExecObjectQuery(Of MenuProgramAccessDTO)(lcCmd)
    '    Catch ex As Exception
    '        loEx.Add(ex)
    '    End Try

    '    loEx.ThrowExceptionIfErrors()

    '    Return loResult
    'End Function

    Public Sub SaveHistory(pcCompId As String, pcUserId As String, pcProgId As String, pcAction As String)
        Dim lcCmd As String = ""
        Dim loEx As New R_Exception()
        Dim loDb As New R_Db

        Try
            'insert to database
            lcCmd = "INSERT INTO SAH_PROGRAM_HISTORY "
            lcCmd += "(CCOMPANY_ID, CUSER_ID, CPROGRAM_ID, CACTION, DCREATE_DATE) "
            lcCmd += "VALUES('{0}', '{1}', '{2}', '{3}', GETDATE())"
            lcCmd = String.Format(lcCmd, pcCompId, pcUserId, pcProgId, pcAction)

            loDb.SqlExecNonQuery(lcCmd)
        Catch ex As Exception
            loEx.Add(ex)
        End Try
        loEx.ThrowExceptionIfErrors()
    End Sub

    Public Function GetInfo(pcAppId As String) As List(Of InfoDTO)
        Dim loEx As New R_Exception()
        Dim loDb As New R_Db
        Dim loRtn As List(Of InfoDTO) = Nothing
        Dim loConn As DbConnection = Nothing
        Dim lcQuery As String = ""
        Dim lcVersion As String = ""

        Try
            loConn = loDb.GetConnection()

            lcQuery = "SELECT CVERSION = CVERSION + '.' + CREVISION "
            lcQuery += "FROM SAB_APP_VERSION (NOLOCK) "
            lcQuery += "WHERE CAPPLICATION_ID = '{0}' "
            lcQuery = String.Format(lcQuery, pcAppId)
            lcVersion = loDb.SqlExecObjectQuery(Of String)(lcQuery, loConn, False).FirstOrDefault

            loRtn.Add(New InfoDTO With {.InfoName = "Database", .InfoDesc = loConn.Database})
            loRtn.Add(New InfoDTO With {.InfoName = "Database Server", .InfoDesc = loConn.DataSource})
            loRtn.Add(New InfoDTO With {.InfoName = "Application Id", .InfoDesc = pcAppId})
            loRtn.Add(New InfoDTO With {.InfoName = "Application Version", .InfoDesc = lcVersion})
        Catch ex As Exception
            loEx.Add(ex)
        Finally
            If loConn IsNot Nothing Then
                If Not (loConn.State = ConnectionState.Closed) Then
                    loConn.Close()
                End If
                loConn.Dispose()
                loConn = Nothing
            End If
        End Try

        loEx.ThrowExceptionIfErrors()
        Return loRtn
    End Function
End Class
