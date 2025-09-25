@echo off
rem Mendapatkan lokasi direktori tempat file batch berada
set "currentDir=%~dp0"

cd /d D:\
cd "%currentDir%"

cd API/AP/BIMASAKTI_AP_API/

dotnet watch
