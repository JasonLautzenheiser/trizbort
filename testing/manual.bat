@echo off

set TESTBASE=.

echo this is a bunch of manual test cases for manipulating the canvas.
echo Instructions should be contained in a marquee room at the top.

echo Manual test case. Edit::Select special
%TESTBASE%\manual-edit-select-special.trizbort
echo Manual test case. Room swaps
%TESTBASE%\manual-room-swap.trizbort
echo Manual test case. Help menu
%TESTBASE%\manual-help-menu.trizbort
echo Manual test case. View menu
%TESTBASE%\manual-view-menu.trizbort
echo Manual test case. Make sure handles and ports are all right.
%TESTBASE%\manual-room-shape-ports-handles.trizbort
echo Manual test case. Make sure control-arrows from connections go to the right rooms.
%TESTBASE%\manual-control-arrow-connections.trizbort
echo Manual test case. Make sure connector chooses right room on (control?)-(bracket/left bracket).
%TESTBASE%\connector-choose-right-room.trizbort
echo Manual test case. Make sure that rotating a connector skips full ports and throws an error if there are no empty ports.
%TESTBASE%\connector-rotate-full-ports.trizbort
echo Manual test case. Make sure that fourths, eighths and sixteenths do not change this.
%TESTBASE%\auto-adjust-fourths.trizbort
echo Manual test case. Make sure that eighths and sixteenths do not change this. Fourths should mangle it.
%TESTBASE%\auto-adjust-eighths.trizbort
echo Manual test case. Make sure that sixteenths do not change this. Eighths and fourths should mangle it.
%TESTBASE%\auto-adjust-sixteenths.trizbort
echo Manual test case. Use shift-arrows to get through the various maps.
%TESTBASE%\shift-arrow-basic-directions.trizbort
echo Manual test case. Use shift-arrows to get through the various maps.
%TESTBASE%\shift-arrow-bent-directions.trizbort
echo Manual test case. Use shift-arrows to get through the various maps.
%TESTBASE%\shift-arrow-off-directions.trizbort
echo Manual test case. Save as PDF and verify each room's description-annotation looks reasonable.
%TESTBASE%\pdf-save-annotation.trizbort
