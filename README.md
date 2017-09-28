# guild-translation-tool
A little helper that converts .loo files for Guild 3 localization to xlsx files and back.

## Usage
Simply drag the .loo or .xlsx file on the executable, it will create the converted version in the same directory the source file was found in. 
It will not overwrite existing files, instead create copies with _1, _2, etc appended.

**IMPORTANT**: Do not modify the column headers in the spreadsheet, nor should you modify the .loo files manually to avoid syntax errors that might lead to problems in parsing with either this tool or the game.

### Editing
If you do not own a copy of Microsoft Office you can use Google Sheets ( https://docs.google.com/spreadsheets/u/0/ ) to import and export the files.

* Import files using File -> Import -> Upload
* Export files by using File -> Download As -> Microsoft Excel

## Community Translations
You can find all information about the community translation effort in our steam thread: http://steamcommunity.com/app/311260/discussions/0/1520386297685072772/ 

## Dependencies
This project uses https://github.com/ClosedXML/ClosedXML to handle xlsx files.