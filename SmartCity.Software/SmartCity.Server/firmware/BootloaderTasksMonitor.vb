Imports System.Windows.Forms
Imports Bwl.Framework

Public Class BootloaderTasksMonitor

    Private _db As DataBase = Nothing
    Private _tcpServer As TcpServer = Nothing
    Private _deviceManager As DeviceManager = Nothing
    Private _tasks As List(Of BootloaderTask) = New List(Of BootloaderTask)
    Private _logger As Logger

    Sub New(db As DataBase, tcpServer As TcpServer, deviceManager As DeviceManager, logger As Logger)
        _db = db
        _tcpServer = tcpServer
        _deviceManager = deviceManager
        _logger = logger
    End Sub

    ''' <summary>
    ''' Добавить задачу обновления в очередь
    ''' </summary>
    ''' <param name="deviceId">Целевое устройство</param>
    ''' <param name="hexPath">Путь до HEX файла</param>
    Public Sub AddTask(deviceId As String, hexPath As String)
        Try
            Dim task As New BootloaderTask
            task.DeviceId = deviceId
            task.HexPath = hexPath
            _tasks.Add(task)
            _db.Execute("INSERT INTO firmware_tasks (device_id, hex_file, hash, state, time) VALUES('" + deviceId + "', '" + IO.Path.GetFileName(hexPath) + "', '" + task.TaskHashId + "', 'In queue', NOW());")
        Catch ex As Exception
            _logger.AddError(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Возвращает ID точки доступа с максимальным уровнем сигнала
    ''' </summary>
    ''' <param name="deviceId">Идентификатор устройства</param>
    ''' <returns>Идентификатор точки доступа</returns>
    Private Function GetAccessPoint(deviceId As String) As String
        Dim sql = "select base_station from traffic_monitor where device_id='" + deviceId + "' ORDER BY rssi_packet ASC LIMIT 1;"
        Dim sqlResult = _db.GetData(sql)
        If sqlResult.Count > 0 Then
            Return _db.GetData(sql).ElementAt(0).Values.ElementAt(0)
        End If
        Throw New Exception(deviceId + " not found in database.")
    End Function

    Public Sub Run()
        Dim th = New Threading.Thread(AddressOf WorkProcess)
        th.IsBackground = True
        th.Start()
    End Sub

    Private Sub WorkProcess()
        While True
            If _tasks.Count > 0 Then
                Try
                    Dim task = _tasks.First()
                    Dim accessPointId = GetAccessPoint(task.DeviceId)
                    Dim accessPointProcess = _tcpServer.EjectProcess(accessPointId)
                    Dim bootloder = New FirmwareUpdateTask(accessPointProcess, task, _logger)
                    _db.Execute("UPDATE firmware_tasks SET state = 'In process', base_station='" + accessPointId + "' WHERE device_id='" + task.DeviceId + "' and hash='" + task.TaskHashId + "'")
                    bootloder.Start()
                    Dim watch As Stopwatch = Stopwatch.StartNew()
                    watch.Start()
                    While bootloder.isComplete <> True
                        Threading.Thread.Sleep(3000)
                        _logger.AddDebug(bootloder.Status)
                        _db.Execute("UPDATE firmware_tasks SET state = '" + bootloder.Status + "', elapsed_time=" + (Math.Round(watch.ElapsedMilliseconds / 1000)).ToString + " WHERE device_id='" + task.DeviceId + "' and hash='" + task.TaskHashId + "';")
                    End While
                    watch.Stop()
                    _db.Execute("UPDATE firmware_tasks SET state = '" + bootloder.Status + "', elapsed_time=" + TimeString + " WHERE device_id='" + task.DeviceId + "' and hash='" + task.TaskHashId + "'")
                    Threading.Thread.Sleep(1000)
                    _tcpServer.InsertProcess(accessPointProcess)
                    _tasks.Remove(task)
                Catch ex As Exception
                    _logger.AddError(ex.Message)
                    Threading.Thread.Sleep(1000)
                End Try
            Else
                Threading.Thread.Sleep(1000)
            End If
        End While
    End Sub

    Public Function GetStates() As String()
        Dim list = New List(Of String)
        For Each task In _tasks
            list.Add(task.DeviceId + ": " + IO.Path.GetFileName(task.HexPath).ToString)
        Next
        Return list.ToArray()
    End Function

    Public Class BootloaderTask
        Public Property DeviceId As String = ""
        Public Property DateTime As String = ""
        Public Property HexPath As String = ""
        Public Property TaskHashId As String = Tool.GenerateRandomString(40)
    End Class
End Class



