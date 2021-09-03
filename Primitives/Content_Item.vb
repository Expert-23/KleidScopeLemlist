<Serializable>
Public Class Content_Item

    Public ID As Integer
    Public ID_Unique As String

    Public ID_Post As Integer
    Public ID_Media As Integer

    Public ID_Source As String
    Public Name_Source As String
    Public Author As String

    Public Category As String
    Public Website As Websites

    Public Title As String
    Public Excerpt As String
    Public Summary As String
    Public Content As String


    Public Date_Published As DateTime
    Public Date_Harvested As DateTime

    Public URL_Content As String
    Public URL_Media As String


    Public URL_Post As String

    Public Media_Name As String
    Public Media_FileName As String
    Public Media_Filepath As String
    Public Media_Format As String

    Public Media_isImage As Boolean
    Public Media_isVideo As Boolean

    Public Image_Height As Integer
    Public Image_Width As Integer

    Public Status As Content_Status

    Public Bin As Byte()

    Public Sub New()

        Initialize_Content_Item()

    End Sub

    Public Sub New(ByVal dr As DataRow)

        Initialize_Content_Item()
        Initialize_Content_Item(dr)

    End Sub

    Private Sub Initialize_Content_Item()

        ID = 0
        ID_Unique = Guid.NewGuid.ToString

        ID_Post = 0
        ID_Media = 0

        ID_Source = String.Empty
        Name_Source = String.Empty
        Author = String.Empty

        Category = "general"
        Website = Websites.unspecified

        Title = String.Empty
        Excerpt = String.Empty
        Summary = String.Empty
        Content = String.Empty

        Date_Published = New Date(1900, 1, 1)
        Date_Harvested = New Date(1900, 1, 1)

        URL_Content = String.Empty
        URL_Media = String.Empty

        URL_Post = String.Empty

        Media_Name = String.Empty
        Media_FileName = String.Empty
        Media_Filepath = String.Empty
        Media_Format = "png"

        Media_isImage = True
        Media_isVideo = False

        Image_Height = 300
        Image_Width = 512

        Status = Content_Status.unspecified
        Bin = {}
    End Sub

    Private Sub Initialize_Content_Item(ByVal dr As DataRow)

        ID = Integer.Parse(dr(0).ToString)
        ID_Unique = dr(1).ToString

        ID_Post = Integer.Parse(dr(2).ToString)
        ID_Media = Integer.Parse(dr(3).ToString)

        ID_Source = dr(4).ToString
        Name_Source = dr(5).ToString
        Author = dr(6).ToString

        Category = dr(7).ToString
        Website = DirectCast([Enum].Parse(GetType(Websites), dr(8).ToString), Websites)

        Title = dr(9).ToString
        Excerpt = dr(10).ToString
        Summary = dr(11).ToString
        Content = dr(12).ToString

        Date_Published = DateTime.Parse(dr(13).ToString)
        Date_Harvested = DateTime.Parse(dr(14).ToString)

        URL_Content = dr(15).ToString
        URL_Media = dr(16).ToString
        URL_Post = dr(17).ToString

        Media_Name = dr(18).ToString
        Media_FileName = dr(19).ToString
        Media_Filepath = dr(20).ToString
        Media_Format = dr(21).ToString

        Media_isImage = Boolean.Parse(dr(22).ToString)
        Media_isVideo = Boolean.Parse(dr(23).ToString)

        Image_Height = Integer.Parse(dr(24).ToString)
        Image_Width = Integer.Parse(dr(25).ToString)

        Status = DirectCast([Enum].Parse(GetType(Content_Status), dr(26).ToString), Content_Status)
        Bin = dr(27)

    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}:{1}:{2}:{3}", ID, Name_Source, Date_Published.ToString("M/d/yy"), Title)
    End Function
    'dummy comment
End Class
