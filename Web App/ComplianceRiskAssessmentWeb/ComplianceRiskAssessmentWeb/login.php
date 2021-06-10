<?php
    $title = "Log In or Sign Up - RegRiskTech";
    include_once 'header.php';
?>

<div class="wrapper">
<div class="login-desc">
    <img src="images/logo_high_resolution_side.png" class="login-image" alt"" style='height: 100%; width: 100%; object-fit: contain'>
    <p>Delivering innovative technology solutions for regulatory, compliance and risk departments within the financial and legal industries.</p>
</div>
<section class="login-form">
    <div class="login-form-form">
        <form action="includes/login.inc.php" method="post">
            <input type="text" name="email" placeholder="Username/Email" value="<?php if(isset($_SESSION['email'])){echo $_SESSION['email'];}?>">
            <input type="password" autocomplete="off" name="pwd" placeholder="Password">
            <button id="login-button" type="submit" name="submit">Log In</button>
        </form>
    </div>
    <a href="#" class="password">Forgot Password?</a>
    <hr class="horizontal">
    <a class="sign-up-button" href="signup.php">Sign Up</a>

    <?php
        // Error messages
        if (isset($_GET["error"])) {
            if ($_GET["error"] == "emptyinput") {
                echo "<p class='warning'>Please complete all fields</p>";
            }
            else if ($_GET["error"] == "wronglogin") {
                echo "<p class='warning'>Incorrect Email/Password</p>";
            }
        }
    ?>
</section>

<script>
//document.querySelector('#login-button').addEventListener('click', function(event) {
 //   event.preventDefault();
//});
</script>

<?php
    include_once 'footer.php';
?>