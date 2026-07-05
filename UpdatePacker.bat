@echo off
setlocal

REM Always run from the script's directory
cd /d "%~dp0"

REM Root-relative path to your build output
set SOURCE=AutoAnki\bin\x64\debug\net8.0-windows

REM Output zip name
set OUTPUT=update.zip

REM Verify source exists
if not exist "%SOURCE%" (
    echo ERROR: Source folder not found:
    echo %SOURCE%
    pause
    exit /b
)

REM Remove old zip if exists
if exist "%OUTPUT%" del "%OUTPUT%"

REM Create a temporary staging folder
set STAGE=%TEMP%\AutoAnkiUpdateStage
if exist "%STAGE%" rmdir /s /q "%STAGE%"
mkdir "%STAGE%"

REM Copy everything except excluded items
robocopy "%SOURCE%" "%STAGE%" /E ^
    /XF *.mp4 *.txt ^
    /XD ScreenOut

REM Pack the staged folder contents (NOT ".")
tar -a -cf "%OUTPUT%" -C "%STAGE%" *

REM Cleanup
rmdir /s /q "%STAGE%"

echo.
echo Update zip created: %OUTPUT%
echo Done.
pause
