Public Class frmLOG

    Private Sub frmLOG_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not e.CloseReason = CloseReason.ApplicationExitCall Then
            e.Cancel = True
            Me.Visible = False
        End If
    End Sub
End Class