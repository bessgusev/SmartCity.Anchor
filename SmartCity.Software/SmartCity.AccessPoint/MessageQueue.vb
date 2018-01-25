Public Class MessageQueue
    Private _server As ServerConnector = Nothing
    Private _lora As LoraController = Nothing
    Private _FifoToServer As List(Of Byte()) = New List(Of Byte())
    Private _FifoToLora As New List(Of Byte())
    Private _loraLastPacketTxResult As Boolean = True

    Private _version As String = "0.22 Test Version"
    Private _apid As String = ""
    Sub New(srv As ServerConnector, lora As LoraController, devId As String)
        _server = srv
        _lora = lora
        _apid = devId
        AddHandler _lora.OnTxComplite, AddressOf onTxDoneLoraHandler
        AddHandler _lora.onRxLoraPacket, AddressOf onPacketLoraHandler
        AddHandler _server.onReceiveHandler, AddressOf onPacketServerHandler
    End Sub

    Public Sub Start()
        Dim loraThread = New Threading.Thread(AddressOf LoraQueueProcessor)
        loraThread.IsBackground = True
        loraThread.Start()
        Dim serverThread = New Threading.Thread(AddressOf ServerQueueProcessor)
        serverThread.IsBackground = True
        serverThread.Start()
        Dim reporter = New Threading.Thread(AddressOf ReportProcess)
        reporter.IsBackground = True
        reporter.Start()
    End Sub

    Private Sub LoraQueueProcessor()
        While True
            Try
                If _FifoToLora.Count > 0 Then
                    _loraLastPacketTxResult = False
                    _lora.RadioWrite(_FifoToLora.ElementAt(0))
                    Dim sendTime = Now
                    While _loraLastPacketTxResult <> True
                        Threading.Thread.Sleep(20)
                    End While
                    _FifoToLora.RemoveAt(0)
                Else
                    Threading.Thread.Sleep(100)
                End If
            Catch ex As Exception
            End Try
        End While
    End Sub

    Private Sub onTxDoneLoraHandler()
        _loraLastPacketTxResult = True
    End Sub

    Private Sub onPacketLoraHandler(packet As LoraPacket)
        Dim list = New List(Of Byte)
        list.AddRange(BitConverter.GetBytes(packet.PacketRSSI))
        list.AddRange(BitConverter.GetBytes(packet.RxOnGoingRSSI))
        list.AddRange(packet.Data)
        Dim result = list.ToArray()
        _FifoToServer.Add(result)
        Console.ForegroundColor = ConsoleColor.Green
        Console.WriteLine("LORA -> SERVER: " + ByteArrayToString(result))
    End Sub

    Private Sub ServerQueueProcessor()
        While True
            If _FifoToServer.Count > 0 Then
                Try
                    _server.Send(_FifoToServer.ElementAt(0))
                    _FifoToServer.RemoveAt(0)
                Catch ex As Exception
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine(ex.Message)
                    Threading.Thread.Sleep(2000)
                End Try
            Else
                Threading.Thread.Sleep(10)
            End If
        End While
    End Sub

    Private Sub onPacketServerHandler(data As Byte())
        Console.ForegroundColor = ConsoleColor.Blue
        Console.WriteLine("SERVER -> LORA: " + ByteArrayToString(data))
        _FifoToLora.Add(data)
    End Sub

    Private Function ByteArrayToString(ByVal ba As Byte()) As String
        Dim hex As String = BitConverter.ToString(ba)
        Return hex.Replace("-", " ")
    End Function


    Private Function DecodeHexData(hex As String) As Byte()
        Dim data(hex.Length / 2 - 1) As Byte
        For i = 0 To data.Length - 1
            data(i) = Convert.ToInt16(hex.Substring(i * 2, 2), 16)
        Next
        Return data
    End Function

    Private Sub ReportProcess()
        While True
            Try
                Dim list = New List(Of Byte)
                list.AddRange(DecodeHexData(_apid))
                list.AddRange({0, 0, 0, 0, 0, 0, 0, 0})
                list.AddRange(Text.Encoding.ASCII.GetBytes(_version))
                Dim result = list.ToArray()
                _FifoToServer.Add(result)
                Console.ForegroundColor = ConsoleColor.Gray
                Console.WriteLine("AP report: " + _version)
            Catch ex As Exception
            End Try
            Threading.Thread.Sleep(60000)
        End While
    End Sub

End Class
