Imports System.Drawing
Imports System.IO
Imports System.Net
Public Class Image_Utilities

    Public Shared Function Create_Random_FilePath(ByVal imageDir As String, ByRef filename As String, Optional ByVal format As String = "png") As String

        format = "." & format.TrimStart(".")
        Dim filename_base As String = "image_"

        Dim modifier As String = Now.Ticks.ToString.Substring(8, 6)
        filename = filename_base & modifier

        Dim filepath As String = imageDir & filename_base & modifier & format

        Return filepath

    End Function

    Public Shared Function Download_Media_To_Filesystem(ByVal url As String,
                                                        ByVal filepath As String,
                                                        Optional ByVal width As Integer = 512,
                                                        Optional ByVal height As Integer = 300) _
                                                        As Boolean

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim success As Boolean

        Try

            Dim request As HttpWebRequest = WebRequest.Create(url)
            Dim response As HttpWebResponse = request.GetResponse()

            With response

                If ((.StatusCode = HttpStatusCode.OK OrElse
                   .StatusCode = HttpStatusCode.Moved OrElse
                   .StatusCode = HttpStatusCode.Redirect) AndAlso
                   .ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase)) Then

                    Using inputStream As Stream = response.GetResponseStream()
                        Using outputStream As Stream = File.OpenWrite(filepath)

                            Dim buffer(4096) As Byte
                            Dim bytesRead As Integer

                            Do

                                bytesRead = inputStream.Read(buffer, 0, buffer.Length)
                                outputStream.Write(buffer, 0, bytesRead)

                            Loop While (bytesRead <> 0)

                        End Using

                    End Using



                    Dim img As Image = Image.FromFile(filepath)

                    Dim bmp As Bitmap = Resize_Image(img, width, height)
                    'we used dispose remove previous access to the image
                    img.Dispose()
                    bmp.Save(filepath, Imaging.ImageFormat.Png)
                    success = True

                End If

            End With

        Catch ex As Exception

            Debug.Print("Failed to download image from web. Reason: " & ex.Message)

        End Try


        Return success

    End Function

    Public Shared Function Resize_Image(ByRef img As Image,
                                 Optional ByVal width As Integer = 1024,
                                 Optional ByVal height As Integer = 1024) _
                                 As Image

        Dim destImage = New Bitmap(width, height)

        Try

            Dim destRect = New Rectangle(0, 0, width, height)

            destImage.SetResolution(img.HorizontalResolution, img.VerticalResolution)

            Using graph = Graphics.FromImage(destImage)

                With graph

                    .CompositingMode = Drawing2D.CompositingMode.SourceCopy
                    .CompositingQuality = Drawing2D.CompositingQuality.HighQuality
                    .InterpolationMode = Drawing2D.InterpolationMode.Bicubic
                    .SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                    .PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality

                    Using wrapMode = New System.Drawing.Imaging.ImageAttributes

                        wrapMode.SetWrapMode(Drawing2D.WrapMode.TileFlipXY)
                        .DrawImage(img, destRect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, wrapMode)

                    End Using

                End With

            End Using

        Catch ex As Exception

            Debug.Print("Failed to resize image. Reason: " & ex.Message)

        End Try

        Return destImage

    End Function

#Region " Old Code "

    'Public Shared Function Resize_Image0(ByRef img As Image,
    '                             Optional ByVal width As Integer = 1024,
    '                             Optional ByVal height As Integer = 1024) _
    '                             As Bitmap

    '    Dim destRect = New Rectangle(0, 0, width, height)
    '    Dim destImage = New Bitmap(width, height)

    '    destImage.SetResolution(img.HorizontalResolution, img.VerticalResolution)

    '    Using graph = Graphics.FromImage(destImage)

    '        With graph

    '            .CompositingMode = Drawing2D.CompositingMode.SourceCopy
    '            .CompositingQuality = Drawing2D.CompositingQuality.HighQuality
    '            .InterpolationMode = Drawing2D.InterpolationMode.Bicubic
    '            .SmoothingMode = Drawing2D.SmoothingMode.HighQuality
    '            .PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality

    '            Using wrapMode = New System.Drawing.Imaging.ImageAttributes

    '                wrapMode.SetWrapMode(Drawing2D.WrapMode.TileFlipXY)
    '                .DrawImage(img, destRect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, wrapMode)

    '            End Using

    '        End With

    '    End Using

    '    Return destImage

    'End Function


    'Public Shared Function Download_Image(ByVal url As String, ByRef success As Boolean) As MemoryStream

    '    success = False
    '    Dim outputStream As New MemoryStream
    '    If String.IsNullOrWhiteSpace(url) Then Return outputStream

    '    Try

    '        Dim request As HttpWebRequest = WebRequest.Create(url)
    '        Dim response As HttpWebResponse = request.GetResponse()

    '        With response

    '            If ((.StatusCode = HttpStatusCode.OK OrElse
    '               .StatusCode = HttpStatusCode.Moved OrElse
    '               .StatusCode = HttpStatusCode.Redirect) AndAlso
    '               .ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase)) Then

    '                Using inputStream As Stream = response.GetResponseStream()
    '                    Using outputStream

    '                        Dim buffer(4096) As Byte
    '                        Dim bytesRead As Integer

    '                        Do

    '                            bytesRead = inputStream.Read(buffer, 0, buffer.Length)
    '                            outputStream.Write(buffer, 0, bytesRead)

    '                        Loop While (bytesRead <> 0)

    '                    End Using
    '                End Using

    '                success = True

    '            End If

    '        End With

    '    Catch ex As Exception

    '        Debug.Print("Failed to download image from web. Reason: " & ex.Message)

    '    End Try


    '    Return outputStream

    'End Function



#End Region

End Class
