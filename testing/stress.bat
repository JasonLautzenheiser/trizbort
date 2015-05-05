@echo off

set TESTBASE=.

echo this is a bunch of stress cases.

echo First, 32x32
%TESTBASE%\big-square.trizbort
echo Now, DBCS all over
%TESTBASE%\dbcs-text.trizbort
echo negative room sizes (3x3 map, from 64x64 to -64x-64)
%TESTBASE%\room-size-stress-case.trizbort