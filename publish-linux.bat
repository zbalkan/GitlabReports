@echo off
dotnet publish src/GitlabReports.csproj --framework net6.0 --runtime linux-x64 -c Release --self-contained true -p:PublishTrimmed=true -p:PublishSingleFile=true
