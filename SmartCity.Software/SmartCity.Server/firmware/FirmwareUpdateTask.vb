Imports Bwl.Framework

Public Class FirmwareUpdateTask
    Private _accessPoint As AccessPointProcessor = Nothing
    Public Property DeviceId As String = ""
    Public Property Status As String = "Wait queue"
    Private _helper As BootloadrAuxiliaryProccess
    Private _task As BootloaderTasksMonitor.BootloaderTask
    Private _logger As Logger
    Private _complete As Boolean = False
    Private _crc As New FrimwareCRC32(&H8004000, &H50000)

    Sub New(ap As AccessPointProcessor, task As BootloaderTasksMonitor.BootloaderTask, logger As Logger)
        _accessPoint = ap
        _task = task
        _helper = New BootloadrAuxiliaryProccess(task.DeviceId)
        _logger = logger
    End Sub

    ''' <summary>
    ''' Проверка завершения работы загрузчика
    ''' </summary>
    ''' <returns></returns>
    Public Function isComplete() As Boolean
        Return (_complete And _helper.MainAppIsRunned)
    End Function
    ''' <summary>
    ''' Вводим устройство в режим загрузчика, отправляя ключевую пследоавтельность раз в секунду
    ''' </summary>
    Private Sub EnterBootMode()
        Status = "Waiting for bootloader mode"
        While (_helper.IsBootMode = False)
            Dim data(2) As Byte
            data(0) = 38
            data(1) = 1
            data(2) = 245
            _accessPoint.Send(DeviceId, data)
            Threading.Thread.Sleep(1000)
        End While
    End Sub

    Public Sub Start()
        Dim th = New Threading.Thread(AddressOf Run)
        th.IsBackground = True
        th.Start()
    End Sub

    Private Sub Run()
        Me.DeviceId = _task.DeviceId
        AddHandler _accessPoint.onPacketReceived, AddressOf _helper.PacketHandler
        Try
            EnterBootMode()
            EraseFlash()
            Dim flashData = New Firmware(_task.HexPath)
            flashData.PrepareMemorySectors()
            Dim size = flashData.GetFirmwareSize
            For i As Integer = 0 To size - 1
                Dim sector = flashData.GetNext
                SendHexToDevice(sector)
                _crc.AppendFirmwareData(sector.FlashAddress, sector.FlashData.ToArray)
                Status = "Flash download: " + (Math.Round(i * 100 / size, 1).ToString + "%")
                _logger.AddDebug(Status)
            Next
            StartMainApplication()
        Catch ex As Exception
            Status = ex.Message
            _logger.AddError(ex.Message)
        End Try
        Dim crcResult = _crc.GetCRC32
        If (crcResult = _helper.GetFlashCRC32) Then
            Status = "Complete. CRC OK"
        Else
            Status = "Error CRC"
        End If
        _logger.AddMessage(Status)
        RemoveHandler _accessPoint.onPacketReceived, AddressOf _helper.PacketHandler
        _complete = True
    End Sub

    Private Sub EraseFlash()
        Status = "Flash erasing"
        Do
            Dim data(2) As Byte
            data(0) = &H55
            data(1) = &HAA
            data(2) = &H55
            _accessPoint.Send(DeviceId, data)
        Loop Until _helper.isErasedFlash(3000)
    End Sub

    Private Sub SendHexToDevice(sector As Firmware.MemorySector)
        Dim list As List(Of Byte) = New List(Of Byte)
        list.Add(76)
        list.Add(74)
        list.Add(98)
        list.AddRange(sector.PacketSerialization)
        Dim packet = list.ToArray
        Do
            _accessPoint.Send(DeviceId, packet)
        Loop Until _helper.FlashWriteIsComplete(sector.FlashAddress, 2000)
        Threading.Thread.Sleep(500)
    End Sub

    ''' <summary>
    ''' Запускаем основной код
    ''' </summary>
    Private Sub StartMainApplication()
        Dim data(6) As Byte
        data(0) = 1
        data(1) = 241
        data(2) = 38
        Dim crc = BitConverter.GetBytes(_crc.GetCRC32)
        Array.Copy(crc, 0, data, 3, crc.Length)
        _accessPoint.Send(DeviceId, data)
        While _helper.MainAppIsRunned <> True
            Threading.Thread.Sleep(1000)
            _accessPoint.Send(DeviceId, data)
        End While
    End Sub
End Class
