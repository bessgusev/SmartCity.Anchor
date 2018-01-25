
Module App
    Private _ap As AccessPoint = Nothing
    Sub Main()
        Dim ip = "localhost"
        Dim port = 8520
        Dim id = "0000000001"
        Dim cmd = Command().Split(" ")

        For Each cmdItem In cmd
            Dim parts = cmdItem.Split("*")
            If parts.Length = 2 Then
                Select Case parts(0)
                    Case "server"
                        ip = parts(1)
                    Case "port"
                        port = CInt(parts(1))
                    Case "apid"
                        id = parts(1)
                End Select
            End If
        Next

        _ap = New AccessPoint(ip, port, id)
        _ap.Run()
        While (True)
            Threading.Thread.Sleep(1000)
        End While
    End Sub

End Module
