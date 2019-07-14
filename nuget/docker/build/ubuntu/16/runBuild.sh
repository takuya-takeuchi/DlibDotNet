#!/bin/bash

TARGET=$1
ARCH=$2
DDNROOT=/opt/data/DlibDotNet

if [ $# -eq 3 ]; then
   OPTION=$3
fi

CONFIG=Release

# create non-root user
NON_ROOT_USER=user
USER_ID=${LOCAL_UID:-9001}
GROUP_ID=${LOCAL_GID:-9001}
echo "Starting with UID : $USER_ID, GID: $GROUP_ID"
useradd -u $USER_ID -o -m $NON_ROOT_USER
groupmod -g $GROUP_ID $NON_ROOT_USER
export HOME=/home/$NON_ROOT_USER

cd ${DDNROOT}/src/DlibDotNet.Native
exec /usr/sbin/gosu $NON_ROOT_USER pwsh Build.ps1 ${CONFIG} ${TARGET} ${ARCH} ${OPTION}

cd ${DDNROOT}/src/DlibDotNet.Native.Dnn
exec /usr/sbin/gosu $NON_ROOT_USER pwsh Build.ps1 ${CONFIG} ${TARGET} ${ARCH} ${OPTION}