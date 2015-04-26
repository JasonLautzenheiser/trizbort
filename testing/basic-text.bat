@echo off

::to rebuild
::7z a all-test-files.zip *.trizbort *.bat

echo Version testing here. All the files should look the same except if I explicitly mention new features are added.
echo The files aren't perfectly identical. Their trizbort versions are different.

::You should be able to run this batch file from Trizbort with no problem, but if not, you can change the variable below to where all the trizbort files are.
set TESTBASE=.

%TESTBASE%\basics-1570.trizbort
%TESTBASE%\basics-1571.trizbort
%TESTBASE%\basics-1580.trizbort
%TESTBASE%\basics-1581.trizbort
%TESTBASE%\basics-1582.trizbort
%TESTBASE%\basics-1584.trizbort
%TESTBASE%\basics-1585.trizbort
%TESTBASE%\basics-1586.trizbort
%TESTBASE%\basics-1587.trizbort
echo All 6 border types appear below.
%TESTBASE%\borders.trizbort
echo Connection types and combinations are tested here.
%TESTBASE%\connections.trizbort
echo All 9 room type fills are tested here. The center is blank. They should form a white octagon.
%TESTBASE%\fills.trizbort
echo All 9 object placements are tested here. The object is listed in the room in the center.
%TESTBASE%\fills.trizbort
echo Stalk lengths are tested: 16, 32, 48
%TESTBASE%\stalk-length-16.trizbort
echo Stalk lengths are tested: 16, 32, 48
%TESTBASE%\stalk-length-32.trizbort
echo Stalk lengths are tested: 16, 32, 48
%TESTBASE%\stalk-length-48.trizbort
echo Line sizes tested: 1, 2, 4
%TESTBASE%\line-size-1.trizbort
echo Line sizes tested: 1, 2, 4
%TESTBASE%\line-size-2.trizbort
echo Line sizes tested: 1, 2, 4
%TESTBASE%\line-size-4.trizbort
echo Text offsets of line annotations: 4, 20, 50
%TESTBASE%\text-offset-4.trizbort
echo Text offsets of line annotations: 4, 20, 50
%TESTBASE%\text-offset-20.trizbort
echo Text offsets of line annotations: 4, 20, 50
%TESTBASE%\text-offset-50.trizbort
echo Arrow sizes tested: 6, 12, 24
%TESTBASE%\arrow-size-6.trizbort
echo Arrow sizes tested: 6, 12, 24
%TESTBASE%\arrow-size-12.trizbort
echo Arrow sizes tested: 6, 12, 24
%TESTBASE%\arrow-size-24.trizbort
echo Make sure all text is copied reliably
%TESTBASE%\text-cut-paste.trizbort
echo make sure unavailable font doesn't cause problems
%TESTBASE%\unavailable-font.trizbort
echo hover over line, click line, create new region (should have orange text)
%TESTBASE%\map-settings-colors.trizbort

