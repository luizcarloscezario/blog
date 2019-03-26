#!/bin/sh

dotnet clean
dotnet build /p:DebugType=Full
dotnet minicover instrument --workdir ../ --assemblies blog.tests/**/bin/**/*.dll --sources blog/**/*.cs --exclude-sources blog/Migrations/**/*.cs --exclude-sources blog/*.cs --exclude-sources blog\Domain\BlogDbContext.cs

dotnet minicover reset --workdir ../

dotnet test --no-build
dotnet minicover uninstrument --workdir ../
dotnet minicover report --workdir ../ --threshold 70