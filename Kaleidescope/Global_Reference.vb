Public Module Global_Reference

    Private mSpam_Phrasess As SortedDictionary(Of Integer, String)

    Public Function Spam_Phrases() As SortedDictionary(Of Integer, String)

        If mSpam_Phrasess Is Nothing Then

            mSpam_Phrasess = New SortedDictionary(Of Integer, String)
            Dim lines As String() = My.Resources.Span_Words.Split(vbCrLf)

            For i = 1 To lines.Count - 1 Step 2

                Dim line As String = lines(i).Replace(ChrW(10), String.Empty).ToLower
                mSpam_Phrasess.Add(mSpam_Phrasess.Count + 1, line)

            Next

        End If

        Return mSpam_Phrasess

    End Function

End Module
