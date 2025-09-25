@echo off
echo ==================================
echo Menghapus folder bin dan obj...
echo ==================================

for /r %%d in (bin obj) do (
    if exist "%%d" (
        echo Menghapus: %%d
        rmdir /s /q "%%d"
    )
)

echo ==================================
echo Selesai!
pause
