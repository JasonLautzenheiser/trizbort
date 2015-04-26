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
echo Make sure all text is copied reliably
%TESTBASE%\text-cut-paste.trizbort