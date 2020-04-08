<#*********************************************************************************
Company: RegRisk Technology
Author: Jamie Baldwin <jamie.baldwin@regrisktech.com>
Date: 28/01/20
Copyright (C) 2020 RegRisk Technology
*********************************************************************************#>

Clear-Host
[reflection.assembly]::LoadWithPartialName("System.Windows.Forms") | Out-Null

$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition
$scriptDir = Split-Path $script:MyInvocation.MyCommand.Path
$fileLocations = "$scriptDir\docs\fileLocations.txt"
$loc = Get-Content -path $fileLocations
$exclude =  @('config.ps1','config.vbs','Auto_Update.ps1','Auto_Update.vbs','main.vbs','docs') 

Get-ChildItem -Path $scriptDir -Exclude $exclude | Where-Object { $_.FullName -ne $PSCommandPath } |ForEach-Object {
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

# Formatting Parameters
$formWidth = 800
$formHeight = 400

$label_x = 23
$label_y = 23
$labelHeight = 23

$outputWidth = $formWidth - $label_x*3
$outputHeight = 200
$output_x = $label_x
$output_y = $label_y

$buttonWidth = 150
$buttonHeight = 29
$button_x = 50
$button_y = $outputHeight + $labelHeight*3
$button_gap = 50

$icon = New-Object system.drawing.icon ("$ScriptDir\docs\logo-small-96x104.ico")

# Creating Form
$Form = New-Object System.Windows.Forms.Form
$Form.Size = New-Object System.Drawing.Size($formWidth,$formheight)
$Form.BackColor = "#F7FAFF"
$Form.Text = "RegRisk Technology"
$Form.FormBorderStyle = 'FixedDialog'
$Form.Icon = $icon

# Output Box
$output = New-Object System.Windows.Forms.RichTextBox
$output.Multiline = $True;
$output.Scrollbars = "Vertical" 
$output.Location = "$output_x,$output_y"
$output.Size = "$outputWidth,$outputHeight"
$output.Font = New-Object System.Drawing.Font("Consolas (True Type)",12,[System.Drawing.FontStyle]::Regular)
$output.ForeColor = "#ffffff"
$output.BackColor = "#1e1d54"
$output.ReadOnly = $True
$Form.Controls.Add($output)

# Update Button
$update_x = $button_x + $button_gap
$updateButton = New-Object System.Windows.Forms.Button
$updateButton.Size = "$buttonWidth,$buttonHeight"
$updateButton.Text = "Update"
$updateButton.Location = "$update_x, $button_y"
$updateButton.Font = New-Object System.Drawing.Font("Consolas (True Type)",10,[System.Drawing.FontStyle]::Regular)
$Form.Controls.Add($updateButton)

$updateButton.Add_Click({
    Update $inputFile $outputFile $attribute
})

# Test Button
$test_x = $button_x + $buttonWidth + $button_gap*2
$testButton = New-Object System.Windows.Forms.Button
$testButton.Size = "$buttonWidth,$buttonHeight"
$testButton.Text = "Test"
$testButton.Location = "$test_x, $button_y"
$testButton.Font = New-Object System.Drawing.Font("Consolas (True Type)",10,[System.Drawing.FontStyle]::Regular)
$Form.Controls.Add($testButton)

$test = "TEST" 

$testButton.Add_Click({
    Update $inputFile $outputFile $attribute $test
})

# Cancel Button
$cancel_x = $button_x + $buttonWidth*2 + $button_gap*3
$cancelButton = New-Object System.Windows.Forms.Button
$cancelButton.Text = "Cancel"
$cancelButton.Location = "$cancel_x, $button_y"
$cancelButton.Size = "$buttonWidth,$buttonHeight"
$cancelButton.Font = New-Object System.Drawing.Font("Consolas (True Type)",10,[System.Drawing.FontStyle]::Regular)
$Form.CancelButton = $cancelButton
$Form.Controls.Add($cancelButton)

$Form.Controls | Where{$_.GetType().Name -eq "Button"} | ForEach{$_.BackColor = '#E3ECFF'; $_.FlatStyle = "Flat"
$_.FlatAppearance.BorderColor = "#CACDFF";
$_.FlatAppearance.BorderSize = 1;}

$Form.ShowDialog()