@ECHO off
 
ECHO "Starting CrystalReports Installation" >> log.txt
msiexec.exe /I "CRRuntime_64bit_13_0_23.msi" /qn
ECHO "Completed CrystalReports Installation" >> log.txt
ECHO "Starting Virtual Directory Creation" >> log.txt
PowerShell -ExecutionPolicy Unrestricted .\VirtDir.ps1 >> log.txt 2>&1
ECHO "Completed Virtual Directory Creation" >> log.txt