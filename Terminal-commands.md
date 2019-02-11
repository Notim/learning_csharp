Command to make Releases apps for differents runtimes

dotnet build -c release -r ubuntu.18.04-x64 -v diag && 
dotnet build -c Debug   -r win-x86          -v diag &&
dotnet build -c release -r win-x64          -v diag && 
dotnet build -c release -r rhel-x64         -v diag &&
dotnet build -c release -r osx-x64          -v diag
 
