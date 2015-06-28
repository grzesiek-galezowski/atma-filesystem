# atma-filesystem

**This page is under construction. Please bear with me**

I strongly believe there are use cases for a path API based on strong types representing each specific path variation as I have myself been in a situation where such API would come in handy.

# Principles

## Strong type safety

When a path is just a string, there is no distinction between absolute paths or relative paths, between paths pointing to files and paths pointing to directories. This is very error-prone when using APIs like: `void SaveConfigIn(string path)` because it is not clear whether we need to specify only a directory path (and a default file name will be used) or a path with file name. This allows passing invalid input undetected.

### A type per path variant

On the other hand, Atma Filesystem clearly distinguishes between path variants, like absolute vs. relative or path to file vs. path to directory. It does so using a separate type for each of the variants and these types are incompatible with each other. So, if a method signature is: `void SaveConfigIn(AbsoluteDirectoryPath path)`, then we cannot pass a file path or a relative path of any sort. Of course, we can convert one type to another.

### Only valid conversion allowed

Whereas .NET `Path` class lets us operate on any arbitrary strings, allowing invocations like `Path.Combine("C:\", "C:\)` to compile, Atma Filesystem types allow only conversions that are guaranteed to produce valid output. For example, when you have an `AbsolutePathWithFileName`, you can get its root path by invoking a `Root()` method. On the other hand, a `RelativePathWithFileName` does not have this method at all.

There are times when we are not sure whether a method will succeed, for example we have an absolute directory path like this: `C:\Dir\` and we can obtain its parent directory, which would give us `C:\`, which is still an absolute directory path. On the other hand, when we have an absolute directory path consisting only from `C:\`, getting its directory is an invalid operation. In such cases, Atma Filesystem methods return a `Maybe<AbsoluteDirectoryPath>`, which either has a value or does not have it, depending on the request for parent directory making sense or not.

### Early checking

When working with strings as paths and the paths are malformed, we sometimes do not get to know this until we actually perform some kind of operation on a filesystem. This makes it more difficult to find errors, since the exceptions are thrown from places that are sometimes very far away from where the errors were made.

Atma Filesystem types make an attempt to verify path format as early as possible, which is when creating a new instance from a string. Thus, each type has a creation method called `Value(string value)`, which throws when the path is malformed. This makes it easier to spot errors as soon as they are made, not after the value is passed through twenty methods and is used in an I/O operation (Note that it does not check things like file existence, which is specific to a filesystem, not the path value itself).

### only valid methods, maybe etc., create only valid types - early checking

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



 * `AbsoluteFilePath` - for absolute paths pointing to files, e.g. `C:\dir\subdir\file.txt`
 * `RelativeFilePath` - for relative paths pointing to files, e.g. `subdir\file.txt`
 * `AnyFilePath` - for both relative and absolute paths pointing to files - this can be useful in cases where we don't really care whether the path is relative or absolute (e.g. where paths are passed as commandline arguments), as long as it is pointing to a file.

three basic directory path types 
 * `AbsoluteDirectoryPath` - for absolute paths pointing to files, e.g. `C:\dir\subdir\file.txt`
 * `RelativeFilePath` - for relative paths pointing to files, e.g. `subdir\file.txt`
 * `AnyFilePath` - for both relative and absolute paths pointing to files - this can be useful in cases where we don't really care whether the path is relative or absolute (e.g. where paths are passed as commandline arguments), as long as it is pointing to a file. 
 
