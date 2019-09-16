Command to make Releases apps for differents runtimes
```console
[notim@Lenovo-ideapad: ~]$ dotnet build -c release -r ubuntu.18.04-x64 -v diag && 
dotnet build -c Debug   -r win-x86          -v diag &&
dotnet build -c release -r win-x64          -v diag && 
dotnet build -c release -r rhel-x64         -v diag &&
dotnet build -c release -r osx-x64          -v diag
```
 
Command to make migrations from EFCore 2.2
```console
[notim@Lenovo-ideapad: ~]$ dotnet ef migrations add [name of migrate]
[notim@Lenovo-ideapad: ~]$ dotnet ef database update
```