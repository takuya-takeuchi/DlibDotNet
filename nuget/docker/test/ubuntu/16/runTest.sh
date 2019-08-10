#!/bin/bash

VERSION=$1
PACKAGE=$2
PLATFORMTARGET=$3
RID=$4

# create non-root user
NON_ROOT_USER=user
USER_ID=${LOCAL_UID:-9001}
GROUP_ID=${LOCAL_GID:-9001}
echo "Starting with UID : $USER_ID, GID: $GROUP_ID"
useradd -u $USER_ID -o -m $NON_ROOT_USER
groupmod -g $GROUP_ID $NON_ROOT_USER
export HOME=/home/$NON_ROOT_USER

DDNROOT=/opt/data/DlibDotNet
NUGETDIR=${DDNROOT}/nuget

cd ${NUGETDIR}

exec /usr/sbin/gosu $NON_ROOT_USER pwsh ./TestPackage.ps1 $PACKAGE $VERSION $PLATFORMTARGET $RID
