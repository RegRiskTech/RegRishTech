<?php

$dbServername = "regrisktech.database.windows.net";
$dbUsername = "jamiebaldwin";
$dbPassword = "RRTPenguin1#";
$dbName = "RRT-RAG-Development";

$conn = new PDO("sqlsrv:Server=$dbServername;Database=$dbName", $dbUsername, $dbPassword);
$conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION); 