param (
    [string]$TargetName,
    [string]$TargetDir
)

Write-Output "TargetName: $TargetName"
Write-Output "TargetDir: $TargetDir"

$deployDir = "$Env:USERPROFILE\Documents\Klei\OxygenNotIncluded\mods\Dev\$TargetName\"
Write-Output "deployDir: $deployDir"

$binPath = Join-Path $TargetDir "$TargetName.dll" 
Write-Output "binPath: $binpath"
$modYamlPath = Join-Path $TargetDir "mod.yaml"
Write-Output "modYamlPath: $modYamlPath"
$modInfoYamlPath = Join-Path $TargetDir  "mod_info.yaml"
Write-Output "modInfoYamlPath: $modInfoYamlPath"

Copy-Item $binPath $deployDir -Force
Copy-Item $modYamlPath $deployDir -Force
Copy-Item $modInfoYamlPath $deployDir -Force

