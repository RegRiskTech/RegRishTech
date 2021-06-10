<?php
if(isset($_POST["submit"])){

    $username = $_POST["email"];
    $pwd = $_POST["pwd"];

    require_once '../../dbh.inc.php';
    require_once 'functions.inc.php';

    if(emptyInputLogin($username, $pwd)  !== false){
        session_start();
        $_SESSION['email']=$_POST['email'];
        header("location: ../login.php?error=emptyinput");
        exit();
    }

    loginUser($conn, $username, $pwd);
}else{
    header("location: ../login.php");
    exit();
}