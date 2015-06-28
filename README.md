# atma-filesystem

# Principles

## Strong typing

## Explicit conversions

## Immutability

## Separation between paths and filesystem elements

# API

## `+` operator

### Directory types

| ADDITION TABLE | <sub>`AnyDirectoryPath`</sub> | <sub>`AnyPath`</sub> | <sub>`AnyFilePath`</sub> | <sub>`DirectoryName`</sub> | <sub>`DirectoryPath`</sub> | <sub>`FileExtension`</sub> | <sub>`FileName`</sub> | <sub>`FileNameWithoutExtension`</sub> | <sub>`AbsoluteFilePath`</sub> | <sub>`RelativeDirectoryPath`</sub> | <sub>`RelativeFilePath`</sub> | 
|-------|-------|-------|-------|-------|-------|-------|-------|-------|-------|-------|-------| 
| <sub>**`AbsoluteDirectoryPath`**</sub> | x | x | x | <sub>`AbsoluteDirectoryPath`</sub> | x | x | <sub>`AbsoluteFilePath`</sub> | <sub>`AbsoluteDirectoryPath`</sub> | 	<sub>`AbsoluteFilePath`</sub> | 
| <sub>**`RelativeDirectoryPath`**</sub> | x | x | <sub>`RelativeDirectoryPath`</sub> | x | x | <sub>`RelativeFilePath`</sub> | x | x | <sub>`RelativeDirectoryPath`</sub> | <sub>`RelativeFilePath`</sub> | 
| <sub>**`AnyDirectoryPath`**</sub> | x | x | x | <sub>`AnyDirectoryPath`</sub> | x | x | <sub>`AnyFilePath`</sub> | x | x | <sub>`AnyDirectoryPath`</sub> | <sub>`AnyFilePath`</sub> | 
| <sub>**`AnyPath`**</sub> | x | x | x | x | x | x | x | x | x | x | x | 

### File types

| ADDITION TABLE | `AnyDirectoryPath` | `AnyPath` | `AnyFilePath` | `DirectoryName` | `DirectoryPath` | `FileExtension` | `FileName` | `FileNameWithoutExtension` | `AbsoluteFilePath` | `RelativeDirectoryPath` | `RelativeFilePath` | 
|-------|-------|-------|-------|-------|-------|-------|-------|-------|-------|-------|-------| 
| **`AbsoluteFilePath`** | x | x | x | x | x | x | x | x | x | x | x | 
| **`RelativeFilePath`** | x | x | x | x | x | x | x | x | x | x | x | 
| **`AnyFilePath`** | x | x | x | x | x | x | x | x | x | x | x | 
| **`AnyPath`** | x | x | x | x | x | x | x | x | x | x | x | 

### Other types

| ADDITION TABLE | `AnyDirectoryPath` | `AnyPath` | `AnyFilePath` | `DirectoryName` | `DirectoryPath` | `FileExtension` | `FileName` | `FileNameWithoutExtension` | `AbsoluteFilePath` | `RelativeDirectoryPath` | `RelativeFilePath` | 
|-------|-------|-------|-------|-------|-------|-------|-------|-------|-------|-------|-------| 
| **`DirectoryName`** | x | x | x | x | x | x | x | x | x | x | x | 
| **`FileExtension`** | x | x | x | x | x | x | x | x | x | x | x | 
| **`FileName`** | x | x | x | x | x | x | x | x | x | x | x | 
| **`FileNameWithoutExtension`** | x | x | x | x | x | `FileName` | x | x | x | x | x | 

## Other conversions

### Directory path types

| Method name / class | `AbsoluteDirectoryPath` | `RelativeDirectoryPath` | `AnyDirectoryPath` | `AnyPath` |
|----|------------------------|-----------------------|------------------|--------|
| **`AsAnyDirectoryPath()`** | `AnyDirectoryPath` | `AnyDirectoryPath` | 
| **`AsAnyPath()`** | `AnyPath` | `AnyPath` | `AnyPath` |
| **`DirectoryName()`** | `DirectoryName` | `DirectoryName` | `DirectoryName` |
| **`Info()`** | `DirectoryInfo` | `DirectoryInfo` | `DirectoryInfo` |
| **`ParentDirectory()`** | `Maybe<AbsoluteDirectoryPath>` | `Maybe<RelativeDirectoryPath>` | `Maybe<AnyDirectoryPath>` |	`Maybe<AnyDirectoryPath>` |
| **`Root()`** | `AbsoluteDirectoryPath` |
| **`ToString()`** | `string` | `string` | `string` | `string` |

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

