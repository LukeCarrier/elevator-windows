@Echo off
SetLocal

set "MiscProjectDir=%~dp0"
set "MiscProjectDir=%MiscProjectDir:~0,-1%"

for %%d in ("%MiscProjectDir%") do set "SolutionDir=%%~dpd"
set "SolutionDir=%SolutionDir:~0,-1%"

set "ElevatorClientDir=%SolutionDir%\ElevatorClient\bin\Debug"
set "ElevatorServerDir=%SolutionDir%\ElevatorServer\bin\Debug"

set "ExtraPaths=%ElevatorClientDir%;%ElevatorServerDir%"
set "PATH=%PATH%;%ElevatorClientDir%;%ElevatorServerDir%"

ElevatorClient whoami
REM ElevatorClient ping -n 2 google.com

EndLocal
Pause