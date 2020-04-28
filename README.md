# combguid-benchmarking
A simple app to benchmark the index fragmentation of a custom Comb Guid implementation

## Background

In case you are not familiar with the premise of the issue, using `Guid` as primary keys in your database (here we're talking about SQL) can cause many issues in your database. A quick DuckDuckGo search can show you how bad it can get.

## Premise

This experimentation is based on this article: [Benchmarking Index Fragmentation With Popular Sequential GUID Algorithms](http://microsoftprogrammers.jebarson.com/benchmarking-index-fragmentation-with-popular-sequential-guid-algorithms/).

The article compares the index fragmentation of a custom Comb Guid implementation taken from NHibernate's codebase (https://stackoverflow.com/a/25472825/2689390). This implementation is quite popular online and the author wanted to see how it performs compared to other methods of Guid generation regarding SQL index fragmentation. I dug into the code of several database drivers for .NET and many of them seem to be using the same algorithm. The article shows that the NHibernate's Guid generation results are very bad reaching 91.66% index fragmentation for clustered indexes.

The problem with the article is that the GUIDs are generated very fast, thus creating the same Date/Time portion for all the Ids, resulting in the same problem where SQL cannot order things, thus producing again high fragmentation. See this discussion for more details: https://github.com/richardtallent/RT.Comb/issues/5

This repo contain the same test as in the original article but two Comb Guid implementations:

- One simple version which uses Span<T>
- [RT.Comb](https://github.com/richardtallent/RT.Comb)  
  
The goal is to run the same type of benchmark as in the article so we can find out how good or bad it performs.

The RT.Comb implementation used uses a feature where the time portion will never repeat. The library keeps some state about the last generated GUID and in case one was generated too fast and the date/time will be the same, it adds 4ms making it "unique" again.

The custom implementation of the Comb Guid present here does not account for this at the moment.

## Results

### Environment:

- SQL Server (mcr.microsoft.com/mssql/server) on a local Linux container
- CPU: Intel Core i7-8750H 2.20GHz
- RAM: 16GB
- Sansung 970 EVO NVMe

|                                     | Identity | C# Guid | Custom Comb Guid | RT.Comb (Utc No repeat) |
|-------------------------------------|---------|-------------------------------|----------------------|------------------|
|Avg Fragmentation % Clustered Index|0,436|99,313|6,756|4,473|


### Environment:

- SQL Server LocalDB
- CPU: Intel Core i7-8750H 2.20GHz
- RAM: 16GB
- Sansung 970 EVO NVMe

|                                     | Identity | C# Guid | Custom Comb Guid | RT.Comb (Utc No repeat) |
|-------------------------------------|---------|-------------------------------|----------------------|------------------|
|Avg Fragmentation % Clustered Index|6,767|99,266|32,419|3,459|

  
For some reason the Custom Comb Guid when on SQL LocalDB performs worse. 🤷‍♂️
