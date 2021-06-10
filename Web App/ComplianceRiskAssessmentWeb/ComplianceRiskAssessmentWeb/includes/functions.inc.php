<?php

function emptyInputSignup($name, $email, $username, $pwd, $pwdRepeat) {
    $result;
    if(empty($name) || empty($email) || empty($username) || empty($pwd) || empty($pwdRepeat)){
        $result = true;
    }else{
        $result = false;
    }
    return $result;
}

function invalidUid($username){
    $result;
    if(!preg_match("/^[a-zA-Z0-9]*$/", $username)){
        $result = true;
    }else{
        $result = false;
    }
    return $result;
}

function invalidEmail($email) {
    $result;
    if(!filter_var($email, FILTER_VALIDATE_EMAIL)){
        $result = true;
    }else{
        $result = false;
    }
    return $result;
}

function pwdMatch($pwd, $pwdRepeat) {
    $result;
    if($pwd !== $pwdRepeat){
        $result = true;
    }else{
        $result = false;
    }
    return $result;
}

function emailExists($conn, $email) {
    $stmt = $conn->prepare("SELECT * FROM Users WHERE UserLogin = :email;");
    $stmt->bindParam(':email', $email, PDO::PARAM_STR);
    $stmt->execute();

    if($stmt->rowCount() < 0){
        // code...
    }else{
        $result = false;
        return $result;
    }
}

function usernameExists($conn, $username) {
    $stmt = $conn->prepare("SELECT * FROM Users WHERE UserUid = :username;");
    $stmt->bindParam(':username', $username, PDO::PARAM_STR);
    $stmt->execute();

    if($stmt->rowCount() < 0){
        // code...
    }else{
        $result = false;
        return $result;
    }
}

function createUser($conn, $name, $email, $username, $pwd) {
    $sql = "";

    $hashedPwd = password_hash($pwd, PASSWORD_DEFAULT);

    $stmt = $conn->prepare("INSERT INTO Users(UserLogin, UserName, UserUid, UserPassword) Values(:userLogin,:userName,:userUid, :userPwd)");
    $stmt->bindParam(':userLogin', $email, PDO::PARAM_STR);
    $stmt->bindParam(':userName', $name, PDO::PARAM_STR);
    $stmt->bindParam(':userUid', $username, PDO::PARAM_STR);
    $stmt->bindParam(':userPwd', $hashedPwd, PDO::PARAM_STR);
    $stmt->execute();


    $stmt = $conn->prepare("SELECT UserID FROM dbo.Users WHERE UserLogin = :email;");
    $stmt->bindParam(':email', $email, PDO::PARAM_STR);
    $stmt->execute();
    $results = $stmt->fetchAll(PDO::FETCH_ASSOC);

    foreach($results as $row){
        $userID = $row["UserID"];
    }

    session_start();
    $_SESSION["userid"] = $userID;
    $_SESSION["username"] = $name;
    $_SESSION["useremail"] = $email;
    $_SESSION["useruid"] = $username;
    header("location: ../index.php");
    exit();
}

function emptyInputLogin($username, $pwd) {
    $result;
    if(empty($username) || empty($pwd)){
        $result = true;
    }else{
        $result = false;
    }
    return $result;
}

function loginUser($conn, $username, $pwd){
    $stmt = $conn->prepare("SELECT * FROM dbo.Users WHERE UserUid =:username OR UserLogin = :email;");
    $stmt->bindParam(':username', $username, PDO::PARAM_STR);
    $stmt->bindParam(':email', $username, PDO::PARAM_STR);
    $stmt->execute();
    $results = $stmt->fetchAll(PDO::FETCH_ASSOC);

    if($stmt->rowCount() > 0){
        // code...
    }else{
        session_start();
        $_SESSION['email']= $username;
        header("location: ../login.php?error=wronglogin");
        exit();
    }
    
    foreach($results as $row){
        $pwdHashed = $row["UserPassword"];
        $userID = $row["UserID"];
        $userName = $row["UserName"];
        $userUid = $row["UserUid"];
        $userEmail = $row["UserLogin"];
    }

    $checkPwd = password_verify($pwd, $pwdHashed);

    if($checkPwd === false){
        session_start();
        $_SESSION['email']= $username;
        header("location: ../login.php?error=wronglogin");
        exit();
    }
    else if($checkPwd === true){
        session_start();
        $_SESSION["userid"] = $userID;
        $_SESSION["username"] = $userName;
        $_SESSION["useremail"] = $userEmail;
        $_SESSION["useruid"] = $userUid;
        header("location: ../inherent-risks.php");
        exit();
    }
}