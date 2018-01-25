Public Interface IDevice
    Property DeviceId As String
    Sub AppendData(packet As DevicePacket, db As DataBase)
    Function IsSupported(id As String) As Boolean
End Interface
