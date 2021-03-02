@ECHO OFF
SET mypath=%~dp0
echo %mypath:~0,-1%
pwsh.exe -Command "& {import-module docfx-toc-generator;Build-TocHereRecursive;}"
PAUSE