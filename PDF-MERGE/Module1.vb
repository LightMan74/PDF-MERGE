Imports PdfSharp
Imports PdfSharp.Drawing
Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.IO
Imports System.IO
Module Module1
    Public Sub Main()
        Dim arguments As String() = Environment.GetCommandLineArgs()
        For i = 1 To Environment.GetCommandLineArgs.GetUpperBound(0)
            Dim argus As String = Environment.GetCommandLineArgs(i)
            Dim file(2) As String
            If argus = "-files" Then
                i += 1
                file(0) = Environment.GetCommandLineArgs(i)
                i += 1
                file(1) = Environment.GetCommandLineArgs(i)
                mergefile({file(0), file(1)})
            ElseIf argus = "-folders" Then
                i += 1
                mergefolder(Environment.GetCommandLineArgs(i))
            ElseIf argus = "-help" Or argus = "-h" Or argus = "-?" Or argus = "?" Then
                Dim m As String = "Arguments Help" & vbLf & "ATTENTION à l'ordre des arguments" & vbLf & vbLf &
                    "Fusionné 2 PDF, le nom de sortie utilisera le nom du premier pdf" & vbLf &
                    "-files " & Chr(34) & "chemin_Fichier1_Nom_de_sortie" & Chr(34) & " " & Chr(34) & "chemin_Fichier2" & Chr(34) & vbLf & vbLf &
                     "Fusionné un dossier avec les sous dossiers, le nom de sortie utilisera le nom du dossier" & vbLf &
                    "-folders " & Chr(34) & "chemin_Dossier" & Chr(34) & " /!\ sans de '\' a la fin"
            End If
        Next i

        'merge({"./FIW-00005.pdf", "./CVG.pdf"})
    End Sub

    Public Sub mergefile(files As String())

        Dim outputDocument As New PdfDocument

        For Each file In files

            Dim inputDocument As New PdfDocument
            inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import)
            Dim counts As Integer = inputDocument.PageCount - 1

            For i = 0 To counts
                Dim page As PdfPage
                page = inputDocument.Pages(i)
                outputDocument.AddPage(page)
            Next i

        Next 'files

        outputDocument.Save(files(0))


        'Process.Start(files(0))

    End Sub

    Public Sub mergefolder(files As String)
        Dim namesave As String = files
        namesave = StrReverse(namesave)
        namesave = namesave.Split("\")(0)
        namesave = StrReverse(namesave)
        namesave = files & "\" & namesave & "-compiltation.pdf"
        Dim outputDocument As New PdfDocument

        Dim di As DirectoryInfo = New DirectoryInfo(files)

        For Each file In di.GetFiles("*", SearchOption.AllDirectories)

            Dim inputDocument As New PdfDocument
            inputDocument = PdfReader.Open(file.FullName, PdfDocumentOpenMode.Import)
            Dim counts As Integer = inputDocument.PageCount - 1

            For i = 0 To counts
                Dim page As PdfPage
                page = inputDocument.Pages(i)
                outputDocument.AddPage(page)
            Next i

        Next 'files
        outputDocument.Save(namesave)


        'Process.Start(files(0))

    End Sub



End Module
