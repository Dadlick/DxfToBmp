Imports System

Public Class Curve
	Public Ax As Single
	Public Ay As Single
	Public Bx As Single
	Public By As Single
	Public Cx As Single
	Public Cy As Single
	Public Ndiv As Integer

	Public Sub New(ByVal ax As Single, ByVal ay As Single, ByVal bx As Single, ByVal by As Single, ByVal cx As Single, ByVal cy As Single, ByVal ndiv As Integer)
		Me.Ax = ax
		Me.Ay = ay
		Me.Bx = bx
		Me.By = by
		Me.Cx = cx
		Me.Cy = cy
		Me.Ndiv = ndiv
	End Sub

	Public Sub New(ByVal ax As Single, ByVal ay As Single, ByVal bx As Single, ByVal by As Single, ByVal cx As Single, ByVal cy As Single)
		Me.Ax = ax
		Me.Ay = ay
		Me.Bx = bx
		Me.By = by
		Me.Cx = cx
		Me.Cy = cy
        Ndiv = CInt(Math.Truncate(Math.Max(Math.Abs(CInt(Math.Truncate(Me.Ax))), Math.Abs(CInt(Math.Truncate(Me.Ay))))))
    End Sub
	Public Sub New()
	End Sub

	Public Sub PutCurve(ByVal ax As Single, ByVal ay As Single, ByVal bx As Single, ByVal by As Single, ByVal cx As Single, ByVal cy As Single)
		Me.Ax = ax
		Me.Ay = ay
		Me.Bx = bx
		Me.By = by
		Me.Cx = cx
		Me.Cy = cy
        Ndiv = CInt(Math.Truncate(Math.Max(Math.Abs(CInt(Math.Truncate(Me.Ax))), Math.Abs(CInt(Math.Truncate(Me.Ay))))))
    End Sub

	Public Sub draw(ByVal hdc As System.IntPtr, ByVal x As Single, ByVal y As Single)
		Dim OrigX As Integer
		Dim OrigY As Integer
		Dim NewX As Integer
		Dim NewY As Integer
		Dim t As Single
		Dim f As Single
		Dim g As Single
		Dim h As Single
		If Ndiv = 0 Then
			Ndiv = 1
		End If

		OrigX = CInt(Math.Truncate(x))
		OrigY = CInt(Math.Truncate(y))
		For i As Integer = 1 To Ndiv
			t = 1.0f / CSng(Ndiv) * CSng(i)
			f = t *t*(3.0f-2.0f *t)
			g = t*(t-1.0f)*(t-1.0f)
			h = t *t*(t-1.0f)
			NewX = CInt(Math.Truncate(x + Ax *f + Bx *g + Cx *h))
			NewY = CInt(Math.Truncate(y + Ay *f + By *g + Cy *h))
            'MoveToEx(hdc, OrigX, OrigY, Nothing)
            'LineTo(hdc, NewX, NewY)
            OrigX = NewX
			OrigY = NewY
		Next i
	End Sub
	Public Function GetCount() As Integer
		If Ndiv = 0 Then
			Ndiv = 1
		End If
		Dim PointCount As Integer = 1

		For i As Integer = 1 To Ndiv
			PointCount += 1
		Next i
		Return PointCount
	End Function
	Public Sub GetCurve(ByVal x As Single, ByVal y As Single, ByVal points() As POINT, ByRef PointCount As Integer)
        Dim X_Renamed As Integer
        Dim Y_Renamed As Integer
        Dim t As Single
        Dim f As Single
        Dim g As Single
        Dim h As Single
        If Ndiv = 0 Then
            Ndiv = 1
        End If

        X_Renamed = CInt(Math.Truncate(x))
        Y_Renamed = CInt(Math.Truncate(y))
        points(PointCount).X = X_Renamed
        points(PointCount).Y = Y_Renamed
        PointCount += 1

        For i As Integer = 1 To Ndiv
            t = 1.0F / CSng(Ndiv) * CSng(i)
            f = t * t * (3.0F - 2.0F * t)
            g = t * (t - 1.0F) * (t - 1.0F)
            h = t * t * (t - 1.0F)
            X_Renamed = CInt(Math.Truncate(x + Ax * f + Bx * g + Cx * h))
            Y_Renamed = CInt(Math.Truncate(y + Ay * f + By * g + Cy * h))
            points(PointCount).X = X_Renamed
            points(PointCount).Y = Y_Renamed
            PointCount += 1
		Next i
	End Sub

End Class

Public Class MSpline
    Implements System.IDisposable

    Public Px() As Single
    Public Py() As Single
    Public Ax() As Single
    Public Ay() As Single
    Public Bx() As Single
    Public By() As Single
    Public Cx() As Single
    Public Cy() As Single
    Public k() As Single
    'C++ TO VB CONVERTER TODO TASK: VB does not have an equivalent to pointers to value types:
    'ORIGINAL LINE: Single* Mat[3];
    Public Mat(2) As Object

    Public NP As Integer

    ' constructor
    Public Sub New(ByVal pt() As Point, ByVal np As Integer)
        Me.NP = np
        Px = New Single(Me.NP - 1) {}
        Py = New Single(Me.NP - 1) {}
        Ax = New Single(Me.NP - 1) {}
        Ay = New Single(Me.NP - 1) {}
        Bx = New Single(Me.NP - 1) {}
        By = New Single(Me.NP - 1) {}
        Cx = New Single(Me.NP - 1) {}
        Cy = New Single(Me.NP - 1) {}
        k = New Single(Me.NP - 1) {}
        Mat(0) = New Single(Me.NP - 1) {}
        Mat(1) = New Single(Me.NP - 1) {}
        Mat(2) = New Single(Me.NP - 1) {}

        For i As Integer = 0 To Me.NP - 1
            Px(i) = CSng(pt(i).X)
            Py(i) = CSng(pt(i).Y)
        Next i

    End Sub

    Public Sub New(ByVal px() As Single, ByVal py() As Single, ByVal np As Integer)
        Me.NP = np
        Me.Px = New Single(Me.NP - 1) {}
        Me.Py = New Single(Me.NP - 1) {}
        Ax = New Single(Me.NP - 1) {}
        Ay = New Single(Me.NP - 1) {}
        Bx = New Single(Me.NP - 1) {}
        By = New Single(Me.NP - 1) {}
        Cx = New Single(Me.NP - 1) {}
        Cy = New Single(Me.NP - 1) {}
        k = New Single(Me.NP - 1) {}
        Mat(0) = New Single(Me.NP - 1) {}
        Mat(1) = New Single(Me.NP - 1) {}
        Mat(2) = New Single(Me.NP - 1) {}

        For i As Integer = 0 To Me.NP - 1
            Me.Px(i) = px(i)
            Me.Py(i) = py(i)
        Next i

    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        'Arrays.DeleteArray(Px)
        'Arrays.DeleteArray(Py)
        'Arrays.DeleteArray(Ax)
        'Arrays.DeleteArray(Ay)
        'Arrays.DeleteArray(Bx)
        'Arrays.DeleteArray(By)
        'Arrays.DeleteArray(Cx)
        'Arrays.DeleteArray(Cy)
        'Arrays.DeleteArray(k)
        'Arrays.DeleteArray(Mat(0))
        'Arrays.DeleteArray(Mat(1))
        'Arrays.DeleteArray(Mat(2))
    End Sub

    Public Sub Generate()
        Dim AMag As Single
        Dim AMagOld As Single
        ' vector A
        Dim i As Integer = 0
        Do While i <= NP - 2
            Ax(i) = Px(i + 1) - Px(i)
            Ay(i) = Py(i + 1) - Py(i)
            i += 1
        Loop
        ' k
        AMagOld = CSng(Math.Sqrt(Ax(0) * Ax(0) + Ay(0) * Ay(0)))
        i = 0
        Do While i <= NP - 3
            AMag = CSng(Math.Sqrt(Ax(i + 1) * Ax(i + 1) + Ay(i + 1) * Ay(i + 1)))
            k(i) = AMagOld / AMag
            AMagOld = AMag
            i += 1
        Loop
        k(NP - 2) = 1.0F

        ' Matrix
        i = 1
        Do While i <= NP - 2
            Mat(0)(i) = 1.0F
            Mat(1)(i) = 2.0F * k(i - 1) * (1.0F + k(i - 1))
            Mat(2)(i) = k(i - 1) * k(i - 1) * k(i)
            i += 1
        Loop
        Mat(1)(0) = 2.0F
        Mat(2)(0) = k(0)
        Mat(0)(NP - 1) = 1.0F
        Mat(1)(NP - 1) = 2.0F * k(NP - 2)

        ' 
        i = 1
        Do While i <= NP - 2
            Bx(i) = 3.0F * (Ax(i - 1) + k(i - 1) * k(i - 1) * Ax(i))
            By(i) = 3.0F * (Ay(i - 1) + k(i - 1) * k(i - 1) * Ay(i))
            i += 1
        Loop
        Bx(0) = 3.0F * Ax(0)
        By(0) = 3.0F * Ay(0)
        Bx(NP - 1) = 3.0F * Ax(NP - 2)
        By(NP - 1) = 3.0F * Ay(NP - 2)

        '
        MatrixSolve(Bx)
        MatrixSolve(By)

        i = 0
        Do While i <= NP - 2
            Cx(i) = k(i) * Bx(i + 1)
            Cy(i) = k(i) * By(i + 1)
            i += 1
        Loop
    End Sub

    Public Sub MatrixSolve(ByVal B() As Single)
        Dim Work(NP - 1) As Single
        Dim WorkB(NP - 1) As Single
        Dim i As Integer = 0
        Do While i <= NP - 1
            Work(i) = B(i) / Mat(1)(i)
            WorkB(i) = Work(i)
            i += 1
        Loop

        For j As Integer = 0 To 9 '/  need convergence judge
            Work(0) = (B(0) - Mat(2)(0) * WorkB(1)) / Mat(1)(0)
            i = 1
            Do While i < NP - 1
                Work(i) = (B(i) - Mat(0)(i) * WorkB(i - 1) - Mat(2)(i) * WorkB(i + 1)) / Mat(1)(i)
                i += 1
            Loop
            Work(NP - 1) = (B(NP - 1) - Mat(0)(NP - 1) * WorkB(NP - 2)) / Mat(1)(NP - 1)

            i = 0
            Do While i <= NP - 1
                WorkB(i) = Work(i)
                i += 1
            Loop
        Next j
        i = 0
        Do While i <= NP - 1
            B(i) = Work(i)
            i += 1
        Loop
        Work = Nothing
        WorkB = Nothing
    End Sub

    Public Sub draw(ByVal hdc As System.IntPtr)
        Dim c As New Curve()
        Dim i As Integer = 0
        Do While i < NP - 1
            c.PutCurve(Ax(i), Ay(i), Bx(i), By(i), Cx(i), Cy(i))
            c.draw(hdc, Px(i), Py(i))
            i += 1
        Loop

    End Sub
    Public Function GetCurveCount() As Integer
        Dim c As New Curve()
        Dim count As Integer = 0
        Dim i As Integer = 0
        Do While i < NP - 1
            c.PutCurve(Ax(i), Ay(i), Bx(i), By(i), Cx(i), Cy(i))
            count += c.GetCount()
            i += 1
        Loop
        Return count
    End Function
    Public Sub GetCurve(ByVal points() As Point, ByRef PointCount As Integer)
        Dim c As New Curve()
        Dim i As Integer = 0
        Do While i < NP - 1
            c.PutCurve(Ax(i), Ay(i), Bx(i), By(i), Cx(i), Cy(i))
            c.GetCurve(Px(i), Py(i), points, PointCount)
            i += 1
        Loop
    End Sub
    '////////// closed cubic spline ////////////////////
    Public Sub GenClosed()
        Dim AMag As Single
        Dim AMagOld As Single
        Dim AMag0 As Single
        ' vector A
        Dim i As Integer = 0
        Do While i <= NP - 2
            Ax(i) = Px(i + 1) - Px(i)
            Ay(i) = Py(i + 1) - Py(i)
            i += 1
        Loop
        Ax(NP - 1) = Px(0) - Px(NP - 1)
        Ay(NP - 1) = Py(0) - Py(NP - 1)

        ' k
        'C++ TO VB CONVERTER WARNING: An assignment within expression was extracted from the following statement:
        'ORIGINAL LINE: AMag0 = AMagOld = (Single)sqrt(Ax[0]*Ax[0] + Ay[0]*Ay[0]);
        AMagOld = CSng(Math.Sqrt(Ax(0) * Ax(0) + Ay(0) * Ay(0)))
        AMag0 = AMagOld
        i = 0
        Do While i <= NP - 2
            AMag = CSng(Math.Sqrt(Ax(i + 1) * Ax(i + 1) + Ay(i + 1) * Ay(i + 1)))
            k(i) = AMagOld / AMag
            AMagOld = AMag
            i += 1
        Loop
        k(NP - 1) = AMagOld / AMag0

        ' Matrix
        i = 1
        Do While i <= NP - 1
            Mat(0)(i) = 1.0F
            Mat(1)(1) = 2.0F * k(i - 1) * (1.0F + k(i - 1))
            Mat(2)(i) = k(i - 1) * k(i - 1) * k(i)
            i += 1
        Loop
        Mat(0)(0) = 1.0F
        Mat(1)(0) = 2.0F * k(NP - 1) * (1.0F + k(NP - 1))
        Mat(2)(0) = k(NP - 1) * k(NP - 1) * k(0)

        ' 
        i = 1
        Do While i <= NP - 1
            Bx(i) = 3.0F * (Ax(i - 1) + k(i - 1) * k(i - 1) * Ax(i))
            By(i) = 3.0F * (Ay(i - 1) + k(i - 1) * k(i - 1) * Ay(i))
            i += 1
        Loop
        Bx(0) = 3.0F * (Ax(NP - 1) + k(NP - 1) * k(NP - 1) * Ax(0))
        By(0) = 3.0F * (Ay(NP - 1) + k(NP - 1) * k(NP - 1) * Ay(0))

        '
        MatrixSolveEX(Bx)
        MatrixSolveEX(By)

        i = 0
        Do While i <= NP - 2
            Cx(i) = k(i) * Bx(i + 1)
            Cy(i) = k(i) * By(i + 1)
            i += 1
        Loop
        Cx(NP - 1) = k(NP - 1) * Bx(0)
        Cy(NP - 1) = k(NP - 1) * By(0)
    End Sub

    '/// tridiagonal matrix + elements of [0][0], [N-1][N-1] //// 
    Public Sub MatrixSolveEX(ByVal B() As Single)
        Dim Work(NP - 1) As Single
        Dim WorkB(NP - 1) As Single

        Dim i As Integer = 0
        Do While i <= NP - 1
            Work(i) = B(i) / Mat(1)(i)
            WorkB(i) = Work(i)
            i += 1
        Loop

        For j As Integer = 0 To 9 ' need judge of convergence
            Work(0) = (B(0) - Mat(0)(0) * WorkB(NP - 1) - Mat(2)(0) * WorkB(1)) / Mat(1)(0)
            i = 1
            Do While i < NP - 1
                Work(i) = (B(i) - Mat(0)(i) * WorkB(i - 1) - Mat(2)(i) * WorkB(i + 1)) / Mat(1)(i)
                i += 1
            Loop
            Work(NP - 1) = (B(NP - 1) - Mat(0)(NP - 1) * WorkB(NP - 2) - Mat(2)(NP - 1) * WorkB(0)) / Mat(1)(NP - 1)

            i = 0
            Do While i <= NP - 1
                WorkB(i) = Work(i)
                i += 1
            Loop
        Next j

        i = 0
        Do While i <= NP - 1
            B(i) = Work(i)
            i += 1
        Loop
    End Sub

    Public Sub drawClosed(ByVal hdc As System.IntPtr)
        Dim c As New Curve()
        For i As Integer = 0 To NP - 1
            c.PutCurve(Ax(i), Ay(i), Bx(i), By(i), Cx(i), Cy(i))
            c.draw(hdc, Px(i), Py(i))
        Next i
    End Sub


End Class

