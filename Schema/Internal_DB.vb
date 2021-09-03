Imports Primitives
Imports System.Data.SqlDbType
Public Class Internal_DB

    Public Class OPS


        Public Shared ReadOnly Property Conn As String

            Get
                Return " Server=192.168.250.85,1433;Database=G23-ContentDatabase;MultipleActiveResultSets=true;User ID=Test;Password=Expert@23"
            End Get

        End Property

        Public Class Content_Item


                Public Shared Table_Name As String = "[dbo].[Content_Item]"

            Public Class ColName


                Public Shared ID As String = "[ID]"
                Public Shared ID_Unique As String = "[ID_Unique]"

                Public Shared ID_Post As String = "[ID_Post]"
                Public Shared ID_Media As String = "[ID_Media]"

                Public Shared ID_Source As String = "[ID_Source]"
                Public Shared Name_Source As String = "[Name_Source]"
                Public Shared Author As String = "[Author]"

                Public Shared Category As String = "[Category]"
                Public Shared Website As String = "[Website]"

                Public Shared Title As String = "[Title]"
                Public Shared Excerpt As String = "[Excerpt]"
                Public Shared Summary As String = "[Summary]"
                Public Shared Content As String = "[Content]"

                Public Shared Date_Published As String = "[Date_Published]"
                Public Shared Date_Harvested As String = "[Date_Harvested]"

                Public Shared URL_Post As String = "[URL_Post]"
                Public Shared URL_Content As String = "[URL_Content]"
                Public Shared URL_Media As String = "[URL_Media]"

                Public Shared Media_Name As String = "[Media_Name]"
                Public Shared Media_FileName As String = "[Media_FileName]"
                Public Shared Media_Filepath As String = "[Media_Filepath]"
                Public Shared Media_Format As String = "[Media_Format]"

                Public Shared Media_isImage As String = "[Media_isImage]"
                Public Shared Media_isVideo As String = "[Media_isVideo]"

                Public Shared Image_Height As String = "[Image_Height]"
                Public Shared Image_Width As String = "[Image_Width]"

                Public Shared Status As String = "[Status]"

                Public Shared Bin As String = "[Bin]"


            End Class



            Public Class NETType

                    Public Shared ID As Type = GetType(Integer)
                    Public Shared ID_Unique As Type = GetType(String)
                    Public Shared ID_Post As Type = GetType(Integer)
                    Public Shared ID_Media As Type = GetType(Integer)

                    Public Shared ID_Source As Type = GetType(String)
                    Public Shared Name_Source As Type = GetType(String)
                    Public Shared Author As Type = GetType(String)

                    Public Shared Category As Type = GetType(String)
                    Public Shared Website As Type = GetType(String)

                    Public Shared Title As Type = GetType(String)
                    Public Shared Excerpt As Type = GetType(String)
                    Public Shared Summary As Type = GetType(String)
                    Public Shared Content As Type = GetType(String)

                    Public Shared Date_Published As Type = GetType(DateTime)
                    Public Shared Date_Harvested As Type = GetType(DateTime)
                Public Shared URL_Post As Type = GetType(String)

                Public Shared URL_Content As Type = GetType(String)
                Public Shared URL_Media As Type = GetType(String)

                    Public Shared Media_Name As Type = GetType(String)
                    Public Shared Media_FileName As Type = GetType(String)
                    Public Shared Media_Filepath As Type = GetType(String)
                    Public Shared Media_Format As Type = GetType(String)

                    Public Shared Media_isImage As Type = GetType(Boolean)
                    Public Shared Media_isVideo As Type = GetType(Boolean)

                    Public Shared Image_Height As Type = GetType(Integer)
                    Public Shared Image_Width As Type = GetType(Integer)

                    Public Shared Status As Type = GetType(String)

                    Public Shared Bin As Type = GetType(Byte())


                End Class



                Public Class SQL



                    Public Shared ID As SqlDbType = SqlDbType.Int
                    Public Shared ID_Unique As SqlDbType = SqlDbType.VarChar

                    Public Shared ID_Post As SqlDbType = SqlDbType.Int
                    Public Shared ID_Media As SqlDbType = SqlDbType.Int

                    Public Shared ID_Source As SqlDbType = SqlDbType.VarChar
                    Public Shared Name_Source As SqlDbType = SqlDbType.VarChar
                    Public Shared Author As SqlDbType = SqlDbType.VarChar

                    Public Shared Category As SqlDbType = SqlDbType.VarChar
                    Public Shared Website As SqlDbType = SqlDbType.VarChar

                    Public Shared Title As SqlDbType = SqlDbType.VarChar
                    Public Shared Excert As SqlDbType = SqlDbType.VarChar
                    Public Shared Summary As SqlDbType = SqlDbType.VarChar
                    Public Shared Content As SqlDbType = SqlDbType.VarChar

                    Public Shared Date_Published As SqlDbType = SqlDbType.DateTime
                Public Shared Date_Harvested As SqlDbType = SqlDbType.DateTime

                Public Shared URL_Post As SqlDbType = SqlDbType.VarChar
                Public Shared URL_Content As SqlDbType = SqlDbType.VarChar
                    Public Shared URL_Media As SqlDbType = SqlDbType.VarChar

                    Public Shared Media_Name As SqlDbType = SqlDbType.VarChar
                    Public Shared Media_FileName As SqlDbType = SqlDbType.VarChar
                    Public Shared Media_Filepath As SqlDbType = SqlDbType.VarChar
                    Public Shared Media_Format As SqlDbType = SqlDbType.VarChar

                    Public Shared Media_isImage As SqlDbType = SqlDbType.Int
                    Public Shared Media_isVideo As SqlDbType = SqlDbType.Int

                    Public Shared Image_Height As SqlDbType = SqlDbType.Int
                    Public Shared Image_Width As SqlDbType = SqlDbType.Int

                    Public Shared Status As SqlDbType = SqlDbType.VarChar

                    Public Shared Bin As SqlDbType = SqlDbType.VarBinary

                End Class


            End Class

        Public Class Job_Solicitation

            Public Shared Table_Name As String = "[dbo].[Job_Solicitation]"

            Public Class ColName

                Public Shared ID As String = "[ID]"
                Public Shared Job_Solicitation As String = "[Job_Solicitation]"
                Public Shared Content_Item_ID As String = "[Content_Item_ID]"
                Public Shared Job_Status As String = "[Job_Status]"
                Public Shared Website As String = "[Website]"
                Public Shared Category As String = "[Category]"
                Public Shared Date_Created As String = "[Date_Created]"

            End Class

            Public Class NETType

                Public Shared ID As Type = GetType(Integer)
                Public Shared Job_Solicitation As Type = GetType(Byte)
                Public Shared Content_Item_ID As Type = GetType(Integer)
                Public Shared Job_Status As Type = GetType(String)
                Public Shared Website As Type = GetType(String)
                Public Shared Category As Type = GetType(String)
                Public Shared Date_Created As Type = GetType(DateTime)

            End Class

            Public Class SQL

                Public Shared ID As SqlDbType = SqlDbType.Int
                Public Shared Job_Solicitation As SqlDbType = SqlDbType.VarBinary
                Public Shared Content_Item_ID As SqlDbType = SqlDbType.Int
                Public Shared Job_Status As SqlDbType = SqlDbType.VarChar
                Public Shared Website As SqlDbType = SqlDbType.VarChar
                Public Shared Category As SqlDbType = SqlDbType.VarChar
                Public Shared Date_Created As SqlDbType = SqlDbType.DateTime

            End Class

        End Class

    End Class
    End Class

