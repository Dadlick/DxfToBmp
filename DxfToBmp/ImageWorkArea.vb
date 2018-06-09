Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.Windows.Forms

Public Class ImageWorkArea
    Private ScaleFactor As Single
    Private SizeToFit As Boolean

    Private ScalerPanelBox(4) As Point
    Private ScalerPanelFullBox(4) As Point
    Private ScalerPanelAnimation As Boolean
    Private ScalerPanelAnimationUp As Boolean

    Public Sub New()
        ScaleFactor = 1
        ScalerPanelBox(0) = New Point(0, 0)
        ScalerPanelBox(1) = New Point(0, 40)
        ScalerPanelBox(2) = New Point(40, 40)
        ScalerPanelBox(3) = New Point(40, 0)
        ScalerPanelBox(4) = New Point(0, 0)

        ScalerPanelFullBox(0) = New Point(0, 0)
        ScalerPanelFullBox(1) = New Point(0, 42)
        ScalerPanelFullBox(2) = New Point(182, 42)
        ScalerPanelFullBox(3) = New Point(182, 0)
        ScalerPanelFullBox(4) = New Point(0, 0)

        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()
        Me.Panel1.BackgroundImage = CreateGridBackground(10, Color.Gainsboro, Color.White)
        ' Добавьте все инициализирующие действия после вызова InitializeComponent().
        EnableDoubleBuffering()
    End Sub
    Public Sub EnableDoubleBuffering()
        Me.SetStyle(ControlStyles.DoubleBuffer Or ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint, True)
        Me.UpdateStyles()
    End Sub
    Public Sub UpdateMe()
        If (Not Me.PictureBox1.Image Is Nothing) Then
            Me.PictureBox1.Visible = True
            If Me.SizeToFit Then
                Me.ScaleFactor = CSng(Math.Min(CDbl((CDbl(Me.Panel1.Height) / CDbl(Me.PictureBox1.Image.Height))), CDbl((CDbl(Me.Panel1.Width) / CDbl(Me.PictureBox1.Image.Width)))))
                If Me.ScaleFactor > tbScaler.Maximum / 10 Then
                    Me.ScaleFactor = tbScaler.Maximum / 10
                ElseIf Me.ScaleFactor < tbScaler.Minimum / 10 Then
                    Me.ScaleFactor = tbScaler.Minimum / 10
                End If
                tbScaler.Value = Me.ScaleFactor * 10
            End If
            Dim size2 As New Size(CInt(Math.Round(CDbl((Me.PictureBox1.Image.Width * Me.ScaleFactor)))), CInt(Math.Round(CDbl((Me.PictureBox1.Image.Height * Me.ScaleFactor)))))
            Me.PictureBox1.Size = size2
            If (Me.PictureBox1.Width < Me.Panel1.Width) Then
                Me.PictureBox1.Left = CInt(Math.Round(CDbl((CDbl((Me.Panel1.Width - Me.PictureBox1.Width)) / 2))))
            Else
                Me.PictureBox1.Left = Me.Panel1.AutoScrollPosition.X
            End If
            If (Me.PictureBox1.Height < Me.Panel1.Height) Then
                Me.PictureBox1.Top = CInt(Math.Round(CDbl((CDbl((Me.Panel1.Height - Me.PictureBox1.Height)) / 2))))
            Else
                Me.PictureBox1.Top = Me.Panel1.AutoScrollPosition.Y
            End If
        Else
            Me.PictureBox1.Visible = False
        End If
    End Sub

    Public Property RedrawImage As Image
        Get
            Return Me.PictureBox1.Image
        End Get
        Set(ByVal value As Image)
            Me.PictureBox1.Image = value
        End Set
    End Property

    Public Property Image As Image
        Get
            Return Me.PictureBox1.Image
        End Get
        Set(ByVal value As Image)
            Me.PictureBox1.Image = value
            Me.ScaleFactor = 1.0!
            Me.UpdateMe()
        End Set
    End Property

    Private Sub ImageWorkArea_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove, PictureBox1.MouseMove
        Dim CursorLocation As Point = Me.PointToClient(System.Windows.Forms.Cursor.Position)
        'Debug.Print("Top=" & Me.Top)
        'Debug.Print("X=" & e.Location.X & " Y=" & e.Location.Y)
        'Debug.Print("X=" & CursorLocation.X & " Y=" & CursorLocation.Y)
        If ScalerPanelAnimation = False And PnlScaler.Visible = False Then
            If PinPolygon(CursorLocation, ScalerPanelBox) = True Then
                PnlScaler.Visible = True
                PnlScaler.Top = -PnlScaler.Height
                ScalerPanelAnimation = True
                ScalerPanelAnimationUp = False
                TmrScalerPanel.Interval = 10
                TmrScalerPanel.Enabled = True
            End If
        End If
    End Sub

    Private Sub ImageWorkArea_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Me.UpdateMe()
    End Sub

    Private Sub TmrScalerPanel_Tick(sender As Object, e As EventArgs) Handles TmrScalerPanel.Tick
        If ScalerPanelAnimation Then
            If ScalerPanelAnimationUp Then
                'Debug.Print("up")
                If -PnlScaler.Top < PnlScaler.Height Then
                    PnlScaler.Top = PnlScaler.Top - 1
                Else
                    ScalerPanelAnimation = False
                    PnlScaler.Visible = False
                    TmrScalerPanel.Enabled = False
                End If
            Else
                'Debug.Print("down")
                If PnlScaler.Top < 0 Then
                    PnlScaler.Top = PnlScaler.Top + 1
                Else
                    TmrScalerPanel.Interval = 5000
                    ScalerPanelAnimation = False
                End If
            End If
        Else
            Dim CursorLocation As Point = Me.PointToClient(System.Windows.Forms.Cursor.Position)
            If PinPolygon(CursorLocation, ScalerPanelFullBox) = False Then
                ScalerPanelAnimationUp = True
                ScalerPanelAnimation = True
                TmrScalerPanel.Interval = 10
            End If
        End If
    End Sub

    Private Function PinPolygon(P As Point, V As Point()) As Boolean
        Dim ret As Boolean
        Dim Path As New GraphicsPath()
        Path.AddLines(V)
        Dim r1 As New Region(Path)
        ret = r1.IsVisible(P)
        r1.Dispose()
        r1 = Nothing
        Path.Dispose()
        Path = Nothing
        Return ret
    End Function

    Private Function CreateGridBackground(cellSize As Integer, cellColor As Color, alternateCellColor As Color) As Bitmap
        Dim result As Bitmap
        Dim width As Integer
        Dim height As Integer
        width = cellSize * 2
        height = cellSize * 2
        result = New Bitmap(width, height)
        Dim g As Graphics = Graphics.FromImage(result)
        Dim brush As Brush = New SolidBrush(cellColor)
        g.FillRectangle(brush, New Rectangle(cellSize, 0, cellSize, cellSize))
        g.FillRectangle(brush, New Rectangle(0, cellSize, cellSize, cellSize))
        brush = New SolidBrush(alternateCellColor)
        g.FillRectangle(brush, New Rectangle(0, 0, cellSize, cellSize))
        g.FillRectangle(brush, New Rectangle(cellSize, cellSize, cellSize, cellSize))
        Return result
    End Function

    Private Sub cbSizeToFit_CheckedChanged(sender As Object, e As EventArgs) Handles cbSizeToFit.CheckedChanged
        Me.tbScaler.Enabled = Not Me.cbSizeToFit.Checked
        Me.SizeToFit = Me.cbSizeToFit.Checked
        If Not Me.cbSizeToFit.Checked Then
            Me.ScaleFactor = Me.tbScaler.Value / 10
        End If
        Me.UpdateMe()
    End Sub

    Private Sub tbScaler_Scroll(sender As Object, e As EventArgs) Handles tbScaler.Scroll
        Me.ScaleFactor = Me.tbScaler.Value / 10
        Me.UpdateMe()
    End Sub
End Class
