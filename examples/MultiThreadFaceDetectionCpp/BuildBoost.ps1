$current = Get-Location

$boost_version = "1.73.0"
$boost_version2 = "1_73_0"
$boost_source_dir = "boost_${boost_version2}"
$boost_base_url = "https://dl.bintray.com/boostorg/release/${boost_version}/source/boost_${boost_version2}"

if ($IsWindows)
{
    $boost_file = "boost_win.zip"
    $boost_dir = "boost_win"
    if (!(Test-Path $boost_file))
    {
        Invoke-WebRequest -Uri "${boost_base_url}.zip" -OutFile $boost_file
    }
    if (!(Test-Path $boost_dir))
    {
        Expand-Archive -Path $boost_file -DestinationPath $boost_dir
    }

    # build boost
    Set-Location "${boost_dir}/${boost_source_dir}"
    cmd.exe /c "bootstrap.bat"
    cmd.exe /c b2.exe install address-model=64 toolset=msvc-14.1 `
                              link=static runtime-link=static,shared `
                              --libdir="win64" `
                              --with-system `
                              --with-thread `
                              --with-filesystem
}
elseif ($IsLinux)
{
    $boost_file = "boost_linux.tar.gz"
    $boost_dir = "boost_linux"
    if (!(Test-Path $boost_file))
    {
        Invoke-WebRequest -Uri "${boost_base_url}.tar.gz" -OutFile $boost_file
    }
    if (!(Test-Path $boost_dir))
    {
        New-Item -ItemType Directory $boost_dir -Force > $Null
        tar -zxvf $boost_file -C $boost_dir
    }

    # build boost
    Set-Location "${boost_dir}/${boost_source_dir}"
    /bin/bash "bootstrap.sh"
    ./b2 install address-model=64 `
                 link=static runtime-link=static,shared `
                 --prefix="install" `
                 --libdir="linux" `
                 --with-system `
                 --with-thread `
                 --with-filesystem
}
elseif ($IsMacOS)
{
    $boost_file = "boost_osx.tar.gz"
    $boost_dir = "boost_osx"
    if (!(Test-Path $boost_file))
    {
        Invoke-WebRequest -Uri "${boost_base_url}.tar.gz" -OutFile $boost_file
    }
    if (!(Test-Path $boost_dir))
    {
        New-Item -ItemType Directory $boost_dir -Force > $Null
        tar -zxvf $boost_file -C $boost_dir
    }

    # build boost
    Set-Location "${boost_dir}/${boost_source_dir}"
    /bin/bash "bootstrap.sh"
    ./b2 install address-model=64 `
                 link=static runtime-link=static,shared `
                 --prefix="install" `
                 --libdir="osx" `
                 --with-system `
                 --with-thread `
                 --with-filesystem
}

Set-Location $current