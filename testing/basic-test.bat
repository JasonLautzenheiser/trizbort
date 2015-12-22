@echo off

::to rebuild
::7z a all-test-files.zip *.trizbort *.bat

echo All 6 border types appear below.
%TESTBASE%\borders.trizbort
echo Connection types and combinations are tested here.
%TESTBASE%\connections.trizbort
echo All 9 room type fills are tested here. The center is blank. They should form a white octagon.
%TESTBASE%\fills.trizbort
echo All 9 object placements are tested here. The object is listed in the room in the center.
%TESTBASE%\object-placement.trizbort
echo Arial bold italic, black and black oblique (room object line)
%TESTBASE%\font-bolditalic-black-blackoblique.trizbort
echo Arial narrow, narrow italic, narrow (room object line)
%TESTBASE%\font-narrow-narrowitalic-italic.trizbort
echo Arial narrow bold, narrow bold italic, bold (room object line)
%TESTBASE%\font-narrowbold-narrowbolditalic-bold.trizbort
echo Arial strikeout+underline, strikeout, underline (room object line)
%TESTBASE%\font-strikeoutunderline-strikeout-underline.trizbort
echo Font size: 12-10-8 (room object line)
%TESTBASE%\font-size-12-10-8.trizbort
echo Font size: 18-16-14 (room object line)
%TESTBASE%\font-size-18-16-14.trizbort
echo Font size: 40-30-20 (room object line)
%TESTBASE%\font-size-40-30-20.trizbort
echo Font size: 7-6-5 (room object line)
%TESTBASE%\font-size-7-6-5.trizbort
echo Stalk lengths are tested: 16, 32, 48
%TESTBASE%\stalk-length-16.trizbort
echo Stalk lengths are tested: 16, 32, 48
%TESTBASE%\stalk-length-32.trizbort
echo Stalk lengths are tested: 16, 32, 48
%TESTBASE%\stalk-length-48.trizbort
echo Line sizes tested: 1, 2, 4
%TESTBASE%\line-width-1.trizbort
echo Line sizes tested: 1, 2, 4
%TESTBASE%\line-width-2.trizbort
echo Line sizes tested: 1, 2, 4
%TESTBASE%\line-width-4.trizbort
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
echo Dark tab sizes tested: 12, 24, 48, 96
%TESTBASE%\dark-tab-12.trizbort
echo Dark tab sizes tested: 12, 24, 48, 96
%TESTBASE%\dark-tab-24.trizbort
echo Dark tab sizes tested: 12, 24, 48, 96
%TESTBASE%\dark-tab-48.trizbort
echo Dark tab sizes tested: 12, 24, 48, 96
%TESTBASE%\dark-tab-96.trizbort
echo One-room lines
%TESTBASE%\one-room-lines.trizbort
echo Fills in different shaped rooms
%TESTBASE%\fill-different-room-shapes.trizbort
echo Fills in dark rooms
%TESTBASE%\fills-orange-region-dark.trizbort
echo Fills in light rooms
%TESTBASE%\fills-orange-region.trizbort
echo Line colors
%TESTBASE%\line-colors.trizbort
echo Rounded room labeled lines
%TESTBASE%\rounded-to-from.trizbort
echo Rounded room labeled lines
%TESTBASE%\rounded-to-from-16ths.trizbort
echo Rounded room gradation
%TESTBASE%\rounded-1-to-25.trizbort
echo Ellipse room labeled lines
%TESTBASE%\ellipse-to-from.trizbort
echo Ellipse room labeled lines
%TESTBASE%\ellipse-to-from-16ths.trizbort
echo odd room links to itself
%TESTBASE%\room-link-to-itself.trizbort
echo use ctrl arrows to autolink
%TESTBASE%\small-room-ctrl-arrow-test.trizbort
echo stray line
%TESTBASE%\stray-line.trizbort
echo purple start room
%TESTBASE%\start-room-purple.trizbort
echo line labeling: all 24 placements here (up, down and middle text from center room)
%TESTBASE%\line-labeling.trizbort
echo line labeling: all 24 placements here (up, down and middle text from center room)
%TESTBASE%\line-labeling-side-by-side.trizbort
echo subtitle positioning and sizing: smaller than title (issue #220)
%TESTBASE%\long-subtitles.trizbort
echo subtitle positioning and sizing: larger than title (issue #219)
%TESTBASE%\long-big-subtitles.trizbort
echo Basic ASCII extended characters should work
%TESTBASE%\ext-chars-test.trizbort
