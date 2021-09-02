Public Class Lemlist
    Public User_Name As String
    Public Password As String
    Public Message As Message
    Public Campaign_Name As String
    Public Labels As List(Of String)

    Public Time_Zone As Time_Zone
    Public Stop_sending As SortedDictionary(Of Stop_Sending_Lead, Boolean)
    Public Tracking As SortedDictionary(Of Tracking, Boolean)

    Public CSV_File As String
    Public Images As List(Of String)
    Public NB_Rows As Integer
    Public Sender_Email As String


    Public Subject As String

    Public Sub New()
        User_Name = String.Empty
        Password = String.Empty

        Campaign_Name = String.Empty
        Labels = New List(Of String)

        Stop_sending = New SortedDictionary(Of Stop_Sending_Lead, Boolean)

        Images = New List(Of String)
        CSV_File = String.Empty
        NB_Rows = 0
        Sender_Email = String.Empty
        Subject = String.Empty
    End Sub
    Public Overrides Function ToString() As String
        Return MyBase.ToString()
    End Function
End Class
