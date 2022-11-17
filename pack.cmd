@echo off
setlocal

IF "%1"=="Debug" (set Configuration=Debug) ELSE (set Configuration=Release)
ECHO packing in %Configuration%

REM Build react views
CALL npm run webpack

call npm run pack -- --configuration %Configuration%
IF %errorlevel% NEQ 0 EXIT /B %errorlevel%

powershell .\build\scripts\pack.ps1
