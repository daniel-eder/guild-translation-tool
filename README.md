# guild-translation-tool
A little helper that converts .loo files for Guild 3 localization to xlsx files and back.

## Usage

`GTT.Terminal.exe <File> [--createlanguagefiles] [--formatfile <FormatFile>]`

* `--createlanguagefiles` creates languages files with their proper names for copy & paste. Not yet useful - required once guild handles itself languages again. 
* `--formatfile` a file used to provide formatting information. Currently not used, will be used to verify max length of texts and other requirements.  An up to date version can always be found in `/Resources` in the repository.

The program will automatically detect if the supplied file is .loo or .xlsx. 
By default subdirectories for each language will be created, and `locdirect_english.loo` files will end up in each. Overwriting the original `locdirect_english.loo` in `The Guild 3\media\localization\` will result in the translation being used.


**IMPORTANT**: Do not modify the column headers in the spreadsheet, nor should you modify the .loo files manually to avoid syntax errors that might lead to problems in parsing with either this tool or the game.

### Editing
If you do not own a copy of Microsoft Office you can use Google Sheets ( https://docs.google.com/spreadsheets/u/0/ ) to import and export the files.

* Import files using File -> Import -> Upload
* Export files by using File -> Download As -> Microsoft Excel

## Community Translations
You can find all information about the community translation effort in our steam thread: http://steamcommunity.com/app/311260/discussions/0/1520386297685072772/ 

## Dependencies
This project uses https://github.com/ClosedXML/ClosedXML to handle xlsx files and https://github.com/commandlineparser/commandline for command line arguments.
Dependencies are managed with NuGet and package information is included in the repository.