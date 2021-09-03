Imports PdfSharp
Imports PdfSharp.Drawing
Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.IO
Imports System.IO
Module Module1
    Public Sub Main()

        Dim argument As String
        For i = 1 To Environment.GetCommandLineArgs.GetUpperBound(0)
            argument += Chr(34) & Environment.GetCommandLineArgs(i) & Chr(34) & " "
        Next

        If Not File.Exists(".\PdfSharp.dll") Then
            pdfsharpdll()
            cmd(argument)
            Exit Sub
        End If
        If Not File.Exists(".\PdfSharp.Charting.dll") Then
            pdfsharpchartingdll()
            cmd(argument)
            Exit Sub
        End If

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
                MsgBox(m, MsgBoxStyle.SystemModal)
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
        namesave = files & "\" & namesave & "-COMPILATION.pdf"
        Dim outputDocument As New PdfDocument

        Dim di As DirectoryInfo = New DirectoryInfo(files)

        For Each file In di.GetFiles("*.pdf", SearchOption.AllDirectories)
            If file.Name.Contains("-COMPILATION.pdf") Then
                Continue For
            End If
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


    Public Sub pdfsharpdll()
        Dim FileName As String = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "PdfSharp.dll")
        Dim BytesToWrite() As Byte = My.Resources.PdfSharp
        Dim FileStream As New System.IO.FileStream(FileName, System.IO.FileMode.OpenOrCreate)
        Dim BinaryWriter As New System.IO.BinaryWriter(FileStream)
        BinaryWriter.Write(BytesToWrite)
        BinaryWriter.Close()
        FileStream.Close()
    End Sub

    Public Sub pdfsharpchartingdll()
        Dim FileName As String = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "PdfSharp.Charting.dll")
        Dim BytesToWrite() As Byte = My.Resources.PdfSharp_Charting
        Dim FileStream As New System.IO.FileStream(FileName, System.IO.FileMode.OpenOrCreate)
        Dim BinaryWriter As New System.IO.BinaryWriter(FileStream)
        BinaryWriter.Write(BytesToWrite)
        BinaryWriter.Close()
        FileStream.Close()
    End Sub


    Public Sub cmd(parametre As String) 'cmd(command As String, arguments As String, permanent As Boolean)
        Dim p As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo()
        'pi.Arguments = " " + If(permanent = True, "/K", "/C") + " " + command + " " + arguments
        pi.Arguments = parametre
        pi.FileName = "PDF-MERGE.exe"
        p.StartInfo = pi
        p.Start()
        p.WaitForExit()
    End Sub

End Module
