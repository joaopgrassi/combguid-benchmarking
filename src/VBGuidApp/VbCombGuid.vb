Public Class VbCombGuid
    Public Shared Function GetNewID() As Guid
        Return New Guid(Guid.NewGuid.ToString.Substring(0, 24) & Hex(DateTime.UtcNow.ToBinary).ToString.Substring(0, 12))
    End Function

End Class
