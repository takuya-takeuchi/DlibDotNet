#!/bin/bash

VERSION=$1
PACKAGE=$2
OS=$3
OSVERSION=$4

DDNROOT=/opt/data/DlibDotNet
WORK=/opt/data/work
TESTDIR=${DDNROOT}/nuget/artifacts/test/${PACKAGE}.${VERSION}/${OS}/${OSVERSION}

mkdir -p ${WORK}
mkdir -p ${TESTDIR}

export DLIBDOTNET_VERSION=$VERSION

cp -Rf ${DDNROOT}/test/DlibDotNet.Native.Tests ${WORK}
cd ${WORK}/DlibDotNet.Native.Tests

# restore package from local nuget pacakge
# And drop stdout message
dotnet add package $PACKAGE -s ${DDNROOT}/nuget/ > /dev/null 2>&1

dotnet test -c Release -r ${TESTDIR} --logger trx
