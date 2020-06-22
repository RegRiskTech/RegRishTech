<#*********************************************************************************
Company: RegRisk Technology
Author: Jamie Baldwin <jamie.baldwin@regrisktech.com>
Date: 28/01/20
Copyright (C) 2020 RegRisk Technology
*********************************************************************************#>

# Outputs all actions to SQL Table when in Update Mode
Function Update_Action_Log($outputFile, $user, $updateAction){
    $updateDate = Get-Date -format "dd-MM-yyyy"
    $updateTime = Get-Date -Format "HH:mm:ss"

    # Output to SQL Table
    $server= "RegRiskTechDemo"
    $database = "RRTDatabases"

    $insertquery=" 
    INSERT INTO [dbo].[ActionLog] 
               ([Date] 
               ,[Time] 
               ,[User]
               ,[Action]) 
         VALUES 
               ('$updateDate' 
               ,'$updateTime' 
               ,'$user'
               ,'$updateAction') 
    GO 
    " 
    Invoke-SQLcmd -ServerInstance $server -query $insertquery -Database $database 
}

# Outputs to Update Tool Console
Function Add-Output($Message,[Parameter(Mandatory = $false)]$colour){
    If($colour -ne $null){
        $output.SelectionColor = $colour
    }
    $output.AppendText("$Message`r`n")
    $output.Refresh()
    $output.ScrollToCaret()
}

# Formats Projects into a comma separted Project Code list
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

# Formats Projects into a comma separted Project Names list
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

# Update Function that will do necessary comparasions between the Goldon Source Database and Active Directory/Classifier Administration, and make the updates to both.
Function Update($inputFile, $outputFile, $attribute, [Parameter(Mandatory = $false)]$test){
    
    # Reports the Update process has started
    $output.Text = ""
    $updateTime = Get-Date -Format "HH:mm:ss"
    If($test -ne "TEST"){
        $updateAction = "Update Process Started"
        Update_Action_Log $outputFile "" $updateAction

        Add-Output -Message $updateTime 'cyan'
        Add-Output -Message $updateAction 'yellow'
    }
    Else{
        $updateAction = "Test Update Process Started"

        Add-Output -Message $updateTime 'cyan'
        Add-Output -Message $updateAction 'yellow'
    }

    # Get Projects and Shortcodes from Classifier
    $projectShortCodes = Classifier_Check_Project_Code

    # Get Closed Projects from Database
    $query = "SELECT ProjectName FROM Projects WHERE Closed = 1"
    $dataClosedProjects = SQL_Query_Database $inputfile $query

    # Removes Closed Projects from Classifier
    $removedProjectUsers = New-Object System.Collections.ArrayList
    foreach($project in $dataClosedProjects){
        Classifier_Remove_Project $project[0].ToString() $removedProjectUsers $outputFile $test
    }

    # Removes Deleted Projects from Clasifier
    $query= "SELECT ProjectName FROM Projects"
    $dataProjectsAll = SQL_Query_Database $inputfile $query

    Classifier_Remove_Deleted_Project $dataProjectsAll $removedProjectUsers $outputFile $test

    # Get Projects from Database
    $query= "SELECT ProjectName FROM Projects WHERE Closed = 0"
    $dataProjects = SQL_Query_Database $inputfile $query

    # Checks Projects are in Classifier, Adds in any new Projects
    foreach($project in $dataProjects){
        Classifier_Check_Project $project[0].ToString() $outputFile $projectShortCodes "NotCheck" $test
    }

    # Get Users from Database
    $query = "SELECT FirstName, LastName, Email, Department FROM Members"
    $dataUsers = SQL_Query_Database $inputfile $query

    foreach($user in $dataUsers){
        $firstName = $user[0].ToString()
        $lastName = $user[1].ToString()
        $email = $user[2].ToString()
        $department = $user[3].ToString()
        $fullName = $firstName + " " + $lastName

        # Get User's Projects from Database
        $query = "SELECT Projects.ProjectName, ProjectMembers.Member FROM Projects INNER JOIN ProjectMembers ON Projects.ProjectName=ProjectMembers.ProjectID WHERE ProjectMembers.Email ='$email'"
        
        #$query = "SELECT Projects.ProjectName, ProjectMembers.Member, Projects.Closed FROM Projects INNER JOIN ProjectMembers ON Projects.ProjectName=ProjectMembers.ProjectID WHERE Projects.Closed = 0 AND ProjectMembers.Email ='$email'"
        
        
        $dataUserProjects = SQL_Query_Database $inputfile $query

        $projects_shortcode = Concat_Projects_To_Shortcode $dataUserProjects $projectShortCodes
        $projectsList = Concat_Projects $dataUserProjects

        # Checks if they are in Active Directory. If they are, it will update their list of Projects in the specified Attribute.
        AD_Check_User $firstName $lastName $email $department $projects_shortcode $projectsList $attribute $outputFile $dataProjectsAll "NotCheck" $test $removedProjectUsers
    }

    # Publishes the Configuration in Classifier Administration in Update Mode
    If($test -ne "TEST"){
        Classifier_Publish_Config
    }

    # Reports the Update process has Finished
    If($test -ne "TEST"){
        $updateTime = Get-Date -Format "HH:mm:ss"
        $updateAction = "Update Process Completed"
        Update_Action_Log $outputFile "" $updateAction

        Add-Output -Message $updateTime 'cyan'
        Add-Output -Message $updateAction 'yellow'
    }
    Else{
        $updateTime = Get-Date -Format "HH:mm:ss"
        $updateAction = "Test Update Process Completed"
        
        Add-Output -Message $updateTime 'cyan'
        Add-Output -Message $updateAction 'yellow'
    }
}