<?php
    $title = "Controls Effectiveness - RegRiskTech";
    include_once 'header.php';
    include_once 'sidebar.php';

    if (!isset($_SESSION['userid'])) {
        header('location: login.php');
        exit(); // <-- terminates the current script
    }

    function getData(){
        $data = array();
        if (isset($_POST['RiskID']))
        {
            $data[1] = $_POST['RiskID'];
        }
        if (isset($_POST['BizID']))
        {
            $data[2] = $_POST['BizID'];
        }
        $data[3] = $_POST['ControlRisk'];
        $data[4] = $_POST['InherentRisk'];

        if (isset($_POST['VersionID']))
        {
            $data[5] = $_POST['VersionID'];
        }

        if (isset($_POST['UserID']))
        {
            $data[6] = $_POST['UserID'];
        }

        $data[7] = $_POST['commentsID'];
        
        $data[8] = "";

        //$data[9] = $_POST['tabID'];
        return $data;
    }

    if(isset($_POST['submit'])){
        $info = getData();

        //CRID
        $queryControl = $conn->prepare("SELECT CRID FROM dbo.Controls WHERE CRLabel = ?;");
        $queryControl->execute([$info[3]]); 
        $resultControl = $queryControl->fetchAll(PDO::FETCH_ASSOC);

        foreach($resultControl as $result){
            $info[3] = $result['CRID'];
        }

        //IRID
        $queryInherent = $conn->prepare("SELECT IRID FROM dbo.InherentRisks WHERE IRLabel = ?;");
        $queryInherent->execute([$info[4]]); 
        $resultInherent = $queryInherent->fetchAll(PDO::FETCH_ASSOC);

        foreach($resultInherent as $result){
            $info[4] = $result['IRID'];
        }              

        date_default_timezone_set("Europe/London");
        $datetime_variable = new DateTime();
        $datetime_formatted = date_format($datetime_variable, 'Y-m-d H:i:s');

        $info[8] = $datetime_formatted;
        
        $query = $conn->prepare("INSERT INTO Assessments(RiskID, BizID, CRID, IRID, VersionID, UserID, Comments, Updated) Values(:RiskID, :BizID, :CRID, :IRID, :VersionID, :UserID, :Comments, :Updated)");
        $versionID =  $info[5] + 1;

        $query->bindParam(':RiskID',$info[1], PDO::PARAM_STR);
        $query->bindParam(':BizID',$info[2], PDO::PARAM_STR);
        $query->bindParam(':CRID',$info[3], PDO::PARAM_STR);
        $query->bindParam(':IRID',$info[4], PDO::PARAM_STR);
        $query->bindParam(':VersionID',$versionID, PDO::PARAM_STR);
        $query->bindParam(':UserID',$info[6], PDO::PARAM_STR);
        $query->bindParam(':Comments',$info[7], PDO::PARAM_STR);
        $query->bindParam(':Updated',$info[8], PDO::PARAM_STR);
        $query->execute();
        
        //header("location: inherent-risks.php");
        header('Location: '.$_SERVER['PHP_SELF']);
    die;
    }
    include_once 'popup.php';
?>

<div class="hero">
    <div class="btn-box">
        <a href="inherent-risks.php">
        <button id="inherentBtn" style="color:black;">Inherent Risks</button>
        </a>
        <a href="controls-effectiveness">
            <button id="controlsBtn" style="color:#2196F3;">Controls Effectiveness</button>
        </a>
        <a href="index.php">
            <button id="residualBtn" style="color:black;">Residual Risks</button>
        </a>
        <input type="hidden" id="tabID" name="tabID" value="Residual" form="updateForm">
    </div>
</div>

<div id="controlsContent" class="controlsContent">
    <div class="relative"> 
    <div class="addRisks" onclick="editHeaders()">Edit Headers</div>
    <table id="RiskAssessmentTable" border="0">
        <?php
        # Column Meta
        echo "<thead><tr>";
        for($i = 0; $i < 3; $i++){
            echo "<td class='blankCell' >".""."</td>";
        }
        $queryBizMeta="SELECT BizMetaID,BizMetaShortDesc,BizMetaLongDesc FROM dbo.BizMeta;";
        $queryRefBizMeta = $conn->query($queryBizMeta);
        $resultBizMeta = $queryRefBizMeta->fetchAll(PDO::FETCH_ASSOC);

        foreach($resultBizMeta as $col){
            $queryBizMetaCount = $conn->prepare("SELECT dbo.Biz.BizID,dbo.Biz.BizGroupID,dbo.Biz.BizShortDesc,dbo.Biz.BizLongDesc
            FROM dbo.Biz
            INNER JOIN dbo.BizGroup
            ON dbo.Biz.BizGroupID = dbo.BizGroup.BizGroupID
            INNER JOIN dbo.BizMeta
            ON dbo.BizGroup.BizMetaID = dbo.BizMeta.BizMetaID
            WHERE dbo.BizMeta.BizMetaID = ?;");
            $queryBizMetaCount->execute([$col["BizMetaID"]]); 
            $resultBizMetaCount = $queryBizMetaCount->fetchAll(PDO::FETCH_ASSOC);
            $row_cntBizMeta = $queryBizMetaCount ->rowCount();

            echo '<th class="bizmetaline" colspan="'.$row_cntBizMeta.'">'.$col["BizMetaShortDesc"]."</th>";
        }

        # Column Groups
        $queryBizGroup="SELECT BizMetaID,BizGroupID,BizGroupShortDesc,BizGroupLongDesc FROM dbo.BizGroup ORDER BY Ordering;";
        $queryRefBizGroup = $conn->query($queryBizGroup);
        $resultBizGroup = $queryRefBizGroup->fetchAll(PDO::FETCH_ASSOC);
        $row_cnt = $queryRefBizGroup ->rowCount();

        echo "<tr>";
        for($i = 0; $i < 3; $i++){
            echo "<td class='blankCell'>".""."</td>";
        }

        foreach($resultBizGroup as $row){
            $queryCol = $conn->prepare("SELECT BizShortDesc FROM dbo.Biz WHERE BizGroupID = ? ORDER BY Ordering;");
            $queryCol->execute([$row["BizGroupID"]]); 
            $resultCol = $queryCol->fetchAll(PDO::FETCH_ASSOC);
            $row_cntCol = $queryCol ->rowCount();

            echo '<th class="bizgroupline" colspan="'.$row_cntCol.'">'.$row["BizGroupShortDesc"]."</th>";
        }
        echo "</tr>";

        # Column Business
        echo '<tr">';
        for($i = 0; $i < 3; $i++){
            echo "<td class='blankCell'>".""."</td>";
        }

        foreach($resultBizGroup as $row){
            $queryBiz = $conn->prepare("SELECT BizShortDesc FROM dbo.Biz WHERE BizGroupID = ? ORDER BY Ordering;");
            $queryBiz->execute([$row["BizGroupID"]]); 
            $resultBiz = $queryBiz->fetchAll(PDO::FETCH_ASSOC);

            foreach($resultBiz as $rowBiz){
                echo "<th title='".$rowBiz["BizShortDesc"]."' class='bizline'>".$rowBiz["BizShortDesc"]."</th>";
            }
        }
        echo "</tr></thead>";

        // Row Headers
        $queryRowMeta="SELECT RiskMetaID,RiskMetaShortDesc,RiskMetaLongDesc FROM dbo.RiskMeta;";
        $queryRefRowMeta = $conn->query($queryRowMeta);
        $resultRowMeta = $queryRefRowMeta->fetchAll(PDO::FETCH_ASSOC);

        echo "<tr>";

        $rowNum = 0; 
        foreach($resultRowMeta as $rowMeta){
            $queryRowMetaCount = $conn->prepare("SELECT dbo.Risks.RiskID,dbo.Risks.RiskGroupID,dbo.Risks.RiskShortDesc
            FROM dbo.Risks
            INNER JOIN dbo.RiskGroup
            ON dbo.Risks.RiskGroupID = dbo.RiskGroup.RiskGroupID
            INNER JOIN dbo.RiskMeta
            ON dbo.RiskGroup.RiskMetaID = dbo.RiskMeta.RiskMetaID
            WHERE dbo.RiskMeta.RiskMetaID = ?;");
            $queryRowMetaCount->execute([$rowMeta["RiskMetaID"]]); 
            $resultRowMetaCount = $queryRowMetaCount->fetchAll(PDO::FETCH_ASSOC);
            $row_cntRowMeta = $queryRowMetaCount->rowCount();

            $gridArr = [];

            for ($i = 0; $i < $row_cntRowMeta ; ++$i) {
                $gridArr[$i] = [];

                for ($j = 0; $j < $row_cntBizMeta; ++$j) {
                    $gridArr[$i][$j] = ["","","","","","","","","","","",""];

                }
            }

            echo '<td class="rowRiskMeta" rowspan="'.$row_cntRowMeta.'"><div class="rotate">'.$rowMeta["RiskMetaShortDesc"].'</div></td>';
            $queryRowGroup = $conn->prepare("SELECT RiskGroupID,RiskGroupShortDesc FROM dbo.RiskGroup WHERE RiskMetaID = ? ORDER BY Ordering;");
            $queryRowGroup->execute([$rowMeta["RiskMetaID"]]); 
            $resultRowGroup = $queryRowGroup->fetchAll(PDO::FETCH_ASSOC);
            
            foreach($resultRowGroup as $rowGroup){
                $queryRowGroupCount = $conn->prepare("SELECT dbo.Risks.RiskID,dbo.Risks.RiskGroupID,dbo.Risks.RiskShortDesc
                FROM dbo.Risks
                INNER JOIN dbo.RiskGroup
                ON dbo.Risks.RiskGroupID = dbo.RiskGroup.RiskGroupID
                WHERE dbo.RiskGroup.RiskGroupID = ?;");
                $queryRowGroupCount->execute([$rowGroup["RiskGroupID"]]); 
                $resultRowGroupCount = $queryRowGroupCount->fetchAll(PDO::FETCH_ASSOC);
                $row_cntRowGroupCount = $queryRowGroupCount ->rowCount();
                echo '<td title="'.$rowGroup["RiskGroupShortDesc"].'" class="rowRiskGroup" rowspan="'.$row_cntRowGroupCount.'"><div class="rotate">'.$rowGroup["RiskGroupShortDesc"].'</div></td>';
            
                $queryRowRisk = $conn->prepare("SELECT dbo.Risks.RiskID,dbo.Risks.RiskGroupID,dbo.Risks.RiskShortDesc
                FROM dbo.Risks
                WHERE dbo.Risks.RiskGroupID = ? ORDER BY Ordering;");
                $queryRowRisk->execute([$rowGroup["RiskGroupID"]]); 
                $resultRowRisk = $queryRowRisk->fetchAll(PDO::FETCH_ASSOC);

                foreach($resultRowRisk as $rowRisk){
                $colNum = 0;
                
                echo '<td title="'.$rowRisk["RiskShortDesc"].'" class="rowRisk">'.$rowRisk["RiskShortDesc"].'</td>';
                    foreach($resultBizGroup as $row){
                        $queryBiz="SELECT BizID,BizShortDesc FROM dbo.Biz WHERE BizGroupID = '".$row["BizGroupID"]."' ORDER BY Ordering;";
                        $queryRefBiz = $conn->query($queryBiz);
                        $resultBiz = $queryRefBiz->fetchAll(PDO::FETCH_ASSOC);
                        

                        foreach($resultBiz as $colBiz){
                            $queryData = $conn->prepare("SELECT Assess.BizID,Assess.RiskID,Assess.CRID,Assess.IRID,Assess.Comments,Assess.UserID,dbo.ResidualRiskMapping.RRID,dbo.DisplayColors.ColorCodeHex,dbo.Controls.CRLabel,dbo.InherentRisks.IRLabel,dbo.Biz.BizShortDesc,dbo.Risks.RiskShortDesc,Assess.VersionID
                            FROM (
                                SELECT dbo.Assessments.RiskID,dbo.Assessments.BizID, MAX(dbo.Assessments.VersionID) AS maxVersion
                                FROM dbo.Assessments GROUP BY RiskID, BizID
                            ) AS Assess2
                            INNER JOIN dbo.Assessments AS Assess 
                            ON Assess.RiskID = Assess2.RiskID AND Assess.BizID = Assess2.BizID AND Assess.VersionID = Assess2.maxVersion                               
                            INNER JOIN dbo.ResidualRiskMapping
                            ON Assess.CRID = dbo.ResidualRiskMapping.CRID AND Assess.IRID = dbo.ResidualRiskMapping.IRID
                            INNER JOIN dbo.DisplayColors
                            ON Assess.CRID = dbo.DisplayColors.ColorID
                            INNER JOIN dbo.Controls
                            ON Assess.CRID = dbo.Controls.CRID
                            INNER JOIN dbo.InherentRisks
                            ON Assess.IRID = dbo.InherentRisks.IRID
                            INNER JOIN dbo.Biz
                            ON Assess.BizID = dbo.Biz.BizID
                            INNER JOIN dbo.Risks
                            ON Assess.RiskID = dbo.Risks.RiskID
                            WHERE Assess.RiskID = ? AND Assess.BizID = ?;");
                            $queryData->execute([$rowRisk["RiskID"],$colBiz["BizID"]]); 
                            $resultData = $queryData->fetchAll(PDO::FETCH_ASSOC);

                            if ($queryData->rowCount() > 0) {
                                foreach($resultData as $rowData){
                                    $gridArr[$rowNum][$colNum][0] = $rowData["RiskID"];
                                    $gridArr[$rowNum][$colNum][1] = $rowData["BizID"];
                                    $gridArr[$rowNum][$colNum][2] = $rowData["CRID"];
                                    $gridArr[$rowNum][$colNum][3] = $rowData["IRID"];
                                    $gridArr[$rowNum][$colNum][4] = $rowData["Comments"];
                                    $gridArr[$rowNum][$colNum][5] = $_SESSION["userid"];
                                    $gridArr[$rowNum][$colNum][6] = $rowData["ColorCodeHex"];
                                    $gridArr[$rowNum][$colNum][7] = $rowData["CRLabel"];
                                    $gridArr[$rowNum][$colNum][8] = $rowData["IRLabel"];
                                    $gridArr[$rowNum][$colNum][9] = $rowData["RiskShortDesc"];
                                    $gridArr[$rowNum][$colNum][10] = $rowData["BizShortDesc"];
                                    $gridArr[$rowNum][$colNum][11] = $rowData["VersionID"];
                                }
                                // Get Inherent Risk Color
                                $queryData = $conn->prepare("SELECT Assess.BizID,Assess.RiskID,Assess.CRID,Assess.IRID,dbo.DisplayColors.ColorCodeHex As IRColor,Assess.VersionID
                                FROM (
                                    SELECT dbo.Assessments.RiskID,dbo.Assessments.BizID, MAX(dbo.Assessments.VersionID) AS maxVersion
                                    FROM dbo.Assessments GROUP BY RiskID, BizID
                                ) AS Assess2
                                INNER JOIN dbo.Assessments AS Assess 
                                ON Assess.RiskID = Assess2.RiskID AND Assess.BizID = Assess2.BizID AND Assess.VersionID = Assess2.maxVersion                               
                                INNER JOIN dbo.DisplayColors
                                ON Assess.IRID = dbo.DisplayColors.ColorID
                                WHERE Assess.RiskID = ? AND Assess.BizID = ?;");
                                $queryData->execute([$rowRisk["RiskID"],$colBiz["BizID"]]); 
                                $resultData = $queryData->fetchAll(PDO::FETCH_ASSOC);
                                
                                foreach($resultData as $rowData){
                                    $gridArr[$rowNum][$colNum][12] = $rowData["IRColor"];
                                }

                                // Get Residual Color
                                $queryData = $conn->prepare("SELECT Assess.BizID,Assess.RiskID,Assess.CRID,Assess.IRID,dbo.DisplayColors.ColorCodeHex As RRColor,Assess.VersionID
                                FROM (
                                    SELECT dbo.Assessments.RiskID,dbo.Assessments.BizID, MAX(dbo.Assessments.VersionID) AS maxVersion
                                    FROM dbo.Assessments GROUP BY RiskID, BizID
                                ) AS Assess2
                                INNER JOIN dbo.Assessments AS Assess 
                                ON Assess.RiskID = Assess2.RiskID AND Assess.BizID = Assess2.BizID AND Assess.VersionID = Assess2.maxVersion
                                INNER JOIN dbo.ResidualRiskMapping
                                ON Assess.CRID = dbo.ResidualRiskMapping.CRID AND Assess.IRID = dbo.ResidualRiskMapping.IRID
                                INNER JOIN dbo.ResidualRisks
                                ON dbo.ResidualRiskMapping.RRID = dbo.ResidualRisks.RRID
                                INNER JOIN dbo.DisplayColors
                                ON dbo.ResidualRisks.ColorID = dbo.DisplayColors.ColorID
                                WHERE Assess.RiskID = ? AND Assess.BizID = ?;");
                                $queryData->execute([$rowRisk["RiskID"],$colBiz["BizID"]]); 
                                $resultData = $queryData->fetchAll(PDO::FETCH_ASSOC);

                                foreach($resultData as $rowData){
                                    $gridArr[$rowNum][$colNum][13] = $rowData["RRColor"];
                                }
                            }
                            else{
                                $gridArr[$rowNum][$colNum][0] = $rowRisk["RiskID"]; //$rowData["RiskID"];
                                $gridArr[$rowNum][$colNum][1] = $colBiz["BizID"]; //$rowData["BizID"];
                                $gridArr[$rowNum][$colNum][2] = "1"; //$rowData["CRID"];
                                $gridArr[$rowNum][$colNum][3] = "1"; //$rowData["IRID"];
                                $gridArr[$rowNum][$colNum][4] = ""; //$rowData["Comments"];
                                $gridArr[$rowNum][$colNum][5] = $_SESSION["userid"];
                                $gridArr[$rowNum][$colNum][6] = ""; //$rowData["ColorCodeHex"];
                                $gridArr[$rowNum][$colNum][7] = ""; //$rowData["CRLabel"];
                                $gridArr[$rowNum][$colNum][8] = ""; //$rowData["IRLabel"];
                                $gridArr[$rowNum][$colNum][9] = $rowRisk["RiskShortDesc"]; //$rowData["RiskShortDesc"];
                                $gridArr[$rowNum][$colNum][10] = $colBiz["BizShortDesc"]; //$rowData["BizShortDesc"];
                                $gridArr[$rowNum][$colNum][11] = "0"; //$rowData["VersionID"];
                                $gridArr[$rowNum][$colNum][12] = ""; //$rowData["IRColor"];
                                $gridArr[$rowNum][$colNum][13] = ""; //$rowData["RRColor"];
                            }
                            $colNum += 1; 
                        }
                    }

                    for($j = 0; $j < $row_cntBizMeta; $j++){
                        $colorCR = $gridArr[$rowNum][$j][6];
                        $colorIR = $gridArr[$rowNum][$j][12];
                        $colorRR = $gridArr[$rowNum][$j][13];

                        echo '<td onclick="alerting('.$gridArr[$rowNum][$j][0].','.$gridArr[$rowNum][$j][1].','.$gridArr[$rowNum][$j][2].','.$gridArr[$rowNum][$j][3].'
                        ,\''.$gridArr[$rowNum][$j][9].'\',\''.$gridArr[$rowNum][$j][10].'\','.$gridArr[$rowNum][$j][5].','.$gridArr[$rowNum][$j][11].')" 
                        class="data" style="background-color:'.$colorCR.'">'.$gridArr[$rowNum][$j][7].'<span class="datatooltiptext">Inherent:<div class="box" style="background-color: '.$colorIR.';"></div><br/>Controls:<div class="box" style="background-color: '.$colorCR.';"></div><br/>Residual:<div class="box" style="background-color: '.$colorRR.';"></div><br/><a onclick="alerting('.$gridArr[$rowNum][$j][0].','.$gridArr[$rowNum][$j][1].','.$gridArr[$rowNum][$j][2].','.$gridArr[$rowNum][$j][3].'
                        ,\''.$gridArr[$rowNum][$j][9].'\',\''.$gridArr[$rowNum][$j][10].'\','.$gridArr[$rowNum][$j][5].','.$gridArr[$rowNum][$j][11].')" class="updateButton" href="#">Update</a></span></td>';
                    }                 
                    echo '</tr>';
                    $rowNum += 1;   
                }
            }
        }
        ?>
    </table>
    </div>
</div>

<script>
    $( ".popup-content" ).draggable({
        containment: "parent"
    });

    $( ".popup-content-Headers" ).draggable({
        containment: "parent"
    });

    function acall(){
        var data = new FormData();
        data.append("RiskIDHistory", document.getElementById("RiskIDHistory").value);
        data.append("BizIDHistory", document.getElementById("BizIDHistory").value);

        var xhr = new XMLHttpRequest();
        xhr.open("POST", "getHistory.php");
        xhr.onload = function(){
            //console.log(this.response);
            document.getElementById("field_name").innerHTML= xhr.responseText;
        };
        xhr.send(data);
        return false;
    }
</script>
<?php
    include_once 'footer.php';
?>