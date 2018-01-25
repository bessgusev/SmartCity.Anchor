''' <summary>
''' Вспомогательный класс для работы загрузчика. Проверяет и детектирует выполнение текущих задач прошивки конкретного устройства
''' </summary>
Public Class BootloadrAuxiliaryProccess

    Private _bootMode As Boolean = False
    Private _flashErased As Boolean = False
    Private _currentFlashAddress As UInt32 = 0
    Private _deviceId As String = ""
    Private _mainAppIsRunned As Boolean = False
    Private _flashCRC32 As UInt32 = 0

    Sub New(deviceId As String)
        _deviceId = deviceId
    End Sub

    Public Function GetFlashCRC32() As UInt32
        Return _flashCRC32
    End Function

    Public Function isErasedFlash(waitTimeout As Integer) As Boolean
        Dim watch As Stopwatch = Stopwatch.StartNew()
        watch.Start()
        While (_flashErased = False And watch.ElapsedMilliseconds < waitTimeout)
            Threading.Thread.Sleep(1)
        End While
        If _flashErased Then
            _flashErased = False
            Return True
        End If
        Return False
    End Function

    Public Function IsBootMode() As Boolean
        If _bootMode Then
            _bootMode = False
            Return True
        End If
        Return False
    End Function

    Public Function FlashWriteIsComplete(addr As UInt32, timeout As Integer) As Boolean
        Dim watch As Stopwatch = Stopwatch.StartNew()
        watch.Start()
        While (_currentFlashAddress <> addr And watch.ElapsedMilliseconds < timeout)
            Threading.Thread.Sleep(1)
        End While
        Return _currentFlashAddress = addr
    End Function

    Public Function MainAppIsRunned() As Boolean
        Return _mainAppIsRunned
    End Function

    Public Sub PacketHandler(pack As DevicePacket)
        If pack.DeviceId = _deviceId Then
            If pack.Data(0) = 48 And pack.Data(1) = 85 And pack.Data(2) = 127 Then
                _bootMode = True
            End If
            If pack.Data(0) = 24 And pack.Data(1) = 42 And pack.Data(2) = 64 Then
                _currentFlashAddress = 0
                _currentFlashAddress = _currentFlashAddress * 256 + pack.Data(3)
                _currentFlashAddress = _currentFlashAddress * 256 + pack.Data(4)
                _currentFlashAddress = _currentFlashAddress * 256 + pack.Data(5)
                _currentFlashAddress = _currentFlashAddress * 256 + pack.Data(6)
            End If
            If pack.Data(0) = 87 And pack.Data(1) = 24 And pack.Data(2) = 73 Then
                _flashCRC32 = _flashCRC32 * 256 + pack.Data(3)
                _flashCRC32 = _flashCRC32 * 256 + pack.Data(4)
                _flashCRC32 = _flashCRC32 * 256 + pack.Data(5)
                _flashCRC32 = _flashCRC32 * 256 + pack.Data(6)
                _mainAppIsRunned = True
            End If
            If pack.Data(0) = &HAA And pack.Data(1) = &H55 And pack.Data(2) = &HAA Then
                _flashErased = True
            End If
        End If
    End Sub
End Class
