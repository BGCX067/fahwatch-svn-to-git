<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEUE
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEUE))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.txtEUE = New System.Windows.Forms.TextBox
        Me.txtUpdate = New System.Windows.Forms.TextBox
        Me.chkEUE = New System.Windows.Forms.CheckBox
        Me.cmbProject = New System.Windows.Forms.ComboBox
        Me.cmbClient = New System.Windows.Forms.ComboBox
        Me.tUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.nIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.tMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.UpdateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.tIcon = New System.Windows.Forms.Timer(Me.components)
        Me.tcMain = New System.Windows.Forms.TabControl
        Me.tcEue = New System.Windows.Forms.TabPage
        Me.rtEUE = New System.Windows.Forms.RichTextBox
        Me.GroupBox1.SuspendLayout()
        Me.tMenu.SuspendLayout()
        Me.tcMain.SuspendLayout()
        Me.tcEue.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.txtEUE)
        Me.GroupBox1.Controls.Add(Me.txtUpdate)
        Me.GroupBox1.Controls.Add(Me.chkEUE)
        Me.GroupBox1.Controls.Add(Me.cmbProject)
        Me.GroupBox1.Controls.Add(Me.cmbClient)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1018, 48)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(919, 14)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(91, 23)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "Options"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(544, 15)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(95, 23)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Update"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtEUE
        '
        Me.txtEUE.Location = New System.Drawing.Point(808, 16)
        Me.txtEUE.Name = "txtEUE"
        Me.txtEUE.ReadOnly = True
        Me.txtEUE.Size = New System.Drawing.Size(103, 20)
        Me.txtEUE.TabIndex = 4
        '
        'txtUpdate
        '
        Me.txtUpdate.Location = New System.Drawing.Point(650, 16)
        Me.txtUpdate.Name = "txtUpdate"
        Me.txtUpdate.ReadOnly = True
        Me.txtUpdate.Size = New System.Drawing.Size(152, 20)
        Me.txtUpdate.TabIndex = 3
        '
        'chkEUE
        '
        Me.chkEUE.AutoSize = True
        Me.chkEUE.Checked = True
        Me.chkEUE.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEUE.Location = New System.Drawing.Point(452, 18)
        Me.chkEUE.Name = "chkEUE"
        Me.chkEUE.Size = New System.Drawing.Size(72, 17)
        Me.chkEUE.TabIndex = 2
        Me.chkEUE.Text = "Only EUE"
        Me.chkEUE.UseVisualStyleBackColor = True
        '
        'cmbProject
        '
        Me.cmbProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProject.FormattingEnabled = True
        Me.cmbProject.Location = New System.Drawing.Point(284, 16)
        Me.cmbProject.Name = "cmbProject"
        Me.cmbProject.Size = New System.Drawing.Size(145, 21)
        Me.cmbProject.TabIndex = 1
        '
        'cmbClient
        '
        Me.cmbClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClient.FormattingEnabled = True
        Me.cmbClient.Location = New System.Drawing.Point(16, 16)
        Me.cmbClient.Name = "cmbClient"
        Me.cmbClient.Size = New System.Drawing.Size(247, 21)
        Me.cmbClient.TabIndex = 0
        '
        'tUpdate
        '
        Me.tUpdate.Interval = 30000
        '
        'nIcon
        '
        Me.nIcon.ContextMenuStrip = Me.tMenu
        Me.nIcon.Text = "nIcon"
        Me.nIcon.Visible = True
        '
        'tMenu
        '
        Me.tMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UpdateToolStripMenuItem, Me.ToolStripMenuItem1, Me.ExitToolStripMenuItem})
        Me.tMenu.Name = "tMenu"
        Me.tMenu.Size = New System.Drawing.Size(113, 54)
        '
        'UpdateToolStripMenuItem
        '
        Me.UpdateToolStripMenuItem.Name = "UpdateToolStripMenuItem"
        Me.UpdateToolStripMenuItem.Size = New System.Drawing.Size(112, 22)
        Me.UpdateToolStripMenuItem.Text = "&Update"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(109, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(112, 22)
        Me.ExitToolStripMenuItem.Text = "&Exit"
        '
        'tIcon
        '
        Me.tIcon.Interval = 1
        '
        'tcMain
        '
        Me.tcMain.Controls.Add(Me.tcEue)
        Me.tcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcMain.Location = New System.Drawing.Point(0, 48)
        Me.tcMain.Name = "tcMain"
        Me.tcMain.SelectedIndex = 0
        Me.tcMain.Size = New System.Drawing.Size(1018, 606)
        Me.tcMain.TabIndex = 2
        '
        'tcEue
        '
        Me.tcEue.Controls.Add(Me.rtEUE)
        Me.tcEue.Location = New System.Drawing.Point(4, 22)
        Me.tcEue.Name = "tcEue"
        Me.tcEue.Padding = New System.Windows.Forms.Padding(3)
        Me.tcEue.Size = New System.Drawing.Size(1010, 580)
        Me.tcEue.TabIndex = 0
        Me.tcEue.Text = "Early Unit End"
        Me.tcEue.UseVisualStyleBackColor = True
        '
        'rtEUE
        '
        Me.rtEUE.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtEUE.Location = New System.Drawing.Point(3, 3)
        Me.rtEUE.Name = "rtEUE"
        Me.rtEUE.ReadOnly = True
        Me.rtEUE.Size = New System.Drawing.Size(1004, 574)
        Me.rtEUE.TabIndex = 1
        Me.rtEUE.Text = ""
        Me.rtEUE.WordWrap = False
        '
        'frmEUE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1018, 654)
        Me.Controls.Add(Me.tcMain)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmEUE"
        Me.Text = "FAHwatch"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.tMenu.ResumeLayout(False)
        Me.tcMain.ResumeLayout(False)
        Me.tcEue.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtUpdate As System.Windows.Forms.TextBox
    Friend WithEvents chkEUE As System.Windows.Forms.CheckBox
    Friend WithEvents cmbProject As System.Windows.Forms.ComboBox
    Friend WithEvents cmbClient As System.Windows.Forms.ComboBox
    Friend WithEvents txtEUE As System.Windows.Forms.TextBox
    Friend WithEvents tUpdate As System.Windows.Forms.Timer
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents nIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents tMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents UpdateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents tIcon As System.Windows.Forms.Timer
    Friend WithEvents tcMain As System.Windows.Forms.TabControl
    Friend WithEvents tcEue As System.Windows.Forms.TabPage
    Friend WithEvents rtEUE As System.Windows.Forms.RichTextBox

End Class
