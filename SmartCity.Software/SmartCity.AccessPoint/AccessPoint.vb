Public Class AccessPoint
    Private _ip As String = ""
    Private _port As Integer = 0
    Private _key As String = ""

    Public Server As ServerConnector = Nothing
    Public Lora As LoraController = Nothing
    Public Queue As MessageQueue = Nothing

    Sub New(ip As String, port As String, key As String)
        _ip = ip
        _port = port
        _key = key
        Server = New ServerConnector(_ip, _port, _key)
        Lora = New LoraController()
        Lora.OpenFirstPort()
        Queue = New MessageQueue(Server, Lora, _key)
    End Sub

    Public Sub Run()
        Server.Run()
        Lora.Start()
        Queue.Start()
    End Sub
End Class
