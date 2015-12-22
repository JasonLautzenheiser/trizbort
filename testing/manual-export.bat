@echo off

set TESTBASE=.

echo This file contains test cases for exporting code from Trizbort.
echo Instructions should be contained in a marquee room at the top.

echo Manual test case. Export to all formats via the clipboard and view in a text editor. Make sire the accented e's become e's. (issue #234)
%TESTBASE%\ext-chars-test.trizbort
