Imports Bwl.Framework
Imports MySql.Data.MySqlClient

Public Class DataBase
    Private _user As String = ""
    Private _password As String = ""
    Private _base As String = ""
    Private _logger As Logger
    Private _con As MySqlConnection

    Sub New(user As String, password As String, base As String)
        _user = user
        _password = password
        _base = base
    End Sub

    Public Sub SetRootLogger(logger As Logger)
        _logger = logger
    End Sub

    Public Sub Open()
        _con = New MySqlConnection()
        Dim setting = "server=localhost;uid=" + _user + ";pwd=" & _password & ";database=" + _base
        _con.ConnectionString = setting
        Try
            _con.Open()
            _logger.AddMessage("MySQL connected")
        Catch ex As MySqlException
            _logger.AddMessage(ex.Message)
        End Try
    End Sub

    Public Sub Execute(sql As String)
        Dim cmd = New MySqlCommand(sql, _con)
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            _logger.AddMessage(ex.Message)
            _con.Dispose()
            Open()
        End Try
    End Sub

    Public Function GetData(sql As String) As List(Of Dictionary(Of String, String))
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
            _logger.AddError(ex.Message)
        End Try
        Return result
    End Function

    Public Function GetDeviceIdList(tableName As String) As List(Of String)
        Dim sql = "SELECT DISTINCT device_id FROM " + tableName + ";"
        Dim result As New List(Of String)
        Try
            Dim cmd As New MySqlCommand
            cmd.Connection = _con
            cmd.CommandText = sql
            Dim reader As MySqlDataReader
            reader = cmd.ExecuteReader()
            While reader.Read()
                result.Add(reader.GetValue(0).ToString)
            End While
            reader.Close()
            cmd.Dispose()
        Catch ex As Exception
            _logger.AddError(ex.Message)
        End Try
        Return result
    End Function

    Private Sub AppendLogger(accessPointId As String, deviceId As String, data As String, dataType As String)
        Dim query = String.Format("INSERT INTO system_logs (base_id, device_id, message, message_type, time) VALUES('{0}','{1}','{2}','{3}',NOW());", accessPointId, deviceId, data, dataType)
        _logger.AddDebug(query)
        Execute(query)
    End Sub

    Public Sub AppendLoggerData(accessPointId As String, deviceId As String, data As Byte())
        AppendLogger(accessPointId, deviceId, BitConverter.ToString(data).Replace("-", " "), "data")
    End Sub

    Public Sub AppendLoggerInfo(accessPointId As String, deviceId As String, message As String)
        AppendLogger(accessPointId, deviceId, message, "info")
    End Sub

    Public Sub AppendLoggerError(accessPointId As String, deviceId As String, message As String)
        AppendLogger(accessPointId, deviceId, message, "error")
    End Sub

    Public Sub AppendLoggerPacket(packet As DevicePacket)
        AppendLogger(packet.AccessPointId, packet.DeviceId + " [" + packet.Rssi.ToString + "dBi]", BitConverter.ToString(packet.Data).Replace("-", " "), "data")
    End Sub
End Class
