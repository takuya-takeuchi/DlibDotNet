function run-command($command)
{
    Write-Host "${command}" -ForegroundColor Green
    Invoke-Expression "${command}"
}

function build-container($Image, $Qemu)
{
    if ($IsLinux)
    {
        $binary = "/usr/bin/qemu-${Qemu}-static"
        if (!(Test-Path $binary))
        {
            Write-Host "'qemu-user-static' is not installed" -ForegroundColor Red
            exit -1
        }
    }

    $containerid = ""

    # Run container and it fails, then get container id
    run-command "docker run -t ${Image}"
    Start-Sleep 10
    $containerid = Invoke-Expression "docker ps -a -l -q --filter ""exited=1"" --filter ""ancestor=${Image}"""

    if ($IsLinux)
    {
        run-command "docker cp ${binary} ${containerid}:${binary}"
    }

    run-command "docker commit ${containerid} ${Image}_amd64"
    run-command "docker rm ${containerid}"
}

if ($IsMac)
{
    Write-Host "This system is not Linux or Windows" -ForegroundColor Red
    exit -1
}

# build-container "arm64v8/ubuntu:16.04" "aarch64"
# build-container "arm32v7/ubuntu:16.04" "arm"