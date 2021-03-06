@ECHO OFF


:ON_START

SET BUILD_LOG_FILENAME=SETUP.BAT.LOG
ECHO ** SETUP.BAT.LOG ** >"%BUILD_LOG_FILENAME%"

IF DEFINED FrameworkVersion SET IS_VISUALSTUDIO_COMMANDLINE=TRUE
IF NOT "%IS_VISUALSTUDIO_COMMANDLINE%" EQU "TRUE" GOTO GOTO_ON_HELP_OF_NOTHING_VISUALSTUDIO_COMMANDLINE

IF /I "%1" EQU "/HELP" GOTO ON_HELP
IF /I "%1" EQU "/v3.5" (
	SET BUILD_FRAMEWORK_VERSION=v3.5
	SET BUILD_SOLUTION_FILENAME=Umc.Core.v3.0.sln
	GOTO ON_START_SETUP
)
IF /I "%1" EQU "/v4.0" (
	SET BUILD_FRAMEWORK_VERSION=v4.0
	SET BUILD_SOLUTION_FILENAME=Umc.Core.v3.0.sln
	GOTO ON_START_SETUP
)

GOTO ON_HELP











:ON_START_SETUP
ECHO ********************** SETUP Umc.Core **********************



SET BUILD_TASK_FOLDER_OF_IMPORTBEFORE=%PROGRAMFILES%\MSBuild\4.0\Microsoft.CSharp.targets\ImportBefore
SET BUILD_TASK_FOLDER_OF_IMPORTAFTER=%PROGRAMFILES%\MSBuild\4.0\Microsoft.CSharp.targets\ImportAfter
SET BUILD_TASK_FILE_OF_IMPORTBEFORE=Umc.Core.ImportBefore.targets
SET BUILD_TASK_FILE_OF_IMPORTAFTER=Umc.Core.ImportAfter.targets
SET BUILD_TASK_FULLPATH=%BUILD_TASK_FOLDER_OF_IMPORTAFTER%\%BUILD_TASK_FILE_OF_IMPORTAFTER%






:ON_SETUP_BUILD_ENVIRONMENT
REM ECHO ** 빌드 테스크를 위한 타겟(.Targets) 파일을 복사합니다........................

REM IF NOT EXIST "%BUILD_TASK_FOLDER_OF_IMPORTBEFORE%" MKDIR "%BUILD_TASK_FOLDER_OF_IMPORTBEFORE%" >>"%BUILD_LOG_FILENAME%"
REM XCOPY ".\CommonCode\%BUILD_TASK_FILE_OF_IMPORTBEFORE%" "%BUILD_TASK_FOLDER_OF_IMPORTBEFORE%" /y >>"%BUILD_LOG_FILENAME%"

REM IF NOT EXIST "%BUILD_TASK_FOLDER_OF_IMPORTAFTER%" MKDIR "%BUILD_TASK_FOLDER_OF_IMPORTAFTER%" >>"%BUILD_LOG_FILENAME%"
REM XCOPY ".\CommonCode\%BUILD_TASK_FILE_OF_IMPORTAFTER%" "%BUILD_TASK_FOLDER_OF_IMPORTAFTER%" /y >>"%BUILD_LOG_FILENAME%"

GOTO ON_BUILD_SOLUTION











:ON_BUILD_SOLUTION
ECHO ** 솔루션 파일을 빌드합니다................................................

REM MSBUILD "%BUILD_SOLUTION_FILENAME%" /t:Rebuild /p:TargetFrameworkVersion=%BUILD_FRAMEWORK_VERSION%;Configuration=Debug.Umc.Core >>"%BUILD_LOG_FILENAME%"
MSBUILD "%BUILD_SOLUTION_FILENAME%" /t:Rebuild /p:TargetFrameworkVersion=%BUILD_FRAMEWORK_VERSION%;Configuration=Debug.Umc.Core
IF NOT %ERRORLEVEL% EQU 0 GOTO ON_ERROR

IF /I "%2" EQU "/WITHTEST" (
	MSTEST /TestMetadata:Umc.Core.vsmdi /Category:"BVT Function"
)

GOTO ON_SUCCESS










:ON_ERROR
ECHO ** 오류가 발생하였습니다...................................................
ECHO ** ERROR LEVEL IS %ERRORLEVEL%
PAUSE
GOTO ON_EXIT









:ON_SUCCESS
ECHO ** 모든 작업이 완료되었습니다...............................................
PAUSE
GOTO ON_EXIT









:GOTO_ON_HELP_OF_NOTHING_VISUALSTUDIO_COMMANDLINE
ECHO ** 오류가 발생하였습니다...................................................
ECHO -
ECHO - 이 솔루션 빌드를 수행하기 위해서
ECHO - 반드시 Visual Studio 2010 의 명령 프롬프트를 이용하기시 바랍니다.
ECHO -
ECHO - 사용 방법 :
ECHO   시작 - 모든 프로그램 - Visual Studio 2010 - Visual Studio Tools - Visual Studio 명령 프롬프트(2010)

GOTO ON_EXIT










:ON_HELP
ECHO **** Umc.Core 도움말 ****
ECHO - 
ECHO - 사용 방법 : SETUP.BAT [옵션1] [옵션2]
ECHO - 
ECHO - 옵션1
ECHO   /HELP		: 도움말
ECHO   /v3.5		: .NET Framework 3.5 버전으로 빌드합니다.
ECHO   /v4.0		: .NET Framework 4.0 버전으로 빌드합니다.
ECHO -
ECHO - 옵션2
ECHO   /WITHTEST	: 빌드가 성공하면 단위 테스트를 수행합니다.

GOTO ON_EXIT










:ON_EXIT

ECHO -
ECHO ** SETUP.BAT 로그를 확인하시려면 SETUP.BAT.LOG 파일을 검토하십시오............

SET IS_VISUALSTUDIO_COMMANDLINE=
SET BUILD_FRAMEWORK_VERSION=
SET BUILD_SOLUTION_FILENAME=

SET BUILD_LOG_FILENAME=
SET BUILD_TASK_FOLDER_OF_IMPORTBEFORE=
SET BUILD_TASK_FOLDER_OF_IMPORTAFTER=
SET BUILD_TASK_FILE_OF_IMPORTBEFORE=
SET BUILD_TASK_FILE_OF_IMPORTAFTER=
SET BUILD_TASK_FULLPATH=

@ECHO ON