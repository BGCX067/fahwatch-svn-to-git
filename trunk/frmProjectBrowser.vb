Public Class frmProjectBrowser
    Public Function ShowBrowser(Optional ByVal Pnumber As String = "0") As Boolean
        cmbProjects.Items.Clear()
        For x = 1 To ProjectInfo.ProjectCount
            cmbProjects.Items.Add(ProjectInfo.Project(x))
        Next
        If ProjectInfo.ProjectKnown(Pnumber) Then
            cmbProjects.Text = Pnumber
        Else
            cmbProjects.Text = cmbProjects.Items(0)
        End If
        RefreshBrowser()
        Me.Show()
    End Function
    Public Function RefreshBrowser()
        Dim pnumber As String = cmbProjects.Text
        lblAtoms.Text = ProjectInfo.NumberOfAtoms(Pnumber)
        lblDeadline.Text = ProjectInfo.FinalDeadline(Pnumber)
        lblPreffered.Text = ProjectInfo.PreferredDays(Pnumber)
        lnkCode.Text = ProjectInfo.Code(Pnumber)
        lblContact.Text = ProjectInfo.Contact(pnumber)
        lnkDescription.Text = ProjectInfo.DescriptionURL(Pnumber)
        lblServerIP.Text = ProjectInfo.ServerIP(pnumber)
        lblCredit.Text = ProjectInfo.Credit(pnumber)
    End Function

    Private Sub cmbProjects_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProjects.SelectedIndexChanged
        RefreshBrowser()
    End Sub

    Private Sub frmProjectBrowser_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            e.Cancel = True
            Me.Hide()
        End If
    End Sub

    Private Sub lnkDescription_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkDescription.LinkClicked
        Process.Start(ProjectInfo.DescriptionURL(cmbProjects.Text))
    End Sub

    Private Sub lnkContact_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Process.Start(ProjectInfo.Contact(cmbProjects.Text))
    End Sub

    Private Sub lnkServer_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Process.Start("http://" & ProjectInfo.ServerIP(cmbProjects.Text))
    End Sub

    Private Sub lnkCode_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCode.LinkClicked
        Process.Start("http://fahwiki.net/index.php/Cores")
    End Sub

    Private Sub frmProjectBrowser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmProjectBrowser_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick

    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub GroupBox1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GroupBox1.MouseClick
        If MouseButtons = Windows.Forms.MouseButtons.Right Then cMenu.Show()
    End Sub

    Private Sub FetchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FetchToolStripMenuItem.Click
        ProjectInfo.GetProjects(clsProjectInfo.eSource.Psummary)
        ShowBrowser(cmbProjects.Text)
    End Sub

    Private Sub ImportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportToolStripMenuItem.Click
        Dim iFile As String
        Dim fBrowser As OpenFileDialog
        With fBrowser
            .ValidateNames = True
            If .ShowDialog() And .FileName <> "" Then
                ProjectInfo.GetProjects(clsProjectInfo.eSource.Psummary, .FileName)
            End If
            Exit Sub
        End With
        ShowBrowser(cmbProjects.Text)
    End Sub
End Class