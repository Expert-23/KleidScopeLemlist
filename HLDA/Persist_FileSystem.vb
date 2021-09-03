Imports G23.Content.Localize
Imports G23.Content.Primatives
Imports G23.Content.Utilities
Public Class Persist_FileSystem



    Public Class Content

        Public Shared Function Save_Items(ByVal filepath As String, ByVal items As SortedDictionary(Of String, Content_Item)) As Boolean

            Dim success As Boolean
            Serialization_Utilities.Serialize_Object_And_Save_FileSystem(items, filepath, success)
            Return success

        End Function

        Public Shared Function Load_Content_Items(ByVal filepath As String, ByRef success As Boolean) As SortedDictionary(Of String, Content_Item)

            Dim items As SortedDictionary(Of String, Content_Item)
            Serialization_Utilities.Load_Object_FileSystem_And_Deserialize(Of SortedDictionary(Of String, Content_Item))(filepath, items, success)

            Return items

        End Function


    End Class

    Public Class Solicitation



    End Class

    Public Class Image


        Public Function Load(ByVal filename As String, ByVal item As Content_Item) As Boolean


        End Function


        Public Function Save(ByVal filename As String, ByVal item As Content_Item) As Boolean


        End Function

    End Class



    Public Class WIP

        Public Shared Function Generate_Random_Curation_Job_Filename() As String

            Dim modifier As String = Now.Ticks.ToString.Substring(8, 6)
            Dim filename As String = "wip_curation_" & modifier & ".bin"

            Return filename

        End Function

        Public Shared Function Save(ByVal filename As String, ByVal job As Job_Curation) As Boolean

                filename = filename.Split(".")(0) & ".bin"
                Dim filepath As String = Work_In_Progress.Directory & "\" & filename

                Dim success As Boolean
                Serialization_Utilities.Serialize_Object_And_Save_FileSystem(job, filepath, success)
                Return success

            End Function

        Public Shared Function Load(ByVal filepath As String, ByRef success As Boolean) As Job_Curation

            'filename = filename.Split(".")(0) & ".bin"
            'Dim filepath As String = Work_In_Progress.Directory & "\" & filename

            Dim job As Job_Curation
            Serialization_Utilities.Load_Object_FileSystem_And_Deserialize(Of Job_Curation)(filepath, job, success)

            Return job

        End Function

    End Class

        Public Class Test

            Public Shared Function Save_Content_Items(ByVal items As Dictionary(Of String, Content_Item)) As Boolean

                Dim success As Boolean
                Serialization_Utilities.Serialize_Object_And_Save_FileSystem(items, Testing.Filepath_Content, success)
                Return success

            End Function



        Public Shared Function Load_Saved_Content_Items(ByRef success As Boolean) As Dictionary(Of String, Content_Item)

            Dim items As Dictionary(Of String, Content_Item)
            Serialization_Utilities.Load_Object_FileSystem_And_Deserialize(Of Dictionary(Of String, Content_Item))(Testing.Filepath_Content, items, success)

            Return items

        End Function


    End Class



End Class
