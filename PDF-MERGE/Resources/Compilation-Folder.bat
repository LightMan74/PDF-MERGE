SET /P _inputname= Merci d'indique le dossier sous forme '20xx-xx' : 
set _cdstart=%CD%
cd..
cd..
cd..
cd COMPTABILITE
set _cdcompta=%CD%
cd %_cdstart%

PDF-MERGE.exe -folders "%_cdcompta%\ACHAT\%_inputname%"

PDF-MERGE.exe -folders "%_cdcompta%\VENTE\%_inputname%"

PDF-MERGE.exe -folders "%_cdcompta%\BANQUE\RC\%_inputname%"

PDF-MERGE.exe -folders "%_cdcompta%\BANQUE\%_inputname%\RELEVER"

if not exist "%_cdcompta%\EXPORT_COMPTABLE\%_inputname%" mkdir "%_cdcompta%\EXPORT_COMPTABLE\%_inputname%"

copy "%_cdcompta%\ACHAT\%_inputname%\%_inputname%-COMPILATION.pdf" "%_cdcompta%\EXPORT_COMPTABLE\%_inputname%\%_inputname%-ACHAT-COMPILATION.pdf"
copy "%_cdcompta%\VENTE\%_inputname%\%_inputname%-COMPILATION.pdf" "%_cdcompta%\EXPORT_COMPTABLE\%_inputname%\%_inputname%-VENTE-COMPILATION.pdf"
copy "%_cdcompta%\BANQUE\RC\%_inputname%\%_inputname%-COMPILATION.pdf" "%_cdcompta%\EXPORT_COMPTABLE\%_inputname%\%_inputname%-RC-COMPILATION.pdf"
copy "%_cdcompta%\BANQUE\%_inputname%\RELEVER\RELEVER-COMPILATION.pdf" "%_cdcompta%\EXPORT_COMPTABLE\%_inputname%\%_inputname%-RELEVER-COMPILATION.pdf"
copy "%_cdcompta%\TVA\COMPTA-%_inputname%.pdf" "%_cdcompta%\EXPORT_COMPTABLE\%_inputname%\COMPTA-%_inputname%.pdf"
copy "%_cdcompta%\TVA\COMPTA-COMPILATION.pdf" "%_cdcompta%\EXPORT_COMPTABLE\%_inputname%\COMPTA-COMPILATION.pdf"
copy "%_cdcompta%\TVA\COMPTA-COMPILATION-NON_PAYEE.pdf" "%_cdcompta%\EXPORT_COMPTABLE\%_inputname%\COMPTA-COMPILATION-NON_PAYEE.pdf"

pause