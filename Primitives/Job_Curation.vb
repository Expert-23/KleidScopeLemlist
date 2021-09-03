<Serializable>
Public Class Job_Curation

    Public Current As Content_Item

    Public Content_All As Dictionary(Of String, Content_Item)
    Public Content_Posted As Dictionary(Of String, Tuple(Of Content_Item, Boolean))
    Public Content_Accepted As Dictionary(Of String, Tuple(Of Content_Item, Boolean))
    Public Content_Solicited As Dictionary(Of String, Tuple(Of Job_Solicitation, Boolean))


    Public Timestamp As DateTime

    Public Sub New()

        Initialize_Job()

    End Sub

    Private Sub Initialize_Job()

        Current = New Content_Item

        Content_All = New Dictionary(Of String, Content_Item)
        Content_Posted = New Dictionary(Of String, Tuple(Of Content_Item, Boolean))
        Content_Accepted = New Dictionary(Of String, Tuple(Of Content_Item, Boolean))
        Content_Solicited = New Dictionary(Of String, Tuple(Of Job_Solicitation, Boolean))

        Timestamp = Now

    End Sub

    Public Function Posted_Succeeded() As Dictionary(Of String, Content_Item)

        Dim output As New Dictionary(Of String, Content_Item)

        For Each entry As Tuple(Of Content_Item, Boolean) In Content_Posted.Values

            Dim success As Boolean = entry.Item2
            Dim content As Content_Item = entry.Item1

            If success Then
                content.Status = Content_Status.posted
                output.Add(content.ID_Unique, content)
            End If

        Next
        Return output
    End Function


    Public Function Posted_Failed() As Dictionary(Of String, Content_Item)

        Dim output As New Dictionary(Of String, Content_Item)

        For Each entry As Tuple(Of Content_Item, Boolean) In Content_Posted.Values

            Dim success As Boolean = entry.Item2
            Dim content As Content_Item = entry.Item1

            If Not success Then
                content.Status = Content_Status.scraped
                output.Add(content.ID_Unique, content)
            End If

        Next
        Return output
    End Function



    Public Function Solicited_Pushed() As Dictionary(Of String, Job_Solicitation)

        Dim output As New Dictionary(Of String, Job_Solicitation)

        For Each entry As Tuple(Of Job_Solicitation, Boolean) In Content_Solicited.Values

            Dim success As Boolean = entry.Item2
            Dim solicit As Job_Solicitation = entry.Item1

            If success Then
                output.Add(solicit.Item_Content.ID_Unique, solicit)
            End If

        Next
        Return output
    End Function

    Public Function Solicited_Ready() As Dictionary(Of String, Job_Solicitation)

        Dim output As New Dictionary(Of String, Job_Solicitation)

        For Each entry As Tuple(Of Job_Solicitation, Boolean) In Content_Solicited.Values

            Dim success As Boolean = entry.Item2
            Dim solicit As Job_Solicitation = entry.Item1

            If Not success Then
                output.Add(solicit.Item_Content.ID_Unique, solicit)
            End If

        Next
        Return output
    End Function

    Public Function Accepted() As Dictionary(Of String, Content_Item)

        Dim output As New Dictionary(Of String, Content_Item)

        For Each entry As Tuple(Of Content_Item, Boolean) In Content_Accepted.Values

            Dim success As Boolean = entry.Item2
            Dim content As Content_Item = entry.Item1

            If Not success Then
                content.Status = Content_Status.accepted
                output.Add(content.ID_Unique, content)
            End If

        Next
        Return output

    End Function



    Public Function Update_Job() As Boolean

        Dim success As Boolean = False
        Dim original_collection As Dictionary(Of String, Content_Item) = Content_All
        Try

            For Each entry In original_collection

                Dim key As String = entry.Key
                Dim value As Content_Item = entry.Value


                If Solicited_Pushed.ContainsKey(key) Then
                    value.Status = Content_Status.solicited_pushed
                ElseIf Solicited_Ready.ContainsKey(key) Then
                    value.Status = Content_Status.solicited_ready
                ElseIf Posted_Succeeded.ContainsKey(key) Then
                    value.Status = Content_Status.posted
                ElseIf Accepted.ContainsKey(key) Then
                    value.Status = Content_Status.accepted
                ElseIf Posted_Failed.ContainsKey(key) Then
                    value.Status = Content_Status.failed
                Else

                    value.Status = Content_Status.ignored
                End If
                Content_All(key) = value
                'updated_collection.Add(key, value)
            Next
            success = True
        Catch ex As Exception

        End Try

        'If success Then Content_All = updated_collection

        Return success
    End Function


    Public Overrides Function ToString() As String
        Return Timestamp.ToString()
    End Function

End Class

#Region " OLD CODE "

'Public Shared Function Posted_Succeeded(ByVal posted As Dictionary(Of String, Tuple(Of Content_Item, Boolean))) As Dictionary(Of String, Content_Item)

'    Dim output As New Dictionary(Of String, Content_Item)

'    For Each entry As Tuple(Of Content_Item, Boolean) In posted.Values

'        Dim success As Boolean = entry.Item2
'        Dim content As Content_Item = entry.Item1

'        If success Then
'            output.Add(content.ID_Unique, content)
'        End If

'    Next
'    Return output
'End Function


'Public Shared Function Posted_Failed(ByVal posted As Dictionary(Of String, Tuple(Of Content_Item, Boolean))) As Dictionary(Of String, Content_Item)

'    Dim output As New Dictionary(Of String, Content_Item)

'    For Each entry As Tuple(Of Content_Item, Boolean) In posted.Values

'        Dim success As Boolean = entry.Item2
'        Dim content As Content_Item = entry.Item1

'        If Not success Then
'            output.Add(content.ID_Unique, content)
'        End If

'    Next
'    Return output
'End Function

'Public Shared Function Solicited_Failed(ByVal solicited As Dictionary(Of String, Tuple(Of Job_Solicitation, Boolean))) As Dictionary(Of String, Job_Solicitation)

'    Dim output As New Dictionary(Of String, Job_Solicitation)

'    For Each entry As Tuple(Of Job_Solicitation, Boolean) In solicited.Values

'        Dim success As Boolean = entry.Item2
'        Dim solicit As Job_Solicitation = entry.Item1

'        If Not success Then
'            output.Add(solicit.Item_Content.ID_Unique, solicit)
'        End If

'    Next
'    Return output
'End Function

'Public Shared Function Solicited_Succeeded(ByVal solicited As Dictionary(Of String, Tuple(Of Job_Solicitation, Boolean))) As Dictionary(Of String, Job_Solicitation)

'    Dim output As New Dictionary(Of String, Job_Solicitation)

'    For Each entry As Tuple(Of Job_Solicitation, Boolean) In solicited.Values

'        Dim success As Boolean = entry.Item2
'        Dim solicit As Job_Solicitation = entry.Item1

'        If success Then
'            output.Add(solicit.Item_Content.ID_Unique, solicit)
'        End If

'    Next
'    Return output
'End Function


#End Region
