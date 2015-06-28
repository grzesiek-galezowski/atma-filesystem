# atma-filesystem

# Principles

## Strong typing

## Explicit conversions

## Immutability

## Separation between paths and filesystem elements

|aaa | AbsoluteDirectoryPath | RelativeDirectoryPath | AnyDirectoryPath | AnyPath |
|----|------------------------|-----------------------|------------------|--------|
| AsAnyDirectoryPath() | AnyDirectoryPath | AnyDirectoryPath | 
| AsAnyPath() | AnyPath | AnyPath | AnyPath |
| DirectoryName() | DirectoryName | DirectoryName | DirectoryName |
| Info() | DirectoryInfo | DirectoryInfo | DirectoryInfo |
| ParentDirectory() | Maybe&lt;AbsoluteDirectoryPath&gt; | Maybe&lt;RelativeDirectoryPath&gt; | Maybe&lt;AnyDirectoryPath&gt; |	Maybe&lt;AnyDirectoryPath&gt; |
| Root() | AbsoluteDirectoryPath |
| ToString() | string | string | string | string |
