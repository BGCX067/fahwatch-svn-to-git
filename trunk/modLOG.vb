Module modLOG
    Public Logging As New frmLOG
    Public Function AddLog(ByVal Message As String)
        Try
            AddLine(Logging.rtLOG, Message)
            Return True
        Catch ex As Exception
            MsgBox("Error while trying to write to the log file." & vbNewLine & vbNewLine & Err.Number & " : " & Err.Description)
            Err.Clear()
            Return False
        End Try
    End Function
    Private Const WM_VSCROLL As Int32 = &H115
    Private Const SB_BOTTOM As Int32 = 7

    Private Declare Auto Function SendMessage Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal wMsg As Int32, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr

    Private Sub AddLine(ByVal Destination As RichTextBox, ByVal Text As String)
        With Destination
            Text = DateTime.Now.ToShortTimeString & " : " & Text
            .AppendText(Text & ControlChars.NewLine)
            SendMessage(Destination.Handle, WM_VSCROLL, New IntPtr(SB_BOTTOM), IntPtr.Zero)
        End With
    End Sub

End Module
