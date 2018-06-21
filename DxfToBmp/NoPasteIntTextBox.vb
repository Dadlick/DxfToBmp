Imports System.Windows.Forms
Public Class NoPasteIntTextBox
    Inherits TextBox

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = &H302 Then Return
        MyBase.WndProc(m)
    End Sub

    Private Sub NoPasteTextbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Dim KeyChar As String = e.KeyChar
        If Not IsNumeric(KeyChar) AndAlso KeyChar <> vbBack Then
            e.Handled = True
        Else
            If KeyChar = "0" Then
                If Len(Me.Text) = 0 Then
                    e.Handled = True
                End If
            End If
        End If
    End Sub
End Class
