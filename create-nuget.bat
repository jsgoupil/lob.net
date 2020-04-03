rd /S /Q .\publish

call build.bat
if %errorlevel% neq 0 exit /b %errorlevel%

md .\publish

dotnet pack .\src\Lob.Net\Lob.Net.csproj -c Release --include-symbols --no-build -o .\publish
if %errorlevel% neq 0 exit /b %errorlevel%

dotnet pack .\src\Lob.Net.Formatters\Lob.Net.Formatters.csproj -c Release --include-symbols --no-build -o .\publish
if %errorlevel% neq 0 exit /b %errorlevel%

dotnet pack .\src\Lob.NetCore.Formatters\Lob.NetCore.Formatters.csproj -c Release --include-symbols --no-build -o .\publish
if %errorlevel% neq 0 exit /b %errorlevel%
