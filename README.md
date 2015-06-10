# Elevator for Windows

Elevator is a simple application that enables you to execute an application with
administrator rights. It does this by elevating its process's permissions via
UAC, then executing the specified application with the specified arguments.

Think of it a little like ```sudo```, only it's for Windows and it pops a UAC
consent dialogue rather than prompting for the password within the shell.

* * *

## How it works

*NOTE:* this is largely theoretical at the moment, but I'm fairly confident
it'll work.

Elevator is designed to work around the design limitations of User Account
Control, which [forbids](http://stackoverflow.com/a/3596354) communication
between user-level and elevated-level processes.

It's comprised of three components:
* ```ElevatorClient```, which you call with the details of the process you'd
  like to launch.
* ```ElevatorServer```, which is launched by the client with elevated
  privileges, thus triggering the UAC consent dialogue. Once consented, it will
  open a WCF service enabling bidirectional communication between the client
  and server.
* ```ElevatorCommon```, which contains the service contracts necessary to bind
  the client and server together.

## Building

Just open the solution in Visual Studio and hit build.

## Installation

Throw ```ElevatorClient.exe``` and ```ElevatorServer.exe``` on your ```PATH```.
