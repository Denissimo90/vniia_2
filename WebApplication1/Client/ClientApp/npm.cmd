@echo off

set npm_dir=%~dp0
for %%I in ("%~dp0.") do for %%J in ("%%~dpI.") do set parent=%%~dpnxJ
call %parent%\env.cmd

echo
echo start npm

SETLOCAL

echo current dir: %npm_dir%
set node_dir=%npm_dir%\node
SET NODE_EXE=%node_dir%\node.exe
IF NOT EXIST %NODE_EXE% (
	echo node.exe not found, install cli from maven
    rem Invoke maven to install the tools
	call mvn -Dmaven.repo.local=%mvn_local% --settings %mvn_settings% frontend:install-node-and-npm
)

SET "NPM_CLI_JS=%node_dir%\node_modules\npm\bin\npm-cli.js"
FOR /F "delims=" %%F IN ('CALL "%NODE_EXE%" "%NPM_CLI_JS%" prefix -g') DO (
SET "NPM_PREFIX_NPM_CLI_JS=%%F\node_modules\npm\bin\npm-cli.js"
)
IF EXIST "%NPM_PREFIX_NPM_CLI_JS%" (
SET "NPM_CLI_JS=%NPM_PREFIX_NPM_CLI_JS%"
)

"%NODE_EXE%" "%NPM_CLI_JS%" %*