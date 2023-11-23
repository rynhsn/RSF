@echo off
:menu
cls
echo.
echo 1. Pull DLL
echo 2. Update DLL Front
echo 3. Update DLL Back
echo 4. Update DLL Menu
echo 5. Update DLL Menu Back
@REM echo 6. Update Blazor Menu Program
@REM echo 7. Update CSS
echo.
set /p choice="Enter your choice: "
if "%choice%"=="1" goto pull_dll
if "%choice%"=="2" goto update_dll_front
if "%choice%"=="3" goto update_dll_back
if "%choice%"=="4" goto update_dll_menu
if "%choice%"=="5" goto update_dll_menu_back
if "%choice%"=="6" goto update_menu_program
if "%choice%"=="7" goto update_css
goto menu

:pull_dll
echo You choose to pull DLL
@REM beri konfirmasi apakah akan melanjutkan atau tidak
set /p choice="Are you sure? (y/n): "
@REM jika ingin melanjutkan
if "%choice%"=="y" goto pull_dll_continue
@REM jika tidak ingin melanjutkan
if "%choice%"=="n" goto menu

:pull_dll_continue
@REM pindah ke direktori RealtaBlazorLibrary
cd .\RealtaBlazorLibrary
@REM pull dari github
git pull
@REM kembali ke direktori awal
cd ..
@REM beri pesan
echo DLL has been pulled
@REM konfirmasi apakah ingin kemenu utama atau tidak
set /p choice="Do you want to go back to the main menu? (y/n): "
@REM jika ingin kembali ke menu utama
if "%choice%"=="y" goto menu
@REM jika tidak ingin kembali ke menu utama
if "%choice%"=="n" goto end

:update_dll_front
echo You choose to update DLL Front
@REM Give confirmation whether to continue or not
set /p choice="Are you sure? (y/n): "
@REM If the user wants to continue
if "%choice%"=="y" goto update_dll_front_continue
@REM If the user doesn't want to continue
if "%choice%"=="n" goto menu

:update_dll_front_continue
@REM Copy DLL Front to the LIBRARY folder
xcopy /s /y /i /e /h /r /k /d ".\RealtaBlazorLibrary\Dll Front\*.*" ".\LIBRARY\Front\"
@REM Give a message
echo DLL Front has been updated
@REM konfirmasi apakah ingin kemenu utama atau tidak
set /p choice="Do you want to go back to the main menu? (y/n): "
@REM jika ingin kembali ke menu utama
if "%choice%"=="y" goto menu
@REM jika tidak ingin kembali ke menu utama
if "%choice%"=="n" goto end

:update_dll_back
echo You choose to update DLL Back
@REM Give confirmation whether to continue or not
set /p choice="Are you sure? (y/n): "
@REM If the user wants to continue
if "%choice%"=="y" goto update_dll_back_continue
@REM If the user doesn't want to continue
if "%choice%"=="n" goto menu

:update_dll_back_continue
@REM Copy DLL Back to the LIBRARY folder
xcopy /s /y /i /e /h /r /k /d ".\RealtaBlazorLibrary\Dll Back\*.*" ".\LIBRARY\Back\"
@REM Give a message
echo DLL Back has been updated
@REM konfirmasi apakah ingin kemenu utama atau tidak
set /p choice="Do you want to go back to the main menu? (y/n): "
@REM jika ingin kembali ke menu utama
if "%choice%"=="y" goto menu
@REM jika tidak ingin kembali ke menu utama
if "%choice%"=="n" goto end

:update_dll_menu
echo You choose to update DLL Menu
@REM Give confirmation whether to continue or not
set /p choice="Are you sure? (y/n): "
@REM If the user wants to continue
if "%choice%"=="y" goto update_dll_menu_continue
@REM If the user doesn't want to continue
if "%choice%"=="n" goto menu

:update_dll_menu_continue
@REM Copy DLL Menu to the LIBRARY folder
xcopy /s /y /i /e /h /r /k /d ".\RealtaBlazorLibrary\Dll Menu\*.*" ".\LIBRARY\Menu\"
@REM Give a message
echo DLL Menu has been updated
@REM konfirmasi apakah ingin kemenu utama atau tidak
set /p choice="Do you want to go back to the main menu? (y/n): "
@REM jika ingin kembali ke menu utama
if "%choice%"=="y" goto menu
@REM jika tidak ingin kembali ke menu utama
if "%choice%"=="n" goto end

:update_dll_menu_back
echo You choose to update DLL Menu Back
@REM Give confirmation whether to continue or not
set /p choice="Are you sure? (y/n): "
@REM If the user wants to continue
if "%choice%"=="y" goto update_dll_menu_back_continue
@REM If the user doesn't want to continue
if "%choice%"=="n" goto menu

:update_dll_menu_back_continue
@REM Copy DLL Menu to the LIBRARY folder
xcopy /s /y /i /e /h /r /k /d ".\RealtaBlazorLibrary\Dll Menu Back\*.*" ".\LIBRARY\MenuBack\"
@REM Give a message
echo DLL Menu Back has been updated
@REM konfirmasi apakah ingin kemenu utama atau tidak
set /p choice="Do you want to go back to the main menu? (y/n): "
@REM jika ingin kembali ke menu utama
if "%choice%"=="y" goto menu
@REM jika tidak ingin kembali ke menu utama
if "%choice%"=="n" goto end

:update_menu_program
echo You choose to update Blazor Menu Program
@REM Give confirmation whether to continue or not
set /p choice="Are you sure? (y/n): "
@REM If the user wants to continue
if "%choice%"=="y" goto update_menu_program_continue
@REM If the user doesn't want to continue
if "%choice%"=="n" goto menu

:update_menu_program_continue
@REM wait for 1 second
timeout /t 10
@REM hapus semua file di folder Menu\BlazorMenu\
del /s /q ".\Menu\BlazorMenu\*.*"

@REM Give a message
echo DLL Menu Back has been updated
@REM konfirmasi apakah ingin kemenu utama atau tidak
set /p choice="Do you want to go back to the main menu? (y/n): "
@REM jika ingin kembali ke menu utama
if "%choice%"=="y" goto menu
@REM jika tidak ingin kembali ke menu utama
if "%choice%"=="n" goto end

:update_css
echo You chose to update CSS
@REM Add your commands here
@REM wait for 1 second
timeout /t 5
goto menu