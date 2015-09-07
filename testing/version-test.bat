@echo off

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
%TESTBASE%\basics-1588.trizbort
echo Added rounded edges
%TESTBASE%\basics-1589.trizbort
%TESTBASE%\basics-15810.trizbort
%TESTBASE%\basics-15811.trizbort
%TESTBASE%\basics-1590.trizbort
%TESTBASE%\basics-1591.trizbort
%TESTBASE%\basics-1592.trizbort
