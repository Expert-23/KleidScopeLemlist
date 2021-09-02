Public Class Wrangler


    Public Function Preprocess_Text(ByVal content As String) As SortedDictionary(Of Integer, String)

        Dim clean As Char() = content.Replace(vbCr, "").Replace(vbLf, " ").ToArray

        Dim sentences As New SortedDictionary(Of Integer, String)

        Dim last As Integer = clean.GetUpperBound(0)
        Dim sentence As String = String.Empty
        For i = 0 To last

            Dim c As Char = clean(i)

            If i = last Then

                sentences.Add(sentences.Count, sentence & ".")

            Else

                If i > last - 3 Then

                    sentence = sentence & c

                Else

                    If c = "." Then

                        Dim nxt(2) As Char
                        nxt(0) = clean(i + 1)
                        nxt(1) = clean(i + 2)
                        nxt(2) = clean(i + 3)

                        If End_of_Sentence(nxt) Then

                            sentences.Add(sentences.Count, sentence & ".")
                            sentence = String.Empty

                        Else

                            sentence = sentence & c

                        End If

                    Else

                        sentence = sentence & c

                    End If

                End If

            End If

        Next

        Return sentences

    End Function


    Public Function Preprocess_Text(ByVal subject As String, ByVal content As String) As SortedDictionary(Of Integer, String)

        Dim clean As Char() = content.Replace(vbCr, "").Replace(vbLf, " ").ToArray

        Dim sentences As New SortedDictionary(Of Integer, String)
        sentences.Add(0, subject)

        Dim last As Integer = clean.GetUpperBound(0)
        Dim sentence As String = String.Empty
        For i = 0 To last

            Dim c As Char = clean(i)

            If i = last Then

                sentences.Add(sentences.Count, sentence)

            Else

                If i > last - 3 Then

                    sentence = sentence & c

                Else

                    If c = "." Then

                        Dim nxt(2) As Char
                        nxt(0) = clean(i + 1)
                        nxt(1) = clean(i + 2)
                        nxt(2) = clean(i + 3)

                        If End_of_Sentence(nxt) Then

                            sentences.Add(sentences.Count, sentence)
                            sentence = String.Empty

                        Else

                            sentence = sentence & c

                        End If

                    Else

                        sentence = sentence & c

                    End If

                End If

            End If

        Next

        Return sentences

    End Function

    Private Function End_of_Sentence(ByVal nxt As Char()) As Boolean

        For Each c In nxt

            If Not Char.IsWhiteSpace(c) Then

                Dim code As Integer = AscW(c)
                If code >= 65 AndAlso code <= 90 Then
                    Return True
                End If

            End If

        Next

        Return False

    End Function

End Class
