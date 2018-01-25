Imports MySql.Data.MySqlClient

Public Class DeviceManager
    Private _supportedDevices As List(Of IDevice) = New List(Of IDevice)
    Private _db As DataBase

    Sub New(db As DataBase)
        _db = db
        _supportedDevices.Add(New TrafficMonitor)
        _supportedDevices.Add(New AccessPoint)
    End Sub

    Public Sub DevicePacketHandler(packet As DevicePacket)
        For Each device In _supportedDevices
            If device.IsSupported(packet.DeviceId) Then
                device.AppendData(packet, _db)
            End If
        Next
    End Sub
End Class
