#!/bin/bash

VERSION=$1
PACKAGE=$2
OS=$3
OSVERSION=$4

DDNROOT=/opt/data/DlibDotNet
NUGETDIR=${DDNROOT}/nuget

cd ${NUGETDIR}

pwsh ./TestPackage.ps1 $PACKAGE $VERSION $OS $OSVERSION