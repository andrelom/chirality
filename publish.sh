#!/bin/sh

# Publishes to an .NET self-contained single executable.
# Avaliable RIDs: https://docs.microsoft.com/dotnet/core/rid-catalog#using-rids

export NAME=chirality
export PROJECT=Chirality.Agent
export FRAMEWORK=net5.0

export RID_UNX=linux-x64
export RID_OSX=osx-x64
export RID_WIN=win-x64

rm -rf ./release

mkdir release

mkdir release/$RID_UNX
mkdir release/$RID_OSX
mkdir release/$RID_WIN

dotnet publish ./src/$PROJECT -r $RID_UNX -c Release --self-contained true /p:PublishSingleFile=true /p:PublishTrimmed=true /p:IncludeNativeLibrariesForSelfExtract=true
dotnet publish ./src/$PROJECT -r $RID_OSX -c Release --self-contained true /p:PublishSingleFile=true /p:PublishTrimmed=true /p:IncludeNativeLibrariesForSelfExtract=true
dotnet publish ./src/$PROJECT -r $RID_WIN -c Release --self-contained true /p:PublishSingleFile=true /p:PublishTrimmed=true /p:IncludeNativeLibrariesForSelfExtract=true

cp ./src/$PROJECT/bin/Release/$FRAMEWORK/$RID_UNX/publish/$PROJECT     ./release/$RID_UNX/$NAME
cp ./src/$PROJECT/bin/Release/$FRAMEWORK/$RID_OSX/publish/$PROJECT     ./release/$RID_OSX/$NAME
cp ./src/$PROJECT/bin/Release/$FRAMEWORK/$RID_WIN/publish/$PROJECT.exe ./release/$RID_WIN/$NAME.exe
