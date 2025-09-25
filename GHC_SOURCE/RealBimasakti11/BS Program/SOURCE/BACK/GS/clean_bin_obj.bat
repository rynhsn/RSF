@echo off

REM Set the base directory
set BASE_DIR=D:\RealCode\RSF\BIMASAKTI_11\1.00\PROGRAM\BS Program\SOURCE\BACK\GS\GS_SPR

REM Loop through each directory in the base directory
for /d %%i in ("%BASE_DIR%\*") do (
    if exist "%%i\bin" (
        echo Deleting bin folder in %%i
        rmdir /s /q "%%i\bin"
    )
    if exist "%%i\obj" (
        echo Deleting obj folder in %%i
        rmdir /s /q "%%i\obj"
    )
)

echo All bin and obj folders have been deleted.
timeout /t 5 >nul

exit