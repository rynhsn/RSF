@echo off
rem Mendapatkan lokasi direktori tempat file batch berada
set "currentDir=%~dp0"

cd /d D:\
cd "%currentDir%"

cd API/TX/BIMASAKTI_TX_API/

dotnet watch
