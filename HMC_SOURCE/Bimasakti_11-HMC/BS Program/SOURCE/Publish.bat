@echo off

rem Mendapatkan lokasi direktori tempat file batch berada
set "currentDir=%~dp0"

set /p tempRes=Masukkan nama proyek (___): 

rem Validasi panjang karakter tempRes
if not "%tempRes%"=="%tempRes:~0,8%" (
    echo Nama proyek harus memiliki panjang tepat 8 karakter.
    pause
    exit /b
)




set module=%tempRes:~0,2%
set project=%tempRes:~0,3%
set code=%tempRes:~4,9%

cd /d D:\
cd "%currentDir%"

dotnet build SERVICE\%module%\%tempRes%Service\%tempRes%Service.csproj -c release
echo Selesai build project

dotnet publish SERVICE\%module%\%tempRes%Service\%tempRes%Service.csproj -c release -o D:\F_Back\%tempRes%

cd D:\F_Back\%tempRes%

del /s /q *.pdb
echo File dengan ekstensi .pdb telah dihapus

del /s /q R_*.dll
echo File dengan awalan "R_" telah dihapus

del /s /q BaseHeaderReportCOMMON.dll
echo File BaseHeaderReportCOMMON telah dihapus

del /s /q GFF*.dll
echo File dengan awalan "GFF" telah dihapus

del /s /q System*.dll
echo File dengan awalan "System*" telah dihapus

cls

echo Semua file dengan ekstensi .pdb telah dihapus
echo Selesai publish project

pause
