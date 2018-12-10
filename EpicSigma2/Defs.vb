Imports netDxf
Imports netDxf.Entities
Imports EpicSigma2.CadEnities

Public Class HelperFunctions
    Public Shared Function ExtractDxfItems(ByVal DxDocs As List(Of DxfDocument)) As List(Of Defs.dxfItem)
        'TODO write a merging function, that aggragates all the dxfitems from the given dxfDocuments

        Dim ResultList As New List(Of Defs.dxfItem)

        For Each DxDoc In DxDocs
            For Each I In DxDoc.Inserts
                ResultList.Add(New Defs.dxfItem With {.item = I, .itemType = Defs.EnumObjectType.Block})
            Next
            For Each I In DxDoc.Hatches
                ResultList.Add(New Defs.dxfItem With {.item = I, .itemType = Defs.EnumObjectType.Hatch})
            Next

            'TODO Complete the list
        Next

        Return ResultList

    End Function
End Class

Public Class Defs

    Public Class TEntry
        Public OutFields As Object

        ''Public ObjectType As EnumObjectType
        Public Conditions As New List(Of TCondition)

        Public Function AddCondition(ByVal NewCondition As TCondition)
            Conditions.Add(NewCondition)
            Return 0
        End Function

        Public Function MatchingElements(ByVal DxDocs As List(Of DxfDocument)) As List(Of dxfItem)

            Dim doesItemMatchConditions As Boolean
            Dim Result As New List(Of Object)
            Dim ReturnSet As New List(Of dxfItem)

            Dim InputDxfItems As List(Of dxfItem) = HelperFunctions.ExtractDxfItems(DxDocs)

            For Each D In InputDxfItems
                doesItemMatchConditions = False
                For Each C In Conditions
                    doesItemMatchConditions = doConditionsMatch(TestablePropertyValue(D, C.TestableProperty), C.MatchValue, C.CompareType)
                Next
                If doesItemMatchConditions = True Then
                    ReturnSet.Add(D)
                End If
            Next

            Return ReturnSet

            'Select Case ObjectType
            '    Case EnumObjectType.Block
            '        ReturnSet.itemType = GetType(netDxf.Entities.Insert)
            '        For Each T In dxDoc.Inserts
            '            doesItemMatchConditions = False

            '            ''TODO additional checks, if this is actually an OK block (excluding xrefs, images, ect)
            '            For Each C In Conditions
            '                doesItemMatchConditions = doConditionsMatch(
            '                    TestablePropertyValue(New dxfItem With {.itemType = T.GetType, .item = T}, C.TestableProperty),
            '                    C.MatchValue,
            '                    C.CompareType)
            '            Next
            '            If doesItemMatchConditions = True Then
            '                ReturnSet.items.Add(T)
            '            End If
            '        Next

            'End Select

            'Return ReturnSet

        End Function

        Private Function TestablePropertyValue(ByVal dxTestableItem As dxfItem, ByVal TestableProperty As TestablePropertyDescriptor) As String

            ''TODO maybe encapsulate the input object dxTestableItem in a custom class together with actual/exact dxObj type descriptor EnumObjectType
            Select Case TestableProperty.TestableProp
                Case EnumTestableProp.Layer
                    Return dxTestableItem.item.Layer.Name
                Case EnumTestableProp.Color
                    Return dxTestableItem.item.Color.ToString()

                    'If dxTestableItem.itemType = EnumObjectType.Block Then Return dxTestableItem.item.Layer.Name
                    ''Dim dxObj As netDxf.Entities.Insert = dxTestableItem.item
                    ''dxObj.Color.ToString()

            End Select
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
        Public itemType As EnumObjectType
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
