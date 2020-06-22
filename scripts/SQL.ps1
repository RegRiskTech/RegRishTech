<#*********************************************************************************
Company: RegRisk Technology
Author: Jamie Baldwin <jamie.baldwin@regrisktech.com>
Date: 28/01/20
Copyright (C) 2020 RegRisk Technology
*********************************************************************************#>

Function SQL_Query_Database($inputfile, $query){
    $server= "RegRiskTechDemo"
    $database = "RRTDatabases"
    $userID = "RegRishTechDemo/jbaldwin"
    $password = ""

    $connection = New-Object System.Data.SqlClient.SqlConnection
    $connection.ConnectionString = "Server=$server; Database=$database; Trusted_Connection=true; User ID= '$userID'; Password='$password';"

    $Cmd = New-Object System.Data.SqlClient.SqlCommand
    $Cmd.Connection = $connection
    $Cmd.CommandText = $query
    $connection.Open()

    $adapter = New-Object System.Data.SqlClient.SqlDataAdapter $Cmd
    $dataSet = New-Object System.Data.DataSet
    [void] $adapter.Fill($dataSet)
    $connection.Close()
    return $dataSet.Tables[0]
}