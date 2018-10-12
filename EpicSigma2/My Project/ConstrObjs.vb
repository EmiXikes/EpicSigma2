Module ConstrObjs

    Public Class TEntry
        Public OutFields As Object
        Public Conditions As New List(Of TCondition)
        Public dxfitems As New List(Of Object)

        Public Function AddCondition(ByVal NewCondition As TCondition)

        End Function

        Public Function AddDxfItem(ByVal NewDxfItem As Object)

        End Function

    End Class

    Public Class TCondition

        Public MatchValue As String
        Public TestValue As TestValueDesc
        Public Type As CondType

        Public Function isMatch() As Boolean
            Return True
        End Function

    End Class

    Public Class TestValueDesc

        Public Section As TValueSection
        Public Name As String

    End Class

    Enum TValueSection
        General
        Attributes
    End Enum

    Enum CondType
        Equals
        Contains
    End Enum

End Module



