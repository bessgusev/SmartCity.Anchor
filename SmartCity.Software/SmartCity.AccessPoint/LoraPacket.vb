Public Class LoraPacket
    Public Property RxOnGoingRSSI As Integer = 0
    Public Property PacketRSSI As Integer = 0
    Public Property IrqFlags As LoraIrqFlags
    Public Property ModemStatus As LoraModemStatus
    Public Property Data As Byte()
End Class

Public Class LoraIrqFlags
    Public Property RxTimeout As Boolean = False
    Public Property RxDone As Boolean = False
    Public Property PayloadCrcError As Boolean = False
    Public Property ValidHeader As Boolean = False
    Public Property TxDone As Boolean = False
    Public Property CadDone As Boolean = False
    Public Property FhssChangeChannel As Boolean = False
    Public Property CadDetected As Boolean = False
End Class

Public Class LoraModemStatus
    Public Property RxCodingRateTwo As Boolean = False
    Public Property RxCodingRateOne As Boolean = False
    Public Property RxCodingRateZero As Boolean = False
    Public Property ModemClear As Boolean = False
    Public Property HeaderInfoValid As Boolean = False
    Public Property RxOnGoin As Boolean = False
    Public Property SignalSynchronized As Boolean = False
    Public Property SignalDetected As Boolean = False
End Class