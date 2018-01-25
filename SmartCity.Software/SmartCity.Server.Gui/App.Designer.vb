<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class App
    Inherits Bwl.Framework.FormAppBase

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.bUpdateAccessPointsList = New System.Windows.Forms.Button()
        Me.listAccessPoints = New System.Windows.Forms.ListBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.listBootloaderTasks = New System.Windows.Forms.ListBox()
        Me.pAddBootloaderTask = New System.Windows.Forms.Button()
        Me.pHexFileSearch = New System.Windows.Forms.Button()
        Me.textHexPath = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.textDeviceIdForBootloader = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.listDevicesId = New System.Windows.Forms.ListBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.timer = New System.Windows.Forms.Timer(Me.components)
        Me.bUpdateDevicesList = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'logWriter
        '
        Me.logWriter.Location = New System.Drawing.Point(2, 318)
        Me.logWriter.Size = New System.Drawing.Size(670, 127)
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(2, 27)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(670, 288)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(662, 262)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TCP Clients"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.bUpdateAccessPointsList)
        Me.GroupBox1.Controls.Add(Me.listAccessPoints)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(225, 250)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Access Points"
        '
        'bUpdateAccessPointsList
        '
        Me.bUpdateAccessPointsList.Location = New System.Drawing.Point(7, 221)
        Me.bUpdateAccessPointsList.Name = "bUpdateAccessPointsList"
        Me.bUpdateAccessPointsList.Size = New System.Drawing.Size(212, 23)
        Me.bUpdateAccessPointsList.TabIndex = 1
        Me.bUpdateAccessPointsList.Text = "Update"
        Me.bUpdateAccessPointsList.UseVisualStyleBackColor = True
        '
        'listAccessPoints
        '
        Me.listAccessPoints.FormattingEnabled = True
        Me.listAccessPoints.Location = New System.Drawing.Point(6, 19)
        Me.listAccessPoints.Name = "listAccessPoints"
        Me.listAccessPoints.Size = New System.Drawing.Size(213, 199)
        Me.listAccessPoints.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox3)
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(662, 262)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Devices"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.listBootloaderTasks)
        Me.GroupBox3.Controls.Add(Me.pAddBootloaderTask)
        Me.GroupBox3.Controls.Add(Me.pHexFileSearch)
        Me.GroupBox3.Controls.Add(Me.textHexPath)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.textDeviceIdForBootloader)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Location = New System.Drawing.Point(231, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(423, 239)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Bootloader"
        '
        'listBootloaderTasks
        '
        Me.listBootloaderTasks.FormattingEnabled = True
        Me.listBootloaderTasks.Location = New System.Drawing.Point(6, 49)
        Me.listBootloaderTasks.Name = "listBootloaderTasks"
        Me.listBootloaderTasks.Size = New System.Drawing.Size(407, 173)
        Me.listBootloaderTasks.TabIndex = 6
        '
        'pAddBootloaderTask
        '
        Me.pAddBootloaderTask.Location = New System.Drawing.Point(365, 15)
        Me.pAddBootloaderTask.Name = "pAddBootloaderTask"
        Me.pAddBootloaderTask.Size = New System.Drawing.Size(48, 23)
        Me.pAddBootloaderTask.TabIndex = 5
        Me.pAddBootloaderTask.Text = "Add"
        Me.pAddBootloaderTask.UseVisualStyleBackColor = True
        '
        'pHexFileSearch
        '
        Me.pHexFileSearch.Location = New System.Drawing.Point(311, 15)
        Me.pHexFileSearch.Name = "pHexFileSearch"
        Me.pHexFileSearch.Size = New System.Drawing.Size(48, 23)
        Me.pHexFileSearch.TabIndex = 4
        Me.pHexFileSearch.Text = "File"
        Me.pHexFileSearch.UseVisualStyleBackColor = True
        '
        'textHexPath
        '
        Me.textHexPath.Location = New System.Drawing.Point(205, 17)
        Me.textHexPath.Name = "textHexPath"
        Me.textHexPath.Size = New System.Drawing.Size(100, 20)
        Me.textHexPath.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(170, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "HEX"
        '
        'textDeviceIdForBootloader
        '
        Me.textDeviceIdForBootloader.Location = New System.Drawing.Point(64, 17)
        Me.textDeviceIdForBootloader.Name = "textDeviceIdForBootloader"
        Me.textDeviceIdForBootloader.Size = New System.Drawing.Size(100, 20)
        Me.textDeviceIdForBootloader.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Device #"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.bUpdateDevicesList)
        Me.GroupBox2.Controls.Add(Me.listDevicesId)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(219, 239)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Devices"
        '
        'listDevicesId
        '
        Me.listDevicesId.FormattingEnabled = True
        Me.listDevicesId.Location = New System.Drawing.Point(6, 23)
        Me.listDevicesId.Name = "listDevicesId"
        Me.listDevicesId.Size = New System.Drawing.Size(207, 173)
        Me.listDevicesId.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(662, 262)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Data base"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'timer
        '
        Me.timer.Enabled = True
        Me.timer.Interval = 2000
        '
        'bUpdateDevicesList
        '
        Me.bUpdateDevicesList.Location = New System.Drawing.Point(7, 202)
        Me.bUpdateDevicesList.Name = "bUpdateDevicesList"
        Me.bUpdateDevicesList.Size = New System.Drawing.Size(206, 23)
        Me.bUpdateDevicesList.TabIndex = 1
        Me.bUpdateDevicesList.Text = "Update"
        Me.bUpdateDevicesList.UseVisualStyleBackColor = True
        '
        'App
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(672, 445)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "App"
        Me.Text = "Smart City Server App"
        Me.Controls.SetChildIndex(Me.logWriter, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents bUpdateAccessPointsList As Button
    Friend WithEvents listAccessPoints As ListBox
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents timer As Timer
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents listBootloaderTasks As ListBox
    Friend WithEvents pAddBootloaderTask As Button
    Friend WithEvents pHexFileSearch As Button
    Friend WithEvents textHexPath As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents textDeviceIdForBootloader As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents listDevicesId As ListBox
    Friend WithEvents bUpdateDevicesList As Button
End Class
