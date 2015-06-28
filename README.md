# atma-filesystem

# Principles

## Strong typing

## Explicit conversions

## Immutability

## Separation between paths and filesystem elements

| Method name / class | AbsoluteDirectoryPath | RelativeDirectoryPath | AnyDirectoryPath | AnyPath |
|----|------------------------|-----------------------|------------------|--------|
| **`AsAnyDirectoryPath()`** | `AnyDirectoryPath` | `AnyDirectoryPath` | 
| **`AsAnyPath()`** | `AnyPath` | `AnyPath` | `AnyPath` |
| **`DirectoryName()`** | `DirectoryName` | `DirectoryName` | `DirectoryName` |
| **`Info()`** | `DirectoryInfo` | `DirectoryInfo` | `DirectoryInfo` |
| **`ParentDirectory()`** | `Maybe<AbsoluteDirectoryPath>` | `Maybe<RelativeDirectoryPath>` | `Maybe<AnyDirectoryPath>` |	`Maybe<AnyDirectoryPath>` |
| **`Root()`** | `AbsoluteDirectoryPath` |
| **`ToString()`** | `string` | `string` | `string` | `string` |
