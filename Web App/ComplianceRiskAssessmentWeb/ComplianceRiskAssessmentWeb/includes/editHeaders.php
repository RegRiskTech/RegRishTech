<?php

require_once '../../dbh.inc.php';

if(isset($_POST['bizGroupID']))
{
    $bizGroupID = $_POST['bizGroupID'];
    $ordering = "100";
    $bizShortDesc = $_POST['bizShortDesc'];
    $bizLongDesc = "";

    $query = $conn->prepare("INSERT INTO Biz(BizGroupID, Ordering, BizShortDesc, BizLongDesc) Values(:BizGroupID, :Ordering, :BizShortDesc, :BizLongDesc)");

    $query->bindParam(':BizGroupID',$bizGroupID, PDO::PARAM_STR);
    $query->bindParam(':Ordering',$ordering, PDO::PARAM_STR);
    $query->bindParam(':BizShortDesc',$bizShortDesc, PDO::PARAM_STR);
    $query->bindParam(':BizLongDesc',$bizLongDesc, PDO::PARAM_STR);

    $response = $query->execute();
    $last_id = $conn->lastInsertId();

    if($response) {
        echo $last_id;
    }
    else{
        echo "error";
    } 
}

if(isset($_POST['bizMetaID']))
{
    $bizMetaID = $_POST['bizMetaID'];
    $ordering = "100";
    $bizGroupShortDesc = $_POST['bizGroupShortDesc'];
    $bizGroupLongDesc = "";

    $query = $conn->prepare("INSERT INTO BizGroup(BizMetaID, Ordering, BizGroupShortDesc, BizGroupLongDesc) Values(:BizMetaID ,:Ordering, :BizGroupShortDesc, :BizGroupLongDesc)");

    $query->bindParam(':BizMetaID',$bizMetaID, PDO::PARAM_STR);
    $query->bindParam(':Ordering',$ordering, PDO::PARAM_STR);
    $query->bindParam(':BizGroupShortDesc',$bizGroupShortDesc, PDO::PARAM_STR);
    $query->bindParam(':BizGroupLongDesc',$bizGroupLongDesc, PDO::PARAM_STR);

    $response = $query->execute();
    $last_id = $conn->lastInsertId();

    $bizShortDesc = "";
    $bizLongDesc = "";
    $query = $conn->prepare("INSERT INTO Biz(BizGroupID, Ordering, BizShortDesc, BizLongDesc) Values(:BizGroupID ,:Ordering, :BizShortDesc, :BizLongDesc)");

    $ordering = "1";
    $query->bindParam(':BizGroupID',$last_id, PDO::PARAM_STR);
    $query->bindParam(':Ordering',$ordering, PDO::PARAM_STR);
    $query->bindParam(':BizShortDesc',$bizShortDesc, PDO::PARAM_STR);
    $query->bindParam(':BizLongDesc',$bizLongDesc, PDO::PARAM_STR);

    $response = $query->execute();

    if($response) {
        echo $last_id;
    }
    else{
        echo "error";
    } 
}

if(isset($_POST['riskGroupID']))
{
    $riskGroupID = $_POST['riskGroupID'];
    $ordering = "100";
    $riskShortDesc = $_POST['riskShortDesc'];
    $riskLongDesc = "";

    $query = $conn->prepare("INSERT INTO Risks(RiskGroupID, Ordering, RiskShortDesc, RiskLongDesc) Values(:RiskGroupID, :Ordering, :RiskShortDesc, :RiskLongDesc)");

    $query->bindParam(':RiskGroupID',$riskGroupID, PDO::PARAM_STR);
    $query->bindParam(':Ordering',$ordering, PDO::PARAM_STR);
    $query->bindParam(':RiskShortDesc',$riskShortDesc, PDO::PARAM_STR);
    $query->bindParam(':RiskLongDesc',$riskLongDesc, PDO::PARAM_STR);

    $response = $query->execute();
    $last_id = $conn->lastInsertId();


    if($response) {
        echo $last_id;
    }
    else{
        echo "error";
    } 
}

if(isset($_POST['riskMetaID']))
{
    $riskMetaID = $_POST['riskMetaID'];
    $ordering = "100";
    $riskGroupShortDesc = $_POST['riskGroupShortDesc'];
    $riskGroupLongDesc = "";

    $query = $conn->prepare("INSERT INTO RiskGroup(RiskMetaID, Ordering, RiskGroupShortDesc, RiskGroupLongDesc) Values(:RiskMetaID ,:Ordering, :RiskGroupShortDesc, :RiskGroupLongDesc)");

    $query->bindParam(':RiskMetaID',$riskMetaID, PDO::PARAM_STR);
    $query->bindParam(':Ordering',$ordering, PDO::PARAM_STR);
    $query->bindParam(':RiskGroupShortDesc',$riskGroupShortDesc, PDO::PARAM_STR);
    $query->bindParam(':RiskGroupLongDesc',$riskGroupLongDesc, PDO::PARAM_STR);

    $response = $query->execute();
    $last_id = $conn->lastInsertId();

    $riskShortDesc = "";
    $riskLongDesc = "";
    $query = $conn->prepare("INSERT INTO Risks(RiskGroupID, Ordering, RiskShortDesc, RiskLongDesc) Values(:RiskGroupID ,:Ordering, :RiskShortDesc, :RiskLongDesc)");

    $ordering = "1";
    $query->bindParam(':RiskGroupID',$last_id, PDO::PARAM_STR);
    $query->bindParam(':Ordering',$ordering, PDO::PARAM_STR);
    $query->bindParam(':RiskShortDesc',$riskShortDesc, PDO::PARAM_STR);
    $query->bindParam(':RiskLongDesc',$riskLongDesc, PDO::PARAM_STR);

    $response = $query->execute();

    if($response) {
        echo $last_id;
    }
    else{
        echo "error";
    } 
}

if (isset($_POST['deletebizID']))
{
    $deletebizID = $_POST['deletebizID'];

    $sql = "DELETE FROM Biz WHERE BizID = ?"  ;     
    $q = $conn->prepare($sql);

    $response = $q->execute(array($deletebizID));  

    if($response) {
        echo "success";
    }
    else{
        echo "error";
    } 
}

if (isset($_POST['deletebizGroupID']))
{
    $deletebizGroupID = $_POST['deletebizGroupID'];

    $sql = "DELETE FROM Biz WHERE BizGroupID = ?"  ;     
    $q = $conn->prepare($sql);
    $q->execute(array($deletebizGroupID));  

    $sql = "DELETE FROM BizGroup WHERE BizGroupID = ?"  ;     
    $q = $conn->prepare($sql);

    $response = $q->execute(array($deletebizGroupID));  

    if($response) {
        echo "success";
    }
    else{
        echo "error";
    } 
}

if (isset($_POST['deleteriskID']))
{
    $deleteriskID = $_POST['deleteriskID'];

    $sql = "DELETE FROM Risks WHERE RiskID = ?"  ;     
    $q = $conn->prepare($sql);

    $response = $q->execute(array($deleteriskID));  

    if($response) {
        echo "success";
    }
    else{
        echo "error";
    } 
}

if (isset($_POST['deleteriskGroupID']))
{
    $deleteriskGroupID = $_POST['deleteriskGroupID'];

    $sql = "DELETE FROM Risks WHERE RiskGroupID = ?"  ;     
    $q = $conn->prepare($sql);
    $q->execute(array($deleteriskGroupID));  

    $sql = "DELETE FROM RiskGroup WHERE RiskGroupID = ?"  ;     
    $q = $conn->prepare($sql);

    $response = $q->execute(array($deleteriskGroupID));  

    if($response) {
        echo "success";
    }
    else{
        echo "error";
    } 
}

if (isset($_POST['bizUpdatedName']))
{
    $bizID = $_POST['bizID'];
    $bizName = $_POST['bizUpdatedName'];

    $sql = "UPDATE Biz SET BizShortDesc=? WHERE BizID=?";
    $conn->prepare($sql)->execute([$bizName, $bizID]);
}

if (isset($_POST['riskUpdatedName']))
{
    $riskID = $_POST['riskID'];
    $riskName = $_POST['riskUpdatedName'];

    $sql = "UPDATE Risks SET RiskShortDesc=? WHERE RiskID=?";
    $conn->prepare($sql)->execute([$riskName, $riskID]);
}

if (isset($_POST['bizGroupUpdatedName']))
{
    $bizGroupID = $_POST['bizID'];
    $bizGroupName = $_POST['bizGroupUpdatedName'];

    $sql = "UPDATE BizGroup SET BizGroupShortDesc=? WHERE BizGroupID=?";
    $conn->prepare($sql)->execute([$bizGroupName, $bizGroupID]);
}

if (isset($_POST['riskGroupUpdatedName']))
{
    $riskGroupID = $_POST['riskID'];
    $riskGroupName = $_POST['riskGroupUpdatedName'];

    $sql = "UPDATE RiskGroup SET RiskGroupShortDesc=? WHERE RiskGroupID=?";
    $conn->prepare($sql)->execute([$riskGroupName, $riskGroupID]);
}

if (isset($_POST['idsInOrderBiz']))
{
    $i = 1;

    foreach ($_POST['idsInOrderBiz'] as $value) {
        $arr = explode('biz', $value);
        $bizID = $arr[1];
        $sql = "UPDATE Biz SET Ordering=? WHERE BizID=?";

        $conn->prepare($sql)->execute([$i, $bizID]);
        $i++;
    }
}

if (isset($_POST['idsInOrderBizGroup']))
{
    $i = 1;

    foreach ($_POST['idsInOrderBizGroup'] as $value) {
        $arr = explode('bizGroup', $value);
        $bizGroupID = $arr[1];
        $sql = "UPDATE BizGroup SET Ordering=? WHERE BizGroupID=?";

        $conn->prepare($sql)->execute([$i, $bizGroupID]);
        $i++;
    }
}

if (isset($_POST['idsInOrderRisk']))
{
    $i = 1;

    foreach ($_POST['idsInOrderRisk'] as $value) {
        $arr = explode('risk', $value);
        $riskID = $arr[1];
        $sql = "UPDATE Risks SET Ordering=? WHERE RiskID=?";

        $conn->prepare($sql)->execute([$i, $riskID]);
        $i++;
    }
}

if (isset($_POST['idsInOrderRiskGroup']))
{
    $i = 1;

    foreach ($_POST['idsInOrderRiskGroup'] as $value) {
        $arr = explode('riskGroup', $value);
        $riskGroupID = $arr[1];
        $sql = "UPDATE RiskGroup SET Ordering=? WHERE RiskGroupID=?";

        $conn->prepare($sql)->execute([$i, $riskGroupID]);
        $i++;
    }
}
?>