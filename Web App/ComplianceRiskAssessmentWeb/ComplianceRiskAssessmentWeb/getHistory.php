<?php
    function get_history($RiskID, $BizID){
        $dbServername = "regrisktech.database.windows.net";
        $dbUsername = "jamiebaldwin";
        $dbPassword = "RRTPenguin1#";
        $dbName = "RRT-RAG-Development";
        $conn = new PDO("sqlsrv:Server=$dbServername;Database=$dbName", $dbUsername, $dbPassword);
        $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION); 
        $queryHistory="SELECT dbo.Controls.CRLabel, dbo.InherentRisks.IRLabel,dbo.Assessments.Comments, dbo.Users.UserName, CONVERT(VARCHAR(10), dbo.Assessments.Updated, 103) + ' '  + convert(VARCHAR(8), dbo.Assessments.Updated, 14) as Updated
        FROM dbo.Assessments
        INNER JOIN dbo.Controls
        ON dbo.Assessments.CRID = dbo.Controls.CRID
        INNER JOIN dbo.InherentRisks
        ON dbo.Assessments.IRID = dbo.InherentRisks.IRID
        INNER JOIN dbo.Users
        ON dbo.Assessments.UserID = dbo.Users.UserID
        WHERE dbo.Assessments.RiskID = '".$RiskID."' AND dbo.Assessments.BizID = '".$BizID."'
        ORDER BY dbo.Assessments.Updated Desc";
        $queryRefHistory = $conn->query($queryHistory);
        $resultHistory = $queryRefHistory->fetchAll(PDO::FETCH_ASSOC);
        return $resultHistory;
    }

    $table_str='<table id="HistoryTable" class="HistoryTable"><tr><th id="historyLeft">Inherent Risk</th><th id="historyMiddle">Control Effectiveness</th><th id="historyComments">Comments</th><th id="historyMiddle">User</th><th id="historyRight">Date</th></tr>';
    $RiskID = $_POST['RiskIDHistory'];
    $BizID = $_POST['BizIDHistory'];
    $historyRows = get_history($RiskID, $BizID);
    $table_str.='<tbody>';
    foreach($historyRows as $row){
        $table_str.='<tr>';
        $table_str.='<td id="dataLeft">'.$row['IRLabel'].'</td><td>'.$row['CRLabel'].'</td><td>'.$row['Comments'].'</td><td>'.$row['UserName'].'</td><td id="dataRight">'.$row['Updated'].'</td>';
        $table_str.='</tr>';        
    }
    $table_str.='</tbody>';
    $table_str.='</table>';
    
    print($table_str);
?>