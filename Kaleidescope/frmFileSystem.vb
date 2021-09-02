
Imports System.IO
Imports System.Windows.Forms
Public Class frmFileSystem

    Public Event FileSystem_Values_Changed(ByVal dir As String, ByVal filename As String, ByVal fullFileRef As String)
    Public File_Selected As Boolean

    Public ReadOnly Property Dir As String
        Get
            Return txtDir.Text
        End Get
    End Property

    Public ReadOnly Property FileName As String
        Get
            Return txtFileName.Text
        End Get
    End Property

    Public ReadOnly Property FullFileRef As String
        Get
            Return txtFullFileRef.Text
        End Get
    End Property


    Public Sub New()

        InitializeComponent()

    End Sub

    Public Sub New(ByVal filename_default As String, ByVal directory_default As String)

        InitializeComponent()

        txtDir.Text = directory_default
        txtFileName.Text = filename_default

    End Sub


    Private Sub btnDir_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDir.Click
        Get_Directory()
    End Sub

    Private Sub btnFullPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFullPath.Click
        Get_Full_File_Ref()
    End Sub

    Private Sub txtDir_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDir.TextChanged
        Build_Full_File_Ref()
    End Sub

    Private Sub txtFilename_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFileName.TextChanged
        Build_Full_File_Ref()
    End Sub

    Public Sub Set_Choose_Directory_Function(ByVal enabled As Boolean)

        btnDir.Enabled = enabled
        btnDir.Visible = enabled

    End Sub


    Private Sub Get_Directory()

        Dim result As DialogResult
        Dim fd As New FolderBrowserDialog

        result = fd.ShowDialog()

        If result = DialogResult.OK OrElse result = DialogResult.Yes Then _
        txtDir.Text = fd.SelectedPath

        RaiseEvent FileSystem_Values_Changed(txtDir.Text, txtFileName.Text, txtFullFileRef.Text)

    End Sub

    Private Sub Get_Full_File_Ref()

        Dim result As DialogResult
        Dim fd As New OpenFileDialog

        Dim dirDefault As String = txtDir.Text
        If My.Computer.FileSystem.DirectoryExists(dirDefault) Then fd.InitialDirectory = dirDefault

        result = fd.ShowDialog()

        If result = DialogResult.OK OrElse result = DialogResult.Yes Then

            Dim path As String = String.Empty
            Dim fileName As String = String.Empty

            File_Utilities.Split_Full_File_Ref_Into_Path_FileName(fd.FileName, path, fileName)

            txtDir.Text = path
            txtFileName.Text = fileName

            RaiseEvent FileSystem_Values_Changed(txtDir.Text, txtFileName.Text, txtFullFileRef.Text)

        End If

    End Sub

    Public Sub Build_Full_File_Ref()

        Dim dir As String = txtDir.Text.TrimEnd("\")
        txtFullFileRef.Text = String.Format("{0}\{1}", dir, txtFileName.Text)

    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        File_Selected = True
        Me.Close()
    End Sub

End Class