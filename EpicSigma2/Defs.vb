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

        Public Function MatchingElements(ByVal dxDoc As DxfDocument) As dxfItemSet

            Dim doesItemMatchConditions As Boolean
            Dim Result As New List(Of Object)
            Dim ReturnSet As New dxfItemSet

            Select Case ObjectType
                Case EnumObjectType.Block
                    ReturnSet.itemType = GetType(netDxf.Entities.Insert)
                    For Each T In dxDoc.Inserts
                        doesItemMatchConditions = False

                        ''TODO additional checks, if this is actually an OK block (excluding xrefs, images, ect)
                        For Each C In Conditions
                            doesItemMatchConditions = doConditionsMatch(
                                TestablePropertyValue(New dxfItem With {.itemType = T.GetType, .item = T}, C.TestableProperty),
                                C.MatchValue,
                                C.CompareType)
                        Next
                        If doesItemMatchConditions = True Then
                            ReturnSet.items.Add(T)
                        End If
                    Next

                Case EnumObjectType.Line
                    ReturnSet.itemType = GetType(netDxf.Entities.Line)
                    For Each T In dxDoc.Lines

                    Next
                Case EnumObjectType.PLine
                    ReturnSet.itemType = GetType(netDxf.Entities.LwPolyline)
                    For Each T In dxDoc.LwPolylines

                    Next
                Case EnumObjectType.Text
                    ReturnSet.itemType = GetType(netDxf.Entities.Text)
                    For Each T In dxDoc.Texts

                    Next
                Case EnumObjectType.MText
                    ReturnSet.itemType = GetType(netDxf.Entities.MText)
                    For Each T In dxDoc.MTexts

                    Next
                Case EnumObjectType.Hatch
                    ReturnSet.itemType = GetType(netDxf.Entities.Hatch)
                    For Each T In dxDoc.Hatches

                    Next
            End Select

            Return ReturnSet

        End Function


        Private Function TestablePropertyValue(ByVal dxTestableItem As dxfItem, ByVal TestableProperty As TestablePropertyDescriptor) As String

            ''TODO write this

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

    Public Class dxfItemSet
        Public itemType As Type
        Public items As New List(Of Object)
    End Class

    Public Class dxfItem
        Public itemType As Type
        Public item As Object
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
