Imports System.Net.Sockets

Public Class AccessPointProcessor
    Private _tcp As TcpClient = Nothing
    Private _stream As NetworkStream
    Event onPacketReceived(packet As DevicePacket)
    Event onClinetDisconnectedEvent(client As AccessPointProcessor)
    Private _crc As New Tool
    Public Property LastTime As DateTime = Now
    Public Property AccessPointid As String = ""
    Private _lockObject As New Object

    Sub New(client As TcpClient)
        _tcp = client
        _stream = _tcp.GetStream
        Dim th = New Threading.Thread(AddressOf ClientHandler)
        th.IsBackground = True
        th.Start()

        Dim testThread = New Threading.Thread(AddressOf TcpTest)
        testThread.IsBackground = True
        testThread.Start()
    End Sub

    Private Sub ClientHandler()
        Dim headerBuffer(4) As Byte
        While _tcp IsNot Nothing
            Try
                If _tcp.Available > 0 Then
                    Dim buffer(10) As Byte
                    _stream.Read(buffer, 0, 1)
                    For j = 0 To headerBuffer.Length - 2
                        headerBuffer(j) = headerBuffer(j + 1)
                    Next
                    headerBuffer(4) = buffer(0)
                    If headerBuffer(0) = &H55 And headerBuffer(1) = &HAA And headerBuffer(2) = &H47 And headerBuffer(3) = &H7A And headerBuffer(4) = &HA7 Then
                        _stream.Read(buffer, 0, 2)
                        Dim datalength As Integer = buffer(0) * 256 + buffer(1)
                        Dim packetBuffer(datalength - 1) As Byte
                        _stream.Read(packetBuffer, 0, packetBuffer.Length)
                        Dim crcBuffer(3) As Byte
                        _stream.Read(crcBuffer, 0, crcBuffer.Length)
                        If (_crc.ComputeChecksum(packetBuffer) = BitConverter.ToUInt32(crcBuffer, 0)) Then
                            LowLevelPacketHandler(packetBuffer)
                        End If
                        _stream.Write(crcBuffer, 0, crcBuffer.Length)
                    End If
                Else
                    Threading.Thread.Sleep(50)
                End If
            Catch ex As Exception

            End Try
        End While
    End Sub


    Private Sub LowLevelPacketHandler(data As Byte())
        Dim packet As New DevicePacket
        Dim apId(9) As Byte
        Dim DeviceId(4) As Byte
        Array.Copy(data, apId, apId.Length)
        Array.Copy(data, apId.Length + 8, DeviceId, 0, DeviceId.Length)
        packet.RssiPacket = BitConverter.ToInt32(data, apId.Length)
        packet.Rssi = BitConverter.ToInt32(data, apId.Length + 4)
        Dim deviceData(data.Length - DeviceId.Length - apId.Length - 1) As Byte
        Array.Copy(data, apId.Length + DeviceId.Length + 8, deviceData, 0, data.Length - apId.Length - DeviceId.Length - 8)
        LastTime = Now
        packet.Data = deviceData
        packet.Client = Me
        packet.AccessPointId = Text.Encoding.ASCII.GetString(apId)
        packet.DeviceId = Tool.ByteArrayToString(DeviceId)
        If AccessPointid.Length = 0 Then
            AccessPointid = packet.AccessPointId
        End If
        RaiseEvent onPacketReceived(packet)
    End Sub

    Public Sub Send(deviceId As String, data As Byte())
        Dim devId = Tool.StringToByteArray(deviceId)
        Dim packet(devId.Length + data.Length - 1) As Byte
        Array.Copy(devId, 0, packet, 0, devId.Length)
        Array.Copy(data, 0, packet, devId.Length, data.Length)
        Dim dataLength As UInt16 = packet.Length
        Dim header As Byte() = {0, 0, 0, 0, 0, &H55, &HAA, &H47, &H7A, &HA7, dataLength >> 8, dataLength And &HFF}
        Dim crc = BitConverter.GetBytes(_crc.ComputeChecksum(packet))
        SyncLock _lockObject
            _stream.Write(header, 0, header.Length)
            _stream.Write(packet, 0, packet.Length)
            _stream.Write(crc, 0, crc.Length)
        End SyncLock
    End Sub

    Private Sub TcpTest()
        While _tcp IsNot Nothing
            Try
                SyncLock _lockObject
                    _stream.Write({0}, 0, 1)
                End SyncLock
            Catch ex As Exception
                _tcp = Nothing
                RaiseEvent onClinetDisconnectedEvent(Me)
            End Try
            Threading.Thread.Sleep(5000)
        End While
    End Sub

End Class

Public Class DevicePacket
    Public Property AccessPointId As String = ""
    Public Property DeviceId As String = ""
    Public Property Data As Byte()
    Public Property Client As AccessPointProcessor
    Public Property Rssi As Integer = 0
    Public Property RssiPacket As Integer = 0
End Class