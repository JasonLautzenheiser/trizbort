@echo off

set TESTBASE=.

echo this is a bunch of manual test cases, where the instructions are provided as the description of a large sized room.

echo Manual test case. Edit::Select special
%TESTBASE%\manual-edit-select-special.trizbort
echo Manual test case. Room swaps
%TESTBASE%\manual-room-swap.trizbort
echo Manual test case. Help menu
%TESTBASE%\manual-help-menu.trizbort
echo Manual test case. View menu
%TESTBASE%\manual-view-menu.trizbort
