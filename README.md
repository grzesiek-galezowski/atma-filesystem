# Atma Filesystem - a path library for .NET

## Why was this library created?

First of all, I do not claim that Atma Filesystem is a better path library than .NET's `Path` or some others. There are many use cases for paths and different APIs address different use cases best.

Having said that, I strongly believe there are use cases for a path API based on strong types representing each specific path variation as I have myself been in a situation where such API would come in handy. These use cases are IMO not addressed well enough, hence I created this library.

# Design Principles

The design of Atma Filesystem is based on the following principles:

## An explicit type per path variant

When a path is just a string, there is no distinction between absolute paths or relative paths, between paths pointing to files and paths pointing to directories. This is very error-prone when using APIs like: `void SaveConfigIn(string path)` because it is not clear whether we need to specify only a directory path (and a default file name will be used) or a path with file name. This allows passing invalid input undetected.

On the other hand, Atma Filesystem clearly distinguishes between path variants, like absolute vs. relative or path to file vs. path to directory. It does so using a separate type for each of the variants and these types are incompatible with each other. So, if a method signature is: `void SaveConfigIn(AbsoluteDirectoryPath path)`, then we cannot pass a file path or a relative path of any sort. Of course, we can convert one type to another.

## Only valid conversions allowed

Whereas .NET `Path` class lets us operate on any arbitrary strings, allowing invocations like `Path.Combine("C:\", "C:\)` to compile, Atma Filesystem types allow only conversions that are guaranteed to produce valid output. For example, when you have an `AbsolutePathWithFileName`, you can get its root path by invoking a `Root()` method. On the other hand, a `RelativePathWithFileName` does not have this method at all.

There are times when we are not sure whether a method will succeed, for example we have an absolute directory path like this: `C:\Dir\` and we can obtain its parent directory, which would give us `C:\`, which is still an absolute directory path. On the other hand, when we have an absolute directory path consisting only from `C:\`, getting its directory is an invalid operation. In such cases, Atma Filesystem methods return a `Maybe<AbsoluteDirectoryPath>`, which either has a value or does not have it, depending on the request for parent directory making sense or not.

## Early checking

When working with strings as paths and the paths are malformed, we sometimes do not get to know this until we actually perform some kind of operation on a filesystem. This makes it more difficult to find errors, since the exceptions are thrown from places that are sometimes very far away from where the errors were made.

Atma Filesystem types make an attempt to verify path format as early as possible, which is when creating a new instance from a string. Thus, each type has a creation method called `Value(string value)`, which throws when the path is malformed. This makes it easier to spot errors as soon as they are made, not after the value is passed through twenty methods and is used in an I/O operation (Note that it does not check things like file existence, which is specific to a filesystem, not the path value itself).

## Only explicit conversions

The decision I made when implementing this library is that each change to the type of a value should be made consciously. Thus, there are no superclasses or interfaces that are implemented by several classes. The only way to get from one type to another is to explicitly say you want to do this.

There are types with weaker guarantees than others, for example, we have `AnyPath` that represents a file that can be both absolute and relative and can point to a directory or a path. One can say that it is a good superclass or interface for other classes to implement from. In Atma Filesystem, however, to get from e.g. `RelativeFilePath` to `AnyPath`, you have to explicitly invoke a `AsAnyPath()` conversion method.

Likewise, for interacting with .NET and third-party libraries, each type implements `ToString()` correctly, returning the path as a `string`.

## Prefer type safety over fluency

There are projects which present beautiful, fluent interfaces for constructing paths. On the other hand, when you look at Atma Filesystem, you may get an impression that it is pretty dull.

In Atma Filesystem, the focus is on a good working and concise type system, because I noticed that in real applications working with paths, it speeds up finding bugs and protects from creating new bugs immensely.

## Immutability

Atma Filesystem treats paths as values. One of the properties usually associated with values is their immutability. As paths are often passed along to many methods and objects, it is important to be sure that one method does not modify a value used in another. This also helps create more type safety, as it protected from unwanted concurrent modifications of path data.

Immutability means that each transformation made on a path does modify an existing object, but rather creates a new one with new data.

## Separation between paths and filesystem elements

The Atma Filesystem path types do not implement filesystem operations at all. The reason is that there are other use cases for paths than making explicit I/O operations, for example loading paths from configuration file and passing them to a third-party zip library. No I/O implementation is needed for such scenarios. Thus, the path values contain only the part that is strictly related to path format, not to the place on a filesystem where the paths point to (there are, however, plans for building a separate API for accessing a filesystem).

A consequence of this is simplified equality. In Atma Filesystem, paths are equal when they have the same type and contain exactly the same data. Real path equality is something that a filesystem is responsible for. For example, we have absolute, relative paths, but we also have paths with `..` in the middle, symbolic links and the ability to mount a directory as a new drive. Thus, without accessing a filesystem, it is impossible to determine whether the two paths really point to the same resource (this is a field of contribution if you have good ideas on how to resolve this matter).
