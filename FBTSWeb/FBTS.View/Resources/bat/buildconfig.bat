@echo off

 

echo Copying web.config file...

call "%1Resources\bat\copyifdifferent.bat" "%1Resources\Config\%2\web.config" "%1web.config"