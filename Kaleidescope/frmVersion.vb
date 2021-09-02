Public Class frmVersion

    Public Sub New(ByVal item As Version)

        InitializeComponent()

        txtVersion.Text = item.Message_Text

    End Sub

End Class