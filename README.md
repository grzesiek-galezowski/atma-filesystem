# atma-filesystem

# Principles

## Strong typing

### only valid methods, maybe etc.

## Explicit conversions

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

| xxx | `AbsoluteFilePath` | `RelativeFilePath` | `AnyFilePath` | `AnyPath` |
|-----|------------------|------------------|-------------|---------|
| **`AsAnyFilePath()`** | `AnyFilePath` | `AnyFilePath` | 		
| **`AsAnyPath()`** | `AnyPath` | `AnyPath` | `AnyPath` | 	
| **`ChangeExtensionTo()`** | `AbsoluteFilePath` | `RelativeFilePath` | `AnyFilePath` | 	
| **`ParentDirectory()`** | `AbsoluteDirectoryPath` | `RelativeDirectoryPath` | `AnyDirectoryPath` | `Maybe<AnyDirectoryPath>` | 
| **`FileName()`** | `FileName` | `FileName` | `FileName` | 
| **`Has()`** | `bool` | `bool` | `bool` | 
| **`Info()`** | `FileInfo` | `FileInfo` | `FileInfo` | 	
| **`Root()`** | `AbsoluteDirectoryPath` | 			
| **`ToString()`** | `string` | `string` | `string` | `string` | 

