Imports System.IO
Imports System.Management
Imports System.ServiceProcess
Imports System.Security.Principal
Module modMAIN
    'Public ProjectInfo As New clsProjectInfo
    Public frmEUE As New frmEUE
    Public intEUErate As Integer
    Public Const pciAti = "1002"
    Public Const pciNvidia = "10DE"
    Public pBrowser As New frmProjectBrowser
    Public xFile As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\FAHmon\config\clientstab.txt"
    Public Enum eUserType
        User = 1
        SystemOperator = 2
        PowerUser = 3
        Guest = 4
        Administrator = 5
        Other = 6
    End Enum
    Private _IsBussy As Boolean = frmEUE._IsBussy
    Private rtEUE As RichTextBox = frmEUE.rtEUE
    Private cmbClient As ComboBox = frmEUE.cmbClient
    Private cmbProject As ComboBox = frmEUE.cmbProject
    Private chkEUE As CheckBox = frmEUE.chkEUE
    Private nIcon As NotifyIcon = frmEUE.nIcon
    Private tIcon As Timer = frmEUE.tIcon
    Private txtEUE As TextBox = frmEUE.txtEUE
    Private txtUpdate As TextBox = frmEUE.txtUpdate
    Private _noBalloon As Boolean = frmEUE._noBalloon
    Public AccType As eUserType
    Public UpdateStartTime As DateTime
    Public UpdateEndTime As DateTime

    Public Sub main()
        Application.Run(frmEUE)
    End Sub
    Public Function FillRTF() As Boolean
        Try
            _IsBussy = True
            Dim nSt As String
            Dim mSearch As ManagementObjectSearcher
            Dim query As ObjectQuery
            Dim queryCollection As ManagementObjectCollection
            AddLog("Filling text area")
            rtEUE.Clear()
            Dim _strUserName As String
            Dim mIdentity As System.Security.Principal.WindowsIdentity = WindowsIdentity.GetCurrent()
            _strUserName = mIdentity.Name
            Dim mPrincipal = New WindowsPrincipal(mIdentity)
            AddLog("Getting username and account type")
            If mPrincipal.IsInRole(WindowsBuiltInRole.Administrator) Then
                AccType = eUserType.Administrator
            ElseIf mPrincipal.IsInRole(WindowsBuiltInRole.Guest) Then
                AccType = eUserType.Guest
            ElseIf mPrincipal.IsInRole(WindowsBuiltInRole.PowerUser) Then
                AccType = eUserType.PowerUser
            ElseIf mPrincipal.IsInRole(WindowsBuiltInRole.SystemOperator) Then
                AccType = eUserType.SystemOperator
            ElseIf mPrincipal.IsInRole(WindowsBuiltInRole.User) Then
                AccType = eUserType.User
            Else
                AccType = eUserType.User
            End If


            With rtEUE
                .Text = .Text & "Os version:" & vbTab & Environment.OSVersion.VersionString & vbNewLine
                .Text = .Text & "User:" & vbTab & vbTab & _strUserName & vbNewLine
                Select Case AccType
                    Case eUserType.Administrator
                        .Text = .Text & "Account:" & vbTab & vbTab & "Administrator" & vbNewLine
                    Case eUserType.Guest
                        .Text = .Text & "Account:" & vbTab & vbTab & "Guest" & vbNewLine
                    Case eUserType.PowerUser
                        .Text = .Text & "Account:" & vbTab & vbTab & "Poweruser" & vbNewLine
                    Case eUserType.SystemOperator
                        .Text = .Text & "Account:" & vbTab & vbTab & "Systemoperator" & vbNewLine
                    Case eUserType.User
                        .Text = .Text & "Account:" & vbTab & vbTab & "User" & vbNewLine
                    Case eUserType.Other
                        .Text = .Text & "Account:" & vbTab & vbTab & "Other" & vbNewLine
                End Select
                .Text = .Text & "---------------------------------------------------------------------------------" & vbNewLine
            End With

            AddLog("Starting enumerating clients")
            For Each nC As sClients In colClients
                If Not cmbClient.SelectedIndex = 0 Then
                    If nC.Name <> cmbClient.Text Then
                        AddLog("Client " & nC.Name & " skipped due to combobox contends")
                        GoTo skip5
                    End If
                End If
                Dim intIndex As Integer = 1
                For Each nC.WU In nC.colWU
                    AddLog("Trying WU " & intIndex.ToString)
                    Application.DoEvents()
                    'Skip good wu's, means that no hwinfo is coupeld with them though but does it matter?
                    If nC.WU.CoreStatus = "CoreStatus = 64 (100)" And chkEUE.Checked Then
                        AddLog("Skipping wu bases on corestatus")
                        GoTo Skip4
                    End If
                    If Not cmbProject.SelectedIndex = 0 Then
                        If Not nC.WU.ProjectNumber = cmbProject.Text Then
                            AddLog("Skipping project based on to combobox contends")
                            GoTo Skip4
                        End If
                    End If
                    If Not cmbProject.Items.Contains(nC.WU.ProjectNumber) Then cmbProject.Items.Add(nC.WU.ProjectNumber)
                    'Check IsLocal then do wmi
                    nSt = "-FAHMON NAME:" & vbTab & nC.Name & vbNewLine
                    nSt = nSt & "INDEX:" & vbTab & vbTab & intIndex & vbNewLine
                    intIndex += 1
                    nSt = nSt & "LOCATION:" & vbTab & nC.Location & vbNewLine
                    nSt = nSt & "LOCAL CLIENT:" & vbTab & nC.IsLocal & vbNewLine
                    If nC.IsLocal Then
                        AddLog("Client is local")
                        With nC.WU
                            nSt = nSt & "CLIENT:" & vbTab & vbTab & .Client & vbNewLine
                            nSt = nSt & "FLAGS:" & vbTab & vbTab & .Flags & vbNewLine
                            nSt = nSt & "CORE:" & vbTab & vbTab & .Core & vbNewLine
                            nSt = nSt & "StartTime:" & vbTab & vbTab & .StartTime & vbNewLine
                            nSt = nSt & "StartPercentage:" & vbTab & vbTab & .StartPercentage & vbNewLine
                            nSt = nSt & "EndTime:" & vbTab & vbTab & vbTab & .EndTime & vbNewLine
                            nSt = nSt & "EndPercentage:" & vbTab & vbTab & .EndPercentage & vbNewLine
                            If .Client.Contains("GPU") Then
                                AddLog("client is gpu client")
                                'Look in flags for -gpu
                                If .Flags = Nothing Then
                                    AddLog("Flags is nothing")
                                    AddLog("Upperbound of gpucollection : " & nC.hwInf.colGPU.Count)
                                    .GPU = nC.hwInf.colGPU(nC.hwInf.colGPU.Count)
                                ElseIf InStr(.Flags.ToUpper, "-GPU") Then
                                    AddLog("Client has -gpu x flag set")
                                    AddLog("*" & .Flags)
                                    Dim iStart As Integer = .Flags.ToUpper.IndexOf("-GPU") + 6
                                    Dim iStr As String = Mid(.Flags, iStart, 1)
                                    AddLog("Upperbound of gpucollection : " & nC.hwInf.colGPU.Count)
                                    .GPU = nC.hwInf.colGPU(CInt(iStr) + 1)
                                ElseIf .Flags = "" Then
                                    AddLog("Flags are empty")
                                    AddLog("Upperbound of gpucollection : " & nC.hwInf.colGPU.Count)
                                    .GPU = nC.hwInf.colGPU(nC.hwInf.colGPU.Count)
                                Else
                                    'assume 0
                                    AddLog("Client has no -gpu x flag.")
                                    AddLog("Upperbound of gpucollection : " & nC.hwInf.colGPU.Count)
                                    .GPU = nC.hwInf.colGPU(nC.hwInf.colGPU.Count)
                                End If

                                nSt = nSt & "-VIDEO CARD" & vbNewLine
                                nSt = nSt & "Caption" & vbTab & vbTab & .GPU.Caption & vbNewLine
                                nSt = nSt & "Description" & vbTab & .GPU.Description & vbNewLine
                                nSt = nSt & "AdapterRam" & vbTab & .GPU.AdapterRam & vbNewLine
                                nSt = nSt & "DriverVersion" & vbTab & .GPU.DriverVersion & vbNewLine
                                nSt = nSt & "DriverDate" & vbTab & .GPU.DriverDate & vbNewLine
                                nSt = nSt & "Index" & vbTab & vbTab & .GPU.Index.ToString & vbNewLine
                                nSt = nSt & "InfSection" & vbTab & .GPU.InfSection & vbNewLine
                                nSt = nSt & "COMPILER:" & vbTab & .Compiler & vbNewLine
                                nSt = nSt & "BUILDHOST:" & vbTab & .BuildHost & vbNewLine
                                nSt = nSt & "BOARD:" & vbTab & vbTab & .Board & vbNewLine
                            Else
                                AddLog("Client is CPU type")
                                .CPU = nC.hwInf.CPU
                                'MsgBox(.Calling) 'nope use FOO
                                If InStr(.Flags.ToUpper, "DEINO") Then
                                    'Verify Deino
                                    AddLog("Checking for Deino service")
                                    Static dOnce As Boolean = False
                                    If dOnce Then GoTo Skip2
                                    nC.hwInf.DeinoInstalled = False : nC.hwInf.GoodFOO = False
                                    For Each sConnt As ServiceController In ServiceController.GetServices
                                        If sConnt.ServiceName.ToUpper.Contains("DeinoPM".ToUpper) Then
                                            nC.hwInf.DeinoInstalled = True
                                            nC.hwInf.DeinoRunningState = sConnt.Status
                                            Exit For
                                        End If
                                    Next
                                    AddLog("Checking for FOO status")
                                    Dim pShell As New Process
                                    With pShell.StartInfo
                                        .FileName = "cmd.exe"
                                        .Arguments = "/c mpiexec -np 2 foo"
                                        .WorkingDirectory = nC.WU.Location & "\"
                                        .RedirectStandardOutput = True
                                        .CreateNoWindow = True
                                        .UseShellExecute = False
                                    End With
                                    pShell.Start()
                                    Dim sRead As StreamReader = pShell.StandardOutput
                                    Dim aText As String = sRead.ReadToEnd
                                    If aText.ToUpper.Contains("If you see this twice, mpi is working".ToUpper & vbNewLine & "If you see this twice, mpi is working".ToUpper) Then
                                        nC.hwInf.GoodFOO = True
                                    End If
Skip2:
                                    nSt = nSt & "ServiceInstalled:" & vbTab & nC.hwInf.DeinoInstalled & vbNewLine
                                    nSt = nSt & "Servicestatus:" & vbTab & nC.hwInf.DeinoRunningState & vbNewLine
                                    nSt = nSt & "GoodFOO:" & vbTab & nC.hwInf.GoodFOO & vbNewLine
                                ElseIf .Client.ToUpper.Contains("SMP") Then
                                    AddLog("Checking for MPICH service")
                                    Static MOnce As Boolean = False
                                    If MOnce Then GoTo Skip3
                                    'mpich check service
                                    nC.hwInf.MpichInstalled = False : nC.hwInf.GoodFOO = False
                                    For Each sConnt As ServiceController In ServiceController.GetServices
                                        If sConnt.ServiceName.ToUpper.Contains("mpich2_smpd".ToUpper) Then
                                            nC.hwInf.MpichInstalled = True
                                            nC.hwInf.MpichRunningState = sConnt.Status
                                            Exit For
                                        End If
                                    Next
                                    AddLog("Checking for FOO result")
                                    Dim pShell As New Process
                                    With pShell.StartInfo
                                        .FileName = "cmd.exe"
                                        .Arguments = "/c mpiexec -np 2 foo"
                                        .WorkingDirectory = nC.WU.Location & "\"
                                        .RedirectStandardOutput = True
                                        .CreateNoWindow = True
                                        .UseShellExecute = False
                                    End With
                                    pShell.Start()
                                    Dim sRead As StreamReader = pShell.StandardOutput
                                    Dim aText As String = sRead.ReadToEnd
                                    If pShell.HasExited = False Then pShell.Close()
                                    If aText.ToUpper.Contains("If you see this twice, mpi is working".ToUpper & vbNewLine & "If you see this twice, mpi is working".ToUpper) Then
                                        nC.hwInf.GoodFOO = True
                                    End If
                                    MOnce = True
Skip3:
                                    nSt = nSt & "ServiceInstalled:" & vbTab & nC.hwInf.MpichInstalled & vbNewLine
                                    nSt = nSt & "ServiceStatus:" & vbTab & nC.hwInf.MpichRunningState & vbNewLine
                                    nSt = nSt & "GoodFOO:" & vbTab & nC.hwInf.GoodFOO & vbNewLine
                                End If
                            End If
                                nSt = nSt & "PROJECT:" & vbTab & .Project & vbNewLine
                                nSt = nSt & "QUEUESLOT:" & vbTab & .Qslot & vbNewLine
                                nSt = nSt & "CORESTATUS:" & vbTab & .CoreStatus & vbNewLine
                                nSt = nSt & "TIMESTAMP:" & vbTab & .CsTimeStamp & vbNewLine
                                If .CoreStatus <> "CoreStatus = 64 (100)" Then
                                    For x As Int16 = nC.WU.colLOG.Count To 1 Step -1
                                        nSt = nSt & nC.WU.colLOG(x).ToString & vbNewLine
                                    Next
                                End If
                                rtEUE.Text = rtEUE.Text & nSt & vbNewLine & vbNewLine
                                nSt = ""
                        End With
                    Else
                        AddLog("Client is non local")
                        With nC.WU
                            nSt = nSt & .Client & vbNewLine
                            nSt = nSt & .Flags & vbNewLine
                            nSt = nSt & .Core & vbNewLine
                            If .Client.Contains("GPU") Then
                                AddLog("Client is GPU")
                                nSt = nSt & "COMPILER:" & vbTab & .Compiler & vbNewLine
                                nSt = nSt & "BUILDHOST:" & vbTab & .BuildHost & vbNewLine
                                nSt = nSt & "BOARD:" & vbTab & vbTab & .Board & vbNewLine
                            Else
                                AddLog("Client is CPU")
                            End If
                            nSt = nSt & .Project & vbNewLine
                            nSt = nSt & .CoreStatus & vbNewLine
                            If .CoreStatus <> "CoreStatus = 64 (100)" Then
                                For x As Int16 = nC.WU.colLOG.Count To 1 Step -1
                                    nSt = nSt & nC.WU.colLOG(x).ToString & vbNewLine
                                Next
                            End If
                            rtEUE.Text = rtEUE.Text & nSt & vbNewLine & vbNewLine
                            nSt = ""
                        End With
                    End If
                    Application.DoEvents()
Skip4:
                Next
                Application.DoEvents()
skip5:
            Next
            AddLog("Fill text area done!")
            _IsBussy = False
            rtEUE.Focus()
            Return True
        Catch ex As Exception
            AddLog("Error : " & Err.Number & " - " & Err.Description)
            Err.Clear()
            If Not Logging.Visible Then Logging.Show()
            Return False
        End Try
    End Function
    Private Function ParseLog(ByVal nClient As sClients, ByVal logFile As String) As Boolean
        Dim sText() As String : ReDim sText(0 To 1)
        Try
            logFile = logFile.Replace("/", "\")
            logFile = Path.GetFullPath(logFile)
            If Not My.Computer.FileSystem.FileExists(logFile) Then
                AddLog("This log file can not be found = " & logFile)
                'Write to intermediate window
                Return True
            End If
            If nIcon.Visible Then Application.DoEvents()
            Dim fStream As FileStream = New FileStream(logFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            Dim sRead As New StreamReader(fStream)
            If sRead.EndOfStream Then
                AddLog("Filestream reports empty file")
                Return True
            End If
            AddLog("Filling temp array")
            While Not sRead.EndOfStream
                sText(sText.GetUpperBound(0)) = sRead.ReadLine
                ReDim Preserve sText(0 To sText.GetUpperBound(0) + 1)
            End While
            sRead.Close()
            fStream.Close()
            sRead = Nothing
            fStream = Nothing
            AddLog("Array filled")
        Catch ex As Exception
            AddLog("Error while using filestream, using alternate reading method")
            My.Computer.FileSystem.CopyFile(logFile, Application.StartupPath & "\__tmpparse.txt", True)
            sText = File.ReadAllLines(Application.StartupPath & "\__tmpparse.txt")
        End Try
        If sText.Length = 0 Then
            AddLog("File " & logFile & " seems to be empty")
            Return True
        End If

        Try
            Dim nWu As New sClients.sWU, xLocation As Integer = 1, xDiscard As Integer = 12
            Do

                With sText(xLocation)
                    'AddLog("Parsing line " & xLocation.ToString)
                    'With sLine

                    '[20:42:07] + Could not connect to Work Server (results)
                    '[20:42:07]     (171.67.108.25:80)
                    '[20:42:07]   Could not transmit unit 03 to Collection server; keeping in queue.

                    '[00:37:23] + Could not connect to Work Server (results)
                    '[00:37:23]     (171.64.65.106:80)
                    '[00:37:23] - Error: Could not transmit unit 01 (completed October 31) to work server.


                    If .ToUpper.Contains("--- Opening Log file".ToUpper) Then
                        'Start of log, get client
                        If xLocation + 3 >= sText.Length Then Exit Do
                        xLocation += 3
                        AddLog("Reading client type")
                        nWu.Client = Mid(sText(xLocation), 3, sText(xLocation).LastIndexOf(" ") - 2)
                        If xLocation + 3 >= sText.Length Then Exit Do
                        xLocation += 3
                        nWu.Client = nWu.Client & " - " & sText(xLocation).Trim(" ")

                    ElseIf .ToUpper.Contains("+ Attempting to get work packet".ToUpper) Then
                        'Clear download
                        For X1 As Integer = xLocation To sText.Length - 2
                            If sText(X1).ToUpper.Contains("LOADED QUEUE SUCCESFULLY") And sText(X1 + 1).ToUpper.Contains("CLOSED CONNECTIONS") Then
                                nClient.NoWork = ""
                                Exit For
                            ElseIf sText(X1).ToUpper.Contains("- Attempt #") Then
                                nClient.NoWork = sText(X1).ToString
                                Exit For
                            ElseIf sText(X1).ToUpper.Contains("+ Processing work unit".ToUpper) Then
                                nClient.NoWork = ""
                                Exit For
                            End If
                        Next
                    ElseIf .Contains("Launch directory:") Then
                        AddLog("Parsing launch directory")
                        nWu.Location = sText(xLocation).Replace("Launch directory: ", "")
                    ElseIf .Contains("Arguments: ") Then
                        AddLog("Parsing arguments")
                        nWu.Flags = sText(xLocation).Replace("Arguments: ", "")
                    ElseIf .Contains("Executable: ") Then
                        AddLog("Skipping Executable: *for now atleast")
                        'not realy needed is it?
                    ElseIf .Contains("*------------------------------*") Then
                        AddLog("Trying to parse core")
                        xDiscard = InStr(sText(xLocation), "*------------------------------*")
                        If xLocation + 1 >= sText.Length Then Exit Do
                        xLocation += 1
                        nWu.Core = Mid(sText(xLocation), xDiscard)
                        If xLocation + 1 >= sText.Length Then Exit Do
                        xLocation += 1
                        nWu.Core = nWu.Core & " - " & Mid(sText(xLocation), xDiscard)
                        If nWu.Core.Contains("GPU") Then
                            AddLog("Core = GPU")
                            AddLog("Parsing extended gpu info")
                            nClient.ClientType = sClients.eClientType.Gpu
                            If xLocation + 2 >= sText.Length Then Exit Do
                            xLocation += 2
                            nWu.Compiler = Mid(sText(xLocation), xDiscard).Replace("Compiler  :", "").Trim
                            If xLocation + 1 >= sText.Length Then Exit Do
                            xLocation += 1
                            nWu.BuildHost = Mid(sText(xLocation), xDiscard).Replace("Build host:", "").Trim
                            If xLocation + 1 >= sText.Length Then Exit Do
                            xLocation += 1
                            nWu.Board = Mid(sText(xLocation), xDiscard).Replace("Board Type:", "").Trim
                        End If
                    ElseIf .Contains("Project: ") Then
                        AddLog("Parsing project")
                        nWu.Project = Mid(sText(xLocation), xDiscard)
                    ElseIf .Contains("Entering M.D.") Or .Contains("Starting GUI Server") Then
                        AddLog("Parsing project start time = " & sText(xLocation))
                        nWu.StartTime = Mid(sText(xLocation), 1, xDiscard - 1).Replace("[", "").Replace("]", "").Replace("{", "").Replace(")", "").Trim
                        For x2 As Integer = xLocation To sText.Length - 1
                            If sText(x2).ToUpper.Contains("COMPLETED") Then
                                AddLog("Parsing project start percentage")
                                Dim strPercentage As String
                                If sText(x2).ToUpper.Contains("STEPS") Then
                                    strPercentage = Mid(sText(x2), sText(x2).ToUpper.LastIndexOf("STEPS") + 6).ToUpper.Replace("COMPLETED", "").Trim(" ").Replace("(", "").Replace(")", "").Replace("%", "").Replace("PERCENT", "").Trim
                                Else
                                    strPercentage = Mid(sText(x2), xDiscard).ToUpper.Replace("COMPLETED", "").Trim(" ").Replace("(", "").Replace(")", "").Replace("%", "")
                                End If
                                nWu.StartPercentage = strPercentage
                                AddLog(Mid(sText(x2), 1, xDiscard) & " -- " & strPercentage)
                                Exit For
                            ElseIf sText(x2).ToUpper.Contains("*------------------------------*") Then
                                AddLog("Skipping project start percentage!")
                                Exit For
                            End If
                        Next
                    ElseIf .Contains("- Calling '") Then
                        AddLog("Parsing - Calling '")
                        nWu.Calling = Mid(sText(xLocation), xDiscard)
                    ElseIf .Contains("Working on queue slot") Then
                        AddLog("Parsing Queue slot")
                        nWu.Qslot = Mid(sText(xLocation), xDiscard)
                    ElseIf .Contains("CoreStatus = ") Then
                        AddLog("Corestatus message found")
                        nWu.CsTimeStamp = Mid(.ToString, 1, xDiscard - 1)
                        nWu.CoreStatus = Mid(sText(xLocation), xDiscard)
                        AddLog("Parsing last frame")
                        For x2 As Integer = xLocation To 0 Step -1
                            If sText(x2).ToUpper.Contains("COMPLETED") And Not sText(x2).ToUpper.Contains("RUN") Then
                                AddLog("Skipping start frame info")
                                Dim strPercentage As String
                                If sText(x2).ToUpper.Contains("STEPS") Then
                                    strPercentage = Mid(sText(x2), sText(x2).ToUpper.LastIndexOf("STEPS") + 6).ToUpper.Replace("COMPLETED", "").Trim(" ").Replace("(", "").Replace(")", "").Replace("%", "").Replace("PERCENT", "").Trim
                                Else
                                    strPercentage = Mid(sText(x2), xDiscard).ToUpper.Replace("COMPLETED", "").Trim(" ").Replace("(", "").Replace(")", "").Replace("%", "")
                                End If
                                nWu.EndPercentage = strPercentage
                                nWu.EndTime = Mid(sText(x2), 1, xDiscard - 1).Replace("[", "").Replace("]", "").Replace("{", "").Replace(")", "").Trim
                                AddLog(Mid(x2, 1, xDiscard) & " -- " & strPercentage)
                                Exit For
                            ElseIf sText(x2).Contains("*------------------------------*") Then
                                AddLog("Skipping end frame info")
                                Exit For
                            End If
                        Next
                        If nWu.CoreStatus <> "CoreStatus = 64 (100)" Then
                            AddLog("Looking up size of usable log snippet")
                            'get last useless data
                            nWu.colLOG = New Collection
                            For XS As Integer = xLocation To 0 Step -1
                                If sText(XS).Contains("Entering M.D.") Or sText(XS).ToUpper.Contains("WORKING ON") Or sText(XS).ToUpper.Contains("STARTING GUI") Or sText(XS).ToUpper.Contains("CALLING -") Or sText(XS).ToUpper.Contains("COMPLETED") Or XS = xLocation - 25 Then
                                    AddLog("Log snippet finished parsing")
                                    If Not sText(XS) = "" Then nWu.colLOG.Add(sText(XS).Trim(vbNewLine))
                                    Exit For
                                Else
                                    If Not sText(XS) = "" Then nWu.colLOG.Add(sText(XS).Trim(vbNewLine))
                                End If
                            Next
                        Else
                            AddLog("Corestatus = 64, no extended parsing")
                        End If
                        AddLog("Adding wu to collection")
                        nClient.colWU.Add(nWu)
                        'Keep Client, Location, Flags but clear the rest.
                        nWu.ClearWU()

                        AddLog("Looking for next project")
                        'Move to next if any
                        For XS As Integer = xLocation To sText.Length - 2
                            'Debug.WriteLine("Location " & XS & " from " & sText.Length & " checked")
                            If sText(XS).Contains("*------------------------------*") Then
                                AddLog("New project found, continue parsing")
                                xLocation = XS - 1
                                Exit For
                                'ElseIf (XS + 1) = sText.Length - 1 Then 
                                Exit For
                            ElseIf Not sText(XS + 1).Contains("*------------------------------*") And XS = sText.Length - 1 Then
                                AddLog("No new projects found in this logfile")
                                Exit Do
                            End If
                        Next
                    End If
                    xLocation += 1
                    If nIcon.Visible Then Application.DoEvents()
                End With
            Loop Until xLocation >= sText.Length - 1
            If My.Computer.FileSystem.FileExists(Application.StartupPath & "\__tmpparse.txt") Then My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\__tmpparse.txt", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.DoNothing)
            Return True
        Catch ex As Exception
            AddLog("Error : " & Err.Number & " - " & Err.Description)
            Return False
        End Try
    End Function
    Public dtUpdate As DateTime
    Public Enum eClientFile
        Tab = 1
        Spaces = 2
    End Enum
    Public Function ReadClients(ByVal KindOfFile As eClientFile) As Boolean
        Try
            colClients.Clear()
            cmbClient.Items.Clear()
            cmbClient.Items.Add("-- ALL CLIENTS--")
            cmbClient.Text = cmbClient.Items(0)
            Select Case KindOfFile
                Case eClientFile.Spaces
                    Using mRead As New Microsoft.VisualBasic.FileIO.TextFieldParser(xFile)
                        mRead.TextFieldType = FileIO.FieldType.Delimited
                        mRead.SetDelimiters("    ")
                        While Not mRead.EndOfData
                            Try
                                Dim currentRow As String()
                                If mRead.PeekChars(1) = "#" Or mRead.PeekChars(1) = " " Then
                                    currentRow = mRead.ReadFields()
                                    GoTo Skip2
                                End If
                                currentRow = mRead.ReadFields()
                                If currentRow.ToString.Contains("#") Then GoTo Skip2
                                Dim currentField As String, nClient As New sClients
                                nClient.Init() : Dim xStep As Integer : xStep = 1
                                For Each currentField In currentRow
                                    If xStep = 1 Then
                                        nClient.Name = currentField
                                    ElseIf xStep = 2 Then
                                        nClient.Location = currentField
                                        If Not My.Computer.FileSystem.DirectoryExists(currentField) Then
                                            AddLog("Folder " & currentField & " does not exists?!?")
                                        End If
                                    End If
                                    xStep += xStep
                                Next
                                If Not My.Computer.FileSystem.FileExists(nClient.Location & "fahlog.txt") Then
                                    AddLog("The logfile located at " & nClient.Location & " can not be found!")
                                End If
                                cmbClient.Items.Add(nClient.Name)
                                If nClient.IsLocal Then nClient.FillHWinfo()
                                colClients.Add(nClient, nClient.Name)
                                Application.DoEvents()
                            Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                                AddLog("Clientstab.txt is not spaces delimited")
                                Return False
                            End Try
Skip2:
                        End While
                    End Using
                    Return True
                Case eClientFile.Tab
                    Using mRead As New Microsoft.VisualBasic.FileIO.TextFieldParser(xFile)
                        mRead.TextFieldType = FileIO.FieldType.Delimited
                        mRead.SetDelimiters(vbTab)
                        While Not mRead.EndOfData
                            Try
                                Dim currentRow As String()
                                If mRead.PeekChars(1) = "#" Or mRead.PeekChars(1) = " " Then
                                    currentRow = mRead.ReadFields()
                                    GoTo Skip1
                                End If
                                currentRow = mRead.ReadFields()
                                If currentRow.ToString.Contains("#") Then GoTo Skip1
                                Dim currentField As String, nClient As New sClients
                                nClient.Init() : Dim xStep As Integer : xStep = 1
                                For Each currentField In currentRow
                                    If xStep = 1 Then
                                        nClient.Name = currentField
                                    ElseIf xStep = 2 Then
                                        nClient.Location = currentField
                                        If Not My.Computer.FileSystem.DirectoryExists(currentField) Then
                                            AddLog("Folder " & currentField & " does not exists?!?")
                                        End If
                                    End If
                                    xStep += xStep
                                Next
                                If Not My.Computer.FileSystem.FileExists(nClient.Location & "fahlog.txt") Then
                                    AddLog("The logfile located at " & nClient.Location & " can not be found!")
                                    'see... everything works as it should..
                                End If
                                cmbClient.Items.Add(nClient.Name)
                                If nClient.IsLocal Then nClient.FillHWinfo()
                                colClients.Add(nClient, nClient.Name)
                                Application.DoEvents()
                            Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                                AddLog("Clientstab.txt is not tab delimited")
                                Return False
                            End Try
Skip1:
                        End While
                    End Using
            End Select
            Return True
        Catch ex As Exception
            AddLog("Clientstab.txt reading error!")
            AddLog("Error: " & Err.Description)
            Return True

        End Try
    End Function
    Public Structure sClients
        Public NoWork As String
        Public Name As String
        Public Location As String
        Public ReadOnly Property IsLocal() As Boolean
            Get
                If Location = Nothing Then Return True
                AddLog("Locality query for " & Location)
                AddLog("*" & (Not InStr(Location, "\\")).ToString & " " & (Not InStr(Location, "//")).ToString)
                If Location.Contains("\\") = False And Location.Contains("//") = False Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property
        Public ReadOnly Property LogFile() As String
            Get
                If IsLocal Then
                    Return Location & "fahlog.txt"
                Else
                    Return Location & "FAHlog.txt"
                End If
            End Get
        End Property
        Public ReadOnly Property PrevLogFile() As String
            Get
                Return Location & "fahlog-prev.txt"
            End Get
        End Property
        Public ReadOnly Property Logfile2() As String
            Get
                Return Location & "fahlog2.txt"
            End Get
        End Property
        Public Structure sWU
            Public Sub ClearWU()
                StartTime = ""
                StartPercentage = ""
                EndTime = ""
                EndPercentage = ""
                Calling = ""
                Project = ""
                CoreStatus = ""
                CsTimeStamp = ""
            End Sub
            Public Sub ClearClient()
                Flags = ""
                Client = ""
                Location = ""
            End Sub
            Public Location As String
            Public Qslot As String
            Public CoreStatus As String
            Public CsTimeStamp As String
            Public Flags As String
            Public Compiler As String
            Public Board As String
            Public BuildHost As String
            Public colLOG As Collection
            Public LastCompleted As String
            Public StartTime As String
            Public StartPercentage As String
            Public EndTime As String
            Public EndPercentage As String
            Public Client As String
            Public Core As String
            Public Calling As String
            Public Project As String 'Project: 2665 (Run 2, Clone 469, Gen 62)
            Public ReadOnly Property Run() As String
                Get
                    If Project = Nothing Then Return ""
                    Dim tS As String = Mid(Project, InStr(Project, "Run "))
                    tS = Mid(tS, 1, tS.IndexOf(","))
                    Return tS
                End Get
            End Property
            Public ReadOnly Property Clone() As String
                Get
                    If Project = Nothing Then Return ""
                    Dim tS As String = Mid(Project, InStr(Project, "Clone "))
                    tS = Mid(tS, 1, tS.IndexOf(","))
                    Return tS
                End Get
            End Property
            Public ReadOnly Property Gen() As String
                Get
                    If Project = Nothing Then Return ""
                    Dim tS As String = Mid(Project, InStr(Project, "Gen "))
                    tS = Mid(tS, 1, tS.IndexOf(")"))
                    Return tS
                End Get
            End Property
            Public ReadOnly Property ProjectNumber() As String
                Get
                    Dim tS As String = Mid(Project, 10)
                    Return Mid(tS, 1, tS.IndexOf(" "))
                End Get
            End Property
            Public ReadOnly Property Clean() As Boolean
                Get
                    Return (CoreStatus = "CoreStatus = 64 (100)")
                End Get
            End Property

            Public CPU As sHWinfo.sCpu
            Public GPU As sHWinfo.sGpu
            Public Sub Init()
                colLOG = New Collection
            End Sub
        End Structure
        Public colWU As Collection
        Public WU As sWU
        Public Enum eClientType
            Gpu = 1
            Cpu = 2
        End Enum
        Public ClientType As eClientType
        Public Structure sHWinfo
            Public Structure sGpu
                Public Caption As String, Description As String, DriverVersion As String, DriverDate As String, AdapterRam As String, InfSection As String, Index As Integer
            End Structure
            Public colGPU As Collection
            Public GPU As sGpu
            Public Sub init()
                colGPU = New Collection
            End Sub
            Public Structure sCpu
                Public Name As String, Manufacturer As String, NumberOfLogicalProcessors As Integer, NumberOfCores As Integer, CurrentClockSpeed As String
            End Structure
            Public CPU As sCpu
            Public DeinoInstalled As Boolean
            Public MpichInstalled As Boolean
            Public DeinoRunningState As Integer
            Public MpichRunningState As Integer
            Public GoodFOO As Boolean
        End Structure
        Public hwInf As sHWinfo
        Public Sub FillHWinfo()
            Try
                hwInf.init()
                AddLog("Starting HWinfo")
                Dim mSearch As ManagementObjectSearcher
                Dim query As ObjectQuery, nSt As String
                Dim queryCollection As ManagementObjectCollection
                AddLog("Looking for processors")
                query = New ObjectQuery("SELECT * FROM Win32_Processor")
                mSearch = New ManagementObjectSearcher(query)
                queryCollection = mSearch.Get()
                For Each m As ManagementObject In queryCollection
                    AddLog("-found " & m.GetPropertyValue("Name"))
                    hwInf.CPU.Name = m.GetPropertyValue("Name")
                    hwInf.CPU.NumberOfCores = m.GetPropertyValue("NumberOfCores")
                    hwInf.CPU.CurrentClockSpeed = m.GetPropertyValue("CurrentClockSpeed")
                    hwInf.CPU.NumberOfLogicalProcessors = m.GetPropertyValue("NumberOfLogicalProcessors")
                    hwInf.CPU.Manufacturer = m.GetPropertyValue("Manufacturer")
                    Exit For
                Next
                AddLog("Looking for Graphic processors")
                query = New ObjectQuery("SELECT * FROM Win32_VideoController")
                mSearch = New ManagementObjectSearcher(query)
                queryCollection = mSearch.Get()
                For Each m As ManagementObject In queryCollection
                    If Mid(m.GetPropertyValue("PNPDeviceID"), 9, 4) = pciAti Or Mid(m.GetPropertyValue("PNPDeviceID"), 9, 4) = pciNvidia Then
                        '"PCI\VEN_10DE&DEV_0622&SUBSYS_23651682&REV_A1\4&F3A193A&0&0008" 0622 = 9600gt
                        Dim nGpu As New sHWinfo.sGpu
                        With nGpu
                            AddLog("-found at index " & hwInf.colGPU.Count & " ~ " & m.GetPropertyValue("Caption"))
                            .Index = hwInf.colGPU.Count
                            .Caption = m.GetPropertyValue("Caption")
                            .Description = m.GetPropertyValue("Description")
                            .AdapterRam = CInt(m.GetPropertyValue("AdapterRAM") / 1024 / 1024).ToString & "MB"
                            .DriverDate = m.GetPropertyValue("Driverdate")
                            .DriverVersion = m.GetPropertyValue("DriverVersion")
                            .InfSection = m.GetPropertyValue("InfSection")
                        End With
                        AddLog("Adding found gpu to collection")
                        hwInf.colGPU.Add(nGpu)
                    End If
                Next
            Catch ex As Exception
                AddLog("Exception occured in sub FillHWInfo")
                AddLog("*" & Err.Number & " - " & Err.Description)
                AddLog("*" & ex.Message)
                AddLog("*" & ex.Data.Values.ToString)
                Err.Clear()
            End Try
        End Sub
        Public Sub Init()
            colWU = New Collection
        End Sub
    End Structure
    Public colClients As New Collection
    Public Function CheckClientsFile() As Boolean
        Try
            AddLog("Checking for clients file")
            If My.Computer.FileSystem.FileExists(ClientsFileLocation) Then xFile = ClientsFileLocation
            If Not My.Computer.FileSystem.FileExists(xFile) Then
                AddLog("Can't find clientstab.txt, asking user")
                Dim ofDiag As New OpenFileDialog()
                ofDiag.InitialDirectory = Application.StartupPath
                ofDiag.Filter = "clientstab.txt|clientstab.txt"
                ofDiag.FilterIndex = 1
                ofDiag.RestoreDirectory = True
                If ofDiag.ShowDialog Then
                    If ofDiag.FileName = "" Then
                        AddLog("No file name selected, closing application")
                        Application.Exit()
                    End If
                    xFile = ofDiag.FileName
                Else
                    AddLog("No file name selected, closing application")
                    Application.Exit()
                End If
            End If
            ClientsFileLocation = xFile
            Return True
        Catch ex As Exception
            AddLog("Error : " & Err.Number & " - " & Err.Description)
            Return False
        End Try
    End Function
    Public Function ReadClients() As Boolean
        Try
            AddLog("Trying to use space delimiter for clientstab.txt")
            If Not ReadClients(eClientFile.Spaces) Then
                AddLog("Trying to use tab delimiter for clientstab.txt")
                If Not ReadClients(eClientFile.Tab) Then
                    MsgBox("Can not read clientstab.txt, please report the error and include the content of clientstab.txt thank you :)")
                    AddLog("Clientstab.txt is not readable in any expected format!")
                    Application.Exit()
                End If
                'MsgBox("Can not read clientstab.txt, please report the error and include the content of clientstab.txt thank you :)")
                'AddLog("Clientstab.txt is not readable in any expected format!")
                'Application.Exit()
            End If
            AddLog("Client read succes")
            Return True
        Catch ex As Exception
            AddLog("Error : " & Err.Number & " - " & Err.Description)
            Return False
        End Try
    End Function
    Public Function StartUpdate()

        Try
            CheckClientsFile()
            ReadClients()

            If nIcon.Visible Then
                nIcon.Icon = My.Resources.Red
                tIcon.Enabled = True
                Application.DoEvents()
            End If





            dtUpdate = DateTime.Now.AddMinutes(UpdateInterval)
            txtUpdate.Text = "Next update on: " & dtUpdate.ToShortTimeString
            cmbProject.Items.Clear()
            cmbProject.Items.Add("-- ALL PROJECTS --")
            cmbProject.Text = cmbProject.Items(0)
            Dim intTotal As Integer, intEUE As Integer
            For Each nC As sClients In colClients
                AddLog("Parsing fahlog.txt for " & nC.Name)
                ParseLog(nC, nC.LogFile)
                'ParseLog(nC, nC.Logfile2)
                ParseLog(nC, nC.PrevLogFile)
                AddLog("Parsing fahlog-prev.txt for " & nC.Name)
                intTotal += nC.colWU.Count
                AddLog("Total WU's in collection = " & intTotal.ToString)
                For Each nC.WU In nC.colWU
                    If nC.WU.CoreStatus <> "CoreStatus = 64 (100)" Then intEUE += 1
                Next
                AddLog("Total EUE wu's in collection = " & intEUE.ToString)
            Next
            Try
                If intEUE = 0 Then
                    intEUErate = 0
                Else
                    intEUErate = CInt(intEUE / (intTotal / 100))
                End If
            Catch ex As Exception : End Try
            txtEUE.Text = intEUE.ToString & " from " & intTotal.ToString & " (" & intEUErate & "%)"
            tIcon.Enabled = False
            nIcon.Icon = frmEUE.Icon
            _noBalloon = False
            If txtEUE.Text.Length > 0 Then nIcon.Text = txtEUE.Text
            If intEUErate > AlertRate Then frmEUE.Show()
            frmEUE._IsBussy = False
            Return True
        Catch ex As Exception
            AddLog("Error! : " & Err.Number & " - " & Err.Description)
            Return False
        End Try
    End Function
End Module
