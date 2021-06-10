<?php
    $title = "Sign Up - RegRiskTech";
    include_once 'header.php';
?>

<div class="wrapper">
<div class="signup-desc">
    <img src="images/logo_high_resolution_side.png" class="login-image" alt"" style='height: 100%; width: 100%; object-fit: contain'>
</div>
<section class="signup-form">
    <div class="signup-form-form">
        <form action="includes/signup.inc.php" method="post">
            <input type="text" name="name" autocomplete="off" placeholder="Full name">
            <input type="text" name="email" autocomplete="off" placeholder="Email">
            <input type="text" name="uid" autocomplete="off" placeholder="Username">
            <input type="password" autocomplete="off" name="pwd" placeholder="Password">

            <!--
            <div class="password-policies"
                <div class="policy-length">
                Minimum 8 Characters
                </div>
                <div class="policy-number">
                At least one Number
                </div>
                <div class="policy-uppercase">
                At least one Uppercase and Lowercase
                </div>
                <div class="policy-special">
                At least one Special Character
                </div>
            </div>
-->
            <input type="password" autocomplete="off" name="pwdrepeat" placeholder="Repeat password">
            <button type="submit" name="submit">Sign up</button>
        </form>
    </div>
  
    <div class="account">Already have an account?<a href="login.php" class="accountLink">Login here</a>
    </div>
    <?php
        // Error messages
        if (isset($_GET["error"])) {
            if ($_GET["error"] == "emptyinput") {
                echo "<p class='warning'>Fill in all fields</p>";
            }
            else if ($_GET["error"] == "invalidemail") {
                echo "<p class='warning'>Please provide a valid Email</p>";
            }
            else if ($_GET["error"] == "passwordsdontmatch") {
                echo "<p class='warning'>Passwords do not match</p>";
            }
            else if ($_GET["error"] == "emailexists") {
                echo "<p class='warning'>Email already exists</p>";
            }
            else if ($_GET["error"] == "usernameexists") {
                echo "<p class='warning'>Username already exists</p>";
            }
            else if ($_GET["error"] == "none") {
                echo "<p class='warning'>You have signed up</p>";
            }
        }
    ?>
</section>

<?php
    include_once 'footer.php';
?>
