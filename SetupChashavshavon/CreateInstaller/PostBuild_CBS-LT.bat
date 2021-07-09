SETLOCAL ENABLEEXTENSIONS
SET "szPath=C:\Program Files\7-Zip\7z.exe"
SET "reposPath=c:\repos_git" 
"%szPath% " a "Chashavshavon_Installer.7z" "%~1%~2\*"
copy /b 7zSD.sfx + SFX_Config.txt + Chashavshavon_Installer.7z Chashavshavon_Installer.exe
DEL "Chashavshavon_Installer.7z"
MOVE "Chashavshavon_Installer.exe" "%reposPath%\Compute\WebSite\Products\Chashavshavon\Chashavshavon_Installer.exe"
"%szPath% " a "%reposPath%\Compute\WebSite\Products\Chashavshavon\Chashavshavon_Installer.zip" "%reposPath%\Compute\WebSite\Products\Chashavshavon\Chashavshavon_Installer.exe"
"SetVersionApp.exe " "%reposPath%\Chashavshavon\Chashavshavon\bin\%~2\Chashavshavon.exe" "%reposPath%\Compute\WebSite\Products\Chashavshavon"