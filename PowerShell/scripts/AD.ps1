<#*********************************************************************************
Company: RegRisk Technology
Author: Jamie Baldwin <jamie.baldwin@regrisktech.com>
Date: 28/01/20
Copyright (C) 2020 RegRisk Technology
*********************************************************************************#>

Function AD_Check_Attribute($attribute){
    $attribute_empty = $true
    foreach($user in Get-ADGroupMember "Domain Users" | Select -ExpandProperty name){
    $info = Get-ADUser -Filter 'Name -like $user' -properties $attribute | select -expandproperty $attribute
        if($info -ne $null){
            Write-Output "$user is using the '$attribute' Attribute`r`n"
            $attribute_empty = $false
        }
    }
    if($attribute_empty -eq $true){
        Write-Output "'$attribute' is not being used`r`n"
        return 1 | Out-Null
    }
    Else{
        return 0 | Out-Null
    }
}

Function Codes_to_Project($project_codes, $projectShortCodes){
    If($project_codes -ne $null){
        $projects_split = $project_codes.Split(",")
    }
    $projects = ""

    foreach($project in $projects_split){
        $value = $projectShortCodes.$project
        If($value -ne $null){
            $projects += $value + ", "
        }
    }
    $projectList = $projects.TrimEnd(", ")
    return $projectList
}

Function Format_Projects($projectList){
    If($projectList -ne $null){
        $projects_split = $projectList.Split(",")
    }
    $projects = ""

    foreach($project in $projects_split){
        If($project -ne $null){
            $projects += $project + ", "
        }
    }
    $projectList = $projects.TrimEnd(", ")
    return $projectList

}

Function AD_Projects($firstName, $lastName, $emailDB, $department, $projectsDB, $projectShortCodes, $dataProjects){
    $user = $firstName + " " + $lastName
    $username = $emailDB.Split("@")[0]
    $project_codes = Get-ADUser -Filter {UserPrincipalName -Eq $emailDB}  -properties $attribute | select -expandproperty $attribute
    $emailAD = Get-ADUser -Filter {UserPrincipalName -Eq $emailDB}  | select -expandproperty userprincipalname

    If (!$emailAD){
        $projectList = Classifier_Check_External_Conditions $emailDB $dataProjects
        $updateAction = "User: $user, Projects: $projectList"
        Add-Output -Message $updateAction
    }
    Else{
        $projectList = Codes_to_Project $project_codes $projectShortCodes
        $updateAction = "User: $user, Projects: $projectList"
        Add-Output -Message $updateAction
    }
}

Function newProjects($projectsAD,$projectsDB){
    If($projectsAD -ne $null){
        $splitProjectsAD = $projectsAD.Split(",")
    }
    If($projectsDB -ne $null){
        $splitProjectsDB = $projectsDB.Split(",")
    }
    $newProjects = ""
    Foreach($proDB in $splitProjectsDB){
        If($proDB -notin $splitProjectsAD){
            $newProjects += "$proDB,"
        }
    }
    If($newProjects){
        return $newProjects.Trim(",")
    }
}

Function removeProjects($projectsAD,$projectsDB){
    If($projectsAD -ne $null){
        $splitProjectsAD = $projectsAD.Split(",")
    }
    If($projectsDB -ne $null){
        $splitProjectsDB = $projectsDB.Split(",")
    }
    $newProjects = ""
    Foreach($proAD in $splitProjectsAD){     
        If($proAD -notin $splitProjectsDB){
            $removeProjects += "$proAD,"
        }
    }
    If($removeProjects){
        return $removeProjects.Trim(",")
    }
}

Function AD_Check_User($firstName, $lastName, $emailDB, $department, $projectsDB, $projectsList, [Parameter(Mandatory = $false)]$dataProjects, $removedProjectUsers, $attribute, $outputFile, [Parameter(Mandatory = $false)]$check, [Parameter(Mandatory = $false)]$test){
    $user = $firstName + " " + $lastName
    $username = $emailDB.Split("@")[0]

    If($projectsDB -eq ""){
        $projectsDB = "EMPTY"
    }

    $name = Get-ADUser -Filter {UserPrincipalName -Eq $emailDB}  -properties * | select -ExpandProperty name
    $projectsAD = Get-ADUser -Filter {UserPrincipalName -Eq $emailDB}  -properties $attribute | select -expandproperty $attribute
    $emailAD = Get-ADUser -Filter {UserPrincipalName -Eq $emailDB}  | select -expandproperty userprincipalname

    If($projectsAD -eq $null){
        $projectsAD = ""
    }

    If($check -ne "Check"){
        If (!$emailAD){
            If($test -ne "TEST"){
                $projectsClassifier = Classifier_Check_External_Conditions $emailDB $dataProjects

                ForEach($projectUser in $removedProjectUsers){
                    If($projectUser.Length -ge $emailDB.Length){          
                        If($projectUser.Substring($projectUser.Length - $emailDB.Length) -eq "$emailDB"){
                            $addProject = $projectUser.SubString(0,$projectUser.Length - $emailDB.Length)
                            If(!$projectsClassifier){
                                $projectsClassifier += "$addProject"
                            }
                            Else{
                                $projectsClassifier += ",$addProject"
                            }
                        }
                    }
                }

                If ($projectsList -ne $projectsClassifier){
                    $new = newProjects $projectsClassifier $projectsList
                    $remove = removeProjects $projectsClassifier $projectsList

                    If($new){
                        # Add Extneral User to Projects
                        $new_split = $new.Split(",")
                        foreach($project in $new_split){
                            Classifier_Add_External_User $emailDB $project
                        }

                        $new_list = Format_Projects $new
                        $updateAction = "$user has been added to projects $new_list in Classifier Administration"
                        Update_Action_Log $outputFile $user $updateAction
                        Add-Output -Message $updateAction
                    }
                    If($remove){
                        # Remove Extneral User from Projects
                        $remove_split = $remove.Split(",")
                        foreach($project in $remove_split){
                            If($project -notin $removedProjectUsers){
                                Classifier_Remove_External_User $emailDB $project
                            }
                        }

                        $remove_list = Format_Projects $remove
                        $updateAction = "$user has been removed from projects $remove_list in Classifier Administration"
                        Update_Action_Log $outputFile $user $updateAction
                        Add-Output -Message $updateAction
                    }
                }
            }
            Else{
                $projectsClassifier = Classifier_Check_External_Conditions $emailDB $dataProjects

                If($projectsList -ne $projectsClassifier){
                    $new = newProjects $projectsClassifier $projectsList
                    $remove = removeProjects $projectsClassifier $projectsList

                    If($new){
                        $new_list = Format_Projects $new
                        $updateAction = "$user will be added to projects $new_list in Classifier Administration"
                        Add-Output -Message $updateAction
                    }
                    If($remove){
                        $remove_list = Format_Projects $remove
                        $updateAction = "$user will be removed from projects $remove_list in Classifier Administration"
                        Add-Output -Message $updateAction
                    }
                }
            }
        }
        Else{
            If($projectsDB -ne $projectsAD){
                If($test -ne "TEST"){
                    If($projectsDB -eq "EMPTY"){
                        Get-ADUser -Filter {UserPrincipalName -Eq $emailDB} | Set-ADUser -Clear $attribute
                        $projectsDB = ""
                    }
                    Else{
                        Get-ADUser -Filter {UserPrincipalName -Eq $emailDB} | Set-ADUser -Replace @{$attribute=$projectsDB}
                    }
                    
                    $new = newProjects $projectsAD $projectsDB
                    $remove = removeProjects $projectsAD $projectsDB

                    If($new){
                        $new_list = Codes_to_Project $new $projectShortCodes
                        $updateAction = "$user has been added to projects $new_list in Active Directory"
                        Update_Action_Log $outputFile $user $updateAction
                        Add-Output -Message $updateAction
                    }
                    If($remove){
                        $remove_list = Codes_to_Project $remove $projectShortCodes
                        $updateAction = "$user has been removed from projects $remove_list in Active Directory"
                        Update_Action_Log $outputFile $user $updateAction
                        Add-Output -Message $updateAction
                    }
                }
                Else{
                    If($projectsDB -eq "EMPTY"){
                        $projectsDB = ""
                    }

                    $new = newProjects $projectsAD $projectsDB
                    $remove = removeProjects $projectsAD $projectsDB

                    If($new){
                        $new_list = Codes_to_Project $new $projectShortCodes
                        $updateAction = "$user will be added to projects $new_list in Active Directory"
                        Add-Output -Message $updateAction
                    }
                    If($remove){
                        $remove_list = Codes_to_Project $remove $projectShortCodes
                        $updateAction = "$user will be removed from projects $remove_list in Active Directory"
                        Add-Output -Message $updateAction
                    }
                }
            }
        }
    }
    Else{
        $user_check = $false
        If (!$emailAD){
            $updateAction = "No user with email: $emailDB exists in Active Directory"
            Add-Output -Message $updateAction
            $user_check=$true
        }
        If($user_check -eq $true){
            return $true
        }
    }
}