Namespace My
    Friend Class MyApplication
        Private WithEvents DLLDomain As AppDomain = AppDomain.CurrentDomain
        Private Function DLL_AssemblyResolve(ByVal sender As Object, ByVal args As System.ResolveEventArgs) As System.Reflection.Assembly Handles DLLDomain.AssemblyResolve
            If args.Name.Contains("pdfsharp") Then
                Return System.Reflection.Assembly.Load(My.Resources.PdfSharp)
            Else
                Return Nothing
            End If
        End Function
    End Class
End Namespace