@echo off

set TESTBASE=.

echo this is a bunch of stress cases.

echo First, ASCII conflict characters
%TESTBASE%\chars-gt-lt-underline-dquote.trizbort
echo Then, DBCS all over
%TESTBASE%\dbcs-text.trizbort
echo Focusing on rooms, negative room sizes (3x3 map, from 64x64 to -64x-64)
%TESTBASE%\room-size-stress-case.trizbort
echo Now, 32x32, oppa Westmark style
%TESTBASE%\big-square.trizbort
