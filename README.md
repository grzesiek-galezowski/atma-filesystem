# atma-filesystem

# Principles

## Strong typing

## Explicit conversions

## Immutability

## Separation between paths and filesystem elements

# API

## `+` operator

| ADDITION TABLE | `AnyDirectoryPath` | `AnyPath` | `AnyFilePath` | `DirectoryName` | `DirectoryPath` | `FileExtension` | `FileName` | `FileNameWithoutExtension` | `AbsoluteFilePath` | `RelativeDirectoryPath` | `RelativeFilePath` | 
|-------|-------|-------|-------|-------|-------|-------|-------|-------|-------|-------|-------| 
| **`AnyDirectoryPath`** | x | x | x | AnyDirectoryPath | x | x | AnyFilePath | x | x | AnyDirectoryPath | AnyFilePath | AnyPath | x | x | x | x | x | x | x | x | x | x | x | 
| **`AnyFilePath`** | x | x | x | x | x | x | x | x | x | x | x | 
| **`DirectoryName`** | x | x | x | x | x | x | x | x | x | x | x | 
| **`AbsoluteDirectoryPath`** | x | x | x | AbsoluteDirectoryPath | x | x | AbsoluteFilePath | AbsoluteDirectoryPath | 	AbsoluteFilePath | 
| **`FileExtension`** | x | x | x | x | x | x | x | x | x | x | x | 
| **`FileName`** | x | x | x | x | x | x | x | x | x | x | x | 
| **`FileNameWithoutExtension`** | x | x | x | x | x | FileName | x | x | x | x | x | 
| **`AbsoluteFilePath`** | x | x | x | x | x | x | x | x | x | x | x | 
| **`RelativeDirectoryPath`** | x | x | RelativeDirectoryPath | x | x | RelativeFilePath | x | x | RelativeDirectoryPath | RelativeFilePath | 
| **`RelativeFilePath`** | x | x | x | x | x | x | x | x | x | x | x | 


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

