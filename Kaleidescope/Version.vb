<Serializable>
Public Class Version

    Public ID As String

    Public Subject As Sentence
    Public Body As SortedDictionary(Of Integer, Sentence)

    Public ReadOnly Property ID_Unique As String
        Get
            Dim sb As New System.Text.StringBuilder
            For Each varNum In Permutation.Values
                sb.Append(varNum)
            Next
            Return sb.ToString
        End Get
    End Property

    Public ReadOnly Property Permutation As SortedDictionary(Of Integer, Integer) 'sentence number, variation chosen
        Get
            Dim chosen As New SortedDictionary(Of Integer, Integer)
            With chosen

                .Add(0, Subject.Chosen_Variation)

                For Each sentnce In Body.Values

                    .Add(.Count, sentnce.Chosen_Variation)

                Next

            End With
            Return chosen
        End Get
    End Property

    Public ReadOnly Property Body_Text As String
        Get
            If Body Is Nothing Then
                Body = New SortedDictionary(Of Integer, Sentence)
            End If
            Return sentences2Text(Body)
        End Get
    End Property

    Public ReadOnly Property Message_Text As String
        Get
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("Subject: " & Subject.ToString)
            sb.AppendLine()
            sb.Append(Body_Text)
            Return sb.ToString
        End Get
    End Property

    Public ReadOnly Property Spam_Triggers As String
        Get
            Dim sb As New System.Text.StringBuilder
            For Each word In Check_For_Spam_Phrases(Message_Text)

                sb.AppendLine(word)

            Next
            Return sb.ToString
        End Get
    End Property

    Public ReadOnly Property Spam_Trigger_Collection As List(Of String)
        Get
            Return Check_For_Spam_Phrases(Message_Text)
        End Get
    End Property

    Public Sub New()

        Initialize_Version()

    End Sub

    Public Sub New(ByVal subj As String, ByVal _body As String, Optional ByVal _id As String = "")

        Initialize_Version()

        ID = _id
        Subject = Sentence.text2Sentences(subj)(0)
        Body = Sentence.text2Sentences(_body)

    End Sub

    Private Sub Initialize_Version()

        ID = String.Empty

        Subject = New Sentence
        Body = New SortedDictionary(Of Integer, Sentence)

    End Sub

    Private Shared Function sentences2Text(ByVal sentences As SortedDictionary(Of Integer, Sentence)) As String

        Dim sb As New System.Text.StringBuilder

        For Each sentence In sentences.Values

            sb.AppendLine(sentence.ToString)
            sb.AppendLine()

        Next

        Return sb.ToString

    End Function

    Public Shared Function Check_For_Spam_Phrases(ByVal text As String) As List(Of String)

        Dim spotted As New List(Of String)

        For Each phrase In Spam_Phrases().Values

            Dim target As String = " " & phrase & " "

            If text.ToLower.Contains(target) Then
                If Not spotted.Contains(phrase) Then spotted.Add(phrase)
            End If

        Next

        Return spotted

    End Function

    Public Shared Function Build_Version(ByVal sentences As SortedDictionary(Of Integer, Sentence),
                                         ByVal permutations As SortedDictionary(Of Integer, Integer)) As Version

        Dim vers As New Version

        With vers

            .Subject = sentences(0)

            For i = 1 To permutations.Count

                If sentences.ContainsKey(i) Then

                    If permutations.ContainsKey(i) Then

                        Dim chosen As Integer = permutations(i)
                        sentences(i).Chosen_Variation = chosen

                        .Body.Add(.Body.Count, sentences(i))

                    End If

                End If

            Next

        End With

        Return vers

    End Function

    Public Overrides Function ToString() As String

        Return String.Format("{0}: {1}", ID_Unique, Subject.ToString)

    End Function

End Class
