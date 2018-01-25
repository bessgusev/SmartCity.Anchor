<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class App
    Inherits Bwl.Framework.FormAppBase

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.checkSignalDetected = New System.Windows.Forms.CheckBox()
        Me.checkSynchronized = New System.Windows.Forms.CheckBox()
        Me.checkRXonGoing = New System.Windows.Forms.CheckBox()
        Me.checkHeaderValid = New System.Windows.Forms.CheckBox()
        Me.checkModemClear = New System.Windows.Forms.CheckBox()
        Me.checkRxCodingRate0 = New System.Windows.Forms.CheckBox()
        Me.checkRxCodingRate1 = New System.Windows.Forms.CheckBox()
        Me.checkRxCodingRate2 = New System.Windows.Forms.CheckBox()
        Me.checkCadDetected = New System.Windows.Forms.CheckBox()
        Me.checkFhssChangeChannel = New System.Windows.Forms.CheckBox()
        Me.checkCadDone = New System.Windows.Forms.CheckBox()
        Me.checkTxDone = New System.Windows.Forms.CheckBox()
        Me.checkValidHeader = New System.Windows.Forms.CheckBox()
        Me.checkPayloadCrcError = New System.Windows.Forms.CheckBox()
        Me.checkRxDone = New System.Windows.Forms.CheckBox()
        Me.checkRxTimeout = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.textRegister = New System.Windows.Forms.TextBox()
        Me.textRegValue = New System.Windows.Forms.TextBox()
        Me.bReadReg = New System.Windows.Forms.Button()
        Me.textSendToLora = New System.Windows.Forms.TextBox()
        Me.bWriteReg = New System.Windows.Forms.Button()
        Me.SmartPoll = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.bSendLora = New System.Windows.Forms.Button()
        Me.ListLoraReceived = New System.Windows.Forms.ListBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.timer = New System.Windows.Forms.Timer(Me.components)
        Me.SmartPoll.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'logWriter
        '
        Me.logWriter.Location = New System.Drawing.Point(2, 423)
        Me.logWriter.Size = New System.Drawing.Size(690, 132)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(144, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 13)
        Me.Label4.TabIndex = 45
        Me.Label4.Text = "RegModemStat"
        '
        'checkSignalDetected
        '
        Me.checkSignalDetected.AutoSize = True
        Me.checkSignalDetected.Location = New System.Drawing.Point(147, 199)
        Me.checkSignalDetected.Name = "checkSignalDetected"
        Me.checkSignalDetected.Size = New System.Drawing.Size(100, 17)
        Me.checkSignalDetected.TabIndex = 44
        Me.checkSignalDetected.Text = "Signal detected"
        Me.checkSignalDetected.UseVisualStyleBackColor = True
        '
        'checkSynchronized
        '
        Me.checkSynchronized.AutoSize = True
        Me.checkSynchronized.Location = New System.Drawing.Point(147, 175)
        Me.checkSynchronized.Name = "checkSynchronized"
        Me.checkSynchronized.Size = New System.Drawing.Size(120, 17)
        Me.checkSynchronized.TabIndex = 43
        Me.checkSynchronized.Text = "Signal synchronized"
        Me.checkSynchronized.UseVisualStyleBackColor = True
        '
        'checkRXonGoing
        '
        Me.checkRXonGoing.AutoSize = True
        Me.checkRXonGoing.Location = New System.Drawing.Point(147, 151)
        Me.checkRXonGoing.Name = "checkRXonGoing"
        Me.checkRXonGoing.Size = New System.Drawing.Size(85, 17)
        Me.checkRXonGoing.TabIndex = 42
        Me.checkRXonGoing.Text = "RX on-going"
        Me.checkRXonGoing.UseVisualStyleBackColor = True
        '
        'checkHeaderValid
        '
        Me.checkHeaderValid.AutoSize = True
        Me.checkHeaderValid.Location = New System.Drawing.Point(147, 127)
        Me.checkHeaderValid.Name = "checkHeaderValid"
        Me.checkHeaderValid.Size = New System.Drawing.Size(106, 17)
        Me.checkHeaderValid.TabIndex = 41
        Me.checkHeaderValid.Text = "Header info valid"
        Me.checkHeaderValid.UseVisualStyleBackColor = True
        '
        'checkModemClear
        '
        Me.checkModemClear.AutoSize = True
        Me.checkModemClear.Location = New System.Drawing.Point(147, 103)
        Me.checkModemClear.Name = "checkModemClear"
        Me.checkModemClear.Size = New System.Drawing.Size(87, 17)
        Me.checkModemClear.TabIndex = 40
        Me.checkModemClear.Text = "Modem clear"
        Me.checkModemClear.UseVisualStyleBackColor = True
        '
        'checkRxCodingRate0
        '
        Me.checkRxCodingRate0.AutoSize = True
        Me.checkRxCodingRate0.Location = New System.Drawing.Point(147, 79)
        Me.checkRxCodingRate0.Name = "checkRxCodingRate0"
        Me.checkRxCodingRate0.Size = New System.Drawing.Size(101, 17)
        Me.checkRxCodingRate0.TabIndex = 39
        Me.checkRxCodingRate0.Text = "RxCodingRate0"
        Me.checkRxCodingRate0.UseVisualStyleBackColor = True
        '
        'checkRxCodingRate1
        '
        Me.checkRxCodingRate1.AutoSize = True
        Me.checkRxCodingRate1.Location = New System.Drawing.Point(147, 55)
        Me.checkRxCodingRate1.Name = "checkRxCodingRate1"
        Me.checkRxCodingRate1.Size = New System.Drawing.Size(101, 17)
        Me.checkRxCodingRate1.TabIndex = 38
        Me.checkRxCodingRate1.Text = "RxCodingRate1"
        Me.checkRxCodingRate1.UseVisualStyleBackColor = True
        '
        'checkRxCodingRate2
        '
        Me.checkRxCodingRate2.AutoSize = True
        Me.checkRxCodingRate2.Location = New System.Drawing.Point(147, 32)
        Me.checkRxCodingRate2.Name = "checkRxCodingRate2"
        Me.checkRxCodingRate2.Size = New System.Drawing.Size(101, 17)
        Me.checkRxCodingRate2.TabIndex = 37
        Me.checkRxCodingRate2.Text = "RxCodingRate2"
        Me.checkRxCodingRate2.UseVisualStyleBackColor = True
        '
        'checkCadDetected
        '
        Me.checkCadDetected.AutoSize = True
        Me.checkCadDetected.Location = New System.Drawing.Point(9, 199)
        Me.checkCadDetected.Name = "checkCadDetected"
        Me.checkCadDetected.Size = New System.Drawing.Size(89, 17)
        Me.checkCadDetected.TabIndex = 36
        Me.checkCadDetected.Text = "CadDetected"
        Me.checkCadDetected.UseVisualStyleBackColor = True
        '
        'checkFhssChangeChannel
        '
        Me.checkFhssChangeChannel.AutoSize = True
        Me.checkFhssChangeChannel.Location = New System.Drawing.Point(9, 175)
        Me.checkFhssChangeChannel.Name = "checkFhssChangeChannel"
        Me.checkFhssChangeChannel.Size = New System.Drawing.Size(124, 17)
        Me.checkFhssChangeChannel.TabIndex = 35
        Me.checkFhssChangeChannel.Text = "FhssChangeChannel"
        Me.checkFhssChangeChannel.UseVisualStyleBackColor = True
        '
        'checkCadDone
        '
        Me.checkCadDone.AutoSize = True
        Me.checkCadDone.Location = New System.Drawing.Point(9, 151)
        Me.checkCadDone.Name = "checkCadDone"
        Me.checkCadDone.Size = New System.Drawing.Size(71, 17)
        Me.checkCadDone.TabIndex = 34
        Me.checkCadDone.Text = "CadDone"
        Me.checkCadDone.UseVisualStyleBackColor = True
        '
        'checkTxDone
        '
        Me.checkTxDone.AutoSize = True
        Me.checkTxDone.Location = New System.Drawing.Point(9, 127)
        Me.checkTxDone.Name = "checkTxDone"
        Me.checkTxDone.Size = New System.Drawing.Size(64, 17)
        Me.checkTxDone.TabIndex = 33
        Me.checkTxDone.Text = "TxDone"
        Me.checkTxDone.UseVisualStyleBackColor = True
        '
        'checkValidHeader
        '
        Me.checkValidHeader.AutoSize = True
        Me.checkValidHeader.Location = New System.Drawing.Point(9, 103)
        Me.checkValidHeader.Name = "checkValidHeader"
        Me.checkValidHeader.Size = New System.Drawing.Size(84, 17)
        Me.checkValidHeader.TabIndex = 32
        Me.checkValidHeader.Text = "ValidHeader"
        Me.checkValidHeader.UseVisualStyleBackColor = True
        '
        'checkPayloadCrcError
        '
        Me.checkPayloadCrcError.AutoSize = True
        Me.checkPayloadCrcError.Location = New System.Drawing.Point(9, 79)
        Me.checkPayloadCrcError.Name = "checkPayloadCrcError"
        Me.checkPayloadCrcError.Size = New System.Drawing.Size(102, 17)
        Me.checkPayloadCrcError.TabIndex = 31
        Me.checkPayloadCrcError.Text = "PayloadCrcError"
        Me.checkPayloadCrcError.UseVisualStyleBackColor = True
        '
        'checkRxDone
        '
        Me.checkRxDone.AutoSize = True
        Me.checkRxDone.Location = New System.Drawing.Point(9, 55)
        Me.checkRxDone.Name = "checkRxDone"
        Me.checkRxDone.Size = New System.Drawing.Size(65, 17)
        Me.checkRxDone.TabIndex = 30
        Me.checkRxDone.Text = "RxDone"
        Me.checkRxDone.UseVisualStyleBackColor = True
        '
        'checkRxTimeout
        '
        Me.checkRxTimeout.AutoSize = True
        Me.checkRxTimeout.Location = New System.Drawing.Point(9, 32)
        Me.checkRxTimeout.Name = "checkRxTimeout"
        Me.checkRxTimeout.Size = New System.Drawing.Size(77, 17)
        Me.checkRxTimeout.TabIndex = 29
        Me.checkRxTimeout.Text = "RxTimeout"
        Me.checkRxTimeout.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 13)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "RegIrqFlags"
        '
        'textRegister
        '
        Me.textRegister.Location = New System.Drawing.Point(9, 20)
        Me.textRegister.Name = "textRegister"
        Me.textRegister.Size = New System.Drawing.Size(124, 20)
        Me.textRegister.TabIndex = 46
        '
        'textRegValue
        '
        Me.textRegValue.Location = New System.Drawing.Point(147, 19)
        Me.textRegValue.Name = "textRegValue"
        Me.textRegValue.Size = New System.Drawing.Size(120, 20)
        Me.textRegValue.TabIndex = 47
        '
        'bReadReg
        '
        Me.bReadReg.Location = New System.Drawing.Point(9, 47)
        Me.bReadReg.Name = "bReadReg"
        Me.bReadReg.Size = New System.Drawing.Size(124, 23)
        Me.bReadReg.TabIndex = 48
        Me.bReadReg.Text = "Read"
        Me.bReadReg.UseVisualStyleBackColor = True
        '
        'textSendToLora
        '
        Me.textSendToLora.Location = New System.Drawing.Point(7, 260)
        Me.textSendToLora.Name = "textSendToLora"
        Me.textSendToLora.Size = New System.Drawing.Size(370, 20)
        Me.textSendToLora.TabIndex = 50
        '
        'bWriteReg
        '
        Me.bWriteReg.Location = New System.Drawing.Point(147, 47)
        Me.bWriteReg.Name = "bWriteReg"
        Me.bWriteReg.Size = New System.Drawing.Size(120, 23)
        Me.bWriteReg.TabIndex = 49
        Me.bWriteReg.Text = "Write"
        Me.bWriteReg.UseVisualStyleBackColor = True
        '
        'SmartPoll
        '
        Me.SmartPoll.Controls.Add(Me.TabPage1)
        Me.SmartPoll.Controls.Add(Me.TabPage2)
        Me.SmartPoll.Location = New System.Drawing.Point(2, 27)
        Me.SmartPoll.Name = "SmartPoll"
        Me.SmartPoll.SelectedIndex = 0
        Me.SmartPoll.Size = New System.Drawing.Size(690, 389)
        Me.SmartPoll.TabIndex = 51
        '
        'TabPage1
        '
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(682, 363)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Board"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox3)
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(682, 363)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "LoRa"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.bSendLora)
        Me.GroupBox3.Controls.Add(Me.ListLoraReceived)
        Me.GroupBox3.Controls.Add(Me.textSendToLora)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 15)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(383, 335)
        Me.GroupBox3.TabIndex = 51
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Communication "
        '
        'bSendLora
        '
        Me.bSendLora.Location = New System.Drawing.Point(7, 287)
        Me.bSendLora.Name = "bSendLora"
        Me.bSendLora.Size = New System.Drawing.Size(370, 23)
        Me.bSendLora.TabIndex = 51
        Me.bSendLora.Text = "SEND"
        Me.bSendLora.UseVisualStyleBackColor = True
        '
        'ListLoraReceived
        '
        Me.ListLoraReceived.FormattingEnabled = True
        Me.ListLoraReceived.Location = New System.Drawing.Point(7, 20)
        Me.ListLoraReceived.Name = "ListLoraReceived"
        Me.ListLoraReceived.Size = New System.Drawing.Size(370, 225)
        Me.ListLoraReceived.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.bReadReg)
        Me.GroupBox2.Controls.Add(Me.textRegister)
        Me.GroupBox2.Controls.Add(Me.textRegValue)
        Me.GroupBox2.Controls.Add(Me.bWriteReg)
        Me.GroupBox2.Location = New System.Drawing.Point(395, 255)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(275, 95)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "RFM Registers"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.checkRxTimeout)
        Me.GroupBox1.Controls.Add(Me.checkRxDone)
        Me.GroupBox1.Controls.Add(Me.checkPayloadCrcError)
        Me.GroupBox1.Controls.Add(Me.checkValidHeader)
        Me.GroupBox1.Controls.Add(Me.checkTxDone)
        Me.GroupBox1.Controls.Add(Me.checkCadDone)
        Me.GroupBox1.Controls.Add(Me.checkFhssChangeChannel)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.checkCadDetected)
        Me.GroupBox1.Controls.Add(Me.checkSignalDetected)
        Me.GroupBox1.Controls.Add(Me.checkRxCodingRate2)
        Me.GroupBox1.Controls.Add(Me.checkSynchronized)
        Me.GroupBox1.Controls.Add(Me.checkRxCodingRate1)
        Me.GroupBox1.Controls.Add(Me.checkRXonGoing)
        Me.GroupBox1.Controls.Add(Me.checkRxCodingRate0)
        Me.GroupBox1.Controls.Add(Me.checkHeaderValid)
        Me.GroupBox1.Controls.Add(Me.checkModemClear)
        Me.GroupBox1.Location = New System.Drawing.Point(395, 15)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(275, 233)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "RFM95 Flags"
        '
        'timer
        '
        Me.timer.Enabled = True
        Me.timer.Interval = 2000
        '
        'App
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(693, 556)
        Me.Controls.Add(Me.SmartPoll)
        Me.Name = "App"
        Me.Text = "SmartCity.AccessPoint"
        Me.Controls.SetChildIndex(Me.logWriter, 0)
        Me.Controls.SetChildIndex(Me.SmartPoll, 0)
        Me.SmartPoll.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label4 As Label
    Friend WithEvents checkSignalDetected As CheckBox
    Friend WithEvents checkSynchronized As CheckBox
    Friend WithEvents checkRXonGoing As CheckBox
    Friend WithEvents checkHeaderValid As CheckBox
    Friend WithEvents checkModemClear As CheckBox
    Friend WithEvents checkRxCodingRate0 As CheckBox
    Friend WithEvents checkRxCodingRate1 As CheckBox
    Friend WithEvents checkRxCodingRate2 As CheckBox
    Friend WithEvents checkCadDetected As CheckBox
    Friend WithEvents checkFhssChangeChannel As CheckBox
    Friend WithEvents checkCadDone As CheckBox
    Friend WithEvents checkTxDone As CheckBox
    Friend WithEvents checkValidHeader As CheckBox
    Friend WithEvents checkPayloadCrcError As CheckBox
    Friend WithEvents checkRxDone As CheckBox
    Friend WithEvents checkRxTimeout As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents textRegister As TextBox
    Friend WithEvents textRegValue As TextBox
    Friend WithEvents bReadReg As Button
    Friend WithEvents textSendToLora As TextBox
    Friend WithEvents bWriteReg As Button
    Friend WithEvents SmartPoll As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents bSendLora As Button
    Friend WithEvents ListLoraReceived As ListBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents timer As Timer
End Class
