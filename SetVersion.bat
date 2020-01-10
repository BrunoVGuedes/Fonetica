echo.
    setlocal enableextensions disabledelayedexpansion

    set "search=%1"
    set "replace=%2"
   
    set "FileReplace=Lider.DPVAT.APIFonetica\versao.json"

    for /f "delims=" %%i in ('type "%FileReplace%" ^& break ^> "%FileReplace%" ') do (
        set "line=%%i"
        setlocal enabledelayedexpansion
        >>"%FileReplace%" echo(!line:%search%=%replace%!
        endlocal
    )
