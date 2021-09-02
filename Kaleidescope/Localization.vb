Public Module Localization

    Private mFolder As String = "C:\Users\pc\Desktop\Software and Files\Expert 23\Kaleidescope-main\DIR\"
    Private mFilename_WIP As String = "message_wip.bin"
    Private mFilename_done As String = "message_done"

    Public ReadOnly Property Filepath_WIP
        Get
            Return mFolder.TrimEnd("\") & "\" & mFilename_WIP
        End Get
    End Property

    Public ReadOnly Property Filename_Done
        Get
            Return mFolder.TrimEnd("\") & "\" & mFilename_done
        End Get
    End Property

End Module
