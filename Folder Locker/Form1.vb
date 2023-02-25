Imports System.Text

Public Class Form1
    Dim rawPassword As String = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Folder Locker").GetValue("Password")
    Dim Password As String = Decrypt(rawPassword)
    Dim Dir As String = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Folder Locker").GetValue("DirPath")
    Dim Unlocked As Boolean = False

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        
        If Not TextBox1.Text = Password Then
            MsgBox("Password non valida", MsgBoxStyle.Critical, "Attenzione") : Exit Sub
        Else
            RunCommand(" -h -s HTGLocker")
            My.Computer.FileSystem.RenameDirectory(Dir & "\HTGLocker", "Private")
            MsgBox("Cartella Sbloccata con Successo!", MsgBoxStyle.Information)
            Button1.Enabled = False
            Button2.Enabled = True
            TextBox1.ReadOnly = True
            Unlocked = True
        End If

    End Sub

    Public Function Decrypt(Decryption As String) As String
        Dim decrypttext As String = String.Empty
        Dim encodedtxt As New UTF8Encoding()
        Dim decode As Decoder = encodedtxt.GetDecoder()
        Dim code_byte As Byte() = Convert.FromBase64String(Decryption)
        Dim charcount As Integer = decode.GetCharCount(code_byte, 0, code_byte.Length)
        Dim decode_char As Char() = New Char(charcount - 1) {}
        decode.GetChars(code_byte, 0, code_byte.Length, decode_char, 0)
        decrypttext = New String(decode_char)

        Return decrypttext
    End Function

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Unlocked = True Then
            If MsgBox("Chiudendo il programma la cartella verrà ribloccata, procedere?", vbYesNo + MsgBoxStyle.Exclamation, "Attenzione") = MsgBoxResult.Yes Then
                My.Computer.FileSystem.RenameDirectory(Dir & "\Private", "HTGLocker")
                RunCommand(" +h +s HTGLocker")
                End

            Else
                e.Cancel = True
            End If
        Else
            End
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Computer.FileSystem.DirectoryExists(Dir & "\HTGLocker") = True Then
            RunCommand(" +h +s HTGLocker")
        Else
            MkDir(Dir & "\HTGLocker")
            RunCommand(" +h +s HTGLocker")
        End If

        Label3.Text = "La cartella protetta da password si trova in " & Dir
    End Sub

    Function RunCommand(ByVal Arguments As String) As String
        Try

            Dim My_Process As New Process()
            Dim My_Process_Info As New ProcessStartInfo()

            My_Process_Info.FileName = "attrib.exe" ' Process filename
            My_Process_Info.Arguments = Arguments ' Process arguments
            My_Process_Info.WorkingDirectory = Dir 'this directory can be different in your case.
            My_Process_Info.CreateNoWindow = True  ' Show or hide the process Window
            My_Process_Info.UseShellExecute = False ' Don't use system shell to execute the process
            My_Process_Info.RedirectStandardOutput = True  '  Redirect (1) Output
            My_Process_Info.RedirectStandardError = True  ' Redirect non (1) Output

            My_Process.EnableRaisingEvents = True ' Raise events
            My_Process.StartInfo = My_Process_Info
            My_Process.Start() ' Run the process NOW

            Dim Process_ErrorOutput As String = My_Process.StandardOutput.ReadToEnd() ' Stores the Error Output (If any)
            Dim Process_StandardOutput As String = My_Process.StandardOutput.ReadToEnd() ' Stores the Standard Output (If any)

            ' Return output by priority
            If Process_ErrorOutput IsNot Nothing Then Return Process_ErrorOutput ' Returns the ErrorOutput (if any)
            If Process_StandardOutput IsNot Nothing Then Return Process_StandardOutput ' Returns the StandardOutput (if any)

        Catch ex As Exception
            Return ex.Message
        End Try

        Return "OK"

    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        My.Computer.FileSystem.RenameDirectory(Dir & "\Private", "HTGLocker")
        RunCommand(" +h +s HTGLocker")
        MsgBox("Cartella Bloccata con Successo", MsgBoxStyle.Information)
        Button2.Enabled = False
        Button1.Enabled = True
        Unlocked = False
        TextBox1.Clear()
        TextBox1.ReadOnly = False
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        NotifyIcon1.Visible = True
        Me.Hide()

    End Sub
End Class
