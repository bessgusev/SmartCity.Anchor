Imports MySql.Data.MySqlClient
Imports SmartCity.Server

Public Class TrafficMonitor
    Implements IDevice
    Public Property DeviceId As String Implements IDevice.DeviceId

    Sub New()
    End Sub

    Public Sub AppendData(packet As DevicePacket, db As DataBase) Implements IDevice.AppendData
        If packet.Data.Length > 15 Then
            Dim minSpeed = packet.Data(1)
            Dim maxSpeed = packet.Data(2)
            Dim meanSpeed = packet.Data(3)
            Dim hh = packet.Data(4)
            Dim mm = packet.Data(5)
            Dim ss = packet.Data(6)
            Dim battery = packet.Data(7) * 0.1
            Dim latitudeWhole = packet.Data(8) * 100 + packet.Data(9)
            Dim latitudeFractional = packet.Data(10) * 100 + packet.Data(11)
            Dim longitudeWhole = packet.Data(12) * 100 + packet.Data(13)
            Dim longitudeFractional = packet.Data(14) * 100 + packet.Data(15)
            Dim query = "INSERT INTO traffic_monitor (device_id, base_station, device_time, latitude, longitude, mean_speed, min_speed, max_speed, rssi_packet, rssi, time, battery_voltage) VALUES('"
            query += packet.DeviceId + "', '" + packet.AccessPointId + "', '" + GetTimeString(hh, mm, ss) + "', '" + GetGpsData(latitudeWhole, latitudeFractional) + "', '" + GetGpsData(longitudeWhole, longitudeFractional) + "', " + meanSpeed.ToString + ", " + minSpeed.ToString + ", "
            query += maxSpeed.ToString + "," + packet.RssiPacket.ToString + ", " + packet.Rssi.ToString + ",  Now(), " + battery.ToString.Replace(",", ".") + ");"
            db.Execute(query)
        Else
            Throw New Exception("Invalid data format")
        End If

    End Sub

    Private Function GetTimeString(hh As Byte, mm As Byte, ss As Byte) As String
        Dim timeLine As String = ""
        If (hh.ToString.Length < 2) Then timeLine += ("0" + hh.ToString) Else timeLine += hh.ToString
        If (mm.ToString.Length < 2) Then timeLine += (":0" + mm.ToString) Else timeLine += (":" + mm.ToString)
        If (ss.ToString.Length < 2) Then timeLine += (":0" + ss.ToString) Else timeLine += (":" + ss.ToString)
        Return timeLine
    End Function

    Private Function GetGpsData(W As Integer, F As Integer) As String
        Dim wS = W.ToString
        Dim fS = F.ToString
        If fS.Length < 4 Then
            While fS.Length < 4
                fS = "0" + fS
            End While
        End If
        Return wS + "." + fS
    End Function

    Public Function IsSupported(id As String) As Boolean Implements IDevice.IsSupported
        If id.Substring(0, 2) = "01" Then Return True
        Return False
    End Function
End Class

