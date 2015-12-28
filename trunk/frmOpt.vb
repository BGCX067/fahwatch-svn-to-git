Public Class frmOpt

    Private Sub frmOpt_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed


    End Sub


    Private Sub frmOpt_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        StartInTray = chkTray.Checked
        UpdateInterval = nudUpdate.Value
        AlertRate = nudAllert.Value
        ShowBalloon = chkBalloon.Checked
        SaveSettings()
    End Sub

    Private Sub cmdDebug_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDebug.Click
        If Logging.Visible = False Then
            Logging.Show()
        Else
            Logging.Focus()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim retVal As MsgBoxResult = MsgBox("Are you sure you want to remove the settings file?", MsgBoxStyle.Information + MsgBoxStyle.YesNo)
        If retVal = MsgBoxResult.No Then Exit Sub
        ClearSettings = True
        RemoveSettingsFile()
    End Sub

    Private Sub frmOpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        pBrowser.ShowBrowser()
    End Sub
End Class