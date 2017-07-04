@echo off

set TESTBASE=.

echo this is a bunch of manual test cases for manipulating the canvas.
echo Instructions should be contained in a marquee room at the top, if there is nothing in the below documents.
echo Some documents may be urposefully simple.

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
echo ctrl-alt-7 to test exported code.
%TESTBASE%\properties-in-brackets-basic.trizbort
echo test zooms with ctrl-home. Should be 21% on 1050 height screen.
%TESTBASE%\17-by-17-1200-margins.trizbort
echo test zooms with ctrl-home. Should be 21% on 1050 height screen.
%TESTBASE%\17-by-17-1200h-margins.trizbort
echo test zooms with ctrl-home. Should be 32% on 1050 height screen.
%TESTBASE%\17-by-17-1200w-margins.trizbort
echo test zooms with ctrl-home. Should be 28% on 1050 height screen.
%TESTBASE%\17-by-17-600-margins.trizbort
echo test zooms with ctrl-home. Should be 44% on 1050 height screen.
%TESTBASE%\17-tall-0margins.trizbort
echo test zooms with ctrl-home. Should be 42% on 1050 height screen.
%TESTBASE%\17-tall.trizbort
echo test zooms with ctrl-home. Should be 58% on 1050 height screen.
%TESTBASE%\17-wide.trizbort
echo should not blow up or have extreme zoom percent
%TESTBASE%\vertical-connector-only.trizbort
echo should not blow up or have extreme zoom percent
%TESTBASE%\vertical-connector-only-2.trizbort
echo should not blow up or have extreme zoom percent
%TESTBASE%\horizontal-connector-only.trizbort
echo should not blow up or have extreme zoom percent
%TESTBASE%\horizontal-connector-only-2.trizbort
