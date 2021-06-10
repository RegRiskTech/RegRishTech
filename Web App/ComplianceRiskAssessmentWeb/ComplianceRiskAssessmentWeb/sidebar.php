
<input type="checkbox" id="check" name="check">
<label for="check">
    <i class="fas fa-bars" id="btn"></i>
    <i class="fas fa-bars" id="cancel"></i>
</label>

<!--sidebar start-->
<div class="sidebar">
    <center>
        <img src="images/logo_high_resolution_white_horizontal.png" class="profile_image" alt"">
    </center>
    <ul>
        <?php
        echo '<li><a href="#"><i class="fas fa-user fa-lg"></i><span>'.$_SESSION["username"].'</span></a></li>';
        ?>
        <li><a href="index.php"><i class="fas fa-th fa-lg"></i><span>Risk Assessment</span></a></li>
        <li><a href="#"><i class="fas fa-envelope fa-lg"></i><span>Contact</span></a></li>
        <li><a href="#"><i class="fas fa-cog fa-lg"></i><span>Settings</span></a></li>
        <li><a href="includes/logout.inc.php"><i class="fas fa-sign-out-alt fa-lg"></i><span>Log Out</span></a></li>
    </ul>
</div>

</div> <!--sidebar end-->

<script>
    const lsKey = "false";

    $('input[name=check]').change(function() {
        if ($(this).is(':checked')) {
        localStorage.setItem(lsKey, "true")
        } else {
        localStorage.setItem(lsKey, "false")
        }
    });

    if(localStorage.getItem(lsKey) === "true"){
        document.getElementById("check").checked = true;
    };
</script>

<div class="content">