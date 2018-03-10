# Change Log
All notable changes to this project will be documented in this file.

## [Unreleased]

### Added
- The room subtitle can now have it's own font and color, both in default settings and individual room properties. (#50)

### Fixed
- Fix for two message boxes showing in room properties when room name is empty.
- Fix for two message boxes showing in map properties when default room name is empty.
- Fix issue where canceling from the color dialog in room properties would change the color to a default color instead of keeping the original color
- Fix issue that caused Paste menu item under Edit to not be enabled appropriately.

## [1.6.0] - 2018-03-03

### Added
- check for new versioned files and show warning when loading map (#353)
- Trizbort now will detect changes to loaded map from another application and ask to reload the map (#365)
- Added export for Quest (Thanks to @ThePix for this) (#392)
- Added option in AppSettings to show the full file path in the main window caption.
- Can now set a default room shape for a map in Map Settings (#93)
- Can add a link to reference another room from a room (done through room properties).  Ctrl-Click on the room with the reference will go to the room being referenced (#370)
- Color picker in map settings now allows setting multiple colors at once.  Just Ctrl or Shift click on the colors to select many and all can be set to same color at once (#108)

### Fixed
- fixed naming consistency between context menu and main menu and room shape names (#356) 
- opening zoom is a bit more accurate and better handles margins (#357)
- Fixed issue where Trizbort wasn't saving the last loaded project if that map was loaded from most recent maps menu (#364)
- Minor adjustment on alignment of in/out text on rounded and octagonal rooms.  (#378)
- fixed error with room property dialog and rooms shapes. (#394, #388)
- copy of room now copies the subtitle.
- default object text color was not respected if object text was inside room and no room specific object text color was chosen. (#402)

### Changed

- Refactored the copy / paste giving some code optimizations and fixing some copy bugs where some properties were not getting copied. (#379, TRIZ-145, TRIZ-152, TRIZ-47)
- Application Settings has been refactored.  App Settings are now stored in an appsettings.json file in the application folder.  The legacy settings.xml stored in the user folder will be read in if it exists and there is no appsettings.json file.  This simplified the code and allows for easier management as we add new options.  Map settings still stored in the map file.
- moved the revisions.txt to a more standard changelog.md file.
- added scrollbars to the Map Settings -> Description and History inputs. (#390, #391)
- App settings are now saved on an affirmative close of the app settings dialog.

## [1.5.9.9] - 2017-07-02

### Added
 - Beginnings of a room validation with graphical indicator of rooms that don't pass validation.  (#333)
 - Put menu items (shortcut keys) in place for set start room (Ctrl-F5) and set end room (Shift-F5).  (#335)
 - Added some stats for doors / connections (#343)
 - Added the end room into stats (#339)
 - Added a micro-zoom (Ctrl-MouseWheelUp/Down) (#341)
 
### Fixed
 - The Room properties dialog parent was not getting set properly which could cause it to get lost on alt-tab making trizbort appear to be locked up. (#332)

### Changed
 - Adjusted tab order and layout slightly to get more commonly used items to the top of the tab order. (#334)


 

## [1.5.9.8] - 2017-04-14

### Added
- I6 support for exporting Doors...thanks to Andrew Schultz and Hugo Labrande

### Fixed
- if the default hand drawn option was not checked in the appsettings, then you could never make a room with hand drawn edges.  This should be corrected.
- Accented characters where not being exported properly to Hugo, I6 and TADS (#330 & #329 - Thanks to Hugo Labrande for the report).  
- Tooltip issue when opening a new map when one was already loaded (#327)

## [1.5.9.7] - 2017-02-15

### Fixed
- Issue fixed with older maps that had blank regions and exporting to I7 which would result in a mostly empty I7 file.
- Issue with the EXITS command with last release that was breaking automap in general.  EXITS rolled back until a proper fix is in place.

## [1.5.9.6] - 2017-01-07

### Added
- Can now specify an end room. (#171)
- Added new option in Automapper, to assume two-way directions (on by default). (#296)
- Automap will now detect when a room is being connected in the same direction as another room. The user will be prompted to keep one or other or both. (#300)
- Add a FIND, currently only on Rooms (name, subtitle, objects and description).  Found elements are selected. (#57)
- F3 / Shift-F3 will cycle through found matches

### Fixed
- Fixed issue with room properties window opening in the upper left corner of the screen (#309)
- Issue in Automapper when restarting, it would sometimes get confused and not display any rooms. (#195)


### Changed
- Beginning to refactor code to take advantage of C#7

### Removed
- Removed the "fill" color in app settings.  It wasn't being used anymore (#310)




## [1.5.9.5] - 2016-08-05

### Added
- "tb dotted" in transcript now forces next connection to be dotted.
- "tb exit [direction]" in transcript will add a connection stub in that direction and "tb noexit [direction]" will remove a connection stub.
- refactored the command line handling.  Commands are now: 
    - **-a, --loadlastproject**
    - **-m, --automap** --> give it a text file transcript.  ex) `trizbort --automap zork1.txt`
    - **-q, --quicksave** --> quicksave the file to the given filename, useful in conjunction with other commands like --automap
	- **-s, --smartsave** --> silently do smartsave based on current smartsave settings.
	- **-x, --exit** --> close trizbort, again useful in conjunction with other arguments.  ex) `trizbort --automap zork1.txt --quicksave zork1.trizbort --exit`

- added command line parameters for exporting maps to source files --inform7, --inform6, --alan, --hugo, --tads, --zil
- Added region support to Hugo export.
- Added Ctrl-K to Force Darkness on selected rooms; Added Ctrl-Shift-K to force lighted on rooms.
- Added support for doors.  It can be setup in the connection properties.  Export to I7 only at this point.
- Add descriptions to connections.  Used only for doors at the moment.
- Allow for fine control of object text positioning.
 Tooltips will now show on items with out a title.  This will help with being able to put different attributes or descriptions on items (connections, rooms).
- Added option to app settings to show / not-show tooltips on objects.

### Fixed 
- Fix wording on context menu for hand-drawn room.

### Changed
- Refactored font code name.
- changed labels in app settings font settings.

## [1.5.9.4] - 2016-04-09

*We made a lot of improvements to exporting to your favorite IF language and cleaning up our own code.  We are also trying to add features that make sense to simplify building of maps while still giving you plenty of shortcuts and power tools.
We are also in the process of improving the export to different languages.  We've added ALAN support and improved support for I7, I6, TADS and ZIL.  As always if you find ways to improve, have suggestions or find bugs, please let us know or submit an issue
on github.  Documentation is a little outdated so apologies on that.  Hopefully we'll be updating that soon.*

*Future enhancements I hope to make:  Continue to improve and expand language export, improve the object language to handle more errors and give more flexibility.*

### Added
- beginning of object definition language in the objects list.  Still in beta (so use at own risk as things may change, but feedback is appreciated) and really focused on I7 at the moment.  We will continue to improve, fix and tweak.
- Command line option to start automapping of file.  -m transcriptfile [trizbort map name]
- Saves app window dimensions on close.
- Also #192, Now we can adjust margins on exported images.  We allow for default margins that can be adjusted and document specific margins.
- Annotations can be now be saved to PDFs (the annotation will be the room description)
- The short cut key '0' will select the starting room.
- Ctrl-B backs up your current project file.
- TADS/Adv3Lite export now supports regions.
- \ (backslash) on room will attempt to reorganize the connections.  Be careful with this as it tries to be smart about room location in relevence to each other.  It can make mistakes so we are looking at ways to improve and make trizbort a bit smarter.
- '[' and ']' as well as Ctrl-[ and Ctrl-] will move the selected connector around to available ports on the connected rooms. 
- Automap improvements: Mark start room.
- Option to auto-load the last saved map when opening trizbort.
- More command line options added (just beginning this, -? for current ones)
- Ctrl-D deletes all connectors from selected room.
- I6 export now implements regions as classes.
- ALAN Export - (Beta)
- Added popup for language definition in the description area.
- Hand-drawn is now a room property.  Global setting for new rooms, can be changed on a room level. 

### Changed
- Wording change on the duplicate room dialog in automapper.
- File dialog for choosing transcript in automapper now filters by *.txt and *.log -- Any other extensions in common use for transcripts?

### Fixed
- Disallow room names to be just numbers as it creates bad I7 code.
- Like #172, Blank room names are not allowed as it will create bad exported code.
- When running a test command in the I7 IDE, command numbers in brackets are created at the front of each prompt line, like '>[7] n', this confused the automapper.
- Trying to improve the export to code with some special characters from non-english languages.  (Further improvement suggestions welcome)
- Fixed some areas that were causing Trizbort to think the map was "dirty" and needed saved when it really wasn't.




## [1.5.9.3] - 2015-09-19

*We've done a lot of work on Trizbort in the first half of the month so I thought I'd get a new release out sooner rather than later.*

### Added
- Alpha of Hugo Exporter....not yet feature complete, please send feedback.
- Added octagonal room shape.  Great for Stop signs.
- Added stat for rooms you can't get out of "Dead Ends".
- Add graphic for straight edge room shape in room settings.
- Added checkbox to link all corners together to same value for rounded corners room type.
- Save the last tab on settings dialog to app settings.
- Set a room as the starting room in room settings. Highlight it in a lime green. This will export to I7 and ZIL.
- Added "About" to I6 exported code
- Added count of dark rooms to stats.


### Changed
- Change language in the settings to change "lines" to "connections".
- There was a flicker in the 'Toggle Darkness' in the Rooms menu
- Default name for new room should not be blank
- Remove the default ("NoRegion") from the list of regions that get exported.
- F1 opens up the online help.
- Better message on SmartSave if it can't save the image file

### Fixed
- Disable "Rooms:Change Regions" if there are 0 regions
- Can no longer attempt to change regions if there are 0 regions.
- Disable "Join rooms" in context menu/Rooms menu if rooms already joined
- hand-drawn does not persist on Ctrl-Arrow to create new room
- Error checking: request for warning box on unsuccessful smartsave
- Some shortcut keys for new menus introduced were triggering other actions.
- Failed PNG write gives successful smart-save message.
- Failed EMF export does not generate error
- darkness does not persist on Ctrl-Arrow to create new room
- Deleting region wasn't reverting it's "ex-rooms" to NoRegion
- Changing items in the Tools:Map Settings dialog wasn't marking the project as dirty.
- Line styles context menu item could erronously appear on the menu for a room.
- Realphabetize region listbox in settings after renaming inline.
- disallow starting spaces in regions and room as it could cause issues with exporting to code.
- Arrow keys moving canvas right / left were backwards.
- Issue with copy / paste and connection color.  Also issue with a colon in the name is fixed.

## [1.5.9.2] - 2015-09-05

### Added
- Enhanced the port calc code so they are more evenly spaced on ellipse and rounded rooms.
- Stats list # of rooms without a region.
- Add ability to change the default room name from "Cave"

### Fixed
- Fix crash when adding a room when an unconnected connection is selected, now adds a connection onto the end.
- Fix issue when changing default connection color and then reload map would change connections to light blue.
- Properties menu was not available in context menu on connection.

### Changed

- Minor formatting changes on stats.
- ZIL exporter tweaks (Thanks to Jesse McGrew)
    - Update main loop so M-END doesn't run for meta verbs.
    - Escape contents of strings.
    - Export history (creating an ABOUT verb).
    - Don't write empty room descriptions.
    - Use a PER routine for conditional exits.
    - Set object SYNONYM and ADJECTIVE separately.
    - Fix spacing/style nits.

## [1.5.9.1] - 2015-08-08

### Fixed
- Fix issue with multicolored rooms not showing more than one color.
- Fix the room dialog that would not allow rounded room radius below 15 (valid range is 1-30)

## [1.5.9.0] - 2015-08-08

### Added
- Allow other room shapes.  Ctrl-R for rounded edges, Ctrl-E for elliptical shape, Ctrl-H to toggle hand drawn and straight lines on an individual room.
- Broke out the Rooms and Connections menu items from Edit into their own main level menus.
- Enhanced the context menu for a room with much of the room editing items added in the last few versions.
- Cleaned up context menu for connections
- Cleaned up context menu for the map canvas.

### Fixed 
- Regions weren't respecting reserved words when exporting to I7


## [1.5.8.11] - 2015-07-20

### Added
- ZIL export now handles objects and various flag settings.

## [1.5.8.10] - 2015-07-19

### Added
- text now wraps on dashes as well as spaces (example room names) #73
- Add menu item to redact text (Ctrl-F4 already an option)  - #71
- History text in map settings will generate an "about" command when exporting to code. (currently only in I7 and Tads3)
- Start of ZIL Export.

### Changed
- Now use some C# 6.0 syntax features. Potentially breaking change in code.
- Change link to online help to point to new help text - #74

## [1.5.8.9] - 2105-06-27

### Added
- beginning of some map statistics.
- swap room names with ctrl-W, swap formats/fills with shift-W, swap regions with alt-W  #49
- automapper does a bit of a better job of ignoring the game title (still not 100%)
- Automap now handles rooms with () a bit better.
- added a Edit->Select Special with sub menu to allow for smart selection of different elements.

### Fixed
- setting a connection to plain (P),  now sets the color back to default and removes the middle text if any
- copy/paste was broken

## [1.5.8.8] - 2015-05-05

### Added
- Added ability to export source (I7, I6, and TADS) to the clipboard.  Menu items under export as well as keystrokes (Ctrl-Alt-7, Ctrl-Alt-6, Ctrl-Alt-T)
- Added feature for swapping objects in two selected rooms. (Select two rooms, hit 'W')
- checkbox in automap settings to start processing from end of transcript.  This gives the ability to restart an existing transcript / map where you left off last time (from Matt Watkins)
- automap can now process multiword special commands (default to 'tb see' and 'tb region').  gives better flexibilty and should better help some keyword collisions with certian games (from Matt Watkins)
- cleaning up automap code, providing better error messages for certain situations. (from Matt Watkins)
- Samples folder now contain many maps of real games
- documentation updated.

## 1.5.8.7 - 2015-04-20

### Added
- now allows for rooms without borders
- fixed connection coloring issue
- lots of bugs.
- handle undo with automapping (from Mr Shifty)

## 1.5.8.6

### Added
- allow for individual connection coloring.
- allow for individual rooms to have different room border styles.

## 1.5.8.5 - Public Release

### Fixed
- fixed issue with zoom text box exception if value is 0.

## 1.5.8.4

### Fixed
- fixed bug with coloring of white rooms on files saved with older versions of trizbort.

## 1.5.8.3

### Added
- added zoom textbox for fine tuning in status bar.
- Changed the room color selector to default to transparent...this allows for the color white to be selected as a valid color.
- added additional 2nd fill color styles (BottomLeft, Left, TopLeft, Top)
 
### Fixed
- fixed issue with room object arrows so only one could be selected.

## 1.5.8.2

### Added
- dark color tab is now adjusted automatically if it is too close in color to the region color.
- In/Out and Up/Down should now be processed correctly when exporting to code.
- add room subtitles
- Adding a room between two rooms when connection is selected now keeps region if both rooms are the same region.

### Fixed 
- minor misc bugs and UX improvements


## 1.5.8.1

### Added
- added some smart save options to allow selection of what to save.
- Fixed up keystrokes to be a little more intuitive to keyboard users in the room properties dialog.
 
### Fixed
- fixed old bug where if the room was a non-standard size, it didn't center on the mouse properly when adding a new room.
- fixed color display on selected rooms when object text was inside the room.
- fixed exception during copy / paste of multiple rooms and connections.
- F11 would crash the app if automapping was not turned on.
- Ctrl-F4 (debugging keystroke) will disable all text on the map.
- widened the region listbox to workaround a bug with listbox control hiding long names for owner-drawn listbox items.

## 1.5.8.0

### Added
- Insert a room between two other connected rooms.
- updates to context menu.
- made the focus after deleting a region make more sense.
- ability to resize multiple rooms at once with keyboard

### Fixed
- Fixed tooltip positions based on room so they don't overlap.
- fixed tab ordering of app settings.


## 1.5.7.1 - Public Release

### Added
- add version number to the saved trizbort files.

## 1.5.7.0

### Added
- Ctrl-Shift-A will now select all rooms with the same region as the currently selected rooms (TRIZ-91)
- Ctrl-Alt-Arrow to resize rooms (TRIZ-114)
- Menu Shortcut keys to export maps to source (TRIZ-125)
- Properties is now on the context menu (TRIZ-127)
- When room(s) are selected, the arrow keys move the rooms and not the map (TRIZ-128)

### Fixed
- double quotes are converted to single quotes in region names to fix I7 export issue (TRIZ-84)
- Colons are disallowed in region names (TRIZ-85)
- Line is left selected after it is drawn (TRIZ-88)
- blank region name is disallowed (TRIZ-98)
- in region setup, the focus is handled more intuitively when creating regions (TRIZ-103)
- Ctrl-Arrow for new room / passage preserves the current line style (TRIZ-104)
- Can't export an empty map to code. (TRIZ-115)
- fixed a null reference exception that sometimes occurred when changing a regions properties (TRIZ-113)
- user could leave the default graphic format blank which caused issues when smartsaving (TRIZ-98)
- region names now default properly on older files (TRIZ-116)
- underscores not allowed in region names (TRIZ-117)
- the UI to indicated selected rooms is much more noticable now. (TRIZ-126)
- Better handles the saving of the trizbort file during smartsave (TRIZ-95)

### Changed
-   renamed Tools: Restore default settings to make more sense now (TRIZ-109)
-   F2/Rename has been moved in menu to be grouped better (TRIZ-57)


## 1.5.6.3 - Public Release

### Added
- Regions are now alphabetized (TRIZ-78)

### Fixed
- fixed issue with region names and certain characters that would throw error when saving...now works like room names (TRIZ-80)
- another small issue with case sensitivity and adding regions names.  (TRIZ-77)

## 1.5.6.2

### Added
- Ctrl-Arrow when creating new room, now keeps the starting rooms region.  (TRIZ-46)
- exporting an image without an extension and "All Files" selected will now use the default image type. (TRIZ-70)

### Fixed
- Auto naming issue when large number of regions (TRIZ-51)
- Map Settings / Region tab, UI is friendlier (TRIZ-52, TRIZ-53)
- Dark room toggle in menu is no longer enabled when a line has the focus.  (TRIZ-60)
- fixed a few issues with the save dialog not remembering directories (TRIZ-72)
- odd issue where disabling the Delete Region button was not working for certain files is fixed (TRIZ-73)
- The Recent Maps menu item handles moved / deleted maps better. (TRIZ-75)
- Export to Inform 7
- Regions now handle the case insensitivty of Inform7 better. (TRIZ-50)  
- Changes the syntax of the I7 output to handle regions that start with "The" (TRIZ-56)
- Blank room names are handled more consistantly (includes I6 and TADS) (TRIZ-61)
- Objects that have the same name as a room are now handled (TRIZ-62)

## 1.5.6.1

### Fixed
- TRIZ-43  Should we be allowed to change NoRegion "region" name?
- TRIZ-44  Changing region name with "change" button allows duplicate region names
- TRIZ-45  Should we grey out "delete region" for no region?

## 1.5.6.0

### Added
- item text inside a colored region uses the room text color.

## 1.5.5.0

### Added
- Ctrl-Arrow keys will create room in that cardinal direction from the current room.  This just seemed natural.  Numpad Ctrl-Arrow keys still work (and only way to do diagonals)
- Regions are now supported in the export to Inform 7
- Automap feature now has a special "region" command to add a region from a transcript.
- Select two rooms, hit 'J' and they are combined in a sensible way based on positioning.
- Tooltips added when hovering over objects on the canvas.

## 1.5.4.0

### Added
- added a small zoom indicator to the map.

### Fixed
- fixed bug that the copy feature was not copying the region.
- fixed bug that would crash Trizbort if the PDF was opened when trying to export to PDF (or SmartSave)
- a new region text color now defaulted to text color in settings.
- when saving a map to an image, there is a app setting that will ignore the zoom level and save at 100%

## 1.5.3.0

### Added
- Added a right-click context menu.  Currently can change region of room and can change the dark status (works on multiple selected rooms)

### Fixed
- fixed issue where going to a new file didn't clear out the prior maps regions
- fixed issue where selecting multiple rooms wasn't being visually indicated.  It used to indicate by changing the fill color of the room, but I now just change the border color to the select line color in the settings.

## 1.5.2.0

### Added
- Allow user to change the region text color.

## 1.5.1.0

### Added
- show filenames that are saved during the SmartSave
- more reliable way to choose filenames for SmartSave, should fix the occasional issue where if two instances of Trizbort open, the filenames could get confused.

## 1.5.0.0

*This initial build is based off the last build from Genstein.  It also pulls in a few other forks that had some useful features.*

##### Features pulled from the Tymian branch
-   Allowed rooms to have their own colors, this overrides the global default room color.  [Room Properties Dialog]
-   Adds the ability to split a rooms color 50/50.  [Room Properties Dialog]
-   Ability to copy / paste of elements.

##### Bugs fixed in Tymian Branch
-   Issue with large maps locking up or slow loading when opening.

##### This also pulls from the tustin2121 fork which adds a few small features.

-   App settings dialog
-   Added invert mouse wheel setting
-   added canvas drag mouse button setting.

##### Then this adds some of my own changes.

- Added Smart Save feature which will save PDF and image file in one click to the project folder.
- Added a default image type in app settings that Smart Save uses for saving the image.
- Now you can add regions.  Regions can be setup in the map settings dialog, named and a color assigned to it.
- In the room properties, you can now assign a room to a region.  This will show the room in the region color assigned to the region.
- some minor bug fixes.

[Unreleased]: https://github.com/JasonLautzenheiser/trizbort/compare/v1.6.0...HEAD
[1.6.0]: https://github.com/JasonLautzenheiser/trizbort/compare/v1.5.9.9...v1.6.0
[1.5.9.9]: https://github.com/JasonLautzenheiser/trizbort/compare/v1.5.9.8...v1.5.9.9
[1.5.9.8]: https://github.com/JasonLautzenheiser/trizbort/compare/v1.5.9.7...v1.5.9.8
[1.5.9.7]: https://github.com/JasonLautzenheiser/trizbort/compare/v1.5.9.6...v1.5.9.7
[1.5.9.6]: https://github.com/JasonLautzenheiser/trizbort/compare/1.5.9.5...v1.5.9.6
[1.5.9.5]: https://github.com/JasonLautzenheiser/trizbort/compare/1.5.9.4...1.5.9.5
[1.5.9.4]: https://github.com/JasonLautzenheiser/trizbort/compare/1.5.9.3...1.5.9.4
[1.5.9.3]: https://github.com/JasonLautzenheiser/trizbort/compare/1.5.9.2...1.5.9.3
[1.5.9.2]: https://github.com/JasonLautzenheiser/trizbort/compare/1.5.9.1...1.5.9.2
[1.5.9.1]: https://github.com/JasonLautzenheiser/trizbort/compare/1.5.9.0...1.5.9.1
[1.5.9.0]: https://github.com/JasonLautzenheiser/trizbort/compare/1.5.8.11...1.5.9.0
[1.5.8.11]: https://github.com/JasonLautzenheiser/trizbort/compare/1.5.8.10...1.5.8.11
[1.5.8.10]: https://github.com/JasonLautzenheiser/trizbort/compare/1.5.8.9...1.5.8.10
[1.5.8.9]:  https://github.com/JasonLautzenheiser/trizbort/compare/1.5.8.8...1.5.8.9
[1.5.8.8]:  https://github.com/JasonLautzenheiser/trizbort/compare/1.5.8.7...1.5.8.8
