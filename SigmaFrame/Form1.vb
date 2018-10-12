Imports EpicSigma2.Defs

Public Class Form1

    Dim LT As New List(Of TEntry)

    Dim LC As New List(Of TCondition)


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'LC.Add(New TCondition With {
        '       .MatchValue = "tyw",
        '       .Type = CondType.Equals
        '       })
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim TE As New TEntry
        'TE.AddCondition(New TCondition With {
        '    .MatchValue = "Match!",
        '    .Type = CondType.Contains
        '                })
        'LT.Add(TE)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        For Each C In LT(0).Conditions
            ''MsgBox(C.isMatch)
        Next
    End Sub
End Class
