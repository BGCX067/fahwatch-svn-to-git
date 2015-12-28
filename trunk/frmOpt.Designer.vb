<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpt
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkTray = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.nudAllert = New System.Windows.Forms.NumericUpDown
        Me.Label3 = New System.Windows.Forms.Label
        Me.chkBalloon = New System.Windows.Forms.CheckBox
        Me.nudUpdate = New System.Windows.Forms.NumericUpDown
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdDebug = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        CType(Me.nudAllert, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.cmdDebug)
        Me.GroupBox1.Controls.Add(Me.chkTray)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.nudAllert)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.chkBalloon)
        Me.GroupBox1.Controls.Add(Me.nudUpdate)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(2, -3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(280, 115)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'chkTray
        '
        Me.chkTray.AutoSize = True
        Me.chkTray.Location = New System.Drawing.Point(13, 64)
        Me.chkTray.Name = "chkTray"
        Me.chkTray.Size = New System.Drawing.Size(114, 17)
        Me.chkTray.TabIndex = 7
        Me.chkTray.Text = "Start in system tray"
        Me.chkTray.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(176, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "EUE rate"
        '
        'nudAllert
        '
        Me.nudAllert.Location = New System.Drawing.Point(126, 38)
        Me.nudAllert.Name = "nudAllert"
        Me.nudAllert.Size = New System.Drawing.Size(47, 20)
        Me.nudAllert.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(113, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Show main window on"
        '
        'chkBalloon
        '
        Me.chkBalloon.AutoSize = True
        Me.chkBalloon.Location = New System.Drawing.Point(174, 14)
        Me.chkBalloon.Name = "chkBalloon"
        Me.chkBalloon.Size = New System.Drawing.Size(101, 17)
        Me.chkBalloon.TabIndex = 3
        Me.chkBalloon.Text = "Show balloontip"
        Me.chkBalloon.UseVisualStyleBackColor = True
        '
        'nudUpdate
        '
        Me.nudUpdate.Location = New System.Drawing.Point(85, 12)
        Me.nudUpdate.Maximum = New Decimal(New Integer() {90, 0, 0, 0})
        Me.nudUpdate.Minimum = New Decimal(New Integer() {15, 0, 0, 0})
        Me.nudUpdate.Name = "nudUpdate"
        Me.nudUpdate.Size = New System.Drawing.Size(37, 20)
        Me.nudUpdate.TabIndex = 2
        Me.nudUpdate.Value = New Decimal(New Integer() {15, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(128, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "minutes"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Update each"
        '
        'cmdDebug
        '
        Me.cmdDebug.Location = New System.Drawing.Point(200, 60)
        Me.cmdDebug.Name = "cmdDebug"
        Me.cmdDebug.Size = New System.Drawing.Size(75, 23)
        Me.cmdDebug.TabIndex = 1
        Me.cmdDebug.Text = "Debug"
        Me.cmdDebug.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(145, 85)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(129, 23)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Clear application data"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(10, 85)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(129, 23)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "Project browser"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'frmOpt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(286, 114)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmOpt"
        Me.Text = "Options"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.nudAllert, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents nudAllert As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkBalloon As System.Windows.Forms.CheckBox
    Friend WithEvents nudUpdate As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkTray As System.Windows.Forms.CheckBox
    Friend WithEvents cmdDebug As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
