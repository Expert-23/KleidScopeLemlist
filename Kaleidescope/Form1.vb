Public Class Form1

    Private mMessage As Message

    Private mRandom As New System.Random
    Private selectedPath As String
    Private mLoading As Boolean

#Region " Form Events "

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Initialize_Form()
    End Sub

    Private Sub mnuItmMessage_Analyze_Click(sender As Object, e As EventArgs) Handles mnuItmMessage_Analyze.Click
        Analyze_Message()
    End Sub

    Private Sub mnuItmMessage_Check_Click(sender As Object, e As EventArgs) Handles mnuItmMessage_Check.Click
        Check_Message(txtOriginal_Body.Text)
    End Sub

    Private Sub mnuItmMessage_Morph_Click(sender As Object, e As EventArgs) Handles mnuItmMessage_Morph.Click
        Load_Next_Message_Version()
    End Sub

    Private Sub mnuItmVersion_Keep_Click(sender As Object, e As EventArgs) Handles mnuItmVersion_Keep.Click
        Keep_Version()
    End Sub

    Private Sub cboSubjects_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubjects.SelectedIndexChanged
        Changed_Subject_Variation()
    End Sub

    Private Sub cklbKept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cklbKept.SelectedIndexChanged
        Kept_Version_Selected()
    End Sub

    Private Sub mnuItmWip_Load_Click(sender As Object, e As EventArgs) Handles mnuItmWip_Load.Click
        Load_Message(False)
    End Sub

    Private Sub mnuItmWIP_Save_Click(sender As Object, e As EventArgs) Handles mnuItmWIP_Save.Click
        Save_Message()
    End Sub

    Private Sub mnuItmCampaign_Export_Click(sender As Object, e As EventArgs) Handles mnuItmCampaign_Export.Click
        Export_Versions()
    End Sub
    Private Sub AdHocToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdHocToolStripMenuItem.Click
        Run_Ad_Hoc()
    End Sub
    Private Sub cboSentence_Variation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSentence_Variation.SelectedIndexChanged
        Select_Sentence()
    End Sub
    Private Sub cboSentence_Number_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSentence_Number.SelectedIndexChanged
        Sentence_Change()
    End Sub
    Private Sub btnSentence_Substitute_Click(sender As Object, e As EventArgs) Handles btnSentence_Substitute.Click
        Change_Variation()
    End Sub

#End Region

#Region " Initialization"

    Private Sub Initialize_Form()

        Load_Test_Message()

    End Sub

    Private Sub Load_Test_Message()

        txtOriginal_Body.Text = My.Resources.Test0
        txtOriginal_Subject.Text = "Take advantage of some of the world's best talent half a world away"

    End Sub

    Private Sub Initialize_Message()
        mMessage = New Message(txtOriginal_Subject.Text, txtOriginal_Body.Text)
    End Sub

    Private Sub Load_Controls()
        Load_Controls_Subject()
        Load_Controls_Body()
    End Sub

    Private Sub Load_Controls_Subject()

        cboSubjects.Items.Clear()
        Dim seen As New SortedDictionary(Of Integer, String)
        For Each entry In mMessage.Sentences(0).Variations

            Dim number = entry.Key
            Dim text = entry.Value

            If Not seen.ContainsValue(text.Trim(" ")) Then
                seen.Add(number, text)
                cboSubjects.Items.Add(number)
            End If

        Next

        cboSubjects.SelectedItem = cboSubjects.Items(0)
        cboSubjects.Tag = seen

        txtSubject_Variations.Text = seen(0)
        txtSubject_Variations.Tag = 0

    End Sub

    Private Sub Load_Controls_Body()

    End Sub

    Private Sub Display_Kept_Versions()

        cklbKept.Items.Clear()

        For Each entry In mMessage.Versions

            cklbKept.Items.Add(entry.Value)

        Next

    End Sub

#End Region

#Region " Persistence "

    Private Sub Save_Message()
        Dim frm As New frmFileSystem("", "C:\Users\TOSHIBA\Desktop\Software And Files\Expert 23\Kaleidescope-main\DIR\")
        frm.ShowDialog()
        selectedPath = frm.FullFileRef()
        Dim success As Boolean
        Serialization_Utilities.Serialize_Object_And_Save_FileSystem(mMessage, selectedPath, success)
    End Sub

    Private Sub Load_Message(ByVal morph As Boolean)
        Dim success As Boolean

        If Not morph Then

            Dim frm As frmFileSystem = New frmFileSystem("", "C:\Users\TOSHIBA\Desktop\Software And Files\Expert 23\Kaleidescope-main\DIR\")
            frm.ShowDialog()
            selectedPath = frm.FullFileRef()
        End If

        If mMessage Is Nothing Then Serialization_Utilities.Load_Object_FileSystem_And_Deserialize(Of Message)(selectedPath, mMessage, success) : txtSubject_Variations.Text = mMessage.Sentences(0).Variations(3) : Exit Sub

        txtSubject_Variations.Text = mMessage.Sentences(0).Variations(3)
    End Sub

#End Region

    Private Sub Changed_Subject_Variation()

        If mLoading Then Exit Sub
        If cboSubjects.SelectedItem Is Nothing Then txtSubject_Variations.Text = String.Empty : Exit Sub

        Dim index As Integer = cboSubjects.SelectedItem
        Dim subjectText As String = cboSubjects.Tag(index)

        txtSubject_Variations.Text = subjectText
        txtSubject_Variations.Tag = index

    End Sub

    Private Sub Check_Message(ByVal content As String)

        Dim sb As New System.Text.StringBuilder
        For Each word In Version.Check_For_Spam_Phrases(content)

            sb.AppendLine(word)

        Next

        txtSpam.Text = sb.ToString

    End Sub

    Private Sub Analyze_Message()

        Initialize_Message()

        Dim s As New Scraper_QB
        Dim variants As Integer = 3
        s.Scrape(mMessage.Sentences, variants)

        Save_Message()

    End Sub

#Region " Message Version "

    Private Sub Load_Next_Message_Version()

        mLoading = True

        Load_Message(True)
        Load_Controls_Subject()

        mMessage.ID = mnuItmCampaign_Name.Text
        mMessage.Varied = Build_Version()

        Dim bodyText As String
        bodyText = mMessage.Varied.Body_Text

        txtVersion.Text = bodyText
        Check_Message(bodyText)

        mLoading = False

        cboSentence_Number.Items.Clear()
        For i = 0 To mMessage.Sentences.Count - 1
            cboSentence_Number.Items.Add(i.ToString())
        Next



    End Sub

    Private Function Build_Version() As Version

        Dim vers As New Version()

        With mMessage

            Dim permutation As New SortedDictionary(Of Integer, Integer)
            permutation = Select_Random_Permutation()

            vers = Version.Build_Version(.Sentences, permutation)

        End With

        Return vers

    End Function

    Private Function Select_Random_Permutation() As SortedDictionary(Of Integer, Integer)

        Dim permutation As New SortedDictionary(Of Integer, Integer) 'line number, variation chosen

        With mMessage

            For i = 1 To .Sentences.Count - 1

                Dim sentnce As Sentence = .Sentences(i)

                If sentnce.Variations.Count > 0 Then

                    Dim which As Integer = mRandom.Next(0, sentnce.Variations.Count)
                    permutation.Add(i, which)

                End If

            Next

        End With

        Return permutation

    End Function

    Private Sub Keep_Version()

        With mMessage

            If .Varied Is Nothing Then Exit Sub
            If .Varied.Message_Text = .Original.Message_Text Then Exit Sub

            .Sentences(0).Chosen_Variation = cboSubjects.SelectedIndex
            .Varied.Subject = .Sentences(0)

            .Versions.Add(.Versions.Count, .Varied)

            Display_Kept_Versions()

        End With

    End Sub

    Private Sub Kept_Version_Selected()

        If mLoading Then Exit Sub
        If cklbKept.SelectedItem Is Nothing Then Exit Sub

        Dim frm As New frmVersion(cklbKept.SelectedItem)
        frm.ShowDialog()

    End Sub

#End Region

    Private Sub Export_Versions()

        Dim driver As New Driver_Email()
        driver.Drive(mMessage)

    End Sub

    Private Sub Run_Ad_Hoc()

    End Sub

#Region "MA"
    Private Sub Select_Variation()
        cboSentence_Variation.Items.Clear()
        For j = 0 To mMessage.Sentences(cboSentence_Number.SelectedIndex).Variations.Count - 1
            cboSentence_Variation.Items.Add(j.ToString())
        Next
    End Sub
    Private Sub Sentence_Change()
        Select_Variation()
        Select_Sentence_Default()
    End Sub
    Private Sub Select_Sentence()
        TextBox3.Text = mMessage.Sentences(cboSentence_Number.SelectedIndex).Variations(cboSentence_Variation.SelectedIndex)
    End Sub
    Private Sub Select_Sentence_Default()
        TextBox3.Text = mMessage.Sentences(cboSentence_Number.SelectedIndex).Variations(0)
    End Sub
    Private Sub Change_Variation()
        Dim senteceIndex As Integer = cboSentence_Number.SelectedIndex
        Dim variationIndex As Integer = cboSentence_Variation.SelectedIndex
        If variationIndex = -1 Then mMessage.Sentences(senteceIndex).Variations(0) = TextBox3.Text Else mMessage.Sentences(senteceIndex).Variations(variationIndex) = TextBox3.Text
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Load_Message(True)
        Dim frm As frmLemlist = New frmLemlist(mMessage)
        frm.ShowDialog()
    End Sub


#End Region

#Region " Old Code "

    'Private Sub Analyze_Message()

    '    Dim w As New Wrangler
    '    Dim sentences As SortedDictionary(Of Integer, String)
    '    sentences = w.Preprocess_Text(txtOriginal_Subject.Text, txtOriginal_Body.Text)

    '    Dim variations As SortedDictionary(Of Integer, SortedDictionary(Of Integer, String))
    '    Dim s As New Scraper_QB
    '    Dim variants As Integer = 3
    '    variations = s.Scrape(sentences, variants)

    '    Dim success As Boolean
    '    Dim filename As String = "G:\Code\keep.bin"
    '    Serialization_Utilities.Serialize_Object_And_Save_FileSystem(variations, filename, success)

    'End Sub


    'Private Sub Load_Next_Message_Version()

    '    mLoading = True

    '    Dim w As New Wrangler
    '    Dim sentences As SortedDictionary(Of Integer, String)
    '    sentences = w.Preprocess_Text(txtOriginal_Subject.Text, txtOriginal_Body.Text)

    '    Dim success As Boolean
    '    Dim filename As String = "G:\Code\keep.bin"
    '    Dim variations As SortedDictionary(Of Integer, SortedDictionary(Of Integer, String))
    '    Serialization_Utilities.Load_Object_FileSystem_And_Deserialize(Of SortedDictionary(Of Integer, SortedDictionary(Of Integer, String)))(filename, variations, success)

    '    Dim email As New System.Text.StringBuilder

    '    cboSubjects.Items.Clear()
    '    Dim seen As New List(Of String)
    '    For Each entry In variations(0)

    '        Dim item As New Variation
    '        item.Number = entry.Key
    '        item.Text = entry.Value
    '        If Not seen.Contains(entry.Value) Then
    '            seen.Add(entry.Value)
    '            cboSubjects.Items.Add(item)
    '        End If

    '    Next

    '    cboSubjects.SelectedItem = cboSubjects.Items(0)


    '    For i = 1 To sentences.Count - 1

    '        Dim chosen As String = sentences(i)
    '        Dim possibles = variations(i)

    '        If possibles.Count > 0 Then

    '            Dim which As Integer = mRandom.Next(0, possibles.Count)
    '            chosen = possibles(which) & "."

    '        End If

    '        email.AppendLine(chosen)
    '        email.AppendLine()

    '    Next

    '    Dim variation As String = email.ToString

    '    txtVersion.Text = variation

    '    Check_Message(variation)

    '    mLoading = False

    'End Sub

#End Region


End Class
