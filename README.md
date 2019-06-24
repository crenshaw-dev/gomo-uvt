# gomo-uvt

gomo-uvt is a utility to calculate Unique View Time, a measure of the amount of a video 
that has been viewed at least once. So if a viewer watches the first ten minutes of a video, 
skips to watch the last three minutes, and then re-watches the first minute, the UVT is 13 
minutes.

This utility is written in .NET Core 2.1 with unit tests in MSTest.

Instructions to run the utility and its unit tests are below. The same steps apply for Linux, Max, and
Windows. Just follow the steps to install .NET Core SDK for your operating system.

## Steps to calculate UVT

 1. [Download](https://dotnet.microsoft.com/download/dotnet-core/2.1) and install the .NET Core 2.1 SDK
 2. [Download the source](https://github.com/mac9416/gomo-uvt/archive/master.zip) and extract the .zip file
 3. Open a command prompt and navigate to the directory containing gomo-uvt.sln
 4. Run `dotnet run` to execute gomo-uvt

By default, gomo-uvt will return `0`. Add space-delimited `{start}-{end}` pairs to specify 
start and end view times in milliseconds. Order of the pairs is not significant.

For example:

```
dotnet run 954-1960 0-150
```

The above command should report a UVT of 1156.

## Steps to run unit tests

 1. Follow steps 1-2 above to set up the development environment
 2. Open a command prompt and navigate to the directory containing UniqueViewTimeTest.csproj
 3. Run `dotnet test`
