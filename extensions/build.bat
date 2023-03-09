@echo off
rem set an array of the projects to build
set array=prometheus

for %%i in (%array%) do (
    echo Building %%i
    cd %%i
    dotnet publish /p:NativeLib=Shared -r win-x64 -c Release
    rem use wls to build the linux so
    wsl dotnet publish /p:NativeLib=Shared -r linux-x64 -c Release
    cd ..
    echo Finished building %%i
)
for %%i in (%array%) do (
    echo Copying %%i
    cd %%i
    xcopy /s /y /i /q /e /h /k /r /d bin\Release\net7.0\win-x64\publish\*.dll ..\..\
    xcopy /s /y /i /q /e /h /k /r /d bin\Release\net7.0\linux-x64\publish\*.so ..\..\
    cd ..
    echo Finished copying %%i
)
