@echo off

set TESTBASE=.

echo this is a bunch of stress cases.

echo First, 32x32
%TESTBASE%\big-square.trizbort
echo Now, DBCS all over
%TESTBASE%\dbcs-text.trizbort