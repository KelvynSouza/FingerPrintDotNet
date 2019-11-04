if not exist "%~dp0GeneratedReports" mkdir "%~dp0GeneratedReports"

if exist "%~dp0GeneratedReports\TestsService.trx" del "%~dp0GeneratedReports\TestsService.trx"
if exist "%~dp0GeneratedReports\TestsService.html" del "%~dp0GeneratedReports\TestsService.html"

cd %~dp0
for /D /R %%X IN (%USERNAME%) DO RD /S /Q "%%X"

CALL :RunTests

exit /b %errorlevel%

:RunTests
cd %~dp0FingerPrint.Tests\
dotnet test --logger "trx;LogFileName=%~dp0GeneratedReports\TestsService.trx"
exit /b %errorlevel%