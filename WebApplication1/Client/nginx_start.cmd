@echo off

set dir=%~dp0
echo current dir: %dir%

echo delete old files %dir%nginx\html\*
del /S /Q %dir%nginx\html\*
echo ''
echo copy new files from %dir%ClientApp\dist\*.* to %dir%nginx\html\
xcopy %dir%ClientApp\dist\*.* %dir%nginx\html /K /D /H /Y
echo ''

echo kill %dir%nginx/nginx.exe if running
call taskkill /f /IM nginx.exe

echo start %dir%nginx\nginx.exe
cd %dir%nginx
start nginx.exe
echo ''

call tasklist /fi "imagename eq nginx.exe"
