Imports Bwl.Framework
Imports SmartCity.Server

Public Class App
    Inherits FormAppBase
    Private _sqlBase As New StringSetting(_storage, "SQL Base name", "database")
    Private _sqlUser As New StringSetting(_storage, "SQL User", "user")
    Private _sqlPassword As New StringSetting(_storage, "SQL Password", "password")
    Private _tcpPort As New IntegerSetting(_storage, "TCP port", 8520)

    Private _app As ApplicationMonitor

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _app = New ApplicationMonitor(_logger)
        _app.SetSqlSetting(_sqlBase.Value, _sqlUser.Value, _sqlPassword.Value)
        _app.SetServerSetting(_tcpPort.Value)
        _app.Run()
    End Sub

    Private Sub bUpdateAccessPointsList_Click(sender As Object, e As EventArgs) Handles bUpdateAccessPointsList.Click
        listAccessPoints.Items.Clear()
        Dim list = _app.GetAccessPoints
        For Each id In list
            listAccessPoints.Items.Add(id)
        Next
    End Sub

    Private Sub timer_Tick(sender As Object, e As EventArgs) Handles timer.Tick
        listAccessPoints.Items.Clear()
        Dim list = _app.GetAccessPoints
        For Each id In list
            listAccessPoints.Items.Add(id)
        Next

        Dim bootStates = _app.GetBootloaderState
        listBootloaderTasks.Items.Clear()
        If bootStates.Count > 0 Then
            For Each task In bootStates
                listBootloaderTasks.Items.Add(task)
            Next
        Else
            listBootloaderTasks.Items.Add("Tasks not found")
        End If
    End Sub

    Private Sub pAddBootloaderTask_Click(sender As Object, e As EventArgs) Handles pAddBootloaderTask.Click
        _app.SetBootloaderTask(textDeviceIdForBootloader.Text, textHexPath.Text)
    End Sub

    Private Sub pHexFileSearch_Click(sender As Object, e As EventArgs) Handles pHexFileSearch.Click
        Dim fileDialog As New OpenFileDialog()
        fileDialog.Filter = "All files (*.*)|*.*"
        fileDialog.FilterIndex = 2
        fileDialog.RestoreDirectory = True
        If fileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            textHexPath.Text = fileDialog.FileName
        End If
    End Sub

    Private Sub bUpdateDevicesList_Click(sender As Object, e As EventArgs) Handles bUpdateDevicesList.Click
        Dim list = _app.GetDevicesList()
        listDevicesId.Items.Clear()
        listDevicesId.Items.AddRange(list.ToArray)
    End Sub

    Private Sub listDevicesId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles listDevicesId.SelectedIndexChanged
        If listDevicesId.SelectedItem IsNot Nothing Then
            textDeviceIdForBootloader.Text = listDevicesId.SelectedItem.ToString
        End If
    End Sub


End Class
