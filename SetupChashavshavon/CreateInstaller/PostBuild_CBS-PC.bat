"C:\Program Files\7-Zip\7z.exe " a "Chashavshavon_Installer.zip" "%~1%~2\*"
"C:\Program Files\ZipGenius 6\zg.exe" -sfx "Chashavshavon_Installer.zip" A1 B1 O0 E0 L0 C"Setup.exe" D"%temp%" U"ChashInstall" H2 I"scroll.ico" S0 T"Extracting Chashavshavon installation files..."
DEL "Chashavshavon_Installer.zip"
MOVE "Chashavshavon_Installer.exe" "D:\Insync\cb@compute.co.il\Repositories\Compute\WebSite\Products\Chashavshavon\Chashavshavon_Installer.exe"
"C:\Program Files\7-Zip\7z.exe " a "D:\Insync\cb@compute.co.il\Repositories\Compute\WebSite\Products\Chashavshavon\Chashavshavon_Installer.zip" "D:\Insync\cb@compute.co.il\Repositories\Compute\WebSite\Products\Chashavshavon\Chashavshavon_Installer.exe"
"SetVersionApp.exe " "D:\Insync\cb@compute.co.il\Repositories\Chashavshavon\Chashavshavon\bin\%~2\Chashavshavon.exe" "D:\Insync\cb@compute.co.il\Repositories\Compute\WebSite\Products\Chashavshavon"