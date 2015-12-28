Imports System.IO
Imports System.Management
Imports System.ServiceProcess
Imports System.Security.Principal
Public Class frmEUE
    Public _IsBussy As Boolean = True

    Private Sub frmEUE_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        nIcon.Visible = False
    End Sub

    Private Sub frmEUE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If DateTime.Now.Date > DateTime.Parse("11-11-2008") Then Application.Exit()
        Me.Text = Me.Text & "  " & My.Application.Info.Version.ToString
        AddLog("Starting application")
        'MsgBox(My.Computer.FileSystem.FileExists("C:\Documents and Settings\marvin\Application Data\Folding@home-gpu\fahlog.txt"))
        'On Error Resume Next

    End Sub

    Private Sub chkEUE_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEUE.CheckedChanged
        If _IsBussy Then Exit Sub
        Application.DoEvents()
        FillRTF()
    End Sub

    Private Sub cmbClient_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClient.SelectedIndexChanged
        If _IsBussy Then Exit Sub
        Application.DoEvents()
        cmbProject.Items.Clear()
        cmbProject.Items.Add("-- ALL PROJECTS --")
        cmbProject.Text = cmbProject.Items(0)
        FillRTF()
    End Sub

    Private Sub cmbProject_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProject.SelectedIndexChanged
        If _IsBussy Then Exit Sub
        Application.DoEvents()
        FillRTF()
    End Sub

    Private strTimer As String

    Private Sub tUpdate_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tUpdate.Tick
        If dtUpdate.ToShortTimeString = DateTime.Now.ToShortTimeString Then
            AddLog("Update timer event")
            dtUpdate = DateTime.Now.AddMinutes(UpdateInterval)
            txtUpdate.Text = "Next update on: " & dtUpdate.ToShortTimeString
            Update()
            FillRTF()
            Application.DoEvents()
            If Me.WindowState = FormWindowState.Minimized Then
                nIcon.BalloonTipTitle = ("FAHwatch")
                If txtEUE.TextLength > 0 Then
                    nIcon.BalloonTipText = txtEUE.Text
                Else
                    nIcon.BalloonTipText = "-"
                End If
                If txtEUE.TextLength > 0 Then nIcon.BalloonTipIcon = ToolTipIcon.Info
                If intEUErate > AlertRate And Me.WindowState = FormWindowState.Minimized Then
                    Me.WindowState = FormWindowState.Normal
                ElseIf ShowBalloon And Not _NoBalloon Then
                    nIcon.ShowBalloonTip(1000)
                End If
            End If
            AddLog("End Update timer event")
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        AddLog("Manual refresh")
        StartUpdate()
        FillRTF()
        Application.DoEvents()
        If txtEUE.TextLength > 0 Then nIcon.BalloonTipText = txtEUE.Text
        AddLog("End manual refresh")
        Me.Cursor = Cursors.Default
        Me.Enabled = True
    End Sub

    Public _NoBalloon As Boolean = False

    Private Sub frmEUE_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            AddLog("Application initiated")
            Me.Enabled = False
            Me.Cursor = Cursors.AppStarting
            LoadSettings()
            If StartInTray = True Then
                _NoBalloon = True
                Me.WindowState = FormWindowState.Minimized
            End If
            StartUpdate()
            FillRTF()
            tUpdate.Enabled = True
            Me.Enabled = True
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            AddLog("Error : " & Err.Number & " - " & Err.Description)
            Err.Clear()
        End Try
    End Sub

    Private Sub frmEUE_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Try
            If Me.WindowState = FormWindowState.Minimized Then
                Me.ShowInTaskbar = False
                nIcon.Icon = Me.Icon
                nIcon.Visible = True
                nIcon.BalloonTipTitle = ("FAHwatch")
                If txtEUE.TextLength > 0 Then
                    nIcon.BalloonTipText = txtEUE.Text
                Else
                    nIcon.BalloonTipText = "-Looking-"
                End If
                If txtEUE.TextLength > 0 Then nIcon.Text = txtEUE.Text
                nIcon.BalloonTipIcon = ToolTipIcon.Info
                If ShowBalloon And Not _NoBalloon Then nIcon.ShowBalloonTip(1000)
            Else
                Me.ShowInTaskbar = True
                nIcon.Visible = False
            End If
        Catch ex As Exception
            AddLog("Error : " & Err.Number & " - " & Err.Description)
            Err.Clear()
            Logging.Show()
        End Try
        
    End Sub

    Private Sub nIcon_BalloonTipClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles nIcon.BalloonTipClicked
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub nIcon_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles nIcon.MouseClick
        If tIcon.Enabled Then Exit Sub
        If e.Button = Windows.Forms.MouseButtons.Right Then
            tMenu.Show()
        Else
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub nIcon_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles nIcon.MouseDoubleClick
        If tIcon.Enabled Then Exit Sub
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub UpdateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateToolStripMenuItem.Click
        Call Button1_Click(sender, e)
        Application.DoEvents()
        nIcon.BalloonTipTitle = ("FAHwatch")
        If txtEUE.TextLength > 0 Then
            nIcon.BalloonTipText = txtEUE.Text
        Else
            nIcon.BalloonTipText = "-Looking-"
        End If
        If txtEUE.TextLength > 0 Then nIcon.Text = txtEUE.Text
        nIcon.BalloonTipIcon = ToolTipIcon.Info
        If ShowBalloon And Not _NoBalloon Then nIcon.ShowBalloonTip(1000)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        frmOpt.nudAllert.Value = AlertRate
        frmOpt.nudUpdate.Value = UpdateInterval
        frmOpt.chkBalloon.Checked = ShowBalloon
        frmOpt.chkTray.Checked = StartInTray
        frmOpt.ShowDialog()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub tIcon_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tIcon.Tick
        Static TickTock As Boolean = False, Bussy As Boolean = False
        If Bussy Then Exit Sub : Bussy = True
        If TickTock Then
            nIcon.Icon = My.Resources.Red
            TickTock = False
        Else
            nIcon.Icon = My.Resources.Yellow
            TickTock = True
        End If
        Application.DoEvents()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub
End Class
