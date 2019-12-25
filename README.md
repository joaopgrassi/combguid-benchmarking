# combguid-benchmarking
A simple app to benchmark the index fragmentation of a custom Comb Guid implementation

## Background

In case you are not familiar with the premise of the issue, using `Guid` as primary keys for database (here we're talking about SQL) can cause problems in your database. The decision to use it must not be made from thin air. There are many articles and debates online about it, but this one is quite good: [GUIDs as PRIMARY KEYs and/or the clustering key](https://www.sqlskills.com/blogs/kimberly/guids-as-primary-keys-andor-the-clustering-key/)

## Premise

This experimentation is completely based on this article: [Benchmarking Index Fragmentation With Popular Sequential GUID Algorithms](http://microsoftprogrammers.jebarson.com/benchmarking-index-fragmentation-with-popular-sequential-guid-algorithms/).

The article compares the index fragmentation of a custom Comb Guid implementation taken from NHibernate's codebase (https://stackoverflow.com/a/25472825/2689390). This implementation is quite popular online and the author wanted to see how it performs regarding SQL index fragmentation. I dug into the code of several databases drivers for .NET and many of them seem to be using the same algorithm.

## Results (Localdb for now)

|                                     | C# Guid | SQL NewSequentialId (Default) | UuidCreateSequential | Custom Comb Guid |
|-------------------------------------|---------|-------------------------------|----------------------|------------------|
| Fragmentation % Clustered Index     | 99.3    | 0.8                           | 11.45                | 52.28            |
| Fragmentation % Non-Clustered Index | 99.29   | 1.77                          | 11.25                | 50.19            |

The custom comb guid implementation here is a bit better than the one in the article but still causes fragmentation.


  

