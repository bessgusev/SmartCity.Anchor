Imports MySql.Data.MySqlClient

Public Class SqlTool
    Private _con As MySqlConnection = Nothing
    Private _sqlServer As String = ""
    Private _sqlUser As String = ""
    Private _sqlPassword As String = ""
    Private _sqlBaseName As String = ""

    Sub New(sqlServer As String, sqlUser As String, sqlPassword As String, sqlBaseName As String)
        _sqlBaseName = sqlBaseName
        _sqlServer = sqlServer
        _sqlPassword = sqlPassword
        _sqlUser = sqlUser
    End Sub

    Public Sub Connect()
        If _con Is Nothing Then
            Try
                _con = New MySqlConnection()
                _con.ConnectionString = "server=" + _sqlServer + ";User id=" + _sqlUser + ";password=" + _sqlPassword + ";database=" + _sqlBaseName
                _con.Open()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Public Function GetJsonResult(sql As String) As String
        Dim result = "{ ""time"":""" + Now.ToString + """, ""result"":["
        Dim res = Global_asax.SQL.Execute(sql)
        If res.Count > 0 Then
            For i = 0 To res.Count - 1
                result += " {"
                Dim row = res(i)
                For Each key In row.Keys
                    Dim row_value = row(key).ToString
                    If row_value.Length > 0 Then
                        While row_value.Substring(row_value.Length - 1, 1).Contains(" ")
                            row_value = row_value.Substring(0, row_value.Length - 1)
                        End While
                    End If
                    result += (Chr(34) + key + Chr(34) + ":" + Chr(34) + row(key) + Chr(34) + ",")
                Next
                result = result.Substring(0, result.Length - 1)
                result += " },"
            Next
            result = result.Substring(0, result.Length - 1)
        End If
        result += "]}"
        Return result
    End Function

    Public Function Execute(sql As String) As List(Of Dictionary(Of String, String))
        Connect()
        Dim result As New List(Of Dictionary(Of String, String))

        Try
            Dim cmd As New MySqlCommand
            cmd.Connection = _con
            cmd.CommandText = sql
            Dim reader As MySqlDataReader
            reader = cmd.ExecuteReader()
            While reader.Read()
                Dim row = New Dictionary(Of String, String)
                For i = 0 To reader.VisibleFieldCount - 1
                    row.Add(reader.GetName(i), reader.GetValue(i).ToString)
                Next
                result.Add(row)
            End While
            reader.Close()
            cmd.Dispose()
        Catch ex As Exception

        End Try
        Return result
    End Function
End Class
