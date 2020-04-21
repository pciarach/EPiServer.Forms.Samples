set SatteliteMSBuildPath=%cd%

cd %ProgramFiles(x86)%\MSBuild\14.0\Bin

msbuild %SatteliteMSBuildPath%\EPiServer.Forms.Samples.proj

pause