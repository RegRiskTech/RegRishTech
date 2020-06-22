<#*********************************************************************************
Company: RegRisk Technology
Author: Jamie Baldwin <jamie.baldwin@regrisktech.com>
Date: 28/01/20
Copyright (C) 2020 RegRisk Technology
*********************************************************************************#>

# Checks to see if any user is using the chosen attribute in Active Directory
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

# Converts the comma separated list of Project codes to a comma separated list of Project names
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

# Formats array of Projects into a comma separated list
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

# Returns list of Projets associated with each User, works for Internal and External users
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

# Checks list of Projects in AD and Database and returns any new projects
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

# Checks list of Projects in AD and Database and returns any projects that have been removed/closed in the Database
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

# For each user it checks if they are in Active Directory. If they are, it will update their list of Projects in the specified Attribute.
# For External Users, it will add them to the necessary conditions in Classifier Administration
Function AD_Check_User($firstName, $lastName, $emailDB, $department, $projectsDB, $projectsList, $attribute, $outputFile, [Parameter(Mandatory = $false)]$dataProjects, [Parameter(Mandatory = $false)]$check, [Parameter(Mandatory = $false)]$test, [Parameter(Mandatory = $false)]$removedProjectUsers){
    $user = $firstName + " " + $lastName
    $username = $emailDB.Split("@")[0]
    $attributeLimit = ""

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
            # thread for updating External Users
            $projectsClassifier = Classifier_Check_External_Conditions $emailDB $dataProjects

            # $removedProjectUsers is a list of External Users that were in a Project that has now been closed/deleted and such deleted in Classifier Admin, it is concatonation
            # of the Project and User's Email, e.g Alphajbaldwin@regrisktech.com
            If($test -ne "TEST"){
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
            }

            If ($projectsList -ne $projectsClassifier){
                # does a check between projects in AD and the DB, returns the new and removed project
                $new = newProjects $projectsClassifier $projectsList
                $remove = removeProjects $projectsClassifier $projectsList

                If($new){
                    # Add Extneral User to Projects
                    If($test -ne "TEST"){
                        $new_split = $new.Split(",")
                        foreach($project in $new_split){
                            Classifier_Add_External_User $emailDB $project
                        }
                    }

                    $new_list = Format_Projects $new
                    If($test -ne "TEST"){
                        $updateAction = "$user has been added to projects $new_list in Classifier Administration"
                        Update_Action_Log $outputFile $user $updateAction
                        Add-Output -Message $updateAction
                    }
                    Else{
                        $updateAction = "$user will be added to projects $new_list in Classifier Administration"
                        Add-Output -Message $updateAction
                    }
                }
                If($remove){
                    # Remove Extneral User from Projects
                    If($test -ne "TEST"){
                    $remove_split = $remove.Split(",")
                        foreach($project in $remove_split){
                            If($project -notin $removedProjectUsers){
                                Classifier_Remove_External_User $emailDB $project
                            }
                        }
                    }

                    $remove_list = Format_Projects $remove
                    If($test -ne "TEST"){
                        $updateAction = "$user has been removed from projects $remove_list in Classifier Administration"
                        Update_Action_Log $outputFile $user $updateAction
                        Add-Output -Message $updateAction
                    }
                    Else{
                        $updateAction = "$user will be removed from projects $remove_list in Classifier Administration"
                        Add-Output -Message $updateAction
                    }
                }
            }
        }
    Else{
        # thread for updating Internal users
        If($projectsDB -ne $projectsAD){
                If($projectsDB -eq "EMPTY"){
                    If($test -ne "TEST"){
                        Get-ADUser -Filter {UserPrincipalName -Eq $emailDB} | Set-ADUser -Clear $attribute
                        $projectsDB = ""
                    }
                    Else{
                        $projectsDB = ""
                    }
                }
                Else{
                    # Check to see if the Attribute length has been exceeded
                    If($test -ne "TEST"){
                        If($projectsDB.Length -gt 1024){
                            $updateAction = "$user has exceeded number of characters in Active Directory"
                            Update_Action_Log $outputFile $user $updateAction
                            Add-Output -Message $updateAction

                            $attributeLimit = "True"
                        }
                        Else{
                            Get-ADUser -Filter {UserPrincipalName -Eq $emailDB} | Set-ADUser -Replace @{$attribute=$projectsDB}
                        }
                    }
                }
                
                # does a check between projects in AD and the DB, returns the new and removed projects    
                $new = newProjects $projectsAD $projectsDB
                $remove = removeProjects $projectsAD $projectsDB

                If($new -and $attributeLimit -eq ""){
                    $new_list = Codes_to_Project $new $projectShortCodes

                    If($test -ne "TEST"){
                    $updateAction = "$user has been added to projects $new_list in Active Directory"
                    Update_Action_Log $outputFile $user $updateAction
                    Add-Output -Message $updateAction
                    }
                    Else{
                        $updateAction = "$user will be added to projects $new_list in Active Directory"
                        Add-Output -Message $updateAction
                    }
                }
                If($remove -and $attributeLimit -eq ""){
                    $remove_list = Codes_to_Project $remove $projectShortCodes
                    If($test -ne "TEST"){
                    $updateAction = "$user has been removed from projects $remove_list in Active Directory"
                    Update_Action_Log $outputFile $user $updateAction
                    Add-Output -Message $updateAction
                    }
                    Else{
                        $updateAction = "$user will be removed from projects $remove_list in Active Directory"
                        Add-Output -Message $updateAction
                    }
                }
            }
        }
    }

    Else{
        # Checking to see which users in Database are not in Active Directory
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
