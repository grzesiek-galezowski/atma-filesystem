# atma-filesystem

# Principles

## Strong typing

When a path is just a string:

 * there is no distinction between absolute paths or relative paths, between paths poiting to files and paths pointing to directories. This is very error-prone when using APIs like: `void SaveConfigIn(string path)` because it is not clear whether we need to specify only a directory path (and a default file name will be used) or a path with file name.


### only valid methods, maybe etc., create only valid types

## Explicit conversions

### no fluent interface

### no inheritance

## Immutability

## Separation between paths and filesystem elements

# API

## `+` operator

### Directory types

#### Adding directory names

`AbsoluteDirectoryPath + DirectoryName = AbsoluteDirectoryPath`

`RelativeDirectoryPath + DirectoryName = RelativeDirectoryPath`

`AnyDirectoryPath + DirectoryName = AnyDirectoryPath`

#### Adding file names

`AbsoluteDirectoryPath + FileName = AbsoluteFilePath`

`RelativeDirectoryPath + FileName = RelativeFilePath`

`AnyDirectoryPath + FileName = AnyFilePath`

#### Appending relative directory path

`AbsoluteDirectoryPath + RelativeDirectoryPath = AbsoluteDirectoryPath`

`RelativeDirectoryPath + RelativeDirectoryPath = RelativeDirectoryPath`

`AnyDirectoryPath + RelativeDirectoryPath = AnyDirectoryPath`

#### Appending relative file path

`AbsoluteDirectoryPath + RelativeFilePath = AbsoluteDirectoryPath`

`RelativeDirectoryPath + RelativeFilePath = RelativeDirectoryPath`

`AnyDirectoryPath + RelativeFilePath = AnyDirectoryPath`

### File types

There is nothing that can be added to file paths to create new values.

### Other types

For the rest of the types, the only valid addition is:

`FileNameWithoutExtension + FileExtension = FileName`

## Other conversions

### Directory path types

| <sub>Method name / class</sub> | <sub>`AbsoluteDirectoryPath`</sub> | <sub>`RelativeDirectoryPath`</sub> | <sub>`AnyDirectoryPath`</sub> | <sub>`AnyPath`</sub> |
|----|------------------------|-----------------------|------------------|--------|
| <sub>**`AsAnyDirectoryPath()`**</sub> | <sub>`AnyDirectoryPath`</sub> | <sub>`AnyDirectoryPath`</sub> | 
| <sub>**`AsAnyPath()`**</sub> | <sub>`AnyPath`</sub> | <sub>`AnyPath`</sub> | <sub>`AnyPath`</sub> |
| <sub>**`DirectoryName()`**</sub> | <sub>`DirectoryName`</sub> | <sub>`DirectoryName`</sub> | <sub>`DirectoryName`</sub> |
| <sub>**`Info()`**</sub> | <sub>`DirectoryInfo`</sub> | <sub>`DirectoryInfo`</sub> | <sub>`DirectoryInfo`</sub> |
| <sub>**`ParentDirectory()`**</sub> | <sub>`Maybe<AbsoluteDirectoryPath>`</sub> | <sub>`Maybe<RelativeDirectoryPath>`</sub> | <sub>`Maybe<AnyDirectoryPath>`</sub> |	<sub>`Maybe<AnyDirectoryPath>`</sub> |
| <sub>**`Root()`**</sub> | <sub>`AbsoluteDirectoryPath`</sub> |
| <sub>**`ToString()`**</sub> | <sub>`string`</sub> | <sub>`string`</sub> | <sub>`string`</sub> | <sub>`string`</sub> |

### File path types

| <sub>Method name / class</sub> | <sub>`AbsoluteDirectoryPath`</sub> | <sub>`RelativeDirectoryPath`</sub> | <sub>`AnyDirectoryPath`</sub> | <sub>`AnyPath`</sub> |
|----|------------------------|-----------------------|------------------|--------|
| <sub>**`AsAnyFilePath()`**</sub> | <sub>`AnyFilePath`</sub> | <sub>`AnyFilePath`</sub> | 		
| <sub>**`AsAnyPath()`**</sub> | <sub>`AnyPath`</sub> | <sub>`AnyPath`</sub> | <sub>`AnyPath`</sub> | 	
| <sub>**`ChangeExtensionTo()`**</sub> | <sub>`AbsoluteFilePath`</sub> | <sub>`RelativeFilePath`</sub> | <sub>`AnyFilePath`</sub> | 	
| <sub>**`ParentDirectory()`**</sub> | <sub>`AbsoluteDirectoryPath`</sub> | <sub>`RelativeDirectoryPath`</sub> | <sub>`AnyDirectoryPath`</sub> | <sub>`Maybe<AnyDirectoryPath>`</sub> | 
| <sub>**`FileName()`**</sub> | <sub>`FileName`</sub> | <sub>`FileName`</sub> | <sub>`FileName`</sub> | 
| <sub>**`Has()`**</sub> | <sub>`bool`</sub> | <sub>`bool`</sub> | <sub>`bool`</sub> | 
| <sub>**`Info()`**</sub> | <sub>`FileInfo`</sub> | <sub>`FileInfo`</sub> | <sub>`FileInfo`</sub> | 	
| <sub>**`Root()`**</sub> | <sub>`AbsoluteDirectoryPath`</sub> | 			
| <sub>**`ToString()`**</sub> | <sub>`string`</sub> | <sub>`string`</sub> | <sub>`string`</sub> | <sub>`string`</sub> | 

