function showMessageModalPopup() {
    var modalPopupBehavior = $find('modalPopupBehaviourError');
    modalPopupBehavior.show();
}

function hideMessageModalPopup() {
    var modalPopupBehavior = $find('modalPopupBehaviourError');
    modalPopupBehavior.hide();
}
function showWaitModalPopup() {
    var modalPopupBehavior = $find('modalPopupBehaviourWait');
    modalPopupBehavior.show();
}

function hideWaitModalPopup() {
    var modalPopupBehavior = $find('modalPopupBehaviourWait');
    modalPopupBehavior.hide();
}
function ShowPreLoaderMasterpage() {
    var modalPopupBehavior = $find('modalPopupBehaviourMaster');
    modalPopupBehavior.show();
}

function hidePreLoaderMasterpage() {
    var modalPopupBehavior = $find('modalPopupBehaviourMaster');
    modalPopupBehavior.hide();
}

function jsPreloaderPostBack(ctrlToDoPostBack) {
    showWaitModalPopup();
    __doPostBack(ctrlToDoPostBack, '');
}
function OpenMultiPickHelp(argsType, txtFld, hidFld) {
    var jv_search = document.getElementById(txtFld).value;
    var jv_Modal = "HelpDialogs/MultipickHelp.aspx?code=" + jv_search + "&type=" + argsType
    var jv_Pro = window.showModalDialog(jv_Modal);

    if (typeof (jv_Pro) != "Undefined") {
        document.getElementById(hidFld).value = jv_Pro;
        document.getElementById(txtFld).value = jv_Pro;
    }
}

function OpenSinglePickHelp(argsType, txtFld) {
    var jv_selected = document.getElementById(txtFld).value;
    var jv_Modal = "HelpDialogs/SinglepickHelp.aspx?code=" + jv_selected + "&type=" + argsType
    var jv_Pro = window.showModalDialog(jv_Modal);

    if (typeof (jv_Pro) != "Undefined") {
        jv_Pro = jv_Pro.split("|");
        document.getElementById(txtFld).value = jv_Pro[0];
    }
}

function bosConfirmation(argsTableId, argsType) {
    if (document.getElementById(argsTableId) != null) {
        var jvTabLength = document.getElementById(argsTableId).rows.length;
        if (jvTabLength > 0) {
            var jvString = "Delete"
            if (argsType == "S") {
                jvString = "Suspend"
            }

            var jvRes = confirm("Are You Sure to " + jvString + " this Record\n\nPress Ok to Continue.... Cancel to Break....");
            return (jvRes);
        }
    }
    else {
        return false;
    }
}
function jsValidateParaDtls(tabID, rowIndex) {

    var jv_Per = document.getElementById(tabID).rows[parseInt(rowIndex) + 1].cells[1].children;
    var jv_val = document.getElementById(tabID).rows[parseInt(rowIndex) + 1].cells[2].children;
    var jv_perval = jv_Per[0].value;
    var jv_value = jv_val[0].value;

    if (TrimAll(jv_perval) == "") {      
        document.getElementById(tabID).rows[parseInt(rowIndex) + 1].cells[1].children[0].value = "0";
        return false;
    }
    if (TrimAll(jv_value) == "") {       
        document.getElementById(tabID).rows[parseInt(rowIndex) + 1].cells[2].children[0].value = "0";
        return false;
    }
}


function jsValidateData(tabID, argsColsCnt) {
    var TabLength;
    var jvEditedvalue;
    var jvRowData;
    var jvRowData1;
    var jvString;
    var jvString1;
    var jvLen;
    var jvMsgString;
    var jvColumnsCount = argsColsCnt;
    if (document.getElementById(tabID) != null) {
        TabLength = document.getElementById(tabID).rows.length;
    }
    for (var Row = 1; Row < parseInt(TabLength); Row++) {
        for (var i = 1; i <= jvColumnsCount; i++) {
            jvRowData = document.getElementById(tabID).rows[Row].cells[i].children;
            jvString = jvRowData[0].value;
            if (isEmpty(jvString)) {
                alert("Code and Description at Row -> " + Row + "  : cannot be blank....");
                jvRowData[0].focus();
                return false;
            }
            else {
                jvLen = jvString.length;
                if (jvLen < 1) {
                    alert("Provided Value to this field is too short....\n\n at least 2 characters are expected");
                    jvRowData[0].focus();
                    return false;
                }
                else {
                    jvEditedvalue = jvString.toLowerCase();
                    for (var Row1 = 1; Row1 < parseInt(TabLength); Row1++) {
                        if (i == 1) { jvMsgString = "Code"; }
                        else { jvMsgString = "Description"; }

                        jvRowData1 = document.getElementById(tabID).rows[Row1].cells[i].children;
                        jvString1 = jvRowData1[0].value;
                        if ((jvString1.toLowerCase() == jvEditedvalue) && (Row != Row1)) {
                            alert(jvMsgString + " Already Exist");
                            jvRowData1[0].focus();
                            return false;
                        }
                    }
                }
            }
        }
    }
    // showWaitModalPopup()
}















function ShowEditModal(jvCode) {

    var frame = $get('IframeEdit');
    frame.src = jvCode
    $find('EditModalPopup').show();
}
function EditCancelScript() {

    var frame = $get('IframeEdit');
    modalPopupBehaviourMaster();
}
function EditOkayScript() {
    RefreshDataGrid();
    EditCancelScript();
}
function RefreshDataGrid() {
    $get('btnSearch1').click();
}
function NewExpanseOkay() {
    $get('btnSearch1').click();
}




function ShowEditModalUpload(jvCode) {

    var frame = $get('IframeEditUpload');
    frame.src = jvCode
    $find('EditModalPopupUpload').show();
}
function EditCancelScript() {

    var frame = $get('IframeEdit');
    modalPopupBehaviourMaster();
}
