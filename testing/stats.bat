@echo off

set TESTBASE=.

echo this is a bunch of map test cases, where the expected results are listed, and Alt-t Up Enter will get you the map statistics.

echo Counting a survey of general options. Use this after adding any stat feature.
%TESTBASE%\map-stats-general.trizbort
echo Counting dead ends.
%TESTBASE%\map-stats-dead-ends.trizbort
