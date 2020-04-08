[reflection.assembly]::LoadWithPartialName("System.Windows.Forms") | Out-Null
$output = New-Object System.Windows.Forms.TextBox

$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition
$scriptDir = Split-Path $script:MyInvocation.MyCommand.Path
$fileLocations = "$scriptDir\docs\fileLocations.txt"
$loc = Get-Content -path $fileLocations
$exclude =  @('Auto_Update.vbs','config.ps1','config.vbs','main.ps1','main.vbs','docs','Testing') 

Get-ChildItem -Path $scriptDir -Exclude $exclude |Where-Object { $_.FullName -ne $PSCommandPath } |ForEach-Object {
    . $_.FullName
}

$ScriptDir = Split-Path $script:MyInvocation.MyCommand.Path
$fileLocations = "$ScriptDir\docs\fileLocations.txt"

$loc = Get-Content -path $fileLocations

$inputFile = $loc[0]
$outputFile = $loc[1]
$licenceFile = $loc[2]
$attribute = $loc[3]

Set-SessionLicence –LicenceFile $licencefile

Update $inputFile $outputFile $attribute

#$updateAction = "Completed Update..."
#Update_Action_Log $outputFile $updateAction