/************************************************************************************
*	The Below Code prints the Content of a container 											*
************************************************************************************/

// function CallPrint(strid)
//    {
//    var prtContent = document.getElementById(strid);
//    var WinPrint = window.open('', '_blank', 'letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');

//    WinPrint.document.write('<html>');
//    WinPrint.document.write('<head>');
//    WinPrint.document.write('<link rel="Stylesheet" type="text/css" href="../../Styles/Layout.css" />');
//    WinPrint.document.write('</head>');
//    WinPrint.document.write('<body>');
//    WinPrint.document.write(prtContent.innerHTML);
//    WinPrint.document.write('</body>');
//    WinPrint.document.write('</html>');
//    WinPrint.document.close();
//    WinPrint.focus();
//    WinPrint.print();
//    WinPrint.close();

//}

function CallPrint(strid) {

    var printContent = document.getElementById(strid);
    var windowUrl = 'about:blank';
    var uniqueName = new Date();
    var windowName = 'Print' + uniqueName.getTime();

    var printWindow = window.open(windowUrl, windowName, 'left=200,top=200,width=800,height=600');

    printWindow.document.write('<html>\n');
    printWindow.document.write('<head>\n');

    if (navigator.userAgent.toLowerCase().indexOf("chrome") > -1) {
        /// <reference path="../css/core/custom.css" />

    }
    else {
        printWindow.document.write('<link href="../../css/dandelion.css" rel="Stylesheet" type="text/css" />\n');
        printWindow.document.write('<link href="../../css/layout.css" rel="Stylesheet" type="text/css" />\n');
    }

    printWindow.document.write('<script>\n');

    if (navigator.userAgent.toLowerCase().indexOf("chrome") > -1) {
        printWindow.document.write('var chromeCss = document.createElement("link");\n');
        printWindow.document.write('chromeCss.rel = "stylesheet";\n');
        printWindow.document.write('chromeCss.href = "../../css/dandelion.css";\n');
        printWindow.document.write('chromeCss.href = "../../css/layout.css";\n');
     
        printWindow.document.write('document.getElementsByTagName("head")[0].appendChild(chromeCss);\n');
    }

    printWindow.document.write('function winPrint()\n');
    printWindow.document.write('{\n');
    printWindow.document.write('window.focus();\n');


    if (navigator.userAgent.toLowerCase().indexOf("chrome") > -1) {
        printWindow.document.write('printChrome();\n');
    }
    else {
        printWindow.document.write('window.print();\n');
    }


    if (navigator.userAgent.toLowerCase().indexOf("firefox") > -1) {
        printWindow.document.write('window.close();\n');
    }
    else {
        printWindow.document.write('chkstate();\n');
    }
    printWindow.document.write('}\n');


    printWindow.document.write('function chkstate()\n');
    printWindow.document.write('{\n');
    printWindow.document.write('if(document.readyState=="complete")');
    printWindow.document.write('{\n');
    printWindow.document.write('window.close();\n');
    printWindow.document.write('}\n');
    printWindow.document.write('else{\n');
    printWindow.document.write('setTimeout("chkstate();",3000);\n');
    printWindow.document.write('}\n');
    printWindow.document.write('}\n');

    printWindow.document.write('function printChrome()\n');
    printWindow.document.write('{\n');
    printWindow.document.write('if(document.readyState=="complete")');
    printWindow.document.write('{\n');
    printWindow.document.write('window.print();\n');
    printWindow.document.write('}\n');
    printWindow.document.write('else{\n');
    printWindow.document.write('setTimeout("printChrome();",3000);\n');
    printWindow.document.write('}\n');
    printWindow.document.write('}\n');


    printWindow.document.write('</scr');
    printWindow.document.write('ipt>');

    printWindow.document.write('</head>');
    printWindow.document.write('<body onload="winPrint()" >');
    printWindow.document.write('<table cellpadding="15px" width="100%" align="center">');
    printWindow.document.write('<tr>');
    printWindow.document.write('<td style="background-color: #d0dee1;">');
    printWindow.document.write('<table class="gray_border" align="center">');
    printWindow.document.write('<tr>');
    printWindow.document.write('<td>');
    printWindow.document.write('<div style="height: 100%;">');
    printWindow.document.write(printContent.innerHTML);
    printWindow.document.write('</div>');
    printWindow.document.write('</td>');
    printWindow.document.write('</tr>');
    printWindow.document.write('</table>');
    printWindow.document.write('</td>');
    printWindow.document.write('</tr>');
    printWindow.document.write('</table>');
    printWindow.document.write('</body>');
    printWindow.document.write('</html>');
    printWindow.document.close();



}

 