var commentList = (function ($) {

    var viewModel = function () {
        this.UserId = ko.observable();
        this.UserName = ko.observable();
        this.Name = ko.observable();
        this.Role = ko.observable();
        this.LoginDate = ko.observable();
        this.LogoutDate = ko.observable();
        this.LoginAccessDurationTime = ko.observable();

        this.UserList = ko.observableArray();

        /*paging*/
        this.currentPageNumber = ko.observable();
        this.totalPageNumber = ko.observable();
        this.totalCount = ko.observable();
        this.firstIndex = ko.observable();
        this.lastIndex = ko.observable();
        this.order = ko.observable();
        
       
        /*paging*/
    };

    var isAdvanced = false, binded = false;
    var item;
    var pub = {};

    function user(UserId, UserName, Name, Role, LoginDate, LogoutDate, LoginAccessDurationTime, CustomProperties) {
        this.UserId = ko.observable(UserId);
        this.UserName = ko.observable(UserName);
        this.Name = ko.observable(Name);
        this.Role = ko.observable(Role);
        this.LoginDate = ko.observable(LoginDate);
        this.LogoutDate = ko.observable(LogoutDate);
        this.LoginAccessDurationTime = ko.observable(LoginAccessDurationTime);

       
    }

    function LoadFromServer(pageNum, url) {
       // blmsCommon.showLoading();
        console.log(url);
        reportService.getDetailAccessTimeReport(url,
            function (data) {

                var totalC = data[1]; //data.totalCount;
                var itemCountInPage = data[2];//data.countInPage;

                item.currentPageNumber(pageNum);
                item.totalCount(totalC);

                if (pageNum == 1) {
                    item.totalPageNumber(totalC == null ? null : Math.ceil(totalC / itemCountInPage));
                    item.firstIndex(item.totalCount() == 0 ? 0 : ((pageNum * itemCountInPage) - (itemCountInPage - 1)));
                    item.lastIndex(pageNum * itemCountInPage);
                }
                else {
                    //item.totalPageNumber(totalC == null ? null : Math.ceil(totalC / itemCountInPage));
                    item.firstIndex((item.currentPageNumber() * $("#tablePageSize").val()) - ($("#tablePageSize").val() - 1));
                    item.lastIndex((item.currentPageNumber() - 1) * $("#tablePageSize").val() + itemCountInPage);
                }

                console.log(data[0])
                bindList(data);

                if (!isAdvanced && $("#linkAdvanced").hasClass("collapse"))
                    $("#linkAdvanced").click();

               // blmsCommon.hideLoading();
            }
        );
    }

    function getFilters(skip, top, isAdvanced) {
        if (isAdvanced) {
            return getAdvancedSearchFilters(skip, top);
        }
    }

    function getAdvancedSearchFilters(skip, top) {
        var programs = $('#programs').val();
        var startDate = $(".startDate").val();
        var endDate = $(".endDate").val();
        var users = $('#ddlUsers').val();

        var tempRoles = [];
        $('#ddlRole :selected').each(function (i, selected) {
            tempRoles[i] = $(selected).val();
        });
        var roles = "";
        for (var i = 0; i < tempRoles.length; i++) { (i == 0) ? roles += tempRoles[i] : roles += "," + tempRoles[i] }

        var cst = [];
        $('input:checkbox[name=cstProp]:checked').each(function (i, checked) {
            cst[i] = $(this).val();
        });
        var customP = "";
        for (var i = 0; i < cst.length; i++) { (i == 0) ? customP += cst[i] : customP += "," + cst[i] }

        var cstFieldNumber = [];
        $('input:checkbox[name=cstProp]:checked').each(function (i, checked) {
            cstFieldNumber[i] = $(this).attr("data-value");
        });
        var customFieldNumber = "";
        for (var i = 0; i < cstFieldNumber.length; i++) { (i == 0) ? customFieldNumber += cstFieldNumber[i] : customFieldNumber += "," + cstFieldNumber[i] }

        return { "programs": "frt", "users": "frt", "roles": "frt", "customProperties": "frt", "cpFieldNumber": "frt", "startDate": "frt", "endDate": "frt", "skip": skip, "top": top };
    }

    pub.pagerAction = function (action, pageNum) {
        /*
        1 - first
        2 - previous
        3 - next
        4 - last
        5 - number
        */

        if (action == 1) { // first
            if ($("li.first").hasClass("disabled")) {
            }
            else {
                LoadFromServer(1, getFilters(0, $("#tablePageSize").val(), isAdvanced));
            }
        }
        else if (action == 2) { // previous
            if ($("li.prev").hasClass("disabled")) {
            }
            else {
                LoadFromServer(item.currentPageNumber() - 1, getFilters((item.currentPageNumber() - 2) * $("#tablePageSize").val(), $("#tablePageSize").val(), isAdvanced));
            }
        }
        else if (action == 3) { // next
            if ($("li.next").hasClass("disabled")) {
            }
            else {
                LoadFromServer(item.currentPageNumber() + 1, getFilters(item.currentPageNumber() * $("#tablePageSize").val(), $("#tablePageSize").val(), isAdvanced));
            }
        }
        else if (action == 4) { // last
            if ($("li.last").hasClass("disabled")) {
            }
            else {
                var skp = parseInt((item.totalPageNumber() - 1) * $("#tablePageSize").val());
                LoadFromServer(item.totalPageNumber(), getFilters(skp, $("#tablePageSize").val(), isAdvanced));
            }
        }
        else if (action == 5) { //number
            if ($("li[id^='nmbr" + pageNum + "']").hasClass("active")) {
            }
            else {
                var skp = parseInt((pageNum - 1) * $("#tablePageSize").val());
                LoadFromServer(pageNum, getFilters(skp, $("#tablePageSize").val(), isAdvanced));
            }
        }
    }

    pub.indexChanged = function () {
        $("#ul_paging").html('');
        if (item.totalPageNumber() > 1) {
            buildPagerDiv();

            if (item.currentPageNumber() == 1) {
                $("li.first").addClass("disabled");
                $("li.prev").addClass("disabled");
            }
            else {
                $("li.first").removeClass("disabled");
                $("li.prev").removeClass("disabled");
            }

            if (item.currentPageNumber() == item.totalPageNumber()) {
                $("li.next").addClass("disabled");
                $("li.last").addClass("disabled");
            }
            else {
                $("li.next").removeClass("disabled");
                $("li.last").removeClass("disabled");
            }

            $("li[id^='nmbr']").removeClass("active");
            $("li[data-id=" + item.currentPageNumber() + "]").addClass("active");
        }
    }

    function bindList(data) {

        //var results = ko.observableArray();
        //ko.mapping.fromJS(data[0], {}, results);
        //item.UserList.removeAll();
        //for (var i = 0; i < results().length; i++) {
        //    item.UserList.push(
        //        new user(
        //            results()[i].UserId(),
        //            results()[i].UserName(),
        //            results()[i].Name(),
        //            results()[i].Role(),
        //            moment.utc(results()[i].LoginDate()).local().format('lll'),
        //            results()[i].LogoutDate() != null ? moment.utc(results()[i].LogoutDate()).local().format('lll') : null,
        //            results()[i].LoginAccessDurationTime(),
        //            results()[i].CustomProperties()
        //        )
        //    );
        //}

        pub.indexChanged();
    }

    function buildPagerDiv() {
        $("#ul_paging").html('');

        var html = '<li class="first paginate_button paginate_button_disabled"><a href="javascript:pagerAction(1, 0);">' + "First" + '</a></li>';
        $("#ul_paging").append(html);

        html = '<li class="prev paginate_button paginate_button_disabled"><a href="javascript:pagerAction(2, 0);">← ' + "Prev" + '</a></li>';
        $("#ul_paging").append(html);

        if (item.totalPageNumber() < 5) {
            for (var i = 1; i <= item.totalPageNumber() ; i++) {
                html = '<li id="nmbr' + i + '" data-id="' + i + '"><a href="javascript:pagerAction(5, ' + i + ');">' + i + '</a></li>';
                $("#ul_paging").append(html);
            }
        }
        else {
            if (item.currentPageNumber() <= 3) {
                for (var i = 1; i <= 5 ; i++) {
                    html = '<li id="nmbr' + i + '" data-id="' + i + '"><a href="javascript:pagerAction(5, ' + i + ');">' + i + '</a></li>';
                    $("#ul_paging").append(html);
                }
            }
            else if (item.currentPageNumber() >= item.totalPageNumber() - 2) {
                for (var i = item.totalPageNumber() - 4; i <= item.totalPageNumber() ; i++) {
                    html = '<li id="nmbr' + i + '" data-id="' + i + '"><a href="javascript:pagerAction(5, ' + i + ');">' + i + '</a></li>';
                    $("#ul_paging").append(html);
                }
            }
            else {
                for (var i = item.currentPageNumber() - 2; i <= item.currentPageNumber() + 2 ; i++) {
                    html = '<li id="nmbr' + i + '" data-id="' + i + '"><a href="javascript:pagerAction(5, ' + i + ');">' + i + '</a></li>';
                    $("#ul_paging").append(html);
                }
            }
        }

        html = '<li class="next"><a href="javascript:pagerAction(3, 0);">' + "Next" + ' →</a></li>';
        $("#ul_paging").append(html);

        html = '<li class="last"><a href="javascript:pagerAction(4, 0);">' + "Last" + '</a></li>';
        $("#ul_paging").append(html);
    }



    $('.checkall').toggle(
           function () {
               $('.commentApprove').attr('Checked', 'Checked');
               $('.aligncenter').each(function () {
                   $(this).find('span').addClass('checked');
               });
           },
           function () {
               $('.commentApprove').removeAttr('Checked');

               $('.aligncenter').each(function () {
                   $(this).find('span').removeClass('checked');
               });
           }
       );

    pub.init = function () {      

       // $(".getReport").click(function () {

            isAdvanced = true;

            if (!binded) item = new viewModel();
           
            LoadFromServer(1, getFilters(0, $("#tablePageSize").val(), isAdvanced));

            if (!binded) {
                ko.applyBindings(item);
                binded = true;
                $(".divOuter").show();
            }            
        //});

        $("#tablePageSize").change(function () {
            LoadFromServer(1, getFilters(0, $("#tablePageSize").val(), isAdvanced));
        });

        $(".exportExcel").click(function () {
            var programs = $('#programs').val();
            var startDate = $(".startDate").val();
            var endDate = $(".endDate").val();
            var users = $('#ddlUsers').val();

            var tempRoles = [];
            $('#ddlRole :selected').each(function (i, selected) {
                tempRoles[i] = $(selected).val();
            });
            var roles = "";
            for (var i = 0; i < tempRoles.length; i++) { (i == 0) ? roles += tempRoles[i] : roles += "," + tempRoles[i] }

            var cst = [];
            $('input:checkbox[name=cstProp]:checked').each(function (i, checked) {
                cst[i] = $(this).val();
            });
            var customP = "";
            for (var i = 0; i < cst.length; i++) { (i == 0) ? customP += cst[i] : customP += "," + cst[i] }

            var cstFieldNumber = [];
            $('input:checkbox[name=cstProp]:checked').each(function (i, checked) {
                cstFieldNumber[i] = $(this).attr("data-value");
            });
            var customFieldNumber = "";
            for (var i = 0; i < cstFieldNumber.length; i++) { (i == 0) ? customFieldNumber += cstFieldNumber[i] : customFieldNumber += "," + cstFieldNumber[i] }


            document.location.href = "/Report/GetUserDetailAccessTimeDownload/?programs=" + programs + "&users=" + users + "&roles=" + roles + "&customProperties=" + customP + "&cpFieldNumber=" + customFieldNumber + "&startDate=" + startDate + "&endDate=" + endDate;
        });
      
    };

    pub.UserList = function () {
        return item.UserList();
    };

    return pub;
}(jQuery));