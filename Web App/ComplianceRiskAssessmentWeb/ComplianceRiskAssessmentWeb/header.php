<?php
    session_start();
    require_once '../dbh.inc.php';
    include_once 'includes/functions.inc.php';
?>

<!DOCTYPE html>
<html lang="en" dir="ltr">
    <head>
        <meta charset="utf-8">
        <title><?php if (isset($title)) {echo $title;}else {echo "RegRiskTech";} ?></title>

        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css"/>
        <link rel="shortcut icon" type="image" href="images/logo_high_resolution_solid_Hqi_icon.ico"/>

        <link rel="stylesheet" href="css/reset.css">
        <link rel="stylesheet" href="css/index.css">

        <script src="http://code.jquery.com/jquery-latest.js"></script>
        <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
        <link href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css" rel="stylesheet">
        <script type="text/javascript" src="js/script.js"></script>
    </head>
<body>


