Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.Windows.Forms

Public Class ImageWorkArea
    Public ScaleFactor As Single
    Public ScalerMaximum As Integer
    Public ScalerMinimum As Integer
    Public SizeToFit As Boolean

    Private ScalerPanelBox(4) As Point
    Private ScalerPanelFullBox(4) As Point
    Private ScalerPanelAnimation As Boolean
    Private ScalerPanelAnimationUp As Boolean

    Public Event ScaleFactorChange(Value As Single)

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
                If Me.ScaleFactor > ScalerMaximum / 10 Then
                    Me.ScaleFactor = ScalerMaximum / 10
                ElseIf Me.ScaleFactor < ScalerMinimum / 10 Then
                    Me.ScaleFactor = ScalerMinimum / 10
                End If
                RaiseEvent ScaleFactorChange(Me.ScaleFactor * 10)

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



    Private Sub ImageWorkArea_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Me.UpdateMe()
    End Sub





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


End Class
