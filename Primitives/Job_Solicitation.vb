Imports System.Drawing
<Serializable>
Public Class Job_Solicitation

    Public Item_Content As Content_Item
    <NonSerialized>
    Public Snippet As Bitmap

    Public Timestamp As DateTime

    Public Sub New()

        Initialize_Job()

    End Sub

    Private Sub Initialize_Job()

        Item_Content = New Content_Item

        Timestamp = Now

    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}:{1}:{2}", Item_Content.ID, Item_Content.Name_Source, Timestamp.ToString("M/d/yy"))
    End Function

    Public Shared Function Get_Test_Snippet() As Bitmap

        Return My.Resources.sample_snippet

    End Function

End Class
