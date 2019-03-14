@echo off

@set ROOT=%cd%
@cd docker
@call Build.bat ubuntu-16 cpu 64
@call Build.bat ubuntu-16 cuda 64
@call Build.bat ubuntu-16 arm 32
@call Build.bat ubuntu-16 arm 64