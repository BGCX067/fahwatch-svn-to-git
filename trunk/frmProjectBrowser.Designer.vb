<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProjectBrowser
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProjectBrowser))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.FetchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ImportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.lblContact = New System.Windows.Forms.Label
        Me.lblCredit = New System.Windows.Forms.Label
        Me.cmbProjects = New System.Windows.Forms.ComboBox
        Me.lblAtoms = New System.Windows.Forms.Label
        Me.lblDeadline = New System.Windows.Forms.Label
        Me.lblPreffered = New System.Windows.Forms.Label
        Me.lnkCode = New System.Windows.Forms.LinkLabel
        Me.lnkDescription = New System.Windows.Forms.LinkLabel
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblServerIP = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.cMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.ContextMenuStrip = Me.cMenu
        Me.GroupBox1.Controls.Add(Me.lblServerIP)
        Me.GroupBox1.Controls.Add(Me.lblContact)
        Me.GroupBox1.Controls.Add(Me.lblCredit)
        Me.GroupBox1.Controls.Add(Me.cmbProjects)
        Me.GroupBox1.Controls.Add(Me.lblAtoms)
        Me.GroupBox1.Controls.Add(Me.lblDeadline)
        Me.GroupBox1.Controls.Add(Me.lblPreffered)
        Me.GroupBox1.Controls.Add(Me.lnkCode)
        Me.GroupBox1.Controls.Add(Me.lnkDescription)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(5, -2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(366, 125)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'cMenu
        '
        Me.cMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FetchToolStripMenuItem, Me.ImportToolStripMenuItem})
        Me.cMenu.Name = "cMenu"
        Me.cMenu.Size = New System.Drawing.Size(111, 48)
        '
        'FetchToolStripMenuItem
        '
        Me.FetchToolStripMenuItem.Name = "FetchToolStripMenuItem"
        Me.FetchToolStripMenuItem.Size = New System.Drawing.Size(110, 22)
        Me.FetchToolStripMenuItem.Text = "Fetch"
        '
        'ImportToolStripMenuItem
        '
        Me.ImportToolStripMenuItem.Name = "ImportToolStripMenuItem"
        Me.ImportToolStripMenuItem.Size = New System.Drawing.Size(110, 22)
        Me.ImportToolStripMenuItem.Text = "Import"
        '
        'lblContact
        '
        Me.lblContact.Location = New System.Drawing.Point(264, 83)
        Me.lblContact.Name = "lblContact"
        Me.lblContact.Size = New System.Drawing.Size(58, 13)
        Me.lblContact.TabIndex = 25
        '
        'lblCredit
        '
        Me.lblCredit.Location = New System.Drawing.Point(98, 83)
        Me.lblCredit.Name = "lblCredit"
        Me.lblCredit.Size = New System.Drawing.Size(58, 13)
        Me.lblCredit.TabIndex = 24
        '
        'cmbProjects
        '
        Me.cmbProjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProjects.FormattingEnabled = True
        Me.cmbProjects.Location = New System.Drawing.Point(7, 11)
        Me.cmbProjects.Name = "cmbProjects"
        Me.cmbProjects.Size = New System.Drawing.Size(239, 21)
        Me.cmbProjects.TabIndex = 23
        '
        'lblAtoms
        '
        Me.lblAtoms.Location = New System.Drawing.Point(264, 40)
        Me.lblAtoms.Name = "lblAtoms"
        Me.lblAtoms.Size = New System.Drawing.Size(58, 13)
        Me.lblAtoms.TabIndex = 22
        '
        'lblDeadline
        '
        Me.lblDeadline.Location = New System.Drawing.Point(98, 62)
        Me.lblDeadline.Name = "lblDeadline"
        Me.lblDeadline.Size = New System.Drawing.Size(58, 13)
        Me.lblDeadline.TabIndex = 21
        '
        'lblPreffered
        '
        Me.lblPreffered.Location = New System.Drawing.Point(98, 40)
        Me.lblPreffered.Name = "lblPreffered"
        Me.lblPreffered.Size = New System.Drawing.Size(58, 13)
        Me.lblPreffered.TabIndex = 20
        '
        'lnkCode
        '
        Me.lnkCode.AutoSize = True
        Me.lnkCode.Location = New System.Drawing.Point(263, 14)
        Me.lnkCode.Name = "lnkCode"
        Me.lnkCode.Size = New System.Drawing.Size(59, 13)
        Me.lnkCode.TabIndex = 19
        Me.lnkCode.TabStop = True
        Me.lnkCode.Text = "LinkLabel8"
        '
        'lnkDescription
        '
        Me.lnkDescription.AutoSize = True
        Me.lnkDescription.Location = New System.Drawing.Point(4, 105)
        Me.lnkDescription.Name = "lnkDescription"
        Me.lnkDescription.Size = New System.Drawing.Size(59, 13)
        Me.lnkDescription.TabIndex = 12
        Me.lnkDescription.TabStop = True
        Me.lnkDescription.Text = "LinkLabel1"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(184, 41)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(56, 13)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "No. Atoms"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(184, 83)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 13)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Contact"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(4, 83)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(34, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Credit"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(4, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Final deadline"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Prefferd days"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(184, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Server IP"
        '
        'lblServerIP
        '
        Me.lblServerIP.Location = New System.Drawing.Point(264, 62)
        Me.lblServerIP.Name = "lblServerIP"
        Me.lblServerIP.Size = New System.Drawing.Size(58, 13)
        Me.lblServerIP.TabIndex = 26
        '
        'frmProjectBrowser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(375, 125)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmProjectBrowser"
        Me.Text = "Project browser"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.cMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lnkCode As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkDescription As System.Windows.Forms.LinkLabel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbProjects As System.Windows.Forms.ComboBox
    Friend WithEvents lblAtoms As System.Windows.Forms.Label
    Friend WithEvents lblDeadline As System.Windows.Forms.Label
    Friend WithEvents lblPreffered As System.Windows.Forms.Label
    Friend WithEvents lblCredit As System.Windows.Forms.Label
    Friend WithEvents lblContact As System.Windows.Forms.Label
    Friend WithEvents cMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents FetchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblServerIP As System.Windows.Forms.Label
End Class
