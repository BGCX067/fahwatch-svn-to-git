Imports System.IO
Module modSettings
    Public ClearSettings As Boolean = False
    Public ProjectInfo As New clsProjectInfo
    Public AlertRate As Integer = 20, ShowBalloon As Boolean = True, UpdateInterval As Integer = 15, StartInTray As Boolean = False, ClientsFileLocation As String
    Private SFile As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\fahWATCH\Settings.txt"
    Public Sub SaveSettings()
        If ClearSettings Then Exit Sub
        Dim allSettings(0 To 4) As String
        allSettings(0) = "UpdateInterval" & vbTab & UpdateInterval.ToString
        allSettings(1) = "ShowBalloon" & vbTab & ShowBalloon.ToString
        allSettings(2) = "AlertRate" & vbTab & AlertRate.ToString
        allSettings(3) = "StartInTray" & vbTab & StartInTray.ToString
        allSettings(4) = "Clientstab.txt" & vbTab & ClientsFileLocation
        If Not My.Computer.FileSystem.DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\fahWATCH\") Then My.Computer.FileSystem.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\fahWATCH\")
        File.WriteAllLines(SFile, allSettings)
        AddLog("Settings file saved")
    End Sub
    Public Sub LoadSettings()
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Settings.txt") Then
            AddLog("Settings file found in startup directory, moving to %appdata%")
            If Not My.Computer.FileSystem.DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\fahWATCH\") Then My.Computer.FileSystem.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\fahWATCH")
            If Not My.Computer.FileSystem.FileExists(SFile) Then My.Computer.FileSystem.CopyFile(Application.StartupPath & "\Settings.txt", SFile)
            My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\Settings.txt", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.DoNothing)
        End If
        If Not My.Computer.FileSystem.FileExists(SFile) Then
            AddLog("First run detected, showing options window")
            frmOpt.ShowDialog(frmEUE)
            Exit Sub
        End If
        AddLog("Reading settings")
        Using mRead As New Microsoft.VisualBasic.FileIO.TextFieldParser(SFile)
            mRead.TextFieldType = FileIO.FieldType.Delimited
            mRead.SetDelimiters(vbTab)
            While Not mRead.EndOfData
                Try
                    Dim currentRow As String()
                    currentRow = mRead.ReadFields()
                    Dim currentField As String
                    Dim xStep As Integer : xStep = 1
                    Dim WhatSetting As String = ""
                    For Each currentField In currentRow
                        If xStep = 1 Then
                            WhatSetting = currentField
                        ElseIf xStep = 2 Then
                            Select Case WhatSetting
                                Case "UpdateInterval"
                                    AddLog("Update interval = " & currentField)
                                    UpdateInterval = CInt(currentField)
                                Case "ShowBalloon"
                                    AddLog("Show balloon tip = " & currentField)
                                    ShowBalloon = CBool(currentField)
                                Case "AlertRate"
                                    AddLog("Alert rate = " & currentField)
                                    AlertRate = CInt(currentField)
                                Case "StartInTray"
                                    AddLog("Start in tray = " & currentField)
                                    StartInTray = CBool(currentField)
                                Case "Clientstab.txt"
                                    AddLog("Clientstab.txt location = " & currentField)
                                    ClientsFileLocation = currentField
                                Case Else
                                    AddLog(("Settings file corruption, value " & WhatSetting & " is unknown"))
                                    MsgBox("Settings file corruption, value " & WhatSetting & " is unknown")
                            End Select
                        End If
                        xStep += xStep
                    Next
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    AddLog("While reading the settings file, line " & ex.LineNumber & "is not valid and will be skipped.")
                    Logging.Show()
                End Try
            End While
        End Using
    End Sub
    Public Sub RemoveSettingsFile()
        Try
            My.Computer.FileSystem.DeleteDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\fahWATCH\", FileIO.DeleteDirectoryOption.DeleteAllContents)
        Catch ex As Exception
            AddLog("While trying to remove the %appdata% folder the following error occured: " & Err.Description)
            Logging.Show()
        End Try
    End Sub
End Module
