# guild-translation-tool
A little helper that converts .loo files forGuild 3 localization to xlsx files and back.

## Usage
Simply drag the .loo or .xlsx file on the executable, it will create the converted version in the target directory. 
It will not overwrite existing files, instead create copies with _1, _2, etc appended.


## Dependencies
This project uses https://github.com/ClosedXML/ClosedXML to handle xlsx files.