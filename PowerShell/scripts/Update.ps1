<#*********************************************************************************
Company: RegRisk Technology
Author: Jamie Baldwin <jamie.baldwin@regrisktech.com>
Date: 31/01/20
Copyright (C) 2020 RegRisk Technology
*********************************************************************************#>

Function Update_Action_Log($outputFile, $user, $updateAction){
    $updateDate = Get-Date -format "dd-MM-yyyy"
    $updateTime = Get-Date -Format "HH:mm:ss"
    [pscustomobject]@{
    "Date" = $updateDate
    "Time" = $updateTime
    "User" =  $user
    "Action" = $updateAction
    } | export-csv -Path $outputFile -Encoding utf8 -Append -Force
}

Function Add-Output($Message,[Parameter(Mandatory = $false)]$colour){
    If($colour -ne $null){
        $output.SelectionColor = $colour
    }
    $output.AppendText("$Message`r`n")
    $output.Refresh()
    $output.ScrollToCaret()
}

Function Concat_Projects_To_Shortcode($dataTable, $projectsDict){
    $projects = ""
    foreach($row in $dataTable){
        $key = $row[0].ToString()
        $value = $projectsDict.$key
        If($value -ne $null){
            $projects += $value + ","
        }
    } 
    return $projects.TrimEnd(",")
}

Function Concat_Projects($dataTable){
    $projects = ""
    foreach($row in $dataTable){
        $pro = $row[0].ToString()
        If($pro -ne $null){
            $projects += $pro + ","
        }
    } 
    return $projects.TrimEnd(",")
}

Function Update($inputFile, $outpuFile, $attribute, [Parameter(Mandatory = $false)]$test){
    $output.Text = ""
    $updateTime = Get-Date -Format "HH:mm:ss"
    If($test -ne "TEST"){
        $updateAction = "Update Process Started"
        Update_Action_Log $outputFile "" $updateAction

        Add-Output -Message $updateTime 'cyan'
        Add-Output -Message $updateAction 'green'
    }
    Else{
        $updateAction = "Test Update Process Started"

        Add-Output -Message $updateTime 'cyan'
        Add-Output -Message $updateAction 'yellow'
    }

    # Get Projects and Shortcodes from Classifier
    $projectShortCodes = Classifier_Check_Project_Code

    # Get Closed Projects from Database
    $query = "SELECT ProjectName FROM Projects WHERE Closed = -1"
    $dataClosedProjects = Access_Query_Database $inputfile $query

    # Remove Closed Projects from Classifier
    $removedProjectUsers = New-Object System.Collections.ArrayList
    foreach($project in $dataClosedProjects){
        Classifier_Remove_Project $project[0].ToString() $removedProjectUsers $outputFile $test
    }

    # Remove Deleted Projects from Clasifier
    $query= "SELECT ProjectName FROM Projects"
    $dataProjectsAll = Access_Query_Database $inputfile $query
    Classifier_Remove_Deleted_Project $dataProjectsAll $removedProjectUsers $outputFile $test

    # Get Projects from Database
    $query= "SELECT ProjectName FROM Projects WHERE Closed = 0"
    $dataProjects = Access_Query_Database $inputfile $query

    # Checks Projects are in Classifier, Adds in any new Projects
    foreach($project in $dataProjects){
        Classifier_Check_Project $project[0].ToString() $outputFile $projectShortCodes "NotCheck" $test
    }

    # Get Users from Database
    $query = "SELECT FirstName, LastName, Email, Department FROM Members"
    $dataUsers = Access_Query_Database $inputfile $query

    foreach($user in $dataUsers){
        $firstName = $user[0].ToString()
        $lastName = $user[1].ToString()
        $email = $user[2].ToString()
        $department = $user[3].ToString()
        $fullName = $firstName + " " + $lastName
    
        #$updateAction = $email
        #Add-Output -Message $updateAction 'red'
            

        # Get User's Projects from Database
        $query = "SELECT Projects.ProjectName, ProjectMembers.Member, Projects.Closed FROM Projects INNER JOIN ProjectMembers ON Projects.ProjectName=ProjectMembers.ProjectID WHERE Projects.Closed = FALSE AND ProjectMembers.Email ='$email'"
        $dataUserProjects = Access_Query_Database $inputfile $query
        $projects_shortcode = Concat_Projects_To_Shortcode $dataUserProjects $projectShortCodes
        $projectsList = Concat_Projects $dataUserProjects

        AD_Check_User $firstName $lastName $email $department $projects_shortcode $projectsList $dataProjectsAll $removedProjectUsers $attribute $outputFile "NotCheck" $test
    }

    If($test -ne "TEST"){
        $updateTime = Get-Date -Format "HH:mm:ss"
        $updateAction = "Update Process Completed"
        Update_Action_Log $outputFile "" $updateAction

        Add-Output -Message $updateTime 'cyan'
        Add-Output -Message $updateAction 'green'
    }
    Else{
        $updateTime = Get-Date -Format "HH:mm:ss"
        $updateAction = "Test Update Process Completed"
        
        Add-Output -Message $updateTime 'cyan'
        Add-Output -Message $updateAction 'yellow'
    }
}