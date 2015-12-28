<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLOG
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
        Me.rtLOG = New System.Windows.Forms.RichTextBox
        Me.SuspendLayout()
        '
        'rtLOG
        '
        Me.rtLOG.Cursor = System.Windows.Forms.Cursors.Default
        Me.rtLOG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtLOG.Location = New System.Drawing.Point(0, 0)
        Me.rtLOG.Name = "rtLOG"
        Me.rtLOG.ReadOnly = True
        Me.rtLOG.Size = New System.Drawing.Size(582, 373)
        Me.rtLOG.TabIndex = 0
        Me.rtLOG.Text = ""
        '
        'frmLOG
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(582, 373)
        Me.Controls.Add(Me.rtLOG)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmLOG"
        Me.Text = "Debug messages"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rtLOG As System.Windows.Forms.RichTextBox
End Class
