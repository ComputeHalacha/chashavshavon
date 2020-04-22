"C:\Program Files\7-Zip\7z.exe " a "Chashavshavon_Installer.7z" "%~1%~2\*"
copy /b 7zSD.sfx + SFX_Config.txt + Chashavshavon_Installer.7z Chashavshavon_Installer.exe
DEL "Chashavshavon_Installer.7z"
MOVE "Chashavshavon_Installer.exe" "D:\Repositories\Compute\WebSite\Products\Chashavshavon\Chashavshavon_Installer.exe"
"C:\Program Files\7-Zip\7z.exe " a "D:\Repositories\Compute\WebSite\Products\Chashavshavon\Chashavshavon_Installer.zip" "D:\Repositories\Compute\WebSite\Products\Chashavshavon\Chashavshavon_Installer.exe"
"SetVersionApp.exe " "D:\Repos_git\chashavshavon\Chashavshavon\bin\%~2\Chashavshavon.exe" "D:\Repositories\Compute\WebSite\Products\Chashavshavon"