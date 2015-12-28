@ECHO OFF

IF NOT EXIST "%1" GOTO ERROR
nuget push %1 -ApiKey 1079176f-b3d0-34d7-8af5-21e8ec48b4cc -Source http://nuget.nexon.com:8081/nexus/service/local/nuget/nuget-nexon/
GOTO DONE


:ERROR
ECHO Usage: NugetPush.bat [.nupkg]

:DONE
@ECHO ON
