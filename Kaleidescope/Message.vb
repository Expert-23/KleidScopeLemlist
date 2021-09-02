<Serializable>
Public Class Message

    Public ID As String

    Public Original As Version
    Public Varied As Version

    Public Sentences As SortedDictionary(Of Integer, Sentence)
    Public Versions As SortedDictionary(Of Integer, Version)

    Public ReadOnly Property Original_Subject As Sentence
        Get
            If Sentences Is Nothing OrElse Sentences.Count = 0 Then
                Sentences = New SortedDictionary(Of Integer, Sentence)
                Sentences.Add(0, New Sentence)
            End If
            Return Sentences(0)
        End Get
    End Property

    Public Sub New(Optional ByVal subj As String = "",
                   Optional ByVal body As String = "")

        Initialize_Message(subj, body)

    End Sub

    Private Sub Initialize_Message(Optional ByVal subj As String = "",
                                   Optional ByVal body As String = "")

        ID = -1

        Original = New Version(subj, body, "original")
        Varied = New Version
        Versions = New SortedDictionary(Of Integer, Version)

        Build_Sentences(subj, body)

    End Sub

    Private Sub Build_Sentences(ByVal subj As String, ByVal body As String)


        Sentences = New SortedDictionary(Of Integer, Sentence)

        Dim sentence_subject As Sentence
        sentence_subject = Sentence.text2Sentences(subj)(0)
        Sentences.Add(0, sentence_subject)

        Dim sentences_body As SortedDictionary(Of Integer, Sentence)
        sentences_body = Sentence.text2Sentences(body)

        For Each sentnce In sentences_body.Values

            Sentences.Add(Sentences.Count, sentnce)

        Next

    End Sub

    Public Overrides Function ToString() As String

        Return String.Format("{0}: {1}", ID, Original.Subject.ToString)

    End Function

    End Class

