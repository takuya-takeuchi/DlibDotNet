$current = $PSScriptRoot
$files = Get-ChildItem -Include "*.cs" -Recurse -Path "${current}"
foreach ($file in $files)
{
    Write-Host "${file}" -ForegroundColor Green
    
    $directory = Split-Path "${file}" -Parent
    $tmp = Join-Path "${directory}" temp.cs

    "#if !LITE" | Out-File -FilePath "${tmp}" -Encoding UTF8
    Get-Content -Path "${file}" -Raw -Encoding UTF8 | Out-File -FilePath "${tmp}" -Append -Encoding UTF8
    Add-Content "${tmp}" "#endif"
    Move-Item "${tmp}" "${file}" -Force
}