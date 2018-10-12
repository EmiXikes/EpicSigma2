Imports netDxf
Imports netDxf.Entities

Public Class Defs
    Public Class TEntry
        Public OutFields As Object

        Public ObjectType As EnumObjectType
        Public Conditions As New List(Of TCondition)

        Public Function AddCondition(ByVal NewCondition As TCondition)
            Conditions.Add(NewCondition)
            Return 0
        End Function

        Public Function MatchingElements(ByVal dxDoc As DxfDocument) As List(Of Object)

            Dim doesItemMatchConditions As Boolean
            Dim Result As New List(Of Object)

            Select Case ObjectType
                Case EnumObjectType.Block
                    For Each T In dxDoc.Inserts
                        doesItemMatchConditions = False

                        ''TODO additional checks, if this is actually an OK block (excluding xrefs, images, ect)
                        For Each C In Conditions
                            doesItemMatchConditions = doConditionsMatch(TestablePropertyValue(T, C.TestableProperty), C.MatchValue, C.CompareType)
                        Next
                        If doesItemMatchConditions = True Then
                            Result.Add(T)
                        End If
                    Next

                Case EnumObjectType.Line
                    For Each T In dxDoc.Lines

                    Next
                Case EnumObjectType.PLine
                    For Each T In dxDoc.LwPolylines

                    Next
                Case EnumObjectType.Text
                    For Each T In dxDoc.Texts

                    Next
                Case EnumObjectType.MText
                    For Each T In dxDoc.MTexts

                    Next
                Case EnumObjectType.Hatch
                    For Each T In dxDoc.Hatches

                    Next
            End Select



        End Function


        Private Function TestablePropertyValue(ByVal dxTestableItem As Object, ByVal CondTestableValDescriptor As TestablePropertyDescriptor) As String

        End Function

        Private Function doConditionsMatch(ByVal TestValue As String, ByVal MatchValue As String, CompareType As EnumCompareType) As Boolean
            Select Case CompareType
                Case EnumCompareType.Equals
                    If TestValue = MatchValue Then Return True
                Case EnumCompareType.Contains
                    If TestValue.Contains(MatchValue) Then Return True
            End Select
            Return False
        End Function

    End Class

    Public Class TCondition

        Public MatchValue As String

        Public ObjectType As EnumObjectType
        Public TestableProperty As TestablePropertyDescriptor

        Public CompareType As EnumCompareType

    End Class



    Public Class TestablePropertyDescriptor

        Public TestableProp As EnumTestableProp
        Public AttrTagName As String

    End Class

    Enum EnumTestableProp
        Layer
        Color
        BPositionX
        BPositionY
        BPositionZ
        THeight
        Attribute
    End Enum



    Enum EnumObjectType
        Block
        Line
        PLine
        Text
        MText
        Hatch
    End Enum


    Enum EnumCompareType
        Equals
        Contains
    End Enum
End Class
