<#*********************************************************************************
Company: RegRisk Technology
Author: Jamie Baldwin <jamie.baldwin@regrisktech.com>
Date: 28/01/20
Copyright (C) 2020 RegRisk Technology
*********************************************************************************#>

Function Access_Query_Database($inputfile, $query){
    $dsn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=$inputfile;"

    # create connection object and open the database
    $objConn = New-Object System.Data.OleDb.OleDbConnection $dsn
    $objCmd  = New-Object System.Data.OleDb.OleDbCommand $query,$objConn
    $objConn.Open()

    # get query results, populate data-adapter, close connection
    $adapter = New-Object System.Data.OleDb.OleDbDataAdapter $objCmd
    $dataset = New-Object System.Data.DataSet
    [void] $adapter.Fill($dataSet)
    $objConn.Close()

    return $dataSet.Tables[0]
}
