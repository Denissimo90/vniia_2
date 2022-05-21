@echo off

set install_dir=%~dp0
for %%I in ("%~dp0.") do for %%J in ("%%~dpI.") do set parent=%%~dpnxJ
call %parent%\env.cmd


echo ''
echo start mvn clean package
echo settings:
echo  %mvn_local% %nexus_mvn% %mvn_settings%
mvn -Dmaven.repo.local=%mvn_local% --settings %mvn_settings% clean package
