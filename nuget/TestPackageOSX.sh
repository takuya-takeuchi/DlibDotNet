#!/bin/bash

VERSION=$1
OS='osx'
OSVERSION=$4

CURDIR=${pwd}
DDNROOT=`dirname $(pwd)`
WORK=${DDNROOT}/work
export DLIBDOTNET_VERSION=$VERSION

packages=(
    "DlibDotNet"
    "DlibDotNet.MKL"
    #"cuda"
)

for package in "${packages[@]}" ; do
    PACKAGE=$package    
    TESTDIR=${DDNROOT}/nuget/artifacts/test/${PACKAGE}.${VERSION}/${OS}
    
    mkdir -p ${WORK}
    mkdir -p ${TESTDIR}    
    
    cp -Rf ${DDNROOT}/test/DlibDotNet.Native.Tests ${WORK}
    cd ${WORK}/DlibDotNet.Native.Tests
    
    # restore package from local nuget pacakge
    # And drop stdout message
    dotnet add package $PACKAGE -v $VERSION --source ${DDNROOT}/nuget/ > /dev/null 2>&1
    
    dotnet test -c Release -r ${TESTDIR} --logger trx

    # move to current
    cd $CURDIR

    # to make sure, delete
    if [ -e ${WORK}] then;
       rm -Rf ${WORK}
    fi
done