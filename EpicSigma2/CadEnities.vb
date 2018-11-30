Imports System.Drawing

Public Class CadEnities
    Class GeneralAttributes
        Public Property Layer As String
        Public Property Color As String
    End Class

    Public Class E_Block
        Inherits GeneralAttributes
        Public Property Position As PointF
        Public Property Scale As Double
        Public Property Rotation As Double
        Public Property BlockTrueName As String
        Public Property Attributes As IDictionary(Of String, String)
    End Class
End Class
