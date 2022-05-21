

set ng_dir=%~dp0
for %%I in ("%~dp0.") do for %%J in ("%%~dpI.") do set parent=%%~dpnxJ

call %parent%\env.cmd
echo
echo start npm
echo current dir: %ng_dir%
set acli_dir="%ng_dir%node_modules\@angular\cli\bin"
echo acli_dir: %acli_dir%
IF NOT EXIST %acli_dir%\ng (
   echo not exist cli dir, install cli from maven
   call mvn -Dmaven.repo.local=%mvn_local% --settings %mvn_settings% install
)
set node_dir=%ng_dir%node
echo node dir: %node_dir%
call %node_dir%\node.exe %acli_dir%\ng %*
