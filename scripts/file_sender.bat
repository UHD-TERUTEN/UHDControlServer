@echo off
for /F "tokens=* USEBACKQ" %%F in (account.txt) do (
set ssh_account=%%F
)
set id_rsa_location=%HOMEDRIVE%%HOMEPATH%\.ssh\id_rsa
echo start update
echo uploading whitelist
echo.
echo sftp connects to %ssh_account%...
sftp -b upload.txt -i %id_rsa_location% %ssh_account%
echo.
echo uploading whitelist has done!
echo finish
