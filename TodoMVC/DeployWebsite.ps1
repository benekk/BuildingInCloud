$sourcepath = $Env:TF_BUILD_SOURCESDIRECTORY+"\TodoMVC\website\*"
xcopy $sourcepath c:\websites\TodoMVC /Y /i /s