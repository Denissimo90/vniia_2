@echo off

echo set env variblies

set dir=%~dp0

if not exist %dir%\.m2 mkdir %dir%\.m2
if not exist %dir%\.m2\repository mkdir %dir%\.m2\repository

set nexus=http://localhost:8081
set nexus_mvn=%nexus%/repository/maven-public/
set mvn_local=%dir%.m2\repository
set mvn_settings=%dir%\settings.xml

set HTTP_PROXY=
set HTTPS_PROXY=
set M2_HOME=%dir%\maven\bin
set JAVA_HOME=%dir%\jdk1.8.0
set PATH=node_modules\.bin;node;%PATH%;%dir%\maven\bin;%dir%\jdk1.8.0\bin;


echo dir %dir%
echo M2_HOME %M2_HOME%
echo JAVA_HOME %JAVA_HOME%
echo PATH %PATH%

mvn -version
