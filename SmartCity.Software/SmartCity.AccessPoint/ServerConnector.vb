Imports System.Net
Imports System.Net.Sockets
Imports System.Text

Public Class ServerConnector
    Private _url As String = ""
    Private _tcp As TcpClient = Nothing
    Private _ip As String = ""
    Private _port As Integer = 0
    Private _stream As NetworkStream = Nothing
    Private _id(9) As Byte
    Private _crc As New Crc32

    Event onReceiveHandler(data As Byte())

    Sub New(ip As String, port As Integer, serverKey As String)
        _ip = ip
        _port = port
        Dim idBytes = Encoding.ASCII.GetBytes(serverKey)
        For i = 0 To idBytes.Length - 1
            If i < idBytes.Length Then _id(i) = idBytes(i)
        Next
    End Sub

    Public Sub Run()
        Dim listenerTh = New Threading.Thread(AddressOf ServerListener)
        listenerTh.IsBackground = True
        listenerTh.Start()
    End Sub

    Private Sub ServerListener()
        Dim headerBuffer(4) As Byte
        While True
            Try
                If _tcp Is Nothing Then
                    Console.WriteLine("SERVER {0}:{1}", _ip, _port)
                    _tcp = New TcpClient(_ip, _port)
                    _stream = _tcp.GetStream()
                    Send({1, 1, 1, 1})
                    Console.ForegroundColor = ConsoleColor.DarkGreen
                    Console.WriteLine("SERVER CONNECTED {0}:{1}", _ip, _port)
                Else
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
                                Console.ForegroundColor = ConsoleColor.DarkGreen
                                Console.WriteLine("CLIENT <- SERVER: {0} bytes", packetBuffer.Length)
                                RaiseEvent onReceiveHandler(packetBuffer)
                            End If
                            _stream.Write(crcBuffer, 0, crcBuffer.Length)
                        End If
                    Else
                        Threading.Thread.Sleep(50)
                    End If
                End If
            Catch ex As Exception
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine("SERVER -> " + ex.Message)
            End Try
        End While
    End Sub

    Public Sub Send(data As Byte())
        Try
            Dim packet(data.Length + _id.Length - 1) As Byte
            Array.Copy(_id, 0, packet, 0, _id.Length)
            Array.Copy(data, 0, packet, _id.Length, data.Length)
            Dim dataLength As UInt16 = packet.Length
            Dim header As Byte() = {0, 0, 0, 0, 0, &H55, &HAA, &H47, &H7A, &HA7, dataLength >> 8, dataLength And &HFF}
            Dim crc = BitConverter.GetBytes(_crc.ComputeChecksum(packet))
            _stream.Write(header, 0, header.Length)
            _stream.Write(packet, 0, packet.Length)
            _stream.Write(crc, 0, crc.Length)
        Catch ex As Exception
            If (_tcp IsNot Nothing) Then _tcp.Close()
            _tcp = Nothing
            Throw New Exception("TCP sending failed: " + ex.Message)
        End Try
    End Sub

End Class
