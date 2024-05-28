
if (typeof String.prototype.trim !== 'function') {
    String.prototype.trim = function () {
        return this.replace(/^\s+|\s+$/g, '');
    };
}

String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.replace(new RegExp(search, 'g'), replacement);
};
var j_days_in_month = [31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 29];
var g_days_in_month = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

//********************************************************************
//  Authorize
//********************************************************************
function Authorize() {

    var cookievalue = $.cookie("UserID");
    if (cookievalue == null) {
        var url = "http://172.16.114.125/login";
        window.location.href = url;
    } else {
        $.ajax({
            url: '../uc/CheckUserAccess2System',
            type: 'Post',
            async: false,
            cache: false,
            success: function (data) {
                if (data.length > 0) {

                    return;
                } else {
                    //alert("شما کاربر مجاز نمی باشید");
                    var url = "http://172.16.114.125/login";
                    window.location.href = url;
                }
                return;
            },
            contentType: "application/json; charset=utf-8",
            dataType: "json",

            data: "{"
                + "'PERSONALID' : " + cookievalue
                + ", 'DBCode' : 1"
                + "}",
            error: OnError
        });
    }
    return;
}
//******************************************************
//  CheckPermission2Action
//******************************************************
function CheckPermission2Action(PermissionType, actionName, controllerName) {
    var flag;
    $.ajax({
        url: '../UC/CheckPermission2Action',
        type: 'Post',
        async: false,
        cache: false,
        success: function (data) {
            flag = data;
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        data: "{'PermissionType' : " + PermissionType.toString()
            + ", 'UserId' : '" + $.cookie("UserId")

            + "', 'actionName' : '" + actionName
            + "', 'controllerName' : '" + controllerName

            + "'}",
        error: function () {
            flag = false;
        }
    });

    return (flag);
}
//******************************************************
//  CheckPermission2Form
//******************************************************
function CheckPermission2Form(actionName, controllerName) {
    var flag;
    $.ajax({
        url: '../UC/CheckPermission2View',
        type: 'Post',
        async: false,
        cache: false,
        success: function (data) {
            flag = data;
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        data: "{'UserId' : '" + $.cookie("UserId")

            + "', 'actionName' : '" + actionName
            + "', 'controllerName' : '" + controllerName

            + "'}",
        error: function () {
            flag = false;
        }
    });

    return (flag);
}
//******************************************************
//  OnError
//******************************************************
function OnError(jqXhr, textStatus, errorThrown) {

    alert(errorThrown);
    alert(this.url);

    return;
}

//******************************************************
//  AddRow2Table
//******************************************************
function AddRow2Table(tablename, text, css) {
    $('#' + tablename).addClass(css);
    $('#' + tablename).html($('#' + tablename).html() + "<tr><td align='center'>" + text + "</td></tr>");

    return;
}
//******************************************************
//  RemoveDropDownlistItems
//******************************************************
function RemoveDropDownlistItems(DropName) {
    var ind;
    var lencombobox;
    var combobox = $("#" + DropName).data('kendoDropDownList');

    lencombobox = combobox.dataSource.data().length;

    for (ind = 0; ind < lencombobox; ind++) {
        var itemoToRemove = combobox.dataSource.at(0);
        combobox.dataSource.remove(itemoToRemove);
    }
}

//******************************************************
//  DeleteRow
//******************************************************

function DeleteRow(e) {

    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var dataSource = $("#" + this.element.prop("id")).data("kendoGrid").dataSource;
    dataSource.remove(dataItem);
    return;
}

//********************************************************************
//  ClearGrid
//********************************************************************
function ClearGrid(gridname) {
    var grid = $("#" + gridname).data("kendoGrid");

    var items = grid.dataSource.data();
    lengthitems = items.length;
    var dataSource = grid.dataSource;
    for (ind = 0; ind < lengthitems; ind++) {
        var item = items[0];
        dataSource.remove(item);
    }
    dataSource.sync();
    return;
}

//******************************************************
//  setKeys
//******************************************************
function setKeys(id) {
    //StandardReferenceGrid, StandardReferenceDesc, PPDB_StandardReference_Id
    id = (id.toString().trim() == "" ? "0" : id.toString().trim());

    if ($("#InsertFlag").val() == "0") {
        $("#SaveBtn").prop("disabled", true);
    } else {
        $("#SaveBtn").prop("disabled", false);
    }

    if (id != 0) {
        if ($("#EditFlag").val() == "0") {
            $("#SaveBtn").prop("disabled", true);
        } else {
            $("#SaveBtn").prop("disabled", false);
        }
    }

    if ($("#DelFlag").val() == "0") {
        $("#DelBtn").prop("disabled", true);
    } else if (id != 0) {
        $("#DelBtn").prop("disabled", false);
    } else {
        $("#DelBtn").prop("disabled", true);
    }

    $("#CancelBtn").prop("disabled", false);

    if (id != 0) {
        if ($("#EditFlag").val() != "0") {
            $("#SaveBtn").html("ویرایش");
        }
    }
    else {
        $("#SaveBtn").html("ثبت");
    }
    return;
}
//******************************************************
//  changebgcolor
//******************************************************
function changebgcolor(gridname) {
    $("#" + gridname + " tr:odd").css("background-color", "#E6E3E6)");
    $("#" + gridname + " tr:odd").css("color", "black");

    return;
}

//******************************************************
//  DisabledKey
//******************************************************
function DisabledKey() {
    //document.getElementById("SaveBtn").disabled = true;
    $('[id=SaveBtn]').disabled = true;
    return;
}
//******************************************************
//  EnableKey
//******************************************************
function EnableKey() {
    //document.getElementById("SaveBtn").disabled = false;
    $('[id=SaveBtn]').disabled = false;
    return;
}
//******************************************************
//  CheckPersianDate
//******************************************************
function CheckPersianDate(PDate) {
    var jYear, jMonth, jDay, date, curDate;
    var flag;
    flag = true;

    if (PDate.length == 10) {
        date = PDate;
        jYear = date.substr(0, 4);
        jMonth = date.substr(5, 2);
        jDay = date.substr(8, 2);

        if (checkDate(jYear, jMonth, jDay) == false) {
            //AddRow2Table("MessageTable", "از تاریخ وارد شده اشتباه است", "ErrorMessage");
            flag = false;
        }

        return flag;
    }
}
//******************************************************
//  checkDate
//******************************************************
function checkDate(j_y, j_m, j_d) {
    return !(j_y < 0 || j_y > 32767 || j_m < 1 || j_m > 12 || j_d < 1 || j_d >
        (j_days_in_month[j_m - 1] + (j_m == 12 && !((j_y - 979) % 33 % 4))));
}
//******************************************************
//  gregorianToJalali
//******************************************************
function gregorianToJalali(g_y, g_m, g_d) {
    g_y = parseInt(g_y);
    g_m = parseInt(g_m);
    g_d = parseInt(g_d);
    var gy = g_y - 1600;
    var gm = g_m - 1;
    var gd = g_d - 1;

    var g_day_no = 365 * gy + parseInt((gy + 3) / 4) - parseInt((gy + 99) / 100) + parseInt((gy + 399) / 400);

    for (var i = 0; i < gm; ++i)
        g_day_no += g_days_in_month[i];
    if (gm > 1 && ((gy % 4 == 0 && gy % 100 != 0) || (gy % 400 == 0)))
        /* leap and after Feb */
        ++g_day_no;
    g_day_no += gd;

    var j_day_no = g_day_no - 79;

    var j_np = parseInt(j_day_no / 12053);
    j_day_no %= 12053;

    var jy = 979 + 33 * j_np + 4 * parseInt(j_day_no / 1461);

    j_day_no %= 1461;

    if (j_day_no >= 366) {
        jy += parseInt((j_day_no - 1) / 365);
        j_day_no = (j_day_no - 1) % 365;
    }

    for (var i = 0; i < 11 && j_day_no >= j_days_in_month[i]; ++i) {
        j_day_no -= j_days_in_month[i];
    }
    var jm = i + 1;
    var jd = j_day_no + 1;

    return (jy.toString() + "/" + (jm < 10 ? "0" : "") + jm.toString() + "/" + (jd < 10 ? "0" : "") + jd.toString());
}
//******************************************************
//  jalaliToGregorian
//******************************************************
function jalaliToGregorian(j_y, j_m, j_d) {
    j_y = parseInt(j_y);
    j_m = parseInt(j_m);
    j_d = parseInt(j_d);
    var jy = j_y - 979;
    var jm = j_m - 1;
    var jd = j_d - 1;

    var j_day_no = 365 * jy + parseInt(jy / 33) * 8 + parseInt((jy % 33 + 3) / 4);
    for (var i = 0; i < jm; ++i) j_day_no += j_days_in_month[i];

    j_day_no += jd;

    var g_day_no = j_day_no + 79;

    var gy = 1600 + 400 * parseInt(g_day_no / 146097); /* 146097 = 365*400 + 400/4 - 400/100 + 400/400 */
    g_day_no = g_day_no % 146097;

    var leap = true;
    if (g_day_no >= 36525) /* 36525 = 365*100 + 100/4 */ {
        g_day_no--;
        gy += 100 * parseInt(g_day_no / 36524); /* 36524 = 365*100 + 100/4 - 100/100 */
        g_day_no = g_day_no % 36524;

        if (g_day_no >= 365)
            g_day_no++;
        else
            leap = false;
    }

    gy += 4 * parseInt(g_day_no / 1461); /* 1461 = 365*4 + 4/4 */
    g_day_no %= 1461;

    if (g_day_no >= 366) {
        leap = false;

        g_day_no--;
        gy += parseInt(g_day_no / 365);
        g_day_no = g_day_no % 365;
    }

    for (var i = 0; g_day_no >= g_days_in_month[i] + (i == 1 && leap); i++)
        g_day_no -= g_days_in_month[i] + (i == 1 && leap);
    var gm = i + 1;
    var gd = g_day_no + 1;

    return (gy + "/" + (gm < 10 ? "0" : "") + gm + "/" + (gd < 10 ? "0" : "") + gd);
}

//******************************************************
//  HideRow
//******************************************************
function HideRow(RowId) {
    $("#" + RowId).removeAttr("style");
    $("#" + RowId).css("position", "inherit");
    $("#" + RowId).css("visibility ", "hidden");
    $("#" + RowId).hide();

    return;
}
//******************************************************
//  ShoweRow
//******************************************************
function ShowRow(RowId) {
    $("#" + RowId).removeAttr("style");
    $("#" + RowId).css("position", "static");
    $("#" + RowId).css("visibility ", "visible");
    $("#" + RowId).show();

    return;
}

//////********************************************************************
//////  ShowMessage
//////********************************************************************
//function ShowMessage(kendonotify, message, messageType, span) {
//    kendonotify.options.appendTo = span;
//    kendonotify.show(message, messageType);
//    switch (messageType) {
//        case "error":
//            $.growl.error({ message: message });
//            break;
//        case "success":
//            $.growl.notice({ message: message });
//            break;
//    }

//    return;
//}
////********************************************************************
////  ShowMessage
////********************************************************************
function ShowMessage1(message, messageType, span) {
    $("#" + span).html(message);
    switch (messageType) {
        case "error":
            $("#" + span).css("color", "tomato");
            break;
        case "success":
            $("#" + span).css("color", "green");
            break;
    }

    return;
}

function ResetNumericTextBox(id) {
    var numerictextbox;

    numerictextbox = $("#" + id.toString()).data("kendoNumericTextBox");
    numerictextbox.value('');
    return;
}

function SetNumericTextBox(id, value) {
    var numerictextbox;

    numerictextbox = $("#" + id.toString()).data("kendoNumericTextBox");
    numerictextbox.value(value);
    return;
}
function changeCursorPointer(obj) {
    $("#" + obj.id).css('cursor', 'pointer');
    return;
}
function CursorPointerChange(id) {
    $("#" + id).css('cursor', 'pointer');
    return;
}
function ConvertDate(objDate) {
    var jyear, jmonth, jday, gDate;

    //fromDate = objDate.toString();
    objDate = toEnglishNumber(objDate);

    jyear = objDate.substr(0, 4);

    jmonth = objDate.substr(5, 2);

    jday = objDate.substr(8, 2);

    gDate = jalaliToGregorian(jyear, jmonth, jday);

    return gDate;
}
function toEnglishNumber(inputNumber2) {
    if (inputNumber2 == undefined) return '';
    var str = $.trim(inputNumber2.toString());
    if (str == "") return "";
    str = str.replace(/۰/g, '0');
    str = str.replace(/۱/g, '1');
    str = str.replace(/۲/g, '2');
    str = str.replace(/۳/g, '3');
    str = str.replace(/۴/g, '4');
    str = str.replace(/۵/g, '5');
    str = str.replace(/۶/g, '6');
    str = str.replace(/۷/g, '7');
    str = str.replace(/۸/g, '8');
    str = str.replace(/۹/g, '9');
    return str;
}
function Convertfa2en(id) {
    $("#" + id).val(toEnglishNumber($("#" + id).val()));
    return;
}

function clearQuot(id) {

    $('#' + id).val($('#' + id).val().replace(/["']/g, ''));
}

function getJustNumber(e) {

    if (event.shiftKey == true) {
        event.preventDefault();
    }

    if ((event.keyCode >= 48 && event.keyCode <= 57) ||
        (event.keyCode >= 96 && event.keyCode <= 105) ||
        event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 37 ||
        event.keyCode == 39 || event.keyCode == 46 || event.keyCode == 190) {

    } else {
        event.preventDefault();
    }

    //if($(this).val().indexOf('.') !== -1 && event.keyCode == 190)
    //    event.preventDefault();    

    return;
}

function IsEmptyObj(id, staticNotification, msg, msgtype, msgPlace) {
    var flag;
    flag = false;


    if ($("#" + id.toString()).val().toString().trim() == "") {
        ShowMessage(staticNotification, msg, msgtype, "#" + msgPlace.toString());
        flag = true;
    }

    return flag;
}

function isEmpty(id, msg, msgtype, msgPlace) {

    var flag;
    flag = false;


    if ($("#" + id.toString()).val().toString().trim() == "") {
        ShowMessage(msgPlace.toString(), msgtype, msg);
        flag = true;
    }
    return flag;
}

function IsEmptyNumericTextBox(id, staticNotification, msg, msgtype, msgPlace) {
    var numerictextbox;
    var flag;
    flag = false;

    numerictextbox = $("#" + id.toString()).data("kendoNumericTextBox");

    if (numerictextbox.value() == "" || numerictextbox.value() == null) {
        ShowMessage(staticNotification, msg, msgtype, "#" + msgPlace.toString());
        flag = true;
    }

    return flag;
}
function CreateModal() {
    var innerHtmlStr = "";
    $("#PartialBody").html("");
    $(this).data('bs.modal', null);
    innerHtmlStr = "<div class=\"modal-dialog\" style=\"width:100%\">";
    innerHtmlStr += "<div class=\"modal-content\" style='width:100%'>";
    innerHtmlStr += "<div class=\"modal-header\">";
    innerHtmlStr += "<button type=\"button\" class=\"close\" data-dismiss=\"modal\">";
    innerHtmlStr += "&times;";
    innerHtmlStr += "</button>";
    innerHtmlStr += "</div>";
    innerHtmlStr += "<div class=\"modal-body\" id=\"PartialBody\" >";
    innerHtmlStr += "</div>";
    innerHtmlStr += "<div class=\"modal-footer\" style=\"text-align: center\">";
    innerHtmlStr += "</div>";
    innerHtmlStr += "</div>";
    innerHtmlStr += "</div>";
    $("#myArshianModal").html(innerHtmlStr);
    return;
}

function ShowMessage(id, objClass, msg) {
    $("#" + id.toString()).css("color", objClass);
    $("#" + id.toString()).html(msg);

    return;
}

function CheckNumber(num, msg, msgtype, msgPlace) {
    var flag = true;

    var reg = /^\d+$/;

    flag = reg.test(num);
    if (!flag) {
        ShowMessage(msgPlace.toString(), msgtype, msg);
    }
    return flag;

}
function ClearMessage(span) {
    $(span).html('');
    return;
}

function ShowAlert(bodyMassage, titleMassage) {

    // نمايش style تنظيمات
    if ($(window).width() > 768) //is not  xs device
    {

        var style = "width: 30%; margin-top: 15%  ";
        //var style = "width: 30%; ";
        $("#errorModalDocument").attr('style', style);
    } else {
        $("#errorModalDocument").attr('style', "");
    }
    //....
    $("#errorModalLabel").html("<b><h4>" + titleMassage + "</h4></b>");
    $("#errorModal").modal('show');
    //$("#errorbodymodal").html("<b><h4>" + bodyMassage + "</h4></b>");  //بدون دكمه ي بازگشت
    $("#errorbodymodal").html("<b><h4>" + bodyMassage + " </h4></b><p   style = ' margin-right: 80%  ' ><button  type='button'  class=' btn btn-default' data-dismiss='modal' aria-label='Close' > <i class=' glyphicon glyphicon-arrow-left'></i> <span >بازگشت</span> </button> </p>");


} // end of function ShowAlert()

////********************************************************************
////  AddEmptyRow2DropDownList
////********************************************************************
//function AddEmptyRow2DropDownList(dropdownlistName, Text, Value) {
//    $("#" + dropdownlistName).data("kendoDropDownList").dataSource.add({ JobTitleDesc: Text, JobCode: Value });
//    return;
//}
