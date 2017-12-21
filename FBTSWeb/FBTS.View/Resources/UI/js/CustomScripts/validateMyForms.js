/************************************************************************************
*	The Below Code Validates Form Data												*
************************************************************************************/

/************************************************************************************
*	The Below Function is to maximize the window to the available window size		*
************************************************************************************/
function MaxWindow() {
    window.moveTo(0, 0);
    window.resizeTo(screen.availWidth, screen.availHeight);
}

/************************************************************************************
*	The Below Function is to remove the space at the beginning of the textbox		*
************************************************************************************/
function trimField(txtField) {
    document.getElementById(txtField).value = TrimAll(document.getElementById(txtField).value);
}
function validateMandatoryField(txtfield, message) {   
    if (isEmpty(TrimAll(txtfield.val())) || txtfield.val()==null) {
        showCustomMessage("Not-Allowed", message, "Error");
        txtfield.focus();
        return false;
    }
    return true;
}
/************************************************************************************
*	The Below Function is to validate all entry textbox on keypresas based on the input:type *
************************************************************************************/
function validentry(type, evt) {
    if (type == "H") {
        //alphanumeric
        var nLastKey = (evt.which) ? evt.which : event.keyCode
        //nLastKey = window.event.keyCode;
        if (!(nLastKey >= 48 && nLastKey <= 57) && !(nLastKey >= 65 && nLastKey <= 90) && !(nLastKey >= 97 && nLastKey <= 122)) {
            if (nLastKey != 8)
                return false;
        }
        return true;
    }

    if (type == "BL") {
        window.event.keyCode = 0;
    }
    if (type == "N") {
        //Only Number
        // nLastKey = window.event.keyCode;
        var nLastKey = (evt.which) ? evt.which : event.keyCode
        if (!(nLastKey >= 48 && nLastKey <= 57) && (nLastKey != 46) || nLastKey == 46) {
            if (nLastKey != 8)
                return false;
        }

        return true;
    }

     if (type == "D") {
        //Only Decimals
        //nLastKey = window.event.keyCode;
        var nLastKey = (evt.which) ? evt.which : event.keyCode
        if (!(nLastKey >= 48 && nLastKey <= 57) && nLastKey != 46) {
            if (nLastKey != 8)
                return false;
        }

        return true;
    }
    if (type == "NM") {
        //only Alphabets , , .

        //nLastKey = window.event.keyCode;
        var nLastKey = (evt.which) ? evt.which : event.keyCode
        if (!(nLastKey >= 97 && nLastKey <= 122) && !(nLastKey >= 65 && nLastKey <= 90) && nLastKey != 46 && nLastKey != 44 && nLastKey != 32) {
            if (nLastKey != 8)
                return false;
        }

        return true;
    }

    if (type == "T") {
        //Only :
        //nLastKey = window.event.keyCode;
        var nLastKey = (evt.which) ? evt.which : event.keyCode

        if (!(nLastKey >= 48 && nLastKey <= 57) && nLastKey != 58) {
            if (nLastKey != 8)
                return false;
        }

        return true;
    }

    if (type == "A") {
        //only Alphabets
        //nLastKey = window.event.keyCode;
        var nLastKey = (evt.which) ? evt.which : event.keyCode
        if (!(nLastKey >= 97 && nLastKey <= 122) && !(nLastKey >= 65 && nLastKey <= 90) && nLastKey != 32) {
            if (nLastKey != 8)
                return false;
        }

        return true;
    }

    if (type == "AD") {
        //only Alphabets , , .
        //nLastKey = window.event.keyCode;
        var nLastKey = (evt.which) ? evt.which : event.keyCode

        if (!(nLastKey >= 97 && nLastKey <= 122) && !(nLastKey >= 65 && nLastKey <= 90) && nLastKey != 46 && nLastKey != 44) {
            if (nLastKey != 8)
                return false;
        }

        return true;
    }
    if (type == "SD") {
        //Only Single and double quotes not allowed
        //nLastKey = window.event.keyCode;
        var nLastKey = (evt.which) ? evt.which : event.keyCode
        if ((nLastKey == 34 || nLastKey == 39)) {
            if (nLastKey != 8)
                return false;
        }

        return true;
    }
    if (type == "HB") {
        //alphanumeric and back-slash
        //nLastKey = window.event.keyCode;
        var nLastKey = (evt.which) ? evt.which : event.keyCode
        if (!(nLastKey >= 47 && nLastKey <= 57) && !(nLastKey >= 65 && nLastKey <= 90) && !(nLastKey >= 97 && nLastKey <= 122)) {
            if (nLastKey != 8)
                return false;
        }

        return true;
    }

    if (type == "SP") //alphanumeric,back-slash,comma,#,dot,-,
    {
        //nLastKey = window.event.keyCode;
        var nLastKey = (evt.which) ? evt.which : event.keyCode
        if (!(nLastKey >= 44 && nLastKey <= 57) && !(nLastKey >= 65 && nLastKey <= 90) && !(nLastKey >= 97 && nLastKey <= 122) && nLastKey != 35 && nLastKey != 64 && nLastKey != 32) {
            if (nLastKey != 8)
                return false;
        }

        return true;
    }
    if (type == "SE") //alphanumeric,back-slash,comma,#,dot,-,
    {
        //nLastKey = window.event.keyCode;
        var nLastKey = (evt.which) ? evt.which : event.keyCode
        if (!(nLastKey >= 44 && nLastKey <= 57) && !(nLastKey >= 65 && nLastKey <= 90) && !(nLastKey >= 97 && nLastKey <= 122) && nLastKey != 45 && nLastKey != 64 && nLastKey != 95 && nLastKey != 46) {
            if (nLastKey != 8)
                return false;
        }

        return true;
    }

    if (type == "SC") //alphanumeric,back-slash,comma,#,dot,-,
    {
        //nLastKey = window.event.keyCode;
        var nLastKey = (evt.which) ? evt.which : event.keyCode
        if (nLastKey != 39) {
            if (!(nLastKey >= 44 && nLastKey <= 57) && !(nLastKey >= 65 && nLastKey <= 90) && !(nLastKey >= 97 && nLastKey <= 122) && nLastKey != 35 && nLastKey != 64 && nLastKey != 32) {
                if (nLastKey != 8)
                    return false;
            }

            return true;
        }
    }
    //valid Phone number
    if (type == "P") {
        //Only Number and -
        //nLastKey = window.event.keyCode;
        var nLastKey = (evt.which) ? evt.which : event.keyCode
        if (!(nLastKey >= 48 && nLastKey <= 57) && (nLastKey != 46 && nLastKey != 58 && nLastKey != 45) || nLastKey == 46) {
            if (nLastKey != 8)
                return false;
        }

        return true;
    }
    //valid email ID
    if (type == "E") {
        //alphanumeric,@,.
        //nLastKey = window.event.keyCode;
        var nLastKey = (evt.which) ? evt.which : event.keyCode
        if (!(nLastKey >= 48 && nLastKey <= 57) && !(nLastKey >= 65 && nLastKey <= 90) && !(nLastKey >= 97 && nLastKey <= 122) && nLastKey == 64 && nLastKey == 46) {
            if (nLastKey != 8)
                return false;
        }

        return true;
    }
    if (type == "SQ") {
        //Only Single quotes not allowed
        //nLastKey = window.event.keyCode;
        var nLastKey = (evt.which) ? evt.which : event.keyCode
        if (nLastKey == 39) {
            if (nLastKey != 8)
                return false;
        }
        return true;
    }
}
/************************************************************************************
*	The Below Function is to validate the Address								*
************************************************************************************/
function validateAddress(txtFld, maxCount, evt) {
    var jvTxtValue = TrimAll(document.getElementById(txtFld).value);

    // nLastKey = window.event.keyCode;
    var nLastKey = (evt.which) ? evt.which : event.keyCode
    if ((nLastKey == 34 || nLastKey == 39))
        return false;
    // return true;
    if (jvTxtValue.length > maxCount)
        return false;

    return true;
}
/************************************************************************************
*	The Below Function is to validate the Decimal value								*
************************************************************************************/
function validatePara(txtfld) {
    var fld = document.getElementById(txtfld)
    var EnteredValue = document.getElementById(txtfld).value;
    var arrParts = EnteredValue.split('.');

    if (arrParts.length-1 >= 2) {
        alert("Invalid Entry")
        fld.value = "";
        fld.focus();
        return false;
    }
}
function isDecimalKey(sender, evt) {
    var fld = sender
    var txt = fld.value;
    var dotcontainer = txt.split('.');
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (!(dotcontainer.length == 1 && charCode == 46) && charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
/************************************************************************************
*	The Below function validate the string length,blank,invalid chars etc			*
************************************************************************************/
function validateName(tststring, min) {
    var str = tststring.toLowerCase();
    var len = str.length;
    var retMsg = 'pass';
    if (min != 0) {
        if (len < min) {
            retMsg = "Provided Value to this field is too short....\n\n atleast " + min + " characters are expected"
            return (retMsg);
        }

        if (str == "") {
            retMsg = "Cannot be Blank.... Please Provide Value To This Field"
            return (retMsg);
        }
    }
    for (i = 0; i < len; i++) {
        k = str.charAt(i);
        m = str.charCodeAt(i);

        if (m == 32 && i == 0) {
            retMsg = "Invalid Character at  " + (i + 1) + " String Cannot Start with a blank space Character"
            return (retMsg);
        }

        if (!((m >= 97) && (m <= 122)) || ((m >= 65) && (m <= 90))) {
            if (m != 32) {
                retMsg = "Invalid Character found " + k + " at position " + (i + 1)
                return (retMsg);
            }
            if (m == 32 && str.charCodeAt(i + 1) == 32) {
                retMsg = "Invalid Character at  " + (i + 1) + " Cannot have multiple spaces between characters"
                return (retMsg);
            }
        }
    }
    return (retMsg);
}
/************************************************************************************
*	The Below Function is to validate the Email id									*
************************************************************************************/
function validateEmail(strID) {
    tab = document.getElementById(strID);
    var str = strID.value;// document.getElementById(strID).value;
    var at = "@"
    var dot = "."
    var lat = str.indexOf(at)
    var lstr = str.length
    var ldot = str.indexOf(dot)
    var retMsg = "pass"
    if (str == "") {
        retMsg = "Cannot be Blank.... Please Provide Value To This Field"
        return (retMsg);
    }
    else if (str.indexOf(at) == -1) {
        retMsg = "Invalid Entry"
    }
    else if (str.indexOf(at) == -1 || str.indexOf(at) == 0 || str.indexOf(at) == lstr) {
        retMsg = "Invalid Entry"
    }
    else if (str.indexOf(dot) == -1 || str.indexOf(dot) == 0 || str.indexOf(dot) == lstr) {
        retMsg = "Invalid Entry"
    }
    else if (str.indexOf(at, (lat + 1)) != -1) {
        retMsg = "Invalid Entry"
    }
    else if (str.substring(lat - 1, lat) == dot || str.substring(lat + 1, lat + 2) == dot) {
        retMsg = "Invalid Entry"
    }
    else if (str.indexOf(dot, (lat + 2)) == -1) {
        retMsg = "Invalid Entry"
    }
    else if (str.indexOf(" ") != -1) {
        retMsg = "Invalid Entry"
    }
    else {
        retMsg = "";
    }
    if (retMsg == "") {
        return true
    }
    {
        showCustomMessage("Not-Allowed", retMsg, "Error");
        //alert(retMsg);
       // document.getElementById(strID).value = "";
        return false;
    }
}
/************************************************************************************
*	The Below Function is to validate the phone number								*
************************************************************************************/
function validatePhone(txtPhone) {
    var str = document.getElementById(txtPhone).value;
    var len = str.length;
    var cnt = 0;
    for (i = 0; i < parseInt(len); i++) {
        m = str.charCodeAt(i);
        //alert("m"+m)
        if (m == 45) {
            cnt = parseInt(cnt) + 1
        }
    }
    if (cnt > 1) {
        alert("Invalid Phone Number");
        $get(txtPhone).value = '';
        $get(txtPhone).focus();
        return false;
    }
}
/************************************************************************************
*	The Below Function is to validate the mobile number								*
************************************************************************************/
function validMobile(txtMobile) {
    var str = document.getElementById(txtMobile).value;
    var len = str.length;
    if (str != '') {
        if (len < 10 || len > 10) {
            alert("Provided Value to this field is short or long....\n\n it should be 10 characters ");
            document.getElementById(txtMobile).focus();
            return false;
        }
    }
}
/* The Below Function is to validate the Pin code	*/

function validPinCode(txtEmail) {
    var str = document.getElementById(txtEmail).value;
    var len = str.length;
    if (str != '') {
        if (len < 6 || len > 6) {
            alert("Provided Value to this field is short or long....\n\n it should be 6 characters ");
            document.getElementById(txtEmail).focus();
            return false;
        }
    }
}

/************************************************************************************
*	The Below Function is to validate the number							    	*
************************************************************************************/
function validateNumber(tststring, min) {
    var str = tststring;
    var len = str.length;
    var retMsg = "pass"

    if (min != 0) {
        if (len < min) {
            retMsg = "Provided Value to this field is too short....\n\n atleast " + min + " characters are expected"
            return (retMsg);
        }

        if (str == "") {
            retMsg = "Cannot be Blank.... Please Provide Value To This Field"
            return (retMsg);
        }
    }

    for (i = 0; i < len; i++) {
        k = parseInt(str.charAt(i));
        n = str.charAt(i);
        if (!((k >= 0) && (k <= 9))) {
            retMsg = "Invalid Character found " + n + " at position " + (i + 1) + "  \n This field expects Numeric values"
            return (retMsg);
        }
    }
    return (retMsg);
}
/************************************************************************************
*	The Below Function is to validate Currency       								*
************************************************************************************/
function validateCurrency(tststring, min) {
    var str = tststring;
    var len = str.length;
    var retMsg = "pass"
    var passCnt = 0;

    if (min != 0) {
        if (len < min) {
            retMsg = "Provided Value to this field is too short....\n\n atleast " + min + " characters are expected"
            return (retMsg);
        }

        if (str == "") {
            retMsg = "Cannot be Blank.... Please Provide Value To This Field"
            return (retMsg);
        }
    }
    for (i = 0; i < len; i++) {
        k = parseInt(str.charAt(i));
        n = str.charAt(i);

        if (n == ".")
            passCnt++;

        if (n != ".") {
            if (!((k >= 0) && (k <= 9))) {
                if (n != "-") {
                    retMsg = "Invalid Character found " + n + " at position " + (i + 1) + "  \n This field expects Numeric values"
                    return (retMsg);
                }
            }
        }
    }

    if (passCnt > 1) {
        retMsg = "Invalid Currency format Please Recheck the entered value"
        return (retMsg);
    }
    return (retMsg);
}

/************************************************************************************
*	The Below Function is to validate the AlphaNumeric								*
************************************************************************************/
function validateAlphanumeric(tststring, min) {
    var str = tststring;
    var len = str.length;
    var retMsg = "pass";

    if (min != 0) {
        if (len < min) {
            retMsg = "Provided Value to this field is too short....\n\n atleast " + min + " characters are expected"
            return (retMsg);
        }
        if (str == "") {
            retMsg = "Cannot be Blank.... Please Provide Value To This Field"
            return (retMsg);
        }
    }
    return (retMsg);
}

/************************************************************************************
*	The Below Function is to validate the User id							*
************************************************************************************/
function validateUID(tststring, min) {
    var str = tststring;
    var len = str.length;
    var retMsg = "pass"

    if (str == "") {
        retMsg = "Cannot be Blank.... Please Provide Value To This Field"
        return (retMsg);
    }

    if (min != 0) {
        if (len < min) {
            retMsg = "Provided Value to this field is too short....\n\n atleast " + min + " characters are expected"
            return (retMsg);
        }
    }

    for (i = 0; i < len; i++) {
        k = str.charAt(i);
        m = str.charCodeAt(i);
        n = parseInt(str.charAt(i));

        if (!(((n >= 0) && (n <= 9)) || ((m >= 97) && (m <= 122)) || ((m >= 65) && (m <= 90)))) {
            //alert(m);
            if (!(m == 95)) {
                retMsg = "Invalid Character found " + k + " at position " + (i + 1) + "  \n\nValid special characters allowed are\n\n( _ underScore )"
                return (retMsg);
            }
        }
    }
    return (retMsg);
}

/************************************************************************************
*	The Below Function is to validate Password								        *
************************************************************************************/
function validatePassword(tststring, tststring1, min) {
    var str = tststring;
    var str1 = tststring1;
    var len = str.length;
    var retMsg = "pass"
    if (min != 0) {
        if (len < min) {
            retMsg = "Provided Value to this field is too short....\n\n atleast " + min + " characters are expected"
            return (retMsg);
        }
    }

    if (str == "") {
        retMsg = "Password Cannot be Blank.... Please Provide Value To This Field"
        return (retMsg);
    }

    if (str1 == "") {
        retMsg = "Renter Password Cannot be Blank.... Please Provide Value To This Field"
        return (retMsg);
    }

    if (!(str == str1)) {
        retMsg = "Password Mismatch ... Please renter agian"
        return (retMsg);
    }

    return (retMsg);
}

/************************************************************************************
*	The Below Function is to validate Time								        *
************************************************************************************/

function validTime(fld) {
    var testHH, testMM, inpHH, inpMM, msg;
    var re = /\b\d{1,2}[\:]\d{1,2}\b/;
    var inp = fld.value;
    if (re.test(inp)) {
        //alert(re.test(inp));
        var delimChar = ":"
        var delim1 = inp.indexOf(delimChar);
        HH = parseInt(inp.substring(0, delim1), 10);
        MM = parseInt(inp.substr(delim1 + 1, 2), 10);
        //alert(MM);
        d = new Date();
        yr = d.getYear();
        mo = d.getMonth() + 1;
        day = d.getDay();

        var testTime = new Date(yr, mo - 1, day, HH, MM);
        //alert(testTime.getHours());
        if (testTime.getMinutes() == MM) {
            if (testTime.getHours() == HH) {
                msg = ""
            }
            else {
                msg = "Invalid Hours... Range [00-23] \n\n Valid Format HH:MM"
            }
        }
        else {
            msg = "Invalid Minutes... Range [00-59] \n\n Valid Format HH:MM"
        }
    }
    else {
        msg = "Invalid Format \n\n Valid Format [00-23]:[00-59] HH:MM"
    }

    if (msg) {
        alert(msg);
        //alert(fld.form.name + " " + fld.name);
        setTimeout("doSelection(document.forms['" + fld.form.name + "'].elements['" + fld.name + "'])", 0);
        return false;
    }
    else {
        return true;
    }
}

/************************************************************************************
*	The Below Function is to validate Date      							        *
************************************************************************************/
function validDateCompare(txtFDate, txtTDate, jvType) {
    
    var fldFDate = txtFDate; //document.getElementById(txtFDate);
    var fldTDate = txtTDate;//document.getElementById(txtTDate);
    var fldFDateValue = fldFDate.val();
    var fldTDateValue = fldTDate.val();
    var jvIsValid = true;
    var re = /\b\d{1,2}[\/-]\d{1,2}[\/-]\d{4}\b/;
    var delimChar = (fldFDateValue.indexOf("/") != -1) ? "/" : "-";
    var objFDate;
    var objTDate;
    if (isEmpty(fldTDateValue) == false && isEmpty(fldTDateValue) == false) {
        // Validate From Date
        if (re.test(fldFDateValue)) {
            // Split the input into day,month and Year

            var delim1 = fldFDateValue.indexOf(delimChar);
            var delim2 = fldFDateValue.lastIndexOf(delimChar);
            day = parseInt(fldFDateValue.substring(0, delim1), 10);
            mo = parseInt(fldFDateValue.substring(delim1 + 1, delim2), 10);
            yr = parseInt(fldFDateValue.substring(delim2 + 1), 10);
            objFDate = new Date(yr, mo - 1, day);

            if (objFDate.getDate() != day) { jvIsValid = false; }
            if (objFDate.getMonth() + 1 != mo) { jvIsValid = false; }
            if (objFDate.getFullYear() != yr) { jvIsValid = false; }
        }
        else { jvIsValid = false; }

        if (jvIsValid == false) {
            var jvMsg = "Invalid Date!!!";
            showCustomMessage("Validation Failed", jvMsg, "Error");
            fldFDate.value = "";
            fldFDate.focus();
            return false;
        }
        else {
            if (day < 10) { day = "0" + day; }
            if (mo < 10) { mo = "0" + mo; }
            fldFDate.value = day + "/" + mo + "/" + yr;
        }

        if (jvType == "EXPC") { fldTDateValue = "01/" + fldTDateValue; }

        // validate To Date
        if (re.test(fldTDateValue)) {
            // Split the input into day,month and Year
            var delim3 = fldTDateValue.indexOf(delimChar);
            var delim4 = fldTDateValue.lastIndexOf(delimChar);

            day = parseInt(fldTDateValue.substring(0, delim3), 10);
            mo = parseInt(fldTDateValue.substring(delim3 + 1, delim2), 10);
            yr = parseInt(fldTDateValue.substring(delim4 + 1), 10);
            objTDate = new Date(yr, mo - 1, day);

            if (objTDate.getDate() != day) { jvIsValid = false; }
            if (objTDate.getMonth() + 1 != mo) { jvIsValid = false; }
            if (objTDate.getFullYear() != yr) { jvIsValid = false; }
        }
        else { jvIsValid = false; }

        if (jvIsValid == false) {
            var jvMsg = "Invalid ToDate!!!";
            showCustomMessage("Validation Failed", jvMsg, "Error");
            fldTDate.value = "";
            fldTDate.focus();
            return false;
        }
        else {
            if (day < 10) { day = "0" + day; }
            if (mo < 10) { mo = "0" + mo; }
            fldTDate.value = day + "/" + mo + "/" + yr;
            if (jvType == "EXPC") { fldTDate.value = mo + "/" + yr; }
        }

        // Comparison
        if (objFDate > objTDate) {
            var jvMsg = "Invalid Date!!!"
            var jvFocusCtrl;
            if (jvType == "COM") {
                jvMsg = "From Date Cannot be Greater Than To Date!!!";
                jvFocusCtrl = fldFDate;
            }
            else if (jvType == "CO") {
                jvMsg = "To Date Cannot be Less Than From Date!!!";
                jvFocusCtrl = fldTDate;
            }
            else if (jvType == "NOFD") {
                jvMsg = "Cannot be Future Date!!!";
                jvFocusCtrl = fldFDate;
            }
            else if (jvType == "EXPC") {
                jvMsg = "Expiry Date Cannot be Lesser Than Current Date!!!";
                jvFocusCtrl = fldTDate;
            }
            else {
                jvMsg = "From Date Cannot be Greater Than To Date!!!";
                jvFocusCtrl = fldFDate;
            }

            showCustomMessage("Validation Failed", jvMsg, "Error");
            //alert(jvMsg)
            jvFocusCtrl.value = "";
            jvFocusCtrl.focus();
            return false;
        }
        return true;
    }
}

function validDate(txtdate) {
    var fld = document.getElementById(txtdate)
    var inp = TrimAll(fld.value);
    if (isEmpty(inp) == false) {
        var jvIsValid = true;
        var re = /\b\d{1,2}[\/-]\d{1,2}[\/-]\d{4}\b/;
        if (re.test(inp)) {
            // Split the input into day,month and Year
            var delimChar = (inp.indexOf("/") != -1) ? "/" : "-";
            var delim1 = inp.indexOf(delimChar);
            var delim2 = inp.lastIndexOf(delimChar);

            day = parseInt(inp.substring(0, delim1), 10);
            mo = parseInt(inp.substring(delim1 + 1, delim2), 10);
            yr = parseInt(inp.substring(delim2 + 1), 10);
            var objDate = new Date(yr, mo - 1, day);

            if (objDate.getDate() != day) { jvIsValid = false; }
            if (objDate.getMonth() + 1 != mo) { jvIsValid = false; }
            if (objDate.getFullYear() != yr) { jvIsValid = false; }
        }
        else { jvIsValid = false; }

        if (jvIsValid == false) {
            alert("Invalid Date!!!");
            fld.value = "";
            fld.focus();
            return false;
        }
        else {
            if (day < 10)
                day = "0" + day;
            if (mo < 10)
                mo = "0" + mo;
            fld.value = day + "/" + mo + "/" + yr;
            return true;
        }
    }
}

// Valid between dates
function validDateBetween(txtADate, txtFDate, txtTDate, jvType,jvmsg) {
    var fldFDate = document.getElementById(txtFDate);
    var fldTDate = document.getElementById(txtTDate);
    var fldADate = document.getElementById(txtADate);
    var jvMessage = document.getElementById(jvmsg);


    var fldFDateValue = fldFDate.value;
    var fldTDateValue = fldTDate.value;
    var fldADateValue = fldADate.value;

    var jvIsValid = true;
    var re = /\b\d{1,2}[\/-]\d{1,2}[\/-]\d{4}\b/;
    var delimChar = (fldADateValue.indexOf("/") != -1) ? "/" : "-";
    var objFDate;
    var objTDate;
    var objADate;

    if (isEmpty(fldADateValue) == false) {
        if (isEmpty(fldTDateValue) == false && isEmpty(fldTDateValue) == false && isEmpty(fldADateValue) == false) {
            // Validate From Date
            if (re.test(fldADateValue)) {
                // Split the input into day,month and Year

                var delim1 = fldADateValue.indexOf(delimChar);
                var delim2 = fldADateValue.lastIndexOf(delimChar);

                day = parseInt(fldADateValue.substring(0, delim1), 10);
                mo = parseInt(fldADateValue.substring(delim1 + 1, delim2), 10);
                yr = parseInt(fldADateValue.substring(delim2 + 1), 10);
                objADate = new Date(yr, mo - 1, day);

                if (objADate.getDate() != day) { jvIsValid = false; }
                if (objADate.getMonth() + 1 != mo) { jvIsValid = false; }
                if (objADate.getFullYear() != yr) { jvIsValid = false; }
            }
            else { jvIsValid = false; }

            if (jvIsValid == false) {
                alert("Invalid Date!!!");
                fldADate.value = "";
                fldADate.focus();
                return false;
            }
            else {
                if (day < 10) { day = "0" + day; }
                if (mo < 10) { mo = "0" + mo; }
                fldADate.value = day + "/" + mo + "/" + yr;
            }

            // From date
            var delim3 = fldFDateValue.indexOf(delimChar);
            var delim4 = fldFDateValue.lastIndexOf(delimChar);

            day = parseInt(fldFDateValue.substring(0, delim3), 10);
            mo = parseInt(fldFDateValue.substring(delim3 + 1, delim4), 10);
            yr = parseInt(fldFDateValue.substring(delim4 + 1), 10);
            objFDate = new Date(yr, mo - 1, day);

            // validate To Date
            // Split the input into day,month and Year
            var delim5 = fldTDateValue.indexOf(delimChar);
            var delim6 = fldTDateValue.lastIndexOf(delimChar);

            day = parseInt(fldTDateValue.substring(0, delim5), 10);
            mo = parseInt(fldTDateValue.substring(delim5 + 1, delim6), 10);
            yr = parseInt(fldTDateValue.substring(delim6 + 1), 10);
            objTDate = new Date(yr, mo - 1, day);
            // Comparision
            //alert(objADate)
            // alert(objFDate)
            // alert(objTDate)
            if (objFDate <= objADate && objTDate >= objADate) {
                return true;
            }
            else {

                //var jvMessenger = document.getElementById("<%=divErrorMessage.ClientID%>");               
                fldADate.value = "";
                fldADate.focus();
                jvMessage.innerText = "Enter Valid Date";
                showMessageModalPopup()
               // return false;

            }
            return true;
        }
    }
}
//

function Insertbar(txtdate, evt) {
    // nLastKey = window.event.keyCode;
    var nLastKey = (evt.which) ? evt.which : event.keyCode
   
        //Only Number
        // nLastKey = window.event.keyCode;
       if (!(nLastKey >= 48 && nLastKey <= 57) && (nLastKey != 46) || nLastKey == 46) {
            if (nLastKey != 8)
                return false;
        }

  
//    if (!(nLastKey >= 48 && nLastKey <= 57) && nLastKey != 46) {
//        return false;
//    }
    var jv_cont = "no"
    var valdate = document.getElementById(txtdate);
    if (valdate.value.length == 2) {
        valdate.value = valdate.value + "/"
    }
    if (valdate.value.length == 5) {
        valdate.value = valdate.value + "/"
    }
    if (valdate.value.length == 10) {
        jv_cont = "yes"
    }
    if (jv_cont == "yes") {
        window.event.keyCode = '';
    }
}
function hiddisplay(txtdate) {
    document.getElementById(txtdate).value = '';
}

/************************************************************************************
*	The Below Function is to Select the given txtfield								 *
************************************************************************************/
function doSelection(txtfld) {
    var fld = document.getElementById(txtfld);
    fld.focus();
    fld.select();
}

function MultiDimensionalArray(iRows, iCols) {
    var i;
    var j;
    var a = new Array(iRows);
    for (i = 0; i < iRows; i++) {
        a[i] = new Array(iCols);
        for (j = 0; j < iCols; j++) {
            a[i][j] = '';
        }
    }
    return (a);
}

function getposOffset(overlay, offsettype) {
    var totaloffset = (offsettype == "left") ? overlay.offsetLeft : overlay.offsetTop;
    var parentEl = overlay.offsetParent;
    while (parentEl != null) {
        totaloffset = (offsettype == "left") ? totaloffset + parentEl.offsetLeft : totaloffset + parentEl.offsetTop;
        parentEl = parentEl.offsetParent;
    }
    return totaloffset;
}

/************************************************************************************
*	The Below Function is to make Overlay using Div tag for messaging						 *
************************************************************************************/
function overlay(curobj, subobjstr, opt_position) {
    if (document.getElementById) {
        //alert(subobjstr);
        var subobj = document.getElementById(subobjstr)
        subobj.style.display = (subobj.style.display != "block") ? "block" : "none"
        var xpos = getposOffset(curobj, "left") + ((typeof opt_position != "undefined" && opt_position.indexOf("right") != -1) ? -(subobj.offsetWidth - curobj.offsetWidth) : 0)
        var ypos = getposOffset(curobj, "top") + ((typeof opt_position != "undefined" && opt_position.indexOf("bottom") != -1) ? curobj.offsetHeight : 0)
        subobj.style.left = xpos + "px"
        subobj.style.top = ypos + "px"
        return false
    }
    else
        return true
}

function overlayclose(subobj) {
    document.getElementById(subobj).style.display = "none"
}

/************************************************************************************
*	The Below Function is to Format Currency        								 *
************************************************************************************/

function formatCurrency(expr, decplaces, includeCommas, NegativeEncloseInBraces) {
    var str = "" + Math.round(parseFloat(expr) * Math.pow(10, decplaces));
    while (str.length <= decplaces) {
        str = "0" + str;
    }
    var decpoint = str.length - decplaces;
    var Prefix = str.substring(0, decpoint);
    var Suffix = str.substring(decpoint, str.length);

    if (Prefix.length == 0)
        Prefix = "0";

    if (includeCommas == "0" && NegativeEncloseInBraces == "0") {
        return (Prefix + "." + Suffix);
    }
    else if (includeCommas == "0" && NegativeEncloseInBraces == "1") {
        if (parseFloat(Prefix) >= 0)
            return (Prefix + "." + Suffix);
        else
            return ("(" + Prefix.substr(1, Prefix.length) + "." + Suffix + ")");
    }
    else if (includeCommas == "1" && NegativeEncloseInBraces == "0") {
        if (parseFloat(Prefix) >= 0) {
            var NewPrefix = "";
            var PrefixIndex = 3;
            for (var i = (Prefix.length - 3); i > 0; i = (i - 3)) {
                subPrefix = Prefix.substr(i, 3);
                NewPrefix = "," + subPrefix + NewPrefix;
                PrefixIndex = i;
            }
            NewPrefix = Prefix.substr(0, PrefixIndex) + NewPrefix;
            return (NewPrefix + "." + Suffix);
        }
        else {
            Prefix = Prefix.substr(1, Prefix.length);

            var NewPrefix = "";
            var PrefixIndex = 3;
            for (var i = (Prefix.length - 3); i > 0; i = (i - 3)) {
                subPrefix = Prefix.substr(i, 3);
                NewPrefix = "," + subPrefix + NewPrefix;
                PrefixIndex = i;
            }
            NewPrefix = Prefix.substr(0, PrefixIndex) + NewPrefix;
            return ("-" + NewPrefix + "." + Suffix);
        }
    }
    else if (includeCommas == "1" && NegativeEncloseInBraces == "1") {
        if (parseFloat(Prefix) >= 0) {
            var NewPrefix = "";
            var PrefixIndex = 3;
            for (var i = (Prefix.length - 3); i > 0; i = (i - 3)) {
                subPrefix = Prefix.substr(i, 3);
                NewPrefix = "," + subPrefix + NewPrefix;
                PrefixIndex = i;
            }
            NewPrefix = Prefix.substr(0, PrefixIndex) + NewPrefix;
            return (NewPrefix + "." + Suffix);
        }
        else {
            Prefix = Prefix.substr(1, Prefix.length);

            var NewPrefix = "";
            var PrefixIndex = 3;
            for (var i = (Prefix.length - 3); i > 0; i = (i - 3)) {
                subPrefix = Prefix.substr(i, 3);
                NewPrefix = "," + subPrefix + NewPrefix;
                PrefixIndex = i;
            }
            NewPrefix = Prefix.substr(0, PrefixIndex) + NewPrefix;
            return ("(" + NewPrefix + "." + Suffix + ")");
        }
    }
}

/************************************************************************************
*	The Below Function is to Format decimal value    								 *
************************************************************************************/
function format(expr, decplaces) {
    var str = "" + Math.round(parseFloat(expr) * Math.pow(10, decplaces));
    //alert(Math.pow(10,decplaces));
    while (str.length <= decplaces) {
        str = "0" + str;
    }
    var decpoint = str.length - decplaces;
    return str.substring(0, decpoint) + "." + str.substring(decpoint, str.length);
}

function formatNew(expr, decplaces) {
    var str = "" + Math.round(eval(expr) * Math.pow(10, decplaces));
    //alert(Math.pow(10,decplaces));
    while (str.length <= decplaces) {
        str = "0" + str;
    }
    var decpoint = str.length - decplaces;
    if (parseFloat(str) < 0)
        return "(" + str.substring(1, decpoint) + "." + str.substring(decpoint, str.length) + ")";

    return str.substring(0, decpoint) + "." + str.substring(decpoint, str.length);
}

function convertToRs(expr) {
    //alert(expr);
    return "Rs. " + format(expr, 2);
}

function ValidateCheckAtRow(w1, Seq) {
    var chkW1 = document.getElementById(w1).checked;
    var chkStr = String(Seq.getAttribute('visible'));
    if (chkW1 = true) {
        document.getElementById(Seq).style.visibility = true
        Seq.style.visibility = 'visible';
        alert(chkStr)
    }
}

function validatePercent(txtfld) {
    var fld = document.getElementById(txtfld);
    if (parseFloat(fld.value) > 100) {
        alert("Cannot exceed more than 100");
        fld.value = fld.value.substr(0, 2);
        fld.focus();
        return false;
    }
    return true;
}

function validateThis(txtfld) {
    var msg;
    var str = document.getElementById(txtfld).value;
    var chkStr = String(fld.getAttribute('rel'));

    var chkType = String(chkStr.substr(0, 4));
    var tmp = parseInt(chkStr.length, 10) - 7
    var min = chkStr.substr(7, tmp);
    var len = str.length;
    var req = String(chkStr.substr(5, 1));
    if (parseInt(req, 10) == 0)
        min = 0;

    if (chkType == "alph") {
        var msg = validateAlphanumeric(str, min);
        if (msg == "pass")
            msg = "";
    }
    else if (chkType == "addr") {
        msg = validateAddress(str);
        if (msg == "pass")
            msg = "";
    }
    else if (chkType == "text") {
        msg = validateName(str, min);
        if (msg == "pass")
            msg = "";
    }
    else if (chkType == "cntc") {
        msg = validatePhone(str, min);
        if (msg == "pass")
            msg = "";
    }
    else if (chkType == "numb") {
        msg = validateNumber(str, min);
        if (msg == "pass")
            msg = "";
    }
    else if (chkType == "webe") {
        msg = validateEmail(str);
        if (msg == "pass")
            msg = "";
    }
    else if (chkType == "curn") {
        msg = validateCurrency(str, min);
        if (msg == "pass") {
            msg = "";
            if (isNaN(format(fld.value, 2)))
                fld.value = "";
            else
                fld.value = format(fld.value, 2);
        }
    }
    else if (chkType == "prcn") {
        msg = validateCurrency(str, min);
        if (msg == "pass") {
            msg = "";
            fld.value = format(fld.value, 2);
        }
        if (parseFloat(fld.value) > 100) {
            msg = "Cannot exceed more than 100";
            fld.value = format(fld.value, 2);
        }
    }
    if (msg) {
        alert(fld.getAttribute('title') + " : " + msg);
        setTimeout("doSelection(document.forms['" + fld.form.name + "'].elements['" + fld.name + "'])", 0);
        return false;
    }
    else {
        return true;
    }
}

function matchFound(inputStr, testString) {
    var re = /\S+/;
    var re = /\s+/;
    if (!inputStr.match(re)) {
        return true;
    }
    return false;
}

function isBlank(inputStr) {
    var re = /\s+/;
    if (!inputStr.match(re)) {
        return true;
    }
    return false;
}

function isEmpty(inputStr) {
    var re = /.+/;
    if (!inputStr.match(re)) {
        return true;
    }
    return false;
}
/************************************************************************************
*	The Below Function is to Set style properties      								 *
************************************************************************************/
function revert() {
    document.releaseCapture();
    hideMenu();
    //hideMenu1();
}
function hideMenu() {
    contextMenu.style.visibility = "hidden";
}

function hideMenu1() {
    contextMenu1.style.visibility = "hidden";
}

function highlight() {
    var elem = event.srcElement;
    if (elem.className == "menuItem") {
        elem.className = "menuItemOn";
    }
}
function unhighlight() {
    var elem = event.srcElement
    if (elem.className == "menuItemOn") {
        elem.className = "menuItem";
    }
}

function setMsg(msg) {
    window.status = msg;
    return true;
}

function monthName(mth) {
    var strMth = "";
    mth = parseInt(mth, 10)
    switch (mth) {
        case 1:
            strMth = "January";
            break;
        case 2:
            strMth = "February";
            break;
        case 3:
            strMth = "March";
            break;
        case 4:
            strMth = "April";
            break;
        case 5:
            strMth = "May";
            break;
        case 6:
            strMth = "June";
            break;
        case 7:
            strMth = "July";
            break;
        case 8:
            strMth = "August";
            break;
        case 9:
            strMth = "September";
            break;
        case 10:
            strMth = "October";
            break;
        case 11:
            strMth = "November";
            break;
        case 12:
            strMth = "December";
            break;
    }
    return (strMth);
}

function sMonthName(mth) {
    var strMth = "";
    mth = parseInt(mth, 10)
    switch (mth) {
        case 1:
            strMth = "Jan";
            break;
        case 2:
            strMth = "Feb";
            break;
        case 3:
            strMth = "Mar";
            break;
        case 4:
            strMth = "Apr";
            break;
        case 5:
            strMth = "May";
            break;
        case 6:
            strMth = "Jun";
            break;
        case 7:
            strMth = "Jul";
            break;
        case 8:
            strMth = "Aug";
            break;
        case 9:
            strMth = "Sep";
            break;
        case 10:
            strMth = "Oct";
            break;
        case 11:
            strMth = "Nov";
            break;
        case 12:
            strMth = "Dec";
            break;
    }
    return (strMth);
}

function showMon(mth) {
    var strmonth = "";
    switch (parseInt(mth, 10)) {
        case 0: strmonth = 'JAN';
            break;
        case 1: strmonth = 'FEB';
            break;
        case 2: strmonth = 'MAR';
            break;
        case 3: strmonth = 'APR';
            break;
        case 4: strmonth = 'MAY';
            break;
        case 5: strmonth = 'JUN';
            break;
        case 6: strmonth = 'JUL';
            break;
        case 7: strmonth = 'AUG';
            break;
        case 8: strmonth = 'SEP';
            break;
        case 9: strmonth = 'OCT';
            break;
        case 10: strmonth = 'NOV';
            break;
        case 11: strmonth = 'DEC';
            break;
    }
    //alert(strmonth);
    return strmonth
}

function translateMonth(mo) {
    mo = mo.toLowerCase();

    if (mo == "jan")
        return 1;
    else if (mo == "feb")
        return 2;
    else if (mo == "mar")
        return 3;
    else if (mo == "apr")
        return 4;
    else if (mo == "may")
        return 5;
    else if (mo == "jun")
        return 6;
    else if (mo == "jul")
        return 7;
    else if (mo == "aug")
        return 8;
    else if (mo == "sep")
        return 9;
    else if (mo == "oct")
        return 10;
    else if (mo == "nov")
        return 11;
    else if (mo == "dec")
        return 12;
}

function convertTextToDate(dtDate, format) {
    var objDate = new Object();

    var inp = dtDate;
    var delimChar = (inp.indexOf("/") != -1) ? "/" : "-";
    var delim1 = inp.indexOf(delimChar);
    var delim2 = inp.lastIndexOf(delimChar);
    if (format == 'ddmmyyyy') {
        var day = parseInt(inp.substring(0, delim1), 10);
        var mo = parseInt(inp.substring(delim1 + 1, delim2), 10);
        var yr = parseInt(inp.substring(delim2 + 1), 10);
    }
    else if (format == 'mmddyyyy') {
        var mo = parseInt(inp.substring(0, delim1), 10);
        var day = parseInt(inp.substring(delim1 + 1, delim2), 10);
        var yr = parseInt(inp.substring(delim2 + 1), 10);
    }
    else if (format == 'ddmonyyyy') {
        var day = parseInt(inp.substring(0, delim1), 10);
        var mo = translateMonth(inp.substring(delim1 + 1, delim2));
        var yr = parseInt(inp.substring(delim2 + 1), 10);
    }

    if (mo < 10)
        mo = "0" + mo;

    if (day < 10)
        day = "0" + day;

    year = yr.toString();

    objDate = { 'dd': day, 'mm': mo, 'yyyy': year, 'yy': year.substring(2, 2), 'month': monthName(mo), 'mon': sMonthName(mo) };

    return (objDate);
}

function splitDate(dtDate) {
    var objDate = new Object();

    var inp = dtDate;
    var delimChar = (inp.indexOf("/") != -1) ? "/" : "-";
    var delim1 = inp.indexOf(delimChar);
    var delim2 = inp.lastIndexOf(delimChar);
    var day = parseInt(inp.substring(0, delim1), 10);
    var mo = parseInt(inp.substring(delim1 + 1, delim2), 10);
    var yr = parseInt(inp.substring(delim2 + 1), 10);

    if (mo < 10)
        mo = "0" + mo;

    if (day < 10)
        day = "0" + day;

    year = yr.toString();
    objDate = { 'dd': day, 'mm': mo, 'yyyy': year, 'yy': year.substring(2, 2), 'month': monthName(mo), 'mon': sMonthName(mo) };
    return (objDate);
}
function trimNewLineChar(obj) {
    var str = obj.value;
    var len = str.length;
    //alert(len);
    var tstString = "";
    for (var i = 0; i < len; i++) {
        var m = str.charCodeAt(i);
        if (m == 10 || m == 13)
            tstString = tstString + " ";
        else
            tstString = tstString + str.charAt(i);
    }
    obj.value = tstString;
    tstString = "";
    str = obj.value;

    for (var i = 0; i < str.length; i++) {
        var m = str.charCodeAt(i);

        if (m == 32 && str.charCodeAt(i + 1) == 32)
            tstString = tstString + "";
        else
            tstString = tstString + str.charAt(i);
    }
    obj.value = tstString;
}

function splitEnteredDate(dtDate, format) {
    var objDate = new Object();
    if (format == "mm-dd-yyyy") {
        var inp = dtDate;
        var delimChar = (inp.indexOf("/") != -1) ? "/" : "-";
        var delim1 = inp.indexOf(delimChar);
        var delim2 = inp.lastIndexOf(delimChar);
        var mo = parseInt(inp.substring(0, delim1), 10);
        var day = parseInt(inp.substring(delim1 + 1, delim2), 10);
        var yr = parseInt(inp.substring(delim2 + 1), 10);

        if (mo < 10)
            mo = "0" + mo;

        if (day < 10)
            day = "0" + day;

        var year = yr.toString();
        objDate = { 'dd': day, 'mm': mo, 'yyyy': year, 'yy': year.substring(2, 2), 'month': monthName(mo), 'mon': sMonthName(mo) };
        return (objDate);
    }
    else if (format == "dd-mm-yyyy") {
        var inp = dtDate;
        var delimChar = (inp.indexOf("/") != -1) ? "/" : "-";
        var delim1 = inp.indexOf(delimChar);
        var delim2 = inp.lastIndexOf(delimChar);
        var day = parseInt(inp.substring(0, delim1), 10);
        var mo = parseInt(inp.substring(delim1 + 1, delim2), 10);
        var yr = parseInt(inp.substring(delim2 + 1), 10);

        if (mo < 10)
            mo = "0" + mo;

        if (day < 10)
            day = "0" + day;

        var year = yr.toString();
        objDate = { 'dd': day, 'mm': mo, 'yyyy': year, 'yy': year.substring(2, 2), 'month': monthName(mo), 'mon': sMonthName(mo) };
        return (objDate);
    }
    else if (format == "dd-mon-yyyy") {
        var inp = dtDate;
        var delimChar = (inp.indexOf("/") != -1) ? "/" : "-";
        var delim1 = inp.indexOf(delimChar);
        var delim2 = inp.lastIndexOf(delimChar);
        var day = parseInt(inp.substring(0, delim1), 10);
        var mo = parseInt(translateMonth(inp.substring(delim1 + 1, delim2)), 10);
        var yr = parseInt(inp.substring(delim2 + 1), 10);

        if (mo < 10)
            mo = "0" + mo;

        if (day < 10)
            day = "0" + day;

        var year = yr.toString();
        objDate = { 'dd': day, 'mm': mo, 'yyyy': year, 'yy': year.substring(2, 2), 'month': monthName(mo), 'mon': sMonthName(mo) };
        return (objDate);
    }
}

function TrimAll(strValue) {
    var objRegExp = /^(\s*)$/;
    if (objRegExp.test(strValue)) {
        strValue = strValue.replace(objRegExp, '');
        if (strValue.length == 0)
            return strValue;
    }

    objRegExp = /^(\s*)([\W\w]*)(\b\s*$)/;

    if (objRegExp.test(strValue)) {
        strValue = strValue.replace(objRegExp, '$2');
    }
    return strValue;
}

function onlyNumbers(e) {
    var KeyCode = (e.keyCode) ? e.keyCode : e.which;
    return ((KeyCode == 8) // backspace
        || (KeyCode == 9) // tab
        || (KeyCode == 37) // left arrow
        || (KeyCode == 39) // right arrow
        || (KeyCode == 46) // delete

        || ((KeyCode > 47) && (KeyCode < 58)) // 0 - 9

        );
}
function onlyCurrency(e) {
    //alert(e.keyCode + " " +  e.which);
    var KeyCode = (e.keyCode) ? e.keyCode : e.which;
    return ((KeyCode == 8) // backspace
        || (KeyCode == 9) // tab
        || (KeyCode == 37) // left arrow
        || (KeyCode == 39) // right arrow
        || (KeyCode == 46) // delete
        || (KeyCode == 45) // -
        || ((KeyCode > 47) && (KeyCode < 58)) // 0 - 9
        );
}
function emailEntry(e) {
    //alert(e.keyCode);
    var KeyCode = (e.keyCode) ? e.keyCode : e.which;
    return ((KeyCode == 8) // backspace
        || (KeyCode == 9) // tab
        || (KeyCode == 37) // left arrow
        || (KeyCode == 39) // right arrow
        || (KeyCode == 46) // delete
        || (KeyCode == 95) // underscore
        || ((KeyCode > 47) && (KeyCode < 58)) // 0 - 9
        || ((KeyCode > 63) && (KeyCode < 91))  // a - z & @
        || ((KeyCode > 96) && (KeyCode < 123)) // A - Z
        );
}
function onlyTime(e) {
    var KeyCode = (e.keyCode) ? e.keyCode : e.which;
    //alert(e.keyCode);
    return ((KeyCode == 8) // backspace
        || (KeyCode == 9) // tab
        || (KeyCode == 37) // left arrow
        || (KeyCode == 39) // right arrow
        || (KeyCode == 46) // delete

        || ((KeyCode > 47) && (KeyCode < 59)) // 0 - 9 :

        );
}
function onlyRate(e) {
    //alert(e.keyCode);
    var KeyCode = (e.keyCode) ? e.keyCode : e.which;
    return ((KeyCode == 8) // backspace
        || (KeyCode == 9) // tab
        || (KeyCode == 37) // left arrow
        || (KeyCode == 39) // right arrow
        || (KeyCode == 46) // delete
        || (KeyCode == 45) // delete
        || ((KeyCode > 47) && (KeyCode < 58)) // 0 - 9

        );
}
function onlyDate(e) {
    //alert(e.keyCode);
    var KeyCode = (e.keyCode) ? e.keyCode : e.which;
    return ((KeyCode == 8) // backspace
        || (KeyCode == 9) // tab
        || (KeyCode == 37) // left arrow
        || (KeyCode == 39) // right arrow
        || (KeyCode == 46) // delete
        || (KeyCode == 45) // -
        || (KeyCode == 47) // /
        || ((KeyCode > 47) && (KeyCode < 58)) // 0 - 9

        );
}
function onlyAlphabets(e) {
    //alert(e.keyCode);
    var KeyCode = (e.keyCode) ? e.keyCode : e.which;
    return ((KeyCode == 8) // backspace
        || (KeyCode == 9) // tab
        || (KeyCode == 37) // left arrow
        || (KeyCode == 39) // right arrow
        || (KeyCode == 46) // delete
        || (KeyCode == 32) // delete
        || ((KeyCode > 63) && (KeyCode < 91))  // a - z & @
        || ((KeyCode > 96) && (KeyCode < 123)) // A - Z
        );
}

var jsalarm = {
    padfield: function (f) {
        return (f < 10) ? "0" + f : f
    },
    showcurrenttime: function () {
        var dateobj = new Date()
        var ct = this.padfield(dateobj.getHours()) + ":" + this.padfield(dateobj.getMinutes()) + ":" + this.padfield(dateobj.getSeconds())
        if (typeof this.hourwake != "undefined") { //if alarm is set
            if (ct == (this.hourwake + ":" + this.minutewake + ":" + this.secondwake)) {
                dtMyDate = jsalarm.timer;
                var curDay = new Date();
                var d = new Date(curDay.getYear(), curDay.getMonth(), curDay.getDay(), this.hourwake, this.minutewake, (parseInt(this.secondwake, 10) + 1800));
                jsalarm.setalarm(d)
                this.createNewPopUp();
            }
        }
    },
    init: function (logedTime) {
        var MinMilli = 1000 * 60;
        var HrMilli = MinMilli * 60;
        var DyMilli = HrMilli * 24;
        //alert(logedTime);
        arrDtDate = logedTime.split(", ");

        var d = new Date(parseInt(arrDtDate[0], 10), parseInt(arrDtDate[1], 10), parseInt(arrDtDate[2], 10), parseInt(arrDtDate[3], 10), parseInt(arrDtDate[4], 10), parseInt(arrDtDate[5], 10));
        var curdtTime = new Date()

        //alert(curdtTime + " " +  d );
        //alert( parseInt(Date.parse(curdtTime),10) + " " +   parseInt(Date.parse(d),10))

        var d, s, t;
        t = parseInt(Date.parse(curdtTime), 10) - parseInt(Date.parse(d), 10)
        //alert(t);
        if (t <= 0)
            jsalarm.setalarm(d);
        else {
            s = Math.round(Math.abs(t / 1000));

            intInc = parseInt(Math.round((s / 1800)), 10);
            if (intInc == 0)
                intInc = 1

            intInc = intInc * 1800;

            d = new Date(parseInt(arrDtDate[0], 10), parseInt(arrDtDate[1], 10), parseInt(arrDtDate[2], 10), parseInt(arrDtDate[3], 10), parseInt(arrDtDate[4], 10), (parseInt(arrDtDate[5], 10) + intInc));

            if (curdtTime >= d)
                d = new Date(parseInt(arrDtDate[0], 10), parseInt(arrDtDate[1], 10), parseInt(arrDtDate[2], 10), parseInt(arrDtDate[3], 10), parseInt(arrDtDate[4], 10), (parseInt(arrDtDate[5], 10) + intInc + 1800));

            jsalarm.setalarm(d);
        }
        setInterval(function () { jsalarm.showcurrenttime() }, 1000)
    },
    setalarm: function (newDate) {
        this.hourwake = this.padfield(newDate.getHours());
        this.minutewake = this.padfield(newDate.getMinutes());
        this.secondwake = this.padfield(newDate.getSeconds());
        //alert(this.hourwake + " " + this.minutewake + " " + this.secondwake);
    },
    createNewPopUp: function () {
        var windowOptions = 'left=0,top=1000,history=no,toolbar=0,location=0,directories=0,status=0,menubar=0,resizable=no,scrollbars=yes,width=5,height=5';
        var fname = strPath + "showReminder.asp"
        //alert(strPath);
        var win = window.open(fname, "", windowOptions);
    }
}

function Left(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else
        return String(str).substring(0, n);
}
function Right(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else {
        var iLen = String(str).length;
        return String(str).substring(iLen, iLen - n);
    }
}


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