Imports System.IO.Ports
Imports Bwl.Framework
Imports Bwl.Hardware.SimplSerial

Public Class App
    Private _ip As StringSetting = New StringSetting(_storage, "IP", "localhost")
    Private _port As StringSetting = New StringSetting(_storage, "Port", "8520")
    Private _key As StringSetting = New StringSetting(_storage, "key", "0000000001")
    Private _ap As AccessPoint = Nothing

    Private Sub App_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            _ap = New AccessPoint(_ip.Value, _port.Value, _key.Value)
            _ap.Run()
            AddHandler _ap.Lora.onRxLoraPacket, AddressOf onPacketHandler
            AddHandler _ap.Lora.OnTxComplite, AddressOf onTxHandler
            AddHandler _ap.Lora.onServicePacket, AddressOf onServiceRadioPacketHandler
        Catch ex As Exception
            _logger.AddError(ex.Message)
        End Try
    End Sub

    Public Sub onServiceRadioPacketHandler(packet As LoraPacket)
        Try
            Me.Invoke(Sub()
                          checkRxCodingRate2.CheckState = If(packet.ModemStatus.RxCodingRateTwo, 1, 0)
                          checkRxCodingRate1.CheckState = If(packet.ModemStatus.RxCodingRateOne, 1, 0)
                          checkRxCodingRate0.CheckState = If(packet.ModemStatus.RxCodingRateZero, 1, 0)
                          checkModemClear.CheckState = If(packet.ModemStatus.ModemClear, 1, 0)
                          checkHeaderValid.CheckState = If(packet.ModemStatus.HeaderInfoValid, 1, 0)
                          checkRXonGoing.CheckState = If(packet.ModemStatus.RxOnGoin, 1, 0)
                          checkSynchronized.CheckState = If(packet.ModemStatus.SignalSynchronized, 1, 0)
                          checkSignalDetected.CheckState = If(packet.ModemStatus.SignalDetected, 1, 0)
                          checkRxTimeout.CheckState = If(packet.IrqFlags.RxTimeout, 1, 0)
                          checkRxDone.CheckState = If(packet.IrqFlags.RxDone, 1, 0)
                          checkPayloadCrcError.CheckState = If(packet.IrqFlags.PayloadCrcError, 1, 0)
                          checkValidHeader.CheckState = If(packet.IrqFlags.ValidHeader, 1, 0)
                          checkTxDone.CheckState = If(packet.IrqFlags.TxDone, 1, 0)
                          checkCadDone.CheckState = If(packet.IrqFlags.CadDone, 1, 0)
                          checkFhssChangeChannel.CheckState = If(packet.IrqFlags.FhssChangeChannel, 1, 0)
                          checkCadDetected.CheckState = If(packet.IrqFlags.CadDetected, 1, 0)
                      End Sub)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub onPacketHandler(packet As LoraPacket)
        Try
            Me.Invoke(Sub()
                          checkRxCodingRate2.CheckState = If(packet.ModemStatus.RxCodingRateTwo, 1, 0)
                          checkRxCodingRate1.CheckState = If(packet.ModemStatus.RxCodingRateOne, 1, 0)
                          checkRxCodingRate0.CheckState = If(packet.ModemStatus.RxCodingRateZero, 1, 0)
                          checkModemClear.CheckState = If(packet.ModemStatus.ModemClear, 1, 0)
                          checkHeaderValid.CheckState = If(packet.ModemStatus.HeaderInfoValid, 1, 0)
                          checkRXonGoing.CheckState = If(packet.ModemStatus.RxOnGoin, 1, 0)
                          checkSynchronized.CheckState = If(packet.ModemStatus.SignalSynchronized, 1, 0)
                          checkSignalDetected.CheckState = If(packet.ModemStatus.SignalDetected, 1, 0)
                          checkRxTimeout.CheckState = If(packet.IrqFlags.RxTimeout, 1, 0)
                          checkRxDone.CheckState = If(packet.IrqFlags.RxDone, 1, 0)
                          checkPayloadCrcError.CheckState = If(packet.IrqFlags.PayloadCrcError, 1, 0)
                          checkValidHeader.CheckState = If(packet.IrqFlags.ValidHeader, 1, 0)
                          checkTxDone.CheckState = If(packet.IrqFlags.TxDone, 1, 0)
                          checkCadDone.CheckState = If(packet.IrqFlags.CadDone, 1, 0)
                          checkFhssChangeChannel.CheckState = If(packet.IrqFlags.FhssChangeChannel, 1, 0)
                          checkCadDetected.CheckState = If(packet.IrqFlags.CadDetected, 1, 0)
                          ListLoraReceived.Items.Add(Now.ToString("HH:mm:ss") + "      " + ByteArrayToString(packet.Data))
                          _logger.AddMessage("RxRssi: " + packet.PacketRSSI.ToString + " OnGoRssi:" + packet.RxOnGoingRSSI.ToString)
                      End Sub)
        Catch ex As Exception
            _logger.AddError(ex.Message)
        End Try
    End Sub

    Public Shared Function StringToByteArray(ByVal hex As String) As Byte()
        Return Enumerable.Range(0, hex.Length).Where(Function(x) x Mod 2 = 0).[Select](Function(x) Convert.ToByte(hex.Substring(x, 2), 16)).ToArray()
    End Function

    Public Shared Function ByteArrayToString(ByVal ba As Byte()) As String
        Dim hex As String = BitConverter.ToString(ba)
        Return hex.Replace("-", " ")
    End Function

    Private Sub bReadReg_Click(sender As Object, e As EventArgs) Handles bReadReg.Click
        Try
            Dim addr = Convert.ToByte(textRegister.Text, 16)
            textRegValue.Text = (_ap.Lora.ReadReg(addr)).ToString()
            _logger.AddMessage("Reading ok")
        Catch ex As Exception
            _logger.AddError(ex.Message)
        End Try
    End Sub

    Private Sub onTxHandler()
        _logger.AddMessage("Lora Tx Done!")
    End Sub

    Private Sub bWriteReg_Click(sender As Object, e As EventArgs) Handles bWriteReg.Click
        Try
            Dim addr = Convert.ToByte(textRegister.Text, 16)
            Dim value = Convert.ToByte(textRegValue.Text, 16)
            _ap.Lora.WriteReg(addr, value)
            _logger.AddMessage("Writing ok")
        Catch ex As Exception
            _logger.AddError(ex.Message)
        End Try

    End Sub

    Private Sub bSendLora_Click(sender As Object, e As EventArgs) Handles bSendLora.Click
        Dim bytes = StringToByteArray(textSendToLora.Text.Replace(" ", ""))
        _ap.Lora.RadioWrite(bytes)
    End Sub
End Class
