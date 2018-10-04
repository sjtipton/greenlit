#!/bin/bash

set -e
run_cmd="dotnet greenlit.dll"

# Install .NET Core on Ubuntu
# Update the packages, install GNU packages, register Microsoft keys/feeds, re-update the packages, and install .NET Core
# https://dev.to/carlos487/installing-dotnet-core-in-ubuntu-1804-7lp

until apt-get update; do
>&2 echo "Updating apt-get packages"
sleep 1
done

until apt-get -y install gnupg gnupg1 gnupg2; do
>&2 echo "Installing GNU packages"
sleep 1
done

until apt-key adv --keyserver packages.microsoft.com --recv-keys EB3E94ADBE1229CF; do
>&2 echo "Installing 1st set of Microsoft keys"
sleep 1
done

until apt-key adv --keyserver packages.microsoft.com --recv-keys 52E16F86FEE04B979B07E28DB02C46DF417A0893; do
>&2 echo "Installing 2nd set of Microsoft keys"
sleep 1
done

sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-bionic-prod bionic main" > /etc/apt/sources.list.d/dotnetdev.list';

until apt-get update; do
>&2 echo "Updating apt-get packages to recycle the Microsoft keys"
sleep 1
done

until apt-get -y install dotnet-sdk-2.1; do
>&2 echo "Installing .NET Core SDK for Ubuntu Bionic"
sleep 1
done

# Using .NET Core, Update SQL Server
until dotnet ef database update; do
>&2 echo "SQL Server is starting up"
sleep 1
done

>&2 echo "SQL Server is up - executing command"

ls -al;

echo "Preparing to run Greenlit...";

exec $run_cmd