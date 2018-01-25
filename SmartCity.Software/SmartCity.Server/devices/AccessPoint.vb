Imports System.Text

Public Class AccessPoint
    Implements IDevice
    Public Property DeviceId As String Implements IDevice.DeviceId

    Public Sub AppendData(packet As DevicePacket, db As DataBase) Implements IDevice.AppendData
        For i = 0 To packet.Data.Length - 1
            If packet.Data(i) < 32 Then packet.Data(i) = 32
        Next
        Dim version = Encoding.ASCII.GetString(packet.Data)
        Dim query = "INSERT INTO access_points (device_id, last_active, current_version) VALUES ('" + packet.AccessPointId + "', NOW(), '" + version + "') ON DUPLICATE KEY UPDATE last_active=NOW(), current_version='" + version + "';"
        db.Execute(query)
    End Sub

    Sub New()
    End Sub

    Public Function IsSupported(id As String) As Boolean Implements IDevice.IsSupported
        If id.Substring(0, 2) = "00" Then Return True
        Return False
    End Function
End Class
