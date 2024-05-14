@echo off
title Library Updater

cls
@REM buat angka random 1 - 4 dan simpan ke variabel
set /a random=%random% %%4 + 1
if %random%==1 goto header_1
if %random%==2 goto header_2
if %random%==3 goto header_3
if %random%==4 goto header_4

:header_1
echo     ____             ____        ____  __                      
echo    / __ \___  ____ _/ / /_____ _/ __ )/ /___ _____  ____  _____
echo   / /_/ / _ \/ __ `/ / __/ __ `/ __  / / __ `/_  / / __ \/ ___/
echo  / _, _/  __/ /_/ / / /_/ /_/ / /_/ / / /_/ / / /_/ /_/ / /    
echo /_/ ^|_^|\___/\__,_/_/\__/\__,_/_____/_/\__,_/ /___/\____/_/     
echo                                      Library Updater v1.0
echo.
goto menu

:header_2
echo     ____             ____        ____  __                       __    _ __                         
echo    / __ \___  ____ _/ / /_____ _/ __ )/ /___ _____  ____  _____/ /   (_) /_  _________ ________  __
echo   / /_/ / _ \/ __ `/ / __/ __ `/ __  / / __ `/_  / / __ \/ ___/ /   / / __ \/ ___/ __ `/ ___/ / / /
echo  / _, _/  __/ /_/ / / /_/ /_/ / /_/ / / /_/ / / /_/ /_/ / /  / /___/ / /_/ / /  / /_/ / /  / /_/ / 
echo /_/ ^|_^|\___/\__,_/_/\__/\__,_/_____/_/\__,_/ /___/\____/_/  /_____/_/_.___/_/   \__,_/_/   \__, /  
echo                                                                                           /____/   
echo                                                                                    Updater v1.0
echo.
goto menu

:header_3
echo     ____             ____        ____  __                                                      
echo    / __ \___  ____ _/ / /_____ _/ __ )/ /___ _____  ____  _____                                
echo   / /_/ / _ \/ __ `/ / __/ __ `/ __  / / __ `/_  / / __ \/ ___/                                
echo  / _, _/  __/ /_/ / / /_/ /_/ / /_/ / / /_/ / / /_/ /_/ / /                                    
echo /_/ ^|_^|\___/\__,_/_/\__/\__,_/_____/_/\__,_/ /___/\____/_/                                     
echo     __    _ __                          __  __          __      __           
echo    / /   (_) /_  _________ ________  __/ / / /___  ____/ /___ _/ /____  _____ v1.0
echo   / /   / / __ \/ ___/ __ `/ ___/ / / / / / / __ \/ __  / __ `/ __/ _ \/ ___/
echo  / /___/ / /_/ / /  / /_/ / /  / /_/ / /_/ / /_/ / /_/ / /_/ / /_/  __/ /    
echo /_____/_/_.___/_/   \__,_/_/   \__, /\____/ .___/\__,_/\__,_/\__/\___/_/     
echo                               /____/     /_/                                                   
echo.
goto menu

:header_4
echo              ____             ____        ____  __
echo             / __ \___  ____ _/ / /_____ _/ __ )/ /___ _____  ____  _____ 
echo            / /_/ / _ \/ __ `/ / __/ __ `/ __  / / __ `/_  / / __ \/ ___/
echo           / _, _/  __/ /_/ / / /_/ /_/ / /_/ / / /_/ / / /_/ /_/ / / 
echo          /_/ ^|_^|\___/\__,_/_/\__/\__,_/_____/_/\__,_/ /___/\____/_/  
echo     __    _ __                          __  __          __      __           
echo    / /   (_) /_  _________ ________  __/ / / /___  ____/ /___ _/ /____  _____ v1.0
echo   / /   / / __ \/ ___/ __ `/ ___/ / / / / / / __ \/ __  / __ `/ __/ _ \/ ___/
echo  / /___/ / /_/ / /  / /_/ / /  / /_/ / /_/ / /_/ / /_/ / /_/ / /_/  __/ /    
echo /_____/_/_.___/_/   \__,_/_/   \__, /\____/ .___/\__,_/\__,_/\__/\___/_/     
echo                               /____/     /_/                                                   
goto menu


:menu
@REM cls
echo 1.  Pull DLL
echo 2.  Update DLL Front
echo 3.  Update DLL Back
echo 4.  Update DLL Menu
echo 5.  Update DLL Menu Back
echo 6.  Update Blazor Menu Program
echo 99. Super Update
echo 0.  Exit
echo.
set /p choice="Enter your choice: "
if "%choice%"=="1" goto pull_dll
if "%choice%"=="2" goto update_dll_front
if "%choice%"=="3" goto update_dll_back
if "%choice%"=="4" goto update_dll_menu
if "%choice%"=="5" goto update_dll_menu_back
if "%choice%"=="6" goto update_menu_program
@REM if "%choice%"=="7" goto update_css
if "%choice%"=="99" goto super_update
if "%choice%"=="0" goto exit
goto menu

:super_update
echo You choose to super update
set /p choice="Are you sure? (y/n): "
if "%choice%"=="y" goto super_update_continue
if "%choice%"=="n" goto menu

:super_update_continue
echo ---------------------------------------------------------------------------------------
echo Pull DLL
echo ---------------------------------------------------------------------------------------
cd .\RealtaBlazorLibrary
git pull
cd ..
echo ---------------------------------------------------------------------------------------
echo DLL has been pulled
echo ---------------------------------------------------------------------------------------

echo ---------------------------------------------------------------------------------------
echo Update DLL Front
echo ---------------------------------------------------------------------------------------
xcopy /s /y /i /e /h /r /k /d ".\RealtaBlazorLibrary\Dll Front\*.*" ".\LIBRARY\Front\"
echo ---------------------------------------------------------------------------------------
echo DLL Front has been updated
echo ---------------------------------------------------------------------------------------

echo ---------------------------------------------------------------------------------------
echo Update DLL Back
echo ---------------------------------------------------------------------------------------
xcopy /s /y /i /e /h /r /k /d ".\RealtaBlazorLibrary\Dll Back\*.*" ".\LIBRARY\Back\"
echo ---------------------------------------------------------------------------------------
echo DLL Back has been updated
echo ---------------------------------------------------------------------------------------

echo ---------------------------------------------------------------------------------------
echo Update DLL Menu
echo ---------------------------------------------------------------------------------------
xcopy /s /y /i /e /h /r /k /d ".\RealtaBlazorLibrary\Dll Menu\*.*" ".\LIBRARY\Menu\"
echo ---------------------------------------------------------------------------------------
echo DLL Menu has been updated
echo ---------------------------------------------------------------------------------------

echo ---------------------------------------------------------------------------------------
echo Update DLL Menu Back
echo ---------------------------------------------------------------------------------------
xcopy /s /y /i /e /h /r /k /d ".\RealtaBlazorLibrary\Dll Menu Back\*.*" ".\LIBRARY\MenuBack\"
echo ---------------------------------------------------------------------------------------
echo DLL Menu Back has been updated
echo ---------------------------------------------------------------------------------------

echo ---------------------------------------------------------------------------------------
echo Update Blazor Menu Program
echo ---------------------------------------------------------------------------------------
mkdir .\Menu\Backup
mkdir .\Menu\Backup\wwwroot
xcopy /y /e /h /r /k /d ".\Menu\BlazorMenu\BlazorMenu.csproj" ".\Menu\Backup\"
xcopy /y /e /h /r /k /d ".\Menu\BlazorMenu\App.razor" ".\Menu\Backup\"
xcopy /y /e /h /r /k /d ".\Menu\BlazorMenu\wwwroot\appsettings.json" ".\Menu\Backup\wwwroot\"
del /q ".\Menu\BlazorMenu\*.*"
xcopy /y /e /h /r /k /d ".\RealtaBlazorLibrary\BlazorMenu\*.*" ".\Menu\BlazorMenu\"
xcopy /y /e /h /r /k /d ".\Menu\Backup\BlazorMenu.csproj" ".\Menu\BlazorMenu\"
xcopy /y /e /h /r /k /d ".\Menu\Backup\App.razor" ".\Menu\BlazorMenu\"
xcopy /y /e /h /r /k /d ".\Menu\Backup\wwwroot\appsettings.json" ".\Menu\BlazorMenu\wwwroot\"
rmdir /s /q ".\Menu\Backup"
@REM ubah warna tulisan menjadi hijau
echo ---------------------------------------------------------------------------------------
echo Menu Program has been updated
echo ---------------------------------------------------------------------------------------

echo Done...............
timeout /t 20
goto menu

:pull_dll
echo You choose to pull DLL
set /p choice="Are you sure? (y/n): "
if "%choice%"=="y" goto pull_dll_continue
if "%choice%"=="n" goto menu

:pull_dll_continue
cd .\RealtaBlazorLibrary
git pull
cd ..
echo DLL has been pulled
timeout /t 20
goto menu

:update_dll_front
echo You choose to update DLL Front
set /p choice="Are you sure? (y/n): "
if "%choice%"=="y" goto update_dll_front_continue
if "%choice%"=="n" goto menu

:update_dll_front_continue
xcopy /s /y /i /e /h /r /k /d ".\RealtaBlazorLibrary\Dll Front\*.*" ".\LIBRARY\Front\"
echo DLL Front has been updated
timeout /t 20
goto menu

:update_dll_back
echo You choose to update DLL Back
set /p choice="Are you sure? (y/n): "
if "%choice%"=="y" goto update_dll_back_continue
if "%choice%"=="n" goto menu

:update_dll_back_continue
xcopy /s /y /i /e /h /r /k /d ".\RealtaBlazorLibrary\Dll Back\*.*" ".\LIBRARY\Back\"
echo DLL Back has been updated
timeout /t 20
goto menu

:update_dll_menu
echo You choose to update DLL Menu
set /p choice="Are you sure? (y/n): "
if "%choice%"=="y" goto update_dll_menu_continue
if "%choice%"=="n" goto menu

:update_dll_menu_continue
xcopy /s /y /i /e /h /r /k /d ".\RealtaBlazorLibrary\Dll Menu\*.*" ".\LIBRARY\Menu\"
echo DLL Menu has been updated
timeout /t 20
goto menu

:update_dll_menu_back
echo You choose to update DLL Menu Back
set /p choice="Are you sure? (y/n): "
if "%choice%"=="y" goto update_dll_menu_back_continue
if "%choice%"=="n" goto menu

:update_dll_menu_back_continue
xcopy /s /y /i /e /h /r /k /d ".\RealtaBlazorLibrary\Dll Menu Back\*.*" ".\LIBRARY\MenuBack\"
echo DLL Menu Back has been updated
timeout /t 20
goto menu

:update_menu_program
echo You choose to update Blazor Menu Program
set /p choice="Are you sure? (y/n): "
if "%choice%"=="y" goto update_menu_program_continue
if "%choice%"=="n" goto menu

:update_menu_program_continue
mkdir .\Menu\Backup
mkdir .\Menu\Backup\wwwroot
xcopy /y /e /h /r /k /d ".\Menu\BlazorMenu\BlazorMenu.csproj" ".\Menu\Backup\"
xcopy /y /e /h /r /k /d ".\Menu\BlazorMenu\App.razor" ".\Menu\Backup\"
xcopy /y /e /h /r /k /d ".\Menu\BlazorMenu\wwwroot\appsettings.json" ".\Menu\Backup\wwwroot\"
del /q ".\Menu\BlazorMenu\*.*"
xcopy /y /e /h /r /k /d ".\RealtaBlazorLibrary\BlazorMenu\*.*" ".\Menu\BlazorMenu\"
xcopy /y /e /h /r /k /d ".\Menu\Backup\BlazorMenu.csproj" ".\Menu\BlazorMenu\"
xcopy /y /e /h /r /k /d ".\Menu\Backup\App.razor" ".\Menu\BlazorMenu\"
xcopy /y /e /h /r /k /d ".\Menu\Backup\wwwroot\appsettings.json" ".\Menu\BlazorMenu\wwwroot\"
rmdir /s /q ".\Menu\Backup"
echo Menu Program has been updated
timeout /t 20
goto menu

:update_css
echo You chose to update CSS
timeout /t 5
goto menu

pause