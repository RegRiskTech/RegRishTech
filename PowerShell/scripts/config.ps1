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
$exclude =  @('Auto_Update.ps1','Auto_Update.vbs','main.ps1','main.vbs','config.vbs','docs','Testing') 

Get-ChildItem -Path $scriptDir -Exclude $exclude |Where-Object { $_.FullName -ne $PSCommandPath } |ForEach-Object {
    . $_.FullName
}

$inputFile = $loc[0]
$outputFile = $loc[1]
$licenceFile = $loc[2]
$attribute = $loc[3]

# Formatting Parameters
$formWidth = 975
$formHeight = 800

$labelWidth = 600
$labelHeight = 29
$label_x = 23
$label_y = 23

$space = 29
$gap = 8

$boxWidth = $labelWidth
$boxHeight = $labelHeight
$box_x = $label_x
$box_y = $label_y + $space

$buttonWidth = 150
$buttonHeight = 29
$button_x = $boxWidth + 50
$button_y = $box_y

$outputWidth = $formWidth - $label_x*3
$outputHeight = 250
$output_x = $label_x
$output_y = $formHeight - $outputHeight*1.5

$icon = New-Object system.drawing.icon ("$scriptDir\docs\logo-small-96x104.ico")

# Creating Form
$Form = New-Object System.Windows.Forms.Form
$Form.Size = New-Object System.Drawing.Size($formWidth,$formheight)
$Form.BackColor = "#F7FAFF"
$Form.Text = "RegRisk Technology Configuration"
$Form.Icon = $icon

# Folder Path Location Box
$folderLabel = New-Object System.Windows.Forms.Label
$folderLabel.Text = "Database File"
$folderLabel.Size = "$boxWidth,$boxHeight"
$folderLabel.Location = "$label_x,$label_y"
$folderLabel.Font = New-Object System.Drawing.Font("Consolas (True Type)",12,[System.Drawing.FontStyle]::Regular)
$folderPathTextBox = New-Object System.Windows.Forms.TextBox
$folderPathTextBox.Location = "$box_x,$box_y"
$folderPathTextBox.Size = "$boxWidth,$boxHeight"
$folderPathTextBox.BackColor = "#ffffff"
$folderPathTextBox.Font = New-Object System.Drawing.Font("Consolas (True Type)",12,[System.Drawing.FontStyle]::Regular)
$Form.Controls.Add($folderLabel)
$Form.Controls.Add($folderPathTextBox)

$folderpathTextBox.Text = $loc[0]

# Folder Select Button
$selectFolderButton = New-Object System.Windows.Forms.Button
$selectFolderButton.Size = "$buttonWidth,$buttonHeight"
$selectFolderButton.Text = "Select File"
$selectFolderButton.Font = New-Object System.Drawing.Font("Consolas (True Type)",10,[System.Drawing.FontStyle]::Regular)
$selectFolderButton.Location = "$button_x,$button_y"
$Form.Controls.Add($selectFolderButton)
$folderBrowser = New-Object System.Windows.Forms.OpenFileDialog
$selectFolderButton.Add_Click({
    $folderBrowser.ShowDialog()
    If($folderBrowser.FileNames -ne $null){
        $folderpathTextBox.Text = $folderBrowser.FileNames
        $loc[0] = $folderBrowser.FileNames
        $loc | Set-Content -Path $fileLocations -Force
    }
})

$folderPathTextBox.ReadOnly = $true

# Output Location Box
$y = $box_y + $boxHeight + $gap
$y_text = $y+$labelHeight
$outputLabel = New-Object System.Windows.Forms.Label
$outputLabel.Text = "Action Log File"
$outputLabel.Size = "$boxWidth,$boxHeight"
$outputLabel.Location = "$label_x,$y"
$outputLabel.Font = New-Object System.Drawing.Font("Consolas (True Type)",12,[System.Drawing.FontStyle]::Regular)
$outputPathTextBox = New-Object System.Windows.Forms.TextBox
$outputPathTextBox.Location = "$box_x,$y_text"
$outputPathTextBox.Size = "$boxWidth,$boxHeight"
$outputPathTextBox.BackColor = "#ffffff"
$outputPathTextBox.Font = New-Object System.Drawing.Font("Consolas (True Type)",12,[System.Drawing.FontStyle]::Regular)
$Form.Controls.Add($outputLabel)
$Form.Controls.Add($outputPathTextBox)

$outputPathTextBox.Text = $loc[1]

# output Select Button
$outputButton = New-Object System.Windows.Forms.Button
$outputButton.Size = "$buttonWidth,$buttonHeight"
$outputButton.Text = "Select File"
$outputButton.Location = "$button_x,$y_text"
$outputButton.Font = New-Object System.Drawing.Font("Consolas (True Type)",10,[System.Drawing.FontStyle]::Regular)
$Form.Controls.Add($outputButton)
$outputBrowser = New-Object System.Windows.Forms.OpenFileDialog
$outputButton.Add_Click({
    $outputBrowser.ShowDialog()
    If($outputBrowser.FileNames -ne $null){
        $outputPathTextBox.Text = $outputBrowser.FileNames
        $loc[1] = $outputBrowser.FileNames
        $loc | Set-Content -Path $fileLocations -Force
    } 
})

$outputPathTextBox.ReadOnly = $true

#################################################################################################
# Licence Path Location Box
$y = $y_text+ $boxHeight + $gap
$y_text = $y+$labelHeight
$licenceLabel = New-Object System.Windows.Forms.Label
$licenceLabel.Text = "Licence File"
$licenceLabel.Size = "$boxWidth,$boxHeight"
$licenceLabel.Location = "$label_x,$y"
$licenceLabel.Font = New-Object System.Drawing.Font("Consolas (True Type)",12,[System.Drawing.FontStyle]::Regular)
$licencePathTextBox = New-Object System.Windows.Forms.TextBox
$licencePathTextBox.Location = "$box_x,$y_text"
$licencePathTextBox.Size = "$boxWidth,$boxHeight"
$licencePathTextBox.BackColor = "#ffffff"
$licencePathTextBox.Font = New-Object System.Drawing.Font("Consolas (True Type)",12,[System.Drawing.FontStyle]::Regular)
$Form.Controls.Add($licenceLabel)
$Form.Controls.Add($licencePathTextBox)

$licencePathTextBox.Text = $loc[2]

# Licence Select Button
$selectlicenceButton = New-Object System.Windows.Forms.Button
$selectlicenceButton.Size = "$buttonWidth,$buttonHeight"
$selectlicenceButton.Text = "Select File"
$selectlicenceButton.Location = "$button_x,$y_text"
$selectlicenceButton.Font = New-Object System.Drawing.Font("Consolas (True Type)",10,[System.Drawing.FontStyle]::Regular)
$Form.Controls.Add($selectlicenceButton)
$licenceBrowser = New-Object System.Windows.Forms.OpenFileDialog
$selectLicenceButton.Add_Click({
    $licenceBrowser.ShowDialog()
    If($licenceBrowser.FileNames -ne $null){
        $licencepathTextBox.Text = $licenceBrowser.FileNames
        $loc[2] = $licenceBrowser.FileNames
        $loc | Set-Content -Path $fileLocations -Force
    } 
})
$licencePathTextBox.ReadOnly = $true

# Combo Box for AD Attribute Checker
$y = $y_text+ $boxHeight + $gap
$y_text = $y+$labelHeight
$ADLabel = New-Object System.Windows.Forms.Label
$ADLabel.Text = "Active Directory Attribute"
$ADLabel.Size = "$boxWidth,$boxHeight"
$ADLabel.Location = "$label_x,$y"
$ADLabel.Font = New-Object System.Drawing.Font("Consolas (True Type)",12,[System.Drawing.FontStyle]::Regular)
$AD_attributes = @("info","msDS-cloudExtensionAttribute1","msDS-cloudExtensionAttribute2")

$cboAD = New-Object System.Windows.Forms.ComboBox
$cboAD.DropDownStyle = [System.Windows.Forms.ComboBoxStyle]::DropDownList
$cboAD.Width = $boxWidth/2 + 23
$cboAD.Height = $boxHeight
$cboAD.location = new-object system.drawing.point($box_x,$y_text)
$cboAD.items.addrange($AD_attributes)
$cboAD.Font = New-Object System.Drawing.Font("Consolas (True Type)",12,[System.Drawing.FontStyle]::Regular)

$Form.Controls.Add($ADLabel)
$Form.Controls.Add($cboAD)

# Check AD Attribute Button
$ADbutton_x = $cboAD.Width + 50
$selectADButton = New-Object System.Windows.Forms.Button
$selectADButton.Size = "$buttonWidth,$buttonHeight"
$selectADButton.Text = "Check"
$selectADButton.Location = "$ADbutton_x,$y_text"
$selectADButton.Font = New-Object System.Drawing.Font("Consolas (True Type)",10,[System.Drawing.FontStyle]::Regular)
$Form.Controls.Add($selectADButton)

$selectADButton.Add_Click({
    If($cboAD.Text -ne ""){
        $ADcheck = AD_Check_Attribute $cboAD.Text
        $output.Text = " $ADcheck"

        $selectAttributeButton.Visible = $true
    }
    Else{
        $output.Text = "Please select a valid Attribute"  
    }
})

# Folder Select Button
$selectAttributeButton = New-Object System.Windows.Forms.Button
$selectAttributeButton.Size = "$buttonWidth,$buttonHeight"
$selectAttributeButton.Text = "Select Attribute"
$selectAttributeButton.Font = New-Object System.Drawing.Font("Consolas (True Type)",10,[System.Drawing.FontStyle]::Regular)
$selectAttributeButton.Location = "$button_x,$y_text"
$selectAttributeButton.Visible = $false
$Form.Controls.Add($selectAttributeButton)

$selectAttributeButton.Add_Click({
    If($cboAD.Text -ne ""){
        $ADcheck = $cboAD.Text
        $loc[3] = $ADcheck
        $loc | Set-Content -Path $fileLocations -Force
        $output.Text = "Active Directory Attribute has been updated to: $ADcheck"
        $selectAttributeButton.Visible = $false
    }
    Else{
        $output.Text = "Please select a valid Attribute"  
    }
})

# Combo Box for Users Check
$y = $y_text+ $boxHeight + $gap
$y_text = $y+$labelHeight
$UserLabel = New-Object System.Windows.Forms.Label
$UserLabel.Text = "Users"
$UserLabel.Size = "$boxWidth,$boxHeight"
$UserLabel.Location = "$label_x,$y"
$UserLabel.Font = New-Object System.Drawing.Font("Consolas (True Type)",12,[System.Drawing.FontStyle]::Regular)
$User_attributes = @("Check Users are in Active Directory","Show Users and Projects")
$cboUser = New-Object System.Windows.Forms.ComboBox
$cboUser.DropDownStyle = [System.Windows.Forms.ComboBoxStyle]::DropDownList
$cboUser.Width = $boxWidth/2 + 23
$cboUser.Height = $boxHeight
$cboUser.location = new-object system.drawing.point($box_x,$y_text)
$cboUser.items.addrange($User_attributes)
$cboUser.Font = New-Object System.Drawing.Font("Consolas (True Type)",12,[System.Drawing.FontStyle]::Regular)
$Form.Controls.Add($UserLabel)
$Form.Controls.Add($cboUser)

# Check Member Button
$UserButton_x = $cboUser.Width + 50
$selectMemberButton = New-Object System.Windows.Forms.Button
$selectMemberButton.Size = "$buttonWidth,$buttonHeight"
$selectMemberButton.Text = "Check"
$selectMemberButton.Location = "$UserButton_x, $y_text"
$selectMemberButton.Font = New-Object System.Drawing.Font("Consolas (True Type)",10,[System.Drawing.FontStyle]::Regular)
$Form.Controls.Add($selectMemberButton)

$selectMemberButton.Add_Click({
    $output.Text = ""
    $case = $cboUser.SelectedIndex
    If($case -eq 0 -or $case -eq 1){
        $case = $cboUser.SelectedIndex
   
        # Get Users from DB
        $query = "SELECT FirstName, LastName, Email, Department FROM Members"
        $dataUsers = Access_Query_Database $folderpathTextBox.Text $query
        $check = $true

        foreach($user in $dataUsers){
            $firstName = $user[0].ToString()
            $lastName = $user[1].ToString()
            $email = $user[2].ToString()
            $department = $user[3].ToString()
            $fullName = $firstName + " " + $lastName
    
            # Get User's Projects from DB
            $query = "SELECT ProjectID FROM ProjectMembers WHERE Member='$fullName'"
            $dataUserProjects = Access_Query_Database $inputfile $query
            $projects_shortcode = Concat_Projects_To_Shortcode $dataUserProjects $projectShortCodes
            $projectsList = Concat_Projects $dataUserProjects
            $dataProjects = ""

            If ($case -eq 0){
                If(AD_Check_User $firstName $lastName $email $department $projects_shortcode $projectsList $dataProjects $attribute $outputFile "Check"){
                    $check = $false
                }
            }
            If ($case -eq 1){
                $query= "SELECT ProjectName FROM Projects"
                $dataProjects = Access_Query_Database $inputfile $query

                $check = $false
                $projectShortCodes = Classifier_Check_Project_Code
                AD_Projects $firstName $lastName $email $department $projects $projectShortCodes $dataProjects
            }
        }
        If($check -eq $true){
            $updateAction = "All users exist in Active Directory"
            Add-Output -Message $updateAction
        }
    }
    Else{
        $output.Text = "Please select a valid entry"
    }
})

# Combo Box for Project
$y = $y_text+ $boxHeight + $gap
$y_text = $y+$labelHeight
$projectLabel = New-Object System.Windows.Forms.Label
$projectLabel.Text = "Projects"
$projectLabel.Size = "$boxWidth,$boxHeight"
$projectLabel.Location = "$label_x,$y"
$projectLabel.Font = New-Object System.Drawing.Font("Consolas (True Type)",12,[System.Drawing.FontStyle]::Regular)

$project_attributes = @("Check Active Projects are in Classifier")

$cboproject = New-Object System.Windows.Forms.ComboBox
$cboproject.DropDownStyle = [System.Windows.Forms.ComboBoxStyle]::DropDownList
$cboproject.Width = $boxWidth/2 + 23
$cboproject.Height = $boxHeight
$cboproject.location = new-object system.drawing.point($box_x,$y_text)
$cboproject.items.addrange($project_attributes)
$cboproject.Font = New-Object System.Drawing.Font("Consolas (True Type)",12,[System.Drawing.FontStyle]::Regular)
$Form.Controls.Add($projectLabel)
$Form.Controls.Add($cboproject)

# Check Member Button
$projectButton_x = $cboproject.Width + 50
$projectButton = New-Object System.Windows.Forms.Button
$projectButton.Size = "$buttonWidth,$buttonHeight"
$projectButton.Text = "Check"
$projectButton.Location = "$projectButton_x, $y_text"
$projectButton.Font = New-Object System.Drawing.Font("Consolas (True Type)",10,[System.Drawing.FontStyle]::Regular)
$Form.Controls.Add($projectButton)

$projectButton.Add_Click({
    $output.Text = ""
    if ($cboproject.Text -ne ""){
        # Get Projects and Shortcodes from Classifier
        $projectShortCodes = Classifier_Check_Project_Code "1"

        # Get Projects from DB
        $query= "SELECT ProjectName FROM Projects WHERE Closed = 0"
        $dataProjects = Access_Query_Database $folderpathTextBox.Text $query

        # Checks Projects are in Classifier, adds in any new Projects
        foreach($project in $dataProjects){
            Classifier_Check_Project $project[0].ToString() $outputFile $projectShortCodes "Check"
        }
    }
    Else{
        $output.Text = "Please select a valid entry"    
    }
})

# Cancel Button
$cancel_x = $buttonWidth*2 + $label_x*3
$cancelButton = New-Object System.Windows.Forms.Button
$cancelButton.Text = "Cancel"
$cancelButton.Location = "$button_x, $y_text"
$cancelButton.Size = "$buttonWidth,$buttonHeight"
$cancelButton.Font = New-Object System.Drawing.Font("Consolas (True Type)",10,[System.Drawing.FontStyle]::Regular)
$Form.CancelButton = $cancelButton
$Form.Controls.Add($cancelButton)

# Output Box
$output = New-Object System.Windows.Forms.TextBox
$output.Multiline = $True;
$output.Scrollbars = "Vertical" 
$y_loc = $formHeight - 400
$output.Location = "$output_x,$output_y"
$output.Size = "$outputWidth,$outputHeight"
$output.Font = New-Object System.Drawing.Font("Consolas (True Type)",12,[System.Drawing.FontStyle]::Regular)
$output.ForeColor = "#ffffff"
$output.BackColor = "#1e1d54"
$output.ReadOnly = $True
$Form.Controls.Add($output)

$Form.Controls | Where{$_.GetType().Name -eq "Button"} | ForEach{$_.BackColor = '#E3ECFF'; $_.FlatStyle = "Flat"
$_.FlatAppearance.BorderColor = "#CACDFF";
$_.FlatAppearance.BorderSize = 1;}

#$Form.Controls | Where{$_.GetType().Name -eq "ComboBox"} | ForEach{$_.BackColor = '#EEEEEE'; $_.FlatStyle = "Flat"}

$Form.ShowDialog()