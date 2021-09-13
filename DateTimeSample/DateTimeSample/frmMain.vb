Public Class frmMain
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        lstIntervalAdd.Items.Add("Day")
        lstIntervalAdd.Items.Add("Month")
        lstIntervalAdd.Items.Add("Year")
        lstOp.Items.Add("Add")
        lstOp.Items.Add("Subtract")

        lstIntervalSpan.Items.Add("Days")
        lstIntervalSpan.Items.Add("Hours")
        lstIntervalSpan.Items.Add("Minutes")
        lstIntervalSpan.Items.Add("Seconds")
        dtmEndSpan.Format = DateTimePickerFormat.Custom
        dtmStartSpan.CustomFormat = "hh:mm:ss"
        dtmEndSpan.Format = DateTimePickerFormat.Custom
        dtmEndSpan.CustomFormat = "hh:mm:ss"

    End Sub

    Private Function ElapsedTime(ByVal dtmEarly As Date, ByVal dtmLate As Date, strInterval As String) As Integer
        Dim tspDifference As TimeSpan = dtmLate.Subtract(dtmEarly)
        Select Case strInterval
            Case "Days"
                Return tspDifference.TotalDays
            Case "Hours"
                Return tspDifference.TotalHours
            Case "Minutes"
                Return tspDifference.TotalMinutes
            Case "Seconds"
                Return tspDifference.TotalSeconds

            Case Else
                Return tspDifference.TotalDays
        End Select
    End Function

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim dtiIntervalType As DateInterval
        Dim blnErrors As Boolean
        Dim dblAmount As Double

        If Not ValidateListBox(lstIntervalAdd, errP) Then
            blnErrors = True

        End If
        If Not ValidateListBox(lstOp, errP) Then
            blnErrors = True

        End If
        If Not ValidateTextBoxNumeric(txtQty, errP) Then
            blnErrors = True
        End If
        If blnErrors Then
            Exit Sub
        End If
        dblAmount = CDbl(txtQty.Text)
        If lstOp.SelectedItem = "Subtract" Then
            dblAmount *= -1
        End If
        Select Case lstIntervalAdd.SelectedItem.ToString
            Case "Day"
                dtiIntervalType = DateInterval.Day
            Case "Month"
                dtiIntervalType = DateInterval.Month
            Case "Year"
                dtiIntervalType = DateInterval.Year
            Case Else
                MessageBox.Show("Unexpected date interbal in btnAdd_Click", "Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Select

        dtmEndAdd.Value = DateAdd(dtiIntervalType, dblAmount, dtmStartAdd.Value)
        lblResult.Text = CStr(dtmEndAdd.Value)
        lblAlt.Text = Format(dtmEndAdd.Value, "MM-dd-yyyy")
    End Sub

    Private Sub btnSpan_Click(sender As Object, e As EventArgs) Handles btnSpan.Click
        lblElapsedUnits.Text = CStr(ElapsedTime(dtmStartSpan.Value, dtmEndSpan.Value, lstIntervalSpan.SelectedItem.ToString))

    End Sub

    Private Sub btnUpdateSpan_Click(sender As Object, e As EventArgs) Handles btnUpdateSpan.Click
        Dim dtmTempDate As Date
        Dim timIn As Date
        Dim datIn As Date

        timIn = FormatDateTime(dtmEndSpan.Value, DateFormat.LongTime)
        datIn = FormatDateTime(dtmEndSpan.Value, DateFormat.ShortDate)
        dtmTempDate = New Date(DatePart(DateInterval.Year, datIn), DatePart(DateInterval.Month, datIn), DatePart(DateInterval.Day, datIn),
                               DatePart(DateInterval.Hour, timIn), DatePart(DateInterval.Minute, timIn), DatePart(DateInterval.Second, timIn))
        dtmStartSpan.Value = dtmTempDate
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    Private Sub btnClearAdd_Click(sender As Object, e As EventArgs) Handles btnClearAdd.Click
        ClearScreenControls(Me)
        lblAlt.Text = ""
        lblResult.Text = ""

    End Sub

    Private Sub btnClearSpan_Click(sender As Object, e As EventArgs) Handles btnClearSpan.Click

        ClearScreenControls(Me)
        lblElapsedUnits.Text = ""


    End Sub


End Class
