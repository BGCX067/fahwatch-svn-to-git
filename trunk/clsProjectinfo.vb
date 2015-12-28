Imports System.Xml
Imports System.Xml.XPath

Public Class clsProjectInfo

#Region "Declarations for collections and file downloading"

    Private Const urlPsummary As String = "http://fah-web.stanford.edu/psummary.html"
    Private Const urlPsummaryC As String = "http://fah-web.stanford.edu/psummaryC.html"
    'Temp file holder
    Private fileTemp As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\FAHwatch\TmpProjects.tmp"
    Private fileXML As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\FAHwatch\Projects.xml"
    'Parsing constants
    Private Const parseHeader As String = "<TD>Contact</TD>"
    Private Const parseEnd As String = "</TABLE>"
    'private properties
    Shared colProjectNumber As New Collection
    Shared colServerIP As New Collection
    Shared colWorkUnitName As New Collection
    Shared colNumberOfAtoms As New Collection
    Shared colPreferredDays As New Collection
    Shared colFinalDeadline As New Collection
    Shared colCredit As New Collection
    Shared colFrames As New Collection
    Shared colCode As New Collection
    Shared colDescriptionURL As New Collection
    Shared colContact As New Collection
    Shared intProjectCount As Int16 = 0
    Public Enum eSource
        Psummary = 1
        PsummaryC = 2
    End Enum

    Public Function GetProjects(ByVal Source As eSource, Optional ByVal xtraFile As String = "") As Boolean
        'check for network
        Try
            AddLog("Start getting projects")
            If Not My.Computer.Network.IsAvailable Then
                AddLog("No network available")
                Return False
            End If
            'Clean collections
            intProjectCount = 0
            colProjectNumber.Clear() : colServerIP.Clear() : colWorkUnitName.Clear()
            colNumberOfAtoms.Clear() : colPreferredDays.Clear() : colFinalDeadline.Clear()
            colCredit.Clear() : colFrames.Clear() : colCode.Clear() : colDescriptionURL.Clear() : colContact.Clear()
            Dim allTEXT As String
            'Start with normal psummary
            If My.Computer.FileSystem.FileExists(fileTemp) Then My.Computer.FileSystem.DeleteFile(fileTemp)
            If Source = eSource.Psummary Then
                Try
                    AddLog("Download psummary file")
                    My.Computer.Network.DownloadFile(urlPsummary, fileTemp)
                Catch ex As Exception
                    AddLog("Error while downloading psummary")
                    AddLog("*" & Err.Number & " - " & Err.Description)
                    AddLog("*" & ex.Message)
                    Err.Clear()
                    Return False
                End Try
                allTEXT = My.Computer.FileSystem.ReadAllText(fileTemp)
                If InStr(allTEXT, parseHeader) = 0 Then
                    AddLog("File does not seem to contain any project info")
                    Return False
                    Exit Function
                Else
                    allTEXT = Mid(allTEXT, InStr(allTEXT, parseHeader) + Len(parseHeader))
                End If
                ParseProjects(allTEXT)
            ElseIf Source = eSource.PsummaryC Then
                Try
                    AddLog("Download psummaryC file")
                    My.Computer.Network.DownloadFile(urlPsummaryC, fileTemp)
                Catch ex As Exception
                    AddLog("Error while downloading psummary")
                    AddLog("*" & Err.Number & " - " & Err.Description)
                    AddLog("*" & ex.Message)
                    Err.Clear()
                    Return False
                End Try
                allTEXT = My.Computer.FileSystem.ReadAllText(fileTemp)
                If InStr(allTEXT, parseHeader) = 0 Then
                    Return False
                    Exit Function
                Else
                    allTEXT = Mid(allTEXT, InStr(allTEXT, parseHeader) + Len(parseHeader))
                End If
                ParseProjects(allTEXT)
            End If
            If My.Computer.FileSystem.FileExists(fileTemp) Then My.Computer.FileSystem.DeleteFile(fileTemp)
            If xtraFile = "" Then
                WriteXML()
                AddLog("Finish getting projects")
                Return True
            End If
            allTEXT = My.Computer.FileSystem.ReadAllText(xtraFile)
            If InStr(allTEXT, parseHeader) = 0 Then
                WriteXML()
                Return True
                Exit Function
            Else
                allTEXT = Mid(allTEXT, InStr(allTEXT, parseHeader) + Len(parseHeader))
            End If
            ParseProjects(allTEXT)
            WriteXML()
            AddLog("Finish getting projects")
            Return True
        Catch ex As Exception
            AddLog("Error while downloading project files")
            AddLog("*" & Err.Number & " - " & Err.Description)
            AddLog("*" & ex.Message)
            Err.Clear()
            Return False
        End Try
    End Function

    Private Sub ParseProjects(ByVal allText As String)
        Dim colKey As String ' every collection key is the projectnumber
        Dim strTmp As String ' needed because parsing in one time is sh1t
        AddLog("Parsing projects")
        Do
            'Cut to after <TD>
            Try
                allText = Mid(allText, InStr(allText, "<TD>") + 4)
                colKey = Mid(allText, 1, InStr(allText, "</TD>") - 1).Trim
                If Not colProjectNumber.Contains(colKey) Then
                    colProjectNumber.Add(colKey, colKey)
                    'Cut to after <TD>
                    allText = Mid(allText, InStr(allText, "<TD>") + 4)
                    If Mid(allText, 1, InStr(allText, "</TD>") - 1).Trim <> "" Then
                        colServerIP.Add(Mid(allText, 1, InStr(allText, "</TD>") - 1).Trim, colKey)
                    Else
                        colServerIP.Add("", colKey)
                    End If
                    'Cut to after <TD>
                    allText = Mid(allText, InStr(allText, "<TD>") + 4)
                    If Mid(allText, 1, InStr(allText, "</TD>") - 1).Trim <> "" Then
                        colWorkUnitName.Add(Mid(allText, 1, InStr(allText, "</TD>") - 1).Trim, colKey)
                    Else
                        colWorkUnitName.Add("", colKey)
                    End If
                    'Cut to after <TD>
                    allText = Mid(allText, InStr(allText, "<TD>") + 4)
                    If Mid(allText, 1, InStr(allText, "</TD>") - 1).Trim <> "" Then
                        colNumberOfAtoms.Add(Mid(allText, 1, InStr(allText, "</TD>") - 1).Trim, colKey)
                    Else
                        colNumberOfAtoms.Add("", colKey)
                    End If
                    'Cut to after <TD>
                    allText = Mid(allText, InStr(allText, "<TD>") + 4)
                    If Mid(allText, 1, InStr(allText, "</TD>") - 1).Trim <> "" Then
                        colPreferredDays.Add(Mid(allText, 1, InStr(allText, "</TD>") - 1).Trim, colKey)
                    Else
                        colPreferredDays.Add("", colKey)
                    End If
                    'Cut to after <TD>
                    allText = Mid(allText, InStr(allText, "<TD>") + 4)
                    If Mid(allText, 1, InStr(allText, "</TD>") - 1).Trim <> "" Then
                        colFinalDeadline.Add(Mid(allText, 1, InStr(allText, "</TD>") - 1).Trim, colKey)
                    Else
                        colFinalDeadline.Add("", colKey)
                    End If
                    'Cut to after <TD>
                    allText = Mid(allText, InStr(allText, "<TD>") + 4)
                    If Mid(allText, 1, InStr(allText, "</TD>") - 1).Trim <> "" Then
                        colCredit.Add(Mid(allText, 1, InStr(allText, "</TD>") - 1).Trim, colKey)
                    Else
                        colCredit.Add("", colKey)
                    End If
                    'Cut to after <TD>
                    allText = Mid(allText, InStr(allText, "<TD>") + 4)
                    If Mid(allText, 1, InStr(allText, "</TD>") - 1).Trim <> "" Then
                        colFrames.Add(Mid(allText, 1, InStr(allText, "</TD>") - 1).Trim, colKey)
                    Else
                        colFrames.Add("", colKey)
                    End If
                    'Cut to after <TD>
                    allText = Mid(allText, InStr(allText, "<TD>") + 4)
                    If Mid(allText, 1, InStr(allText, "</TD>") - 1).Trim <> "" Then
                        colCode.Add(Mid(allText, 1, InStr(allText, "</TD>") - 1).Trim, colKey)
                    Else
                        colCode.Add("", colKey)
                    End If
                    'Cut to after <TD>
                    allText = Mid(allText, InStr(allText, "<TD>") + 4)
                    strTmp = Mid(allText, 1, InStr(allText, "</TD>") - 1).Trim
                    'Looks like "<a href=http://fah-web.stanford.edu/cgi-bin/fahproject?p=772>Description</a>"
                    If Mid(allText, 1, InStr(allText, "</TD>") - 1).Trim <> "" Then
                        colDescriptionURL.Add(Mid(strTmp, InStr(strTmp, "http"), InStr(strTmp, ">Des") - 9), colKey)
                    Else
                        colDescriptionURL.Add("", colKey)
                    End If
                    'Cut to after <TD>
                    allText = Mid(allText, InStr(allText, "<TD>") + 4)
                    strTmp = Mid(allText, 1, InStr(allText, "</TD>") - 1).Trim
                    'looks like "<font size=-1>vvishal</font>"
                    strTmp = Mid(strTmp, InStr(strTmp, ">") + 1)
                    colContact.Add(Mid(strTmp, 1, Len(strTmp) - 7), colKey)
                Else
                    'cut alltext
                    For tentimes As Int16 = 1 To 10
                        allText = Mid(allText, InStr(allText, "<TD>") + 4)
                    Next
                End If
            Catch ex As Exception
                AddLog("Error while parsing projects from file")
                AddLog("*" & Err.Number & " - " & Err.Description)
                AddLog("*" & ex.Message)
                Err.Clear()
                Exit Do
            End Try
            If InStr(allText, "<TD>") = 0 Then Exit Do
        Loop
    End Sub

    Private Sub WriteXML()
        Try
            AddLog("Writing projects to xml file")
            If My.Computer.FileSystem.FileExists(fileXML) Then My.Computer.FileSystem.DeleteFile(fileXML)
            Dim xSettings As New XmlWriterSettings
            xSettings.Indent = True
            xSettings.IndentChars = "    "
            Dim xWriter As XmlWriter = XmlWriter.Create(fileXML, xSettings)
            xWriter.WriteStartElement("Projects")
            xWriter.WriteStartElement("Count")
            xWriter.WriteString(colProjectNumber.Count.ToString)
            xWriter.WriteEndElement()
            For x As Integer = 1 To colProjectNumber.Count
                With xWriter
                    .WriteStartElement("Project")
                    .WriteStartElement("Projectnumber")
                    .WriteString(colProjectNumber(x).ToString)
                    .WriteEndElement()
                    .WriteStartElement("ServerIP")
                    .WriteString(colServerIP(x).ToString)
                    .WriteEndElement()
                    .WriteStartElement("WorkunitName")
                    .WriteString(colWorkUnitName(x).ToString)
                    .WriteEndElement()
                    .WriteStartElement("NumberOfAtoms")
                    .WriteString(colNumberOfAtoms(x).ToString)
                    .WriteEndElement()
                    .WriteStartElement("PreferredDays")
                    .WriteString(colPreferredDays(x).ToString)
                    .WriteEndElement()
                    .WriteStartElement("FinalDeadline")
                    .WriteString(colFinalDeadline(x).ToString)
                    .WriteEndElement()
                    .WriteStartElement("Credit")
                    .WriteString(colCredit(x).ToString)
                    .WriteEndElement()
                    .WriteStartElement("Frames")
                    .WriteString(colFrames(x).ToString)
                    .WriteEndElement()
                    .WriteStartElement("Code")
                    .WriteString(colCode(x).ToString)
                    .WriteEndElement()
                    .WriteStartElement("Description")
                    .WriteString(colDescriptionURL(x).ToString)
                    .WriteEndElement()
                    .WriteStartElement("Contact")
                    .WriteString(colContact(x).ToString)
                    .WriteEndElement()
                    .WriteEndElement()
                End With
            Next
            xWriter.WriteEndElement()
            xWriter.Close()
            xWriter = Nothing
            AddLog("Finished writing projects")
        Catch ex As Exception
            AddLog("Error while writing projects.xml")
            AddLog("*" & Err.Number & " - " & Err.Description)
            AddLog("*" & ex.Message)
            Err.Clear()
        End Try
    End Sub

    Private Sub ReadXML()
        Try
            AddLog("Starting to read projects xml")
            Dim Xreader As XmlReader = XmlReader.Create(fileXML)
            Dim colKey As String, intCount As Int16
            'Clean collections
            intProjectCount = 0
            colProjectNumber.Clear() : colServerIP.Clear() : colWorkUnitName.Clear()
            colNumberOfAtoms.Clear() : colPreferredDays.Clear() : colFinalDeadline.Clear()
            colCredit.Clear() : colFrames.Clear() : colCode.Clear() : colDescriptionURL.Clear() : colContact.Clear()
            Xreader.ReadStartElement("Projects")
            Xreader.ReadStartElement("Count")
            intCount = CInt(Xreader.ReadString)
            Xreader.ReadEndElement()
            AddLog("Projects expected: " & intCount)
            For Counter As Int16 = 1 To intCount
                With Xreader
                    .ReadStartElement("Project")
                    .ReadStartElement("Projectnumber")
                    colKey = .ReadString
                    colProjectNumber.Add(colKey, colKey)
                    .ReadEndElement()
                    .ReadStartElement("ServerIP")
                    colServerIP.Add(.ReadString, colKey)
                    .ReadEndElement()
                    .ReadStartElement("WorkunitName")
                    colWorkUnitName.Add(.ReadString, colKey)
                    .ReadEndElement()
                    .ReadStartElement("NumberOfAtoms")
                    colNumberOfAtoms.Add(.ReadString, colKey)
                    .ReadEndElement()
                    .ReadStartElement("PreferredDays")
                    colPreferredDays.Add(.ReadString, colKey)
                    .ReadEndElement()
                    .ReadStartElement("FinalDeadline")
                    colFinalDeadline.Add(.ReadString, colKey)
                    .ReadEndElement()
                    .ReadStartElement("Credit")
                    colCredit.Add(.ReadString, colKey)
                    .ReadEndElement()
                    .ReadStartElement("Frames")
                    colFrames.Add(.ReadString, colKey)
                    .ReadEndElement()
                    .ReadStartElement("Code")
                    colCode.Add(.ReadString, colKey)
                    .ReadEndElement()
                    .ReadStartElement("Description")
                    colDescriptionURL.Add(.ReadString, colKey)
                    .ReadEndElement()
                    .ReadStartElement("Contact")
                    colContact.Add(.ReadString, colKey)
                    .ReadEndElement()
                    .ReadEndElement()
                End With
            Next
            Xreader.Close()
            Xreader = Nothing
            AddLog("Finished reading projects from xml")
        Catch ex As Exception
            AddLog("Error while writing projects.xml")
            AddLog("*" & Err.Number & " - " & Err.Description)
            AddLog("*" & ex.Message)
            Err.Clear()
        End Try
    End Sub

#End Region

#Region "Project info properties"

    Public ReadOnly Property ServerIP(ByVal Projectnumber As String) As String
        Get
            ServerIP = colServerIP(Projectnumber).ToString
        End Get
    End Property
    Public ReadOnly Property WorkUnitName(ByVal ProjectNumber As String) As String
        Get
            WorkUnitName = colWorkUnitName(ProjectNumber).ToString
        End Get
    End Property
    Public ReadOnly Property NumberOfAtoms(ByVal ProjectNumber As String) As String
        Get
            NumberOfAtoms = colNumberOfAtoms(ProjectNumber).ToString
        End Get
    End Property
    Public ReadOnly Property PreferredDays(ByVal ProjectNumber As String) As String
        Get
            PreferredDays = colPreferredDays(ProjectNumber).ToString
        End Get
    End Property
    Public ReadOnly Property FinalDeadline(ByVal ProjectNumber As String) As String
        Get
            FinalDeadline = colFinalDeadline(ProjectNumber).ToString
        End Get
    End Property
    Public ReadOnly Property Credit(ByVal ProjectNumber As String) As String
        Get
            Credit = colCredit(ProjectNumber).ToString
        End Get
    End Property
    Public ReadOnly Property Frames(ByVal ProjectNumber As String) As String
        Get
            Frames = colFrames(ProjectNumber).ToString
        End Get
    End Property
    Public ReadOnly Property Code(ByVal ProjectNumber As String) As String
        Get
            Code = colCode(ProjectNumber).ToString
        End Get
    End Property
    Public ReadOnly Property DescriptionURL(ByVal ProjectNumber As String) As String
        Get
            DescriptionURL = colDescriptionURL(ProjectNumber).ToString
        End Get
    End Property
    Public ReadOnly Property Contact(ByVal ProjectNumber As String) As String
        Get
            Contact = colContact(ProjectNumber).ToString
        End Get
    End Property
    Public ReadOnly Property ProjectKnown(ByVal ProjectNumber As String) As Boolean
        Get
            If colProjectNumber.Contains(ProjectNumber) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    Public ReadOnly Property Project(ByVal Index As Integer) As String
        Get
            Project = colProjectNumber(Index).ToString
        End Get
    End Property
    Public ReadOnly Property ProjectCount() As Integer
        Get
            ProjectCount = colProjectNumber.Count
        End Get
    End Property

#End Region

#Region "Class starting point"

    Public Sub New()
        If My.Computer.FileSystem.FileExists(fileXML) Then
            ReadXML()
        Else
            GetProjects(eSource.Psummary)
        End If
    End Sub

#End Region

End Class
