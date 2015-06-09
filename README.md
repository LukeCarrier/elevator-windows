# Elevator for Windows

Elevator is a simple application that enables you to execute an application with
administrator rights. It does this by elevating its process's permissions via
UAC, then executing the specified application with the specified arguments.

Think of it a little like ```sudo```, only it's for Windows and it pops a UAC
confirmation dialogue rather than prompting for the password within the shell.

* * *

## Building

Just open the solution in Visual Studio and hit build.

## Installation

Throw ```Elevator.exe``` on your ```PATH```.
