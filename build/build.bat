
REM SET BUILD=Debug
SET BUILD=Release

COPY ..\src\ServiceStack.Text\bin\%BUILD%\*.* ..\..\ServiceStack\release\latest\
COPY ..\src\ServiceStack.Text\bin\%BUILD%\*.* ..\..\ServiceStack\release\latest\ServiceStack.Text\
COPY ..\src\ServiceStack.Text\bin\%BUILD%\*.* ..\..\ServiceStack\lib
COPY ..\src\ServiceStack.Text\bin\%BUILD%\*.* ..\..\ServiceStack.Contrib\lib
COPY ..\src\ServiceStack.Text\bin\%BUILD%\*.* ..\..\ServiceStack.Redis\lib
COPY ..\src\ServiceStack.Text\bin\%BUILD%\*.* ..\..\ServiceStack.Examples\lib
COPY ..\src\ServiceStack.Text\bin\%BUILD%\*.* ..\..\ServiceStack.RedisWebServices\lib

COPY ..\src\ServiceStack.Text\bin\%BUILD%\*.* ..\NuGet\lib
