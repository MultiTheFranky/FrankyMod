@echo off
cd extensions
call build.bat
cd ..
hemtt.exe build
pause
