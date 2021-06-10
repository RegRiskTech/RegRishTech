<div class="popup">
    <form id="updateForm" method = "POST" value = "Update"></form>
    <!--<form id="historyForm" onsubmit="return acall()" value = "History"></form>-->
    <div id="popup-content" class="popup-content">
        <div id="popup-contentheader"><span id="popup-contentheaderspan"></span></div>
        <img src="images/cancel_32px.png" alt="Close" class="close" onclick ="closePopUp()">
        <div class="block">
        <?php
            $queryIR="SELECT IRLabel,IRValue FROM dbo.InherentRisks ORDER BY IRValue;";
            $queryRefIR = $conn->query($queryIR);
            $resultIR = $queryRefIR->fetchAll(PDO::FETCH_ASSOC);
        ?>
            <label for="InherentRisk" class="unselectable" >&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbspInherent Risk:</label>
            <select id="InherentRisk" name="InherentRisk" class="dropdown" form="updateForm">

            <?php
            foreach($resultIR as $row){?>
                <option value="<?php echo $row["IRLabel"]; ?>"><?php echo $row["IRLabel"];?></option>
            <?php } ?>

            </select>
        </div>
        <div class="block">
        <?php
            $queryCR="SELECT CRLabel,CRValue FROM dbo.Controls ORDER BY CRValue;";
            $queryRefCR = $conn->query($queryCR);
            $resultCR = $queryRefCR->fetchAll(PDO::FETCH_ASSOC);
        ?>

            <label for="ControlRisk" class="unselectable">Controls Effectiveness:</label>

            <select id="ControlRisk" name="ControlRisk" class="dropdown" form="updateForm">

            <?php
            foreach($resultCR as $row){?>
                <option value="<?php echo $row["CRLabel"]; ?>"><?php echo $row["CRLabel"];?></option>
            <?php } ?>

            </select>
        </div>
        <div class="block">
            <input type="text" id="commentsID" name="commentsID" class="resizedTextbox" autocomplete="off" placeholder="Comments" form="updateForm">
        </div>
        <input type="hidden" id="RiskID" name="RiskID" value="" form="updateForm">
        <input type="hidden" id="BizID" name="BizID" value="" form="updateForm">

        <input type="hidden" id="RiskIDHistory" name="RiskIDHistory" value="" form="historyForm">
        <input type="hidden" id="BizIDHistory" name="BizIDHistory" value="" form="historyForm">

        <?php
            echo '<input type="hidden" id="UserID" name="UserID" value="'.$_SESSION['userid'].'" form="updateForm">';
        ?>
        <input type="hidden" id="VersionID" name="VersionID" value="" form="updateForm">
        <div>
            <button type="submit" id="submit" name="submit" class="button" id="submit" form="updateForm">Update</button>
        </div>
        <div id="divHistory">
            <form onsubmit="return acall()" >  
                <button type="submit" value="Show History" href ="#" id="history" name="submit" class="history" onclick="openHistory();">
                <i class="fas fa-history fa-lg"></i><span>  History</span>
                </button>
            </form>
        </div>

        <div id="divCloseHistory">
            <a  href="#" class="closehistory" onclick="closeHistory()"><i class="fas fa-history fa-lg"></i><span>  Close History</span></a>
            <div id="historyTableDiv">
                <div id="field_name"></div>
            </div>
        </div>
    </div>
</div>

<div class="popupHeaders">
    <form id="updateHeadersForm" name="updateHeadersForm" method = "POST"></form>
    <!--<form id="historyForm" onsubmit="return acall()" value = "History"></form>-->
    <div id="popup-content-Headers" class="popup-content-Headers">
        <div id="popup-content-Headersheader"></div>
        <img src="images/cancel_32px.png" alt="Close" class="close" onclick ="closePopUpHeaders()">

        <div id="tabs" class="tabs">
            <div class="stickyheader">
            <div class="tab-header">
                <div class="active">Businesses</div>
                <div>Risks</div>
            </div>
            <div class="tab-indicator"></div>
            </div>

            <div class="tab-body">
                <div class="active">
                    <div id="sortableDiv" class="addHeaders">
                    <p class="editGroups" onclick="editGroups('biz')"><i class="fas fa-arrow-left"></i> Edit Groups</p>
                        <input type="text" id="addBiz" name="addBiz" class="addHeader" autocomplete="off" placeholder="Add Business...">
                        <select id="headerDropdownBiz" name="headerDropdown" class="headerDropdown" style='font-size: 10pt;'>

                            <?php
                            $queryBizGroup="SELECT BizMetaID,BizGroupID,BizGroupShortDesc,BizGroupLongDesc,Ordering FROM dbo.BizGroup ORDER BY Ordering;";
                            $queryRefBizGroup = $conn->query($queryBizGroup);
                            $resultBizGroup = $queryRefBizGroup->fetchAll(PDO::FETCH_ASSOC);

                            foreach($resultBizGroup as $row){?>

                            <option id="" value="<?php echo $row["BizGroupID"]; ?>"><?php echo $row["BizGroupShortDesc"];?></option>

                            <?php } ?>

                        </select>
                        <button id="biz_submit" name="biz_submit" class="biz_submit">Submit</button>
                        <p id="successHeaderBiz" class="successHeader"></p>
                    </div>
                    
                    <br>
                    <?php
                    foreach($resultBizGroup as $row){
                    $queryBiz = $conn->prepare("SELECT dbo.Biz.BizID, dbo.Biz.BizGroupID,dbo.BizGroup.BizGroupShortDesc,dbo.Biz.BizShortDesc,dbo.Biz.Ordering FROM dbo.Biz
                    INNER JOIN dbo.BizGroup
                    ON dbo.Biz.BizGroupID = dbo.BizGroup.BizGroupID
                    WHERE dbo.Biz.BizGroupID = ? ORDER BY Ordering;");
                    $queryBiz->execute([$row["BizGroupID"]]); 
                    $resultBiz = $queryBiz->fetchAll(PDO::FETCH_ASSOC);
                    ?>

                    <p class="groupHeader"><?php echo $row["BizGroupShortDesc"] ?></p>
                    <div id="sortableDiv" class="sortableDiv">
                        <ul id="bizlist<?php echo $row["BizGroupID"]?>" class="sortableBiz">
                            <?php foreach($resultBiz as $rowBiz){
                            $bizarray = array($rowBiz["BizID"], $rowBiz["Ordering"]);
                            if ($rowBiz["BizShortDesc"] != ""){
                            ?>
                            <li id="biz<?php echo $rowBiz["BizID"] ?>" value="biz<?php echo $rowBiz["BizID"] ?>"><span class="arrow" style="cursor:pointer"><i class="fas fa-arrows-alt-v"></i></span><span class="liText" contenteditable="true"><?php echo $rowBiz["BizShortDesc"] ?></span><span onclick="deletebiz('<?php echo $rowBiz['BizID'] ?>','<?php echo $rowBiz['BizShortDesc'] ?>')" class="deleteBiz" name="deleteBiz" id="<?php echo $rowBiz["BizID"] ?>" value="<?php echo $rowBiz["BizShortDesc"] ?>" style="cursor:pointer"><i class="fas fa-times"></i></span></li>
                            <?php
                            }else{
                            ?>
                                <li id="biz<?php echo $rowBiz["BizID"] ?>" value="biz<?php echo $rowBiz["BizID"] ?>"><span class="arrow" style="cursor:pointer"><i class="fas fa-arrows-alt-v"></i></span><span class="liTextEmptyBiz" contenteditable="true"></span><span onclick="deletebiz('<?php echo $rowBiz['BizID'] ?>','<?php echo $rowBiz['BizShortDesc'] ?>')" class="deleteBiz" name="deleteBiz" id="<?php echo $rowBiz["BizID"] ?>" value="<?php echo $rowBiz["BizShortDesc"] ?>" style="cursor:pointer"><i class="fas fa-times"></i></span></li>
                            <?php
                            }
                            }
                            ?>
                        </ul>
                    </div>
                    <br>  
                    <?php
                    }?>
                </div>
            
                <div style="display:none;">    
                    <div id="sortableDiv" class="addHeaders">
                        <p class="editGroups" onclick="editGroups('risk')"><i class="fas fa-arrow-left"></i> Edit Groups</p>
                            <input type="text" id="addRisk" name="addRisk" class="addHeader" autocomplete="off" placeholder="Add Risk...">
                            <select id="headerDropdownRisk" name="headerDropdown" class="headerDropdown" style='font-size: 10pt;'>

                            <?php
                            $queryRiskGroup="SELECT RiskMetaID,RiskGroupID,RiskGroupShortDesc,RiskGroupLongDesc,Ordering FROM dbo.RiskGroup ORDER BY Ordering;";
                            $queryRefRiskGroup = $conn->query($queryRiskGroup);
                            $resultRiskGroup = $queryRefRiskGroup->fetchAll(PDO::FETCH_ASSOC);

                            foreach($resultRiskGroup as $row){?>

                            <option id="" value="<?php echo $row["RiskGroupID"]; ?>"><?php echo $row["RiskGroupShortDesc"];?></option>

                            <?php } ?>
        

                            </select>
                            <button id="risk_submit" name="risk_submit" class="risk_submit">Submit</button>
                            <p id="successHeaderRisk" class="successHeader"></p>
                        </div>
                        
                        <br>
                        <?php
                        foreach($resultRiskGroup as $row){
                        $queryRisk = $conn->prepare("SELECT dbo.Risks.RiskID, dbo.Risks.RiskGroupID,dbo.RiskGroup.RiskGroupShortDesc,dbo.Risks.RiskShortDesc,dbo.Risks.Ordering FROM dbo.Risks
                        INNER JOIN dbo.RiskGroup
                        ON dbo.Risks.RiskGroupID = dbo.RiskGroup.RiskGroupID
                        WHERE dbo.Risks.RiskGroupID = '".$row["RiskGroupID"]."' ORDER BY Ordering;");
                        $queryRisk->execute([$row["RiskGroupID"]]); 
                        $resultRisk = $queryRisk->fetchAll(PDO::FETCH_ASSOC);
                        ?>
                        <p class="groupHeader"><?php echo $row["RiskGroupShortDesc"] ?></p>
                        <div id="sortableDiv" class="sortableDiv">
                            <ul id="risklist<?php echo $row["RiskGroupID"]?>" class="sortableRisk">

                                <?php foreach($resultRisk as $rowRisk){
                                $riskarray = array($rowRisk["RiskID"], $rowRisk["Ordering"]);
                                if ($rowRisk["RiskShortDesc"] != ""){
                                ?>
                                    <li id="risk<?php echo $rowRisk["RiskID"] ?>" value="risk<?php echo $rowRisk["RiskID"] ?>"><span class="arrow" style="cursor:pointer"><i class="fas fa-arrows-alt-v"></i></span><span class="liText" contenteditable="true"><?php echo $rowRisk["RiskShortDesc"] ?></span><span onclick="deleterisk('<?php echo $rowRisk['RiskID'] ?>','<?php echo $rowRisk['RiskShortDesc'] ?>')" class="deleteRisk" name="deleteRisk" id="<?php echo $rowRisk["RiskID"] ?>" value="<?php echo $rowRisk["RiskShortDesc"] ?>" style="cursor:pointer"><i class="fas fa-times"></i></span></li>
                                <?php
                                }else{
                                ?>
                                    <li id="risk<?php echo $rowRisk["RiskID"] ?>" value="risk<?php echo $rowRisk["RiskID"] ?>"><span class="arrow" style="cursor:pointer"><i class="fas fa-arrows-alt-v"></i></span><span class="liTextEmptyRisk" contenteditable="true"></span><span onclick="deleterisk('<?php echo $rowRisk['RiskID'] ?>','<?php echo $rowRisk['RiskShortDesc'] ?>')" class="deleteRisk" name="deleteRisk" id="<?php echo $rowRisk["RiskID"] ?>" value="<?php echo $rowRisk["RiskShortDesc"] ?>" style="cursor:pointer"><i class="fas fa-times"></i></span></li>
                                <?php
                                }
                                }
                                ?>
                            </ul>

                        </div>
                        <br>  
                        <?php
                        }?>
                    </div>
                </div>
            </div>

        <!-- New Tab for Groups ///////////////////////////////////////////////////////////////////////////////////////////////////////////-->
        <div id="tabsGroup" class="tabsGroup">
            <div class="stickyheader">
            <div class="tab-header-group">
                <div class="active">Businesses</div>
                <div>Risks</div>
            </div>
            <div class="tab-indicator-group"></div>
            </div>

            <div class="tab-body-group">
                <div class="active">
                    <div id="sortableDiv" class="addHeaders">
                        <p class="editGroups" onclick="editHeader('biz')"><i class="fas fa-arrow-left"></i> Edit Headers</p>
                        <input type="text" id="addBizGroup" name="addBizGroup" class="addHeader" autocomplete="off" placeholder="Add Business Group...">
                        <button id="bizGroup_submit" name="bizGroup_submit" class="bizGroup_submit">Submit</button>
                        <p id="successHeaderBizGroup" class="successHeader"></p>
                    </div>
                    <br>
                    <?php
                    $queryBizMeta="SELECT BizMetaID,BizMetaShortDesc FROM dbo.BizMeta";
                    $queryRefBizMeta = $conn->query($queryBizMeta);
                    $resultBizMeta = $queryRefBizMeta->fetchAll(PDO::FETCH_ASSOC);
                    foreach($resultBizMeta as $row){
                        $queryBiz = $conn->prepare("SELECT BizGroupID,BizMetaID,BizGroupShortDesc,Ordering FROM dbo.BizGroup WHERE BizMetaID = ? ORDER BY Ordering;");
                        $queryBiz->execute([$row["BizMetaID"]]); 
                        $resultBiz = $queryBiz->fetchAll(PDO::FETCH_ASSOC);
                        ?>

                        <div id="sortableDiv" class="sortableDiv">
                            <ul id="bizGrouplist<?php echo $row["BizMetaID"]?>" class="sortableBizGroup">
                                <?php foreach($resultBiz as $rowBiz){
                                $bizarray = array($rowBiz["BizGroupID"], $rowBiz["Ordering"]);
                                ?>
                                <li id="bizGroup<?php echo $rowBiz["BizGroupID"] ?>" value="bizGroup<?php echo $rowBiz["BizGroupID"] ?>"><span class="arrow" style="cursor:pointer"><i class="fas fa-arrows-alt-v"></i></span><span class="liText" contenteditable="true"><?php echo $rowBiz["BizGroupShortDesc"] ?></span><span onclick="deletebizGroup('<?php echo $rowBiz['BizGroupID'] ?>','<?php echo $rowBiz['BizGroupShortDesc'] ?>')" class="deleteBizGroup" name="deleteBizGroup" id="<?php echo $rowBiz["BizGroupID"] ?>" value="<?php echo $rowBiz["BizGroupShortDesc"] ?>" style="cursor:pointer"><i class="fas fa-times"></i></span></li>
                                <?php
                                }
                                ?>
                            </ul>
                        </div>
                        <br>  
                    <?php
                    }?>
                </div>
            
                <div style="display:none;">    
                    <div id="sortableDiv" class="addHeaders">
                        <p class="editGroups" onclick="editHeader('risk')"><i class="fas fa-arrow-left"></i> Edit Headers</p>
                        <input type="text" id="addRiskGroup" name="addRiskGroup" class="addHeader" autocomplete="off" placeholder="Add Risk Group...">
                        <button id="riskGroup_submit" name="riskGroup_submit" class="riskGroup_submit">Submit</button>
                        <p id="successHeaderRiskGroup" class="successHeader"></p>
                    </div>
                        
                    <br>
                    <?php
                    $queryRiskMeta="SELECT RiskMetaID,RiskMetaShortDesc FROM dbo.RiskMeta";
                    $queryRefRiskMeta = $conn->query($queryRiskMeta);
                    $resultRiskMeta = $queryRefRiskMeta->fetchAll(PDO::FETCH_ASSOC);
                    foreach($resultRiskMeta as $row){
                    $queryRiskGroup = $conn->prepare("SELECT RiskGroupID,RiskGroupShortDesc,RiskMetaID, Ordering FROM dbo.RiskGroup WHERE RiskMetaID = ? ORDER BY Ordering;");
                    $queryRiskGroup->execute([$row["RiskMetaID"]]); 
                    $resultRiskGroup = $queryRiskGroup->fetchAll(PDO::FETCH_ASSOC);
                    ?>

                    <div id="sortableDiv" class="sortableDiv">
                        <ul id="riskGrouplist<?php echo $row["RiskMetaID"]?>" class="sortableRiskGroup">
                            <?php foreach($resultRiskGroup as $rowRisk){
                            $riskarray = array($rowRisk["RiskGroupID"], $rowRisk["Ordering"]);
                            ?>
                            <li id="riskGroup<?php echo $rowRisk["RiskGroupID"] ?>" value="riskGroup<?php echo $rowRisk["RiskGroupID"] ?>"><span class="arrow" style="cursor:pointer"><i class="fas fa-arrows-alt-v"></i></span><span class="liText" contenteditable="true"><?php echo $rowRisk["RiskGroupShortDesc"] ?></span><span onclick="deleteriskGroup('<?php echo $rowRisk['RiskGroupID'] ?>','<?php echo $rowRisk['RiskGroupShortDesc'] ?>')" class="deleteRiskGroup" name="deleteRiskGroup" id="<?php echo $rowRisk["RiskGroupID"] ?>" value="<?php echo $rowRisk["RiskGroupShortDesc"] ?>" style="cursor:pointer"><i class="fas fa-times"></i></span></li>
                            <?php
                            }
                            ?>
                        </ul>
                    </div>
                    <br> 
                    <?php
                    }?> 
                </div>
            </div>
        </div>
    </div>
    <script>

    function editGroups(header){
        if(header == "biz"){
            document.getElementById("tabs").style.display = "none";
            document.getElementById("tabsGroup").style.display = "block";
        }else{
            document.getElementById("tabs").style.display = "none";
            document.getElementById("tabsGroup").style.display = "block";

            //Need to turn off Business Group screen and show Risk Group

            // Similar for Biz 
        }
    }

    function editHeader(header){
        if(header == "biz"){            
            document.getElementById("tabsGroup").style.display = "none";
            document.getElementById("tabs").style.display = "block";

        }else{
            document.getElementById("tabsGroup").style.display = "none";
            document.getElementById("tabs").style.display = "block";
        }
    }

    function deletebiz(bizID_,bizShortDesc_){
        var bizID = bizID_;
        var bizShortName = String(bizShortDesc_);

        if(confirm('Are you sure you want to delete ' + bizShortName + '? All data and history will be lost.')){
            $.ajax({
                type: "POST",
                url: "./includes/editHeaders.php",
                data: {deletebizID:bizID},
                success: function(data){                    
                    $('#biz' + bizID).remove();  
                }
            });
            document.getElementById("successHeaderBiz").innerHTML = bizShortName + " has been succesfully removed";  
            document.getElementById("successHeaderBiz").style.display = "block";    
        };
    }

    function deletebizGroup(bizGroupID_,bizGroupShortDesc_){
        var bizGroupID = bizGroupID_;
        var bizGroupShortName = String(bizGroupShortDesc_);

        if(confirm('Are you sure you want to delete ' + bizGroupShortName + '? All data and history will be lost for sub businesses.')){
            $.ajax({
                type: "POST",
                url: "./includes/editHeaders.php",
                data: {deletebizGroupID:bizGroupID},
                success: function(result){      
                    alert(result);             
                    $('#bizGroup' + bizGroupID).remove();  
                }
            });
            document.getElementById("successHeaderBizGroup").innerHTML = bizGroupShortName + " has been succesfully removed";  
            document.getElementById("successHeaderBizGroup").style.display = "block";    
        };
    }

    function deleterisk(riskID_,riskShortDesc_){
        var riskID = riskID_;
        var riskShortName = String(riskShortDesc_);

        if(confirm('Are you sure you want to delete ' + riskShortName + '? All data and history will be lost.')){
            $.ajax({
                type: "POST",
                url: "./includes/editHeaders.php",
                data: {deleteriskID:riskID},
                success: function(data){                 
                    $('#risk' + riskID).remove();  
                }
            });
            document.getElementById("successHeaderRisk").innerHTML = riskShortName + " has been succesfully removed";  
            document.getElementById("successHeaderRisk").style.display = "block";    
        };
    }

    function deleteriskGroup(riskGroupID_,riskGroupShortDesc_){
        var riskGroupID = riskGroupID_;
        var riskGroupShortName = String(riskGroupShortDesc_);

        if(confirm('Are you sure you want to delete ' + riskGroupShortName + '? All data and history will be lost for sub risks.')){
            $.ajax({
                type: "POST",
                url: "./includes/editHeaders.php",
                data: {deleteriskGroupID:riskGroupID},
                success: function(result){                
                    $('#riskGroup' + riskGroupID).remove();  
                }
            });
            document.getElementById("successHeaderRiskGroup").innerHTML = riskGroupShortName + " has been succesfully removed";  
            document.getElementById("successHeaderRiskGroup").style.display = "block";    
        };
    }

    $(document).ready(function(){
        $('.biz_submit').click(function(){

            if (document.getElementById("addBiz").value === ""){
                document.getElementById("successHeaderBiz").innerHTML = "Please enter a valid Business"; 
                document.getElementById("successHeaderBiz").style.color = "red"; 
                document.getElementById("successHeaderBiz").style.display = "block";   
            }else{ 
                var bizName = document.getElementById("addBiz").value;
                var bizGroupID_ = document.getElementById("headerDropdownBiz").value;

                var bizGroup = document.getElementById("headerDropdownBiz");
                var bizGroupName_ = bizGroup.options[bizGroup.selectedIndex].text;
                
                $.ajax({
                    type: "POST",
                    url: "./includes/editHeaders.php",
                    data : {bizShortDesc:bizName, bizGroupID:bizGroupID_},
                    success: function(result){            
                        var ul = document.getElementById('bizlist' + bizGroupID_);
                        var li = document.createElement("li");
                        var proc = '<span class="arrow"><i class="fas fa-arrows-alt-v" style="cursor:pointer"></i></span><span class="liText" contenteditable="true">'+bizName+'</span><span onclick="deletebiz(\''+ result +'\',\''+ bizName +'\')" class="deleteBiz" name="deleteBiz" id="'+ result +'" value="'+ bizName +'" style="cursor:pointer"><i class="fas fa-times"></i></span></li>';

                        li.setAttribute("id", "biz"+result);
                        $("li").attr("value","biz"+result);
                        li.innerHTML = proc;
                        ul.appendChild(li);
                    }
                });
                    document.getElementById("addBiz").value = "";   
                    document.getElementById("successHeaderBiz").style.color = "#5adb27"; 
                    document.getElementById("successHeaderBiz").innerHTML = bizName+ " has been succesfully added to " + bizGroupName_;  
                    document.getElementById("successHeaderBiz").style.display = "block";        
            }
        });
    });

    $(document).ready(function(){
        $('.bizGroup_submit').click(function(){
            if (document.getElementById("addBizGroup").value === ""){
                document.getElementById("successHeaderBizGroup").innerHTML = "Please enter a valid Business Group"; 
                document.getElementById("successHeaderBizGroup").style.color = "red"; 
                document.getElementById("successHeaderBizGroup").style.display = "block";   
            }
            else{
            var bizGroupName = document.getElementById("addBizGroup").value;
            
            /////Need Meta???
            //var bizGroupID_ = document.getElementById("headerDropdownBiz").value;
            // need to get UL list
            var bizMetaID_ = "1";

            //var bizGroup = document.getElementById("headerDropdownBiz");
            //var bizGroupName_ = bizGroup.options[bizGroup.selectedIndex].text;
            
            $.ajax({
                type: "POST",
                url: "./includes/editHeaders.php",
                data : {bizGroupShortDesc:bizGroupName, bizMetaID:bizMetaID_},
                success: function(result){                           
                    var ul = document.getElementById('bizGrouplist' + bizMetaID_);
                    var li = document.createElement("li");
                    var proc = '<span class="arrow"><i class="fas fa-arrows-alt-v" style="cursor:pointer"></i></span><span class="liText" contenteditable="true">'+bizGroupName+'</span><span onclick="deletebizGroup(\''+ result +'\',\''+ bizGroupName +'\')" class="deleteBizGroup" name="deleteBizGroup" id="'+ result +'" value="'+ bizGroupName +'" style="cursor:pointer"><i class="fas fa-times"></i></span></li>';

                    li.setAttribute("id", "bizGroup"+result);
                    $("li").attr("value","bizGroup"+result);
                    li.innerHTML = proc;
                    ul.appendChild(li);
                }
            });
                document.getElementById("addBizGroup").value = "";   
                document.getElementById("successHeaderBizGroup").style.color = "#5adb27"; 
                document.getElementById("successHeaderBizGroup").innerHTML = bizGroupName+ " has been succesfully added";  
                document.getElementById("successHeaderBizGroup").style.display = "block";        
            }
        });
    });

    $(document).ready(function(){
        $('.risk_submit').click(function(){
            if (document.getElementById("addRisk").value === ""){
                document.getElementById("successHeaderRisk").innerHTML = "Please enter a valid Risk"; 
                document.getElementById("successHeaderRisk").style.color = "red"; 
                document.getElementById("successHeaderRisk").style.display = "block";   
            }else{
            var riskName = document.getElementById("addRisk").value;
            var riskGroupID_ = document.getElementById("headerDropdownRisk").value;

            var riskGroup = document.getElementById("headerDropdownRisk");
            var riskGroupName_ = riskGroup.options[riskGroup.selectedIndex].text;
            
            $.ajax({
                type: "POST",
                url: "./includes/editHeaders.php",
                data : {riskShortDesc:riskName, riskGroupID:riskGroupID_},
                success: function(result){            
                    var ul = document.getElementById('risklist' + riskGroupID_);
                    var li = document.createElement("li");
                    var proc = '<span class="arrow"><i class="fas fa-arrows-alt-v" style="cursor:pointer"></i></span><span class="liText" contenteditable="true">'+riskName+'</span><span onclick="deleterisk(\''+ result +'\',\''+ riskName +'\')" class="deleteRisk" name="deleteRisk" id="'+ result +'" value="'+ riskName +'" style="cursor:pointer"><i class="fas fa-times"></i></span></li>';

                    li.setAttribute("id", "risk"+result);
                    $("li").attr("value","risk"+result);
                    li.innerHTML = proc;
                    ul.appendChild(li);
                }
            });  
                document.getElementById("addRisk").value = "";  
                document.getElementById("successHeaderRisk").style.color = "#5adb27"; 
                document.getElementById("successHeaderRisk").innerHTML = riskName + " has been succesfully added to " + riskGroupName_;  
                document.getElementById("successHeaderRisk").style.display = "block";       
            }});
    });

    $(document).ready(function(){
        $('.riskGroup_submit').click(function(){
            if (document.getElementById("addRiskGroup").value === ""){
                document.getElementById("successHeaderRiskGroup").innerHTML = "Please enter a valid Risk Group"; 
                document.getElementById("successHeaderRiskGroup").style.color = "red"; 
                document.getElementById("successHeaderRiskGroup").style.display = "block";   
            }
            else{
            var riskGroupName = document.getElementById("addRiskGroup").value;
            /////Need Meta???
            //var bizGroupID_ = document.getElementById("headerDropdownBiz").value;
            // need to get UL list
            var riskMetaID_ = "1";

            //var bizGroup = document.getElementById("headerDropdownBiz");
            //var bizGroupName_ = bizGroup.options[bizGroup.selectedIndex].text;
            
            $.ajax({
                type: "POST",
                url: "./includes/editHeaders.php",
                data : {riskGroupShortDesc:riskGroupName, riskMetaID:riskMetaID_},
                success: function(result){                     
                    var ul = document.getElementById('riskGrouplist' + riskMetaID_);
                    var li = document.createElement("li");
                    var proc = '<span class="arrow"><i class="fas fa-arrows-alt-v" style="cursor:pointer"></i></span><span class="liText" contenteditable="true">'+riskGroupName+'</span><span onclick="deleteriskGroup(\''+ result +'\',\''+ riskGroupName +'\')" class="deleteRiskGroup" name="deleteRiskGroup" id="'+ result +'" value="'+ riskGroupName +'" style="cursor:pointer"><i class="fas fa-times"></i></span></li>';

                    li.setAttribute("id", "riskGroup"+result);
                    $("li").attr("value","riskGroup"+result);
                    li.innerHTML = proc;
                    ul.appendChild(li);
                }
            });
                document.getElementById("addRiskGroup").value = "";   
                document.getElementById("successHeaderRiskGroup").style.color = "#5adb27"; 
                document.getElementById("successHeaderRiskGroup").innerHTML = riskGroupName+ " has been succesfully added";  
                document.getElementById("successHeaderRiskGroup").style.display = "block";        
            }
        });
    });


    $(document).on("click",'li[id^="biz"]',function(e){
        placeCaretAtEnd($("[contenteditable]",this).focus()[0])

        if (!e) {
            e = window.event;
        }
        if (e.preventDefault) {
            e.preventDefault();
        } else {
            e.returnValue = false;
        }
    })

    $(document).on("click",'li[id^="risk"]',function(e){
        placeCaretAtEnd($("[contenteditable]",this).focus()[0])

        if (!e) {
            e = window.event;
        }
        if (e.preventDefault) {
            e.preventDefault();
        } else {
            e.returnValue = false;
        }
    })

    $('li[id^="biz"]').keypress(function(e){ return e.which != 13; });
    $('li[id^="risk"]').keypress(function(e){ return e.which != 13; });

    $(document).on("blur",'li[id^="biz"]',function(){
        
        var bizID = $(this).attr('id');
        if(bizID.startsWith("bizGroup")){
            return;
        }
        else{
            var ID = bizID.split('biz')[1]

            var bizName =  $(this).text();
            $.ajax({
                data: {bizUpdatedName:bizName, bizID:ID},
                type: "POST",
                url: "./includes/editHeaders.php"
            });
        }
    })

    $(document).on("blur",'li[id^="risk"]',function(){
        var riskID = $(this).attr('id');
        if(riskID.startsWith("riskGroup")){
            return;
        }else{
            var ID = riskID.split('risk')[1]

            var riskName =  $(this).text();
            $.ajax({
                data: {riskUpdatedName:riskName, riskID:ID},
                type: "POST",
                url: "./includes/editHeaders.php"
            });
        }
    })

    $(document).on("blur",'li[id^="bizGroup"]',function(){
        var bizID = $(this).attr('id');
        var ID = bizID.split('bizGroup')[1]

        var bizName = $(this).text();
        $.ajax({
            data: {bizGroupUpdatedName:bizName, bizID:ID},
            type: "POST",
            url: "./includes/editHeaders.php"
        });
    })

    $(document).on("blur",'li[id^="riskGroup"]',function(){
        var riskID = $(this).attr('id');
        var ID = riskID.split('riskGroup')[1]

        var riskName =  $(this).text();
        $.ajax({
            data: {riskGroupUpdatedName:riskName, riskID:ID},
            type: "POST",
            url: "./includes/editHeaders.php"
        });
    })

    $( function() {
        $( ".sortableBiz" ).sortable({
            update: function(e, ui) {
                var groupID = $(this).attr('id');
                var idsInOrder_ = $("#bizlist"+groupID.split('bizlist')[1]).sortable('toArray', { attribute: 'id' });
                
                $.ajax({
                    data: {idsInOrderBiz:idsInOrder_},
                    type: "POST",
                    url: "./includes/editHeaders.php"
                });
            }
        });
        $( ".sortableBiz" ).disableSelection();
    });

    $( function() {
        $( ".sortableBizGroup" ).sortable({
            update: function(e, ui) {
                var groupID = $(this).attr('id');
                var idsInOrder_ = $("#bizGrouplist"+groupID.split('bizGrouplist')[1]).sortable('toArray', { attribute: 'id' });
                $.ajax({
                    data: {idsInOrderBizGroup:idsInOrder_},
                    type: "POST",
                    url: "./includes/editHeaders.php"
                });
            }
        });
        $( ".sortableBizGroup" ).disableSelection();
    });

    $( function() {
        $( ".sortableRisk" ).sortable({
            update: function(e, ui) {
                var groupID = $(this).attr('id');
                var idsInOrder_ = $("#risklist"+groupID.split('risklist')[1]).sortable('toArray', { attribute: 'id' });
                
                $.ajax({
                    data: {idsInOrderRisk:idsInOrder_},
                    type: "POST",
                    url: "./includes/editHeaders.php"
                });
            }
        });
        $( ".sortableRisk" ).disableSelection();
    });

    $( function() {
        $( ".sortableRiskGroup" ).sortable({
            update: function(e, ui) {
                var groupID = $(this).attr('id');
                var idsInOrder_ = $("#riskGrouplist"+groupID.split('riskGrouplist')[1]).sortable('toArray', { attribute: 'id' });
                
                $.ajax({
                    data: {idsInOrderRiskGroup:idsInOrder_},
                    type: "POST",
                    url: "./includes/editHeaders.php"
                });
            }
        });
        $( ".sortableRiskGroup" ).disableSelection();
    });

    function placeCaretAtEnd(el) {
        el.focus();
        if (typeof window.getSelection != "undefined"
            && typeof document.createRange != "undefined") {
            var range = document.createRange();
            range.selectNodeContents(el);
            range.collapse(false);
            var sel = window.getSelection();
            sel.removeAllRanges();
            sel.addRange(range);
        } else if (typeof document.body.createTextRange != "undefined") {
            var textRange = document.body.createTextRange();
            textRange.moveToElementText(el);
            textRange.collapse(false);
            textRange.select();
        }
    }

    let tabHeader = document.getElementsByClassName("tab-header")[0];
    let tabIndicator = document.getElementsByClassName("tab-indicator")[0];
    let tabBody = document.getElementsByClassName("tab-body")[0];
    let tabsPane = tabHeader.getElementsByTagName("div");

    for(let i=0;i<tabsPane.length;i++){
    tabsPane[i].addEventListener("click",function(){
        tabHeader.getElementsByClassName("active")[0].classList.remove("active");
        tabsPane[i].classList.add("active");
        tabBody.getElementsByClassName("active")[0].style.display = "none";
        tabBody.getElementsByClassName("active")[0].classList.remove("active");

        tabBody.querySelectorAll("div:not(#sortableDiv)")[i].classList.add("active");
        tabBody.querySelectorAll("div:not(#sortableDiv)")[i].style.display = "block";
        tabIndicator.style.left = `calc(calc(100% / 2) * ${i})`;

        document.getElementById("successHeaderBiz").style.display = "none";   
        document.getElementById("successHeaderRisk").style.display = "none";   
    });
    }

    let tabHeaderGroup = document.getElementsByClassName("tab-header-group")[0];
    let tabIndicatorGroup = document.getElementsByClassName("tab-indicator-group")[0];
    let tabBodyGroup = document.getElementsByClassName("tab-body-group")[0];
    let tabsPaneGroup = tabHeaderGroup.getElementsByTagName("div");

    
    for(let i=0;i<tabsPaneGroup.length;i++){
    tabsPaneGroup[i].addEventListener("click",function(){
        tabHeaderGroup.getElementsByClassName("active")[0].classList.remove("active");
        tabsPaneGroup[i].classList.add("active");
        tabBodyGroup.getElementsByClassName("active")[0].style.display = "none";
        tabBodyGroup.getElementsByClassName("active")[0].classList.remove("active");

        tabBodyGroup.querySelectorAll("div:not(#sortableDiv)")[i].classList.add("active");
        tabBodyGroup.querySelectorAll("div:not(#sortableDiv)")[i].style.display = "block";
        tabIndicatorGroup.style.left = `calc(calc(100% / 2) * ${i})`;

        document.getElementById("successHeaderBizGroup").style.display = "none";   
        document.getElementById("successHeaderRiskGroup").style.display = "none";   
    });
    }
    </script>
</div>
