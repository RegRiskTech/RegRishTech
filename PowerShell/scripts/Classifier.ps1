<#*********************************************************************************
Company: RegRisk Technology
Author: Jamie Baldwin <jamie.baldwin@regrisktech.com>
Date: 03/02/20
Copyright (C) 2020 RegRisk Technology
*********************************************************************************#>

Function Classifier_Add_Project_And_Conditions($project, $attribute, $shortcode){
    $colour = "#{0:X6}" -f (Get-Random -Maximum 0xFFFFFF)

    # "Projects" may need dynamically changing ************************************************************************************
    # Step 1: Creating selector value
    New-SelectorValue -SelectorName Projects -ValueName $project -Colour $colour -Alternate1 $shortcode | Out-Null

    # Step 2: Add Content Expression
    New-ContentExpression -Name $project -ExpressionString "Project w/3 $project" | Out-Null

    # Step 3: Add User Message
    New-UserMessage -Name "REF - $project" -MessageTitle "Content references Project $project." -MessageText "Content references Project $project." | Out-Null

    # Step 4: Add "ADU & DOC content" Condition
    $condition = New-Condition -Name "$project ADU and DOC content"

    Add-ConditionType -Condition $condition -ConditionTypeName 'Active Directory Attribute Values of User'
    Add-ConditionEntry -Condition $condition -ConditionTypeName 'Active Directory Attribute Values of User' -Name "$attribute" -Check Contains -Value $shortcode

    Add-ConditionType -Condition $condition -ConditionTypeName 'Document Content'
    $exp = Get-ContentExpression -Name $project
    Add-ConditionEntry -Condition $condition -ConditionTypeName 'Document Content' -Check Contains -Value $exp.Id

    Set-Condition -Condition $condition

    # Step 5: Add "ADU & MSG content" Condition
    $condition = New-Condition -Name "$project ADU and MSG content"

    Add-ConditionType -Condition $condition -ConditionTypeName 'Active Directory Attribute Values of User'
    Add-ConditionEntry -Condition $condition -ConditionTypeName 'Active Directory Attribute Values of User' -Name "$attribute" -Check Contains -Value $shortcode

    Add-ConditionType -Condition $condition -ConditionTypeName 'Message Content'
    $exp = Get-ContentExpression -Name $project
    Add-ConditionEntry -Condition $condition -ConditionTypeName 'Message Content' -Check Contains -Value $exp.Id

    Set-Condition -Condition $condition

    # Step 6: Add "CLR - PROJECT Internal" Condition
    $condition = New-Condition -Name "CLR - $project Internal"

    Add-ConditionType -Condition $condition -ConditionTypeName 'Dynamic Clearance User Properties'
    Add-ConditionEntry -Condition $condition -ConditionTypeName 'Dynamic Clearance User Properties' -Name "$attribute" -Check Contains -Value $shortcode

    Set-Condition -Condition $condition

    # Add "CLR - PROJECT External" Condition
    $condition = New-Condition -Name "CLR - $project External"

    Add-ConditionType -Condition $condition -ConditionTypeName 'Dynamic Clearance User Properties'

    Set-Condition -Condition $condition

    #************************************************************************************************************************************************

    # Step 7: Select Policy Selectory Value as True
    Set-PolicySelectorValue -PolicyName 'Commercial Policy' -SelectorName Projects -ValueName $project -Selected $True

    # Step 8: Add Policy Connected Selector Value
    ####################### NEED TO MAKE SURE CONNECT SELECTORS IS TURNED ON SOMEHOW *******************************************************************
    Add-PolicyConnectedSelectorValue -PolicyName 'Commercial Policy' -Path \\Projects -SelectorName Projects -ValueName $project

    # Step 9: Add Policy Suggested Classification
    $SelectorsOn = New-Object System.Collections.Generic.List[ClassifierAdminModels.Libraries.Selectors.Values.SelectorValueBase]
        $SelectorsOn.Add((Get-SelectorValue -SelectorName Projects -ValueName $project))

    $Conditions = New-Object System.Collections.Generic.List[ClassifierAdminModels.Libraries.Condition.Condition]
        $Conditions.Add((Get-Condition -Name "$project ADU and DOC content"))
        $Conditions.Add((Get-Condition -Name "$project ADU and MSG content"))

    $udm = Get-UserMessage -Name "REF - $project"

    Add-PolicySuggestedClassification -PolicyName 'Commercial Policy' -SuggestedName $project -SelectorValuesToSet $SelectorsOn -CannotApplyUserMessage $udm -FulfilledConditions $Conditions -ApplyWhenPossible $true

    # Step 10: Add Dynamic Clearance for Internal and External
    $condition = Get-Condition "CLR - $project Internal"
    $clearance = New-SelectorClearance -Selector Projects -Value $project -Enabled $True
    Set-SelectorClearance -Clearance $clearance -Selector Projects -Value $project -Enabled $True

    $dynamicClearance = New-DynamicClearance -Name "$project Internal" -Clearance $clearance -Conditions $condition

    $condition = Get-Condition "CLR - $project External"
    $clearance = New-SelectorClearance -Selector Projects -Value $project -Enabled $True
    Set-SelectorClearance -Clearance $clearance -Selector Projects -Value $project -Enabled $True

    $dynamicClearance = New-DynamicClearance -Name "$project External" -Clearance $clearance -Conditions $condition
}

Function Classifier_Remove_Project_And_Conditions($project, $removedProjectUsers){
    # Step 10: Remove Dynamic Clearance
    Remove-DynamicClearance -Name "$project Internal" -Confirm:$false
    Remove-DynamicClearance -Name "$project External" -Confirm:$false

    # Step 9: Remove Policy Suggested Classification
    Remove-PolicySuggestedClassification -PolicyName 'Commercial Policy' -SuggestedName $project -Confirm:$false

    # Step 8: Remove Policy Connected Selector Value
    Remove-PolicyConnectedSelectorValue -PolicyName 'Commercial Policy' -Path \\Projects\$project -Confirm:$false

    # Step 7: Remove Policy Selector Value
    Set-PolicySelectorValue -PolicyName 'Commercial Policy' -SelectorName Projects -ValueName $project -Selected $False

    # Step 6 & 5 & 4: Remove Conditions
    $con = Get-Condition -Name "CLR - $project External"
    $numExternal = $con.ConditionTypes[0].ConditionEntries.Count - 1
    
    $removedProjectUsers.Add($project)       
    for($i=0; $i -le $numExternal; $i++){
        $val = $con.ConditionTypes[0].ConditionEntries[$i].Value
        $projectUser = "$project$val"
        $removedProjectUsers.Add($projectUser)
    }

    Remove-Condition -Name "CLR - $project External" -Confirm:$false
    Remove-Condition -Name "CLR - $project Internal" -Confirm:$false
    Remove-Condition -Name "$project ADU and MSG content" -Confirm:$false
    Remove-Condition -Name "$project ADU and DOC content" -Confirm:$false

    # Step 3: Remove User Message
    Remove-UserMessage -Name "REF - $project" -Confirm:$false

    # Step 2: Remove Content Expression
    Remove-ContentExpression -Name $project -Confirm:$false

    # Step 1: Remove Selector Value
    Remove-SelectorValue -SelectorName Projects -ValueName $project -Confirm:$false

    return $removedProjectUsers
}

Function Classifier_Add_Project($project, $outputFile, $projectShortCodes, [Parameter(Mandatory = $false)]$test){ 
        $shortcode = Shortcode $project $projectShortCodes 

        $projectShortCodes["$project"] = $shortcode
        $projectShortCodes["$shortcode"] = $project
        
        If($test -ne "TEST"){
            Classifier_Add_Project_And_Conditions $project $attribute $shortcode

            $updateAction = "Project $project has been created in Classifier Administration"
            Update_Action_Log $outputFile "" $updateAction
            Add-Output -Message $updateAction
        }
        Else{
            $updateAction = "Project $project will be created in Classifier Administration"
            Add-Output -Message $updateAction
        }
}

Function Classifier_Check_Project($project, $outputFile, $projectShortCodes, [Parameter(Mandatory = $false)]$check, [Parameter(Mandatory = $false)]$test){
    $project_exists = $false
    $selectors = Get-Selector | Select -ExpandProperty SelectorName

    foreach ($selector in $selectors){
        $valueSelectors = Get-SelectorValue -SelectorName $selector | Select -ExpandProperty ValueName
        foreach ($value in $valueSelectors){
            If($project -eq $value){
                $project_exists = $true
            }
        } 
    }

    If($check -ne "Check"){
        If ($project_exists -eq $true){
            #Write-Output "Project $project already exists`r`n"
        }
        Else{
            If($test -ne "TEST"){
                Classifier_Add_Project $project $outputFile $projectShortCodes
            }
            Else{
                Classifier_Add_Project $project $outputFile $projectShortCodes $test
            }
        }
    }
    Else{
        If ($project_exists -eq $true){
            $updateAction = "Project $project exists in Classifier Administration"
            Add-Output -Message $updateAction
        }
        Else{
            $updateAction = "Project $project does not exist in Classifier Administration"
            Add-Output -Message $updateAction
        }
    }
}

# CURRENTLY JUST GETTING PROJECTS VALUES *****************************************************************************************
Function Classifier_Check_Project_Code(){
    $projectCodes = @{}
    $valueSelectors = Get-SelectorValue -SelectorName Projects | Select -ExpandProperty ValueName
    foreach($value in $valueSelectors){
        $code = Get-SelectorValue -SelectorName Projects -ValueName $value | Select -ExpandProperty Alternate1

        $projectCodes["$code"] = "$value"
        $projectCodes["$value"] = "$code"
    }
    return $projectCodes
}

Function Classifier_Remove_Project($project, $removedProjectUsers, $outputFile, [Parameter(Mandatory = $false)]$test){
    $selectors = Get-Selector | Select -ExpandProperty SelectorName

    foreach ($selector in $selectors){
        $valueSelectors = Get-SelectorValue -SelectorName $selector | Select -ExpandProperty ValueName
        foreach ($value in $valueSelectors){
            If($project -eq $value){
                If($test -ne "TEST"){
                    Classifier_Remove_Project_And_Conditions $project $removedProjectUsers

                    $updateAction = "Project $project has been removed from Classifier Administration"
                    Update_Action_Log $outputFile "" $updateAction
                    Add-Output -Message $updateAction
                }
                Else{
                    $updateAction = "Project $project will be removed from Classifier Administration"
                    Add-Output -Message $updateAction
                }
            }
        } 
    }
}

Function Classifier_Remove_Deleted_Project($projects, $removedProjectUsers, $outputFile, [Parameter(Mandatory = $false)]$test){
    #$selectors = Get-Selector | Select -ExpandProperty SelectorName
    $selectors = Get-Selector -Name Projects

    foreach ($selector in $selectors){
        $valueSelectors = Get-SelectorValue -SelectorName $selector | Select -ExpandProperty ValueName
        foreach ($value in $valueSelectors){
            $exist = $false
            foreach($row in $projects){

                $project = $row[0].ToString()
                If($value -eq $project){
                    $exist = $true
                }
            }
            If($exist -eq $false){
                If($test -ne "TEST"){
                    Classifier_Remove_Project_And_Conditions $value $removedProjectUsers

                    $updateAction = "Project $value does not exist in Database. It has been removed from Classifier Administration"
                    Update_Action_Log $outputFile "" $updateAction
                    Add-Output -Message $updateAction
                }
                Else{
                    $updateAction = "Project $value does not exist in Database. It will be removed from Classifier Administration"
                    Add-Output -Message $updateAction
                }
            }
        } 
    }
}

Function Classifier_Check_External_Conditions($email, $dataProjects){
    $conditions = Get-Condition | Select -ExpandProperty Name
    $projectsClassifier = ""

    foreach($condition in $conditions){ 
        If($condition.Substring($condition.Length - 8) -eq "External"){
            $project = $condition.split(" ")[2]

            $con = Get-Condition -Name $condition
            $numExternal = $con.ConditionTypes[0].ConditionEntries.Count - 1
           
            for($i=0; $i -le $numExternal; $i++){
                $val = $con.ConditionTypes[0].ConditionEntries[$i].Value
                If($email -eq $val){
                    $projectsClassifier += "$project,"
                }
            }
        }
    }
    If($projectsClassifier -ne $null){
        return $projectsClassifier.Trim(",")
    }
}

# Add External User to a Project
Function Classifier_Add_External_User($email, $project){
    $condition = Get-Condition -Name "CLR - $project External"
    $entryCombiningOR = [ClassifierAdminModels.Libraries.Condition.ConditionEntryCombiningTypes]::OR

    If($condition.ConditionTypes.Count -eq 0){
        Add-ConditionType -Condition $condition -ConditionTypeName 'Dynamic Clearance User Properties'
    }
    Add-ConditionEntry -Condition $condition -ConditionTypeName 'Dynamic Clearance User Properties' -Name "mail" -Check Contains -Value $email
    $condition.ConditionTypes[0].ConditionEntryCombining = $entryCombiningOR
    Set-Condition $condition
}

# Remove External User to a Project
Function Classifier_Remove_External_User($email, $project){
    If(Get-Condition -Name "CLR - $project External"){
        $condition = Get-Condition -Name "CLR - $project External"
        $numExternal = $condition.ConditionTypes[0].ConditionEntries.Count - 1

        # Loop through backwards to avoid skipping any values
        for($i=$numExternal; $i -ge 0; $i--){
            $val = $condition.ConditionTypes[0].ConditionEntries[$i].Value
            If($email -eq $val){
                Remove-ConditionEntry -Condition $condition -ConditionTypeName 'Dynamic Clearance User Properties' -Index $i
            }
        }
        Set-Condition $condition
    }
}



Function Classifier_Publish_Config(){

    # Publish as test config
    #Publish-ServerConfiguration -Location TestFolder -TestName "ProjectNew" -Reason "Project Added"
    
    
    # Real
    #Publish-ServerConfiguration -Location DefaultFolder -TestName "RealProjectUpdate" -Reason "Project Added"

}

Function Shortcode($project, $projectDict){
    If($project.Length -ge 2){
        $code_start = $project.ToUpper().Substring(0,2)
        $code_end = -join ((65..90) | Get-Random -Count 2 | % {[char]$_})
        $code = $code_start + $code_end
        foreach($key in $projectDict.Keys){
            If($code -eq $projectDict.key -OR $code -eq $project){
                Shortcode $project $projectDict
            }
        }
        return $code
    }
    If($project.Length -eq 1){
        $code_start = $project.ToUpper().Substring(0,1)
        $code_end = -join ((65..90) | Get-Random -Count 3 | % {[char]$_})
        $code = $code_start + $code_end
        foreach($key in $projectDict.Keys){
            If($code -eq $projectDict.key -OR $code -eq $project){
                Shortcode $project $projectDict
            }
        }
        return $code
    }
}