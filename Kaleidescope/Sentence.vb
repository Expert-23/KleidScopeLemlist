<Serializable>
Public Class Sentence

    Public Number As Integer
    Public Chosen_Variation As Integer

    Public Variations As SortedDictionary(Of Integer, String)

    Public ReadOnly Property Original As String
        Get
            If Variations Is Nothing OrElse Variations.Count = 0 Then
                Variations = New SortedDictionary(Of Integer, String)
                Variations.Add(0, String.Empty)
            End If
            Return Variations(0)
        End Get
    End Property

    Public Sub New(Optional ByVal orig As String = "")

        Initialize_Sentence(orig)

    End Sub

    Private Sub Initialize_Sentence(Optional ByVal orig As String = "")

        Number = -1

        Chosen_Variation = 0

        Variations = New SortedDictionary(Of Integer, String)
        Variations.Add(0, orig)

    End Sub

    Public Shared Function text2Sentences(ByVal text As String) As SortedDictionary(Of Integer, Sentence)

        Dim sentences As New SortedDictionary(Of Integer, Sentence)

        Dim w As New Wrangler
        For Each entry In w.Preprocess_Text(text)

            Dim item As New Sentence(entry.Value)
            sentences.Add(sentences.Count, item)

        Next

        Return sentences

    End Function

    Public Overrides Function ToString() As String

        Dim result As String = Original
        If Variations.ContainsKey(Chosen_Variation) Then result = Variations(Chosen_Variation)
        Return result

    End Function

End Class
