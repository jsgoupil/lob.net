rd /S /Q .\publish

call build.bat
if %errorlevel% neq 0 exit /b %errorlevel%

md .\publish

dotnet pack .\src\Lob.Net.csproj -c Release --include-symbols --no-build -o ..\publish
if %errorlevel% neq 0 exit /b %errorlevel%
