echo change postgres password in "appsettings.json"

set dir=%~dp0
cd %dir%\LoginService\

echo generate certs
dotnet dev-certs https --clean
dotnet dev-certs https -t

call dotnet LoginService.dll"


