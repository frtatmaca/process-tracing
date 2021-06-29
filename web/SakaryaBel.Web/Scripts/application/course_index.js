var course_index = (function ($) {

    var viewModel = function () {
        this.ActivityId = ko.observable();
        this.Name = ko.observable();
        this.Description = ko.observable();
        this.Abbreviation = ko.observable();
        this.ActiveStatus = ko.observable();
        this.ActivityType = ko.observable();
        this.AlwaysOnTop = ko.observable();
        this.BeginDate = ko.observable();
        this.EndDate = ko.observable();
        this.FileEmbed = ko.observable();
        this.SortOrder = ko.observable();
        this.TaskDetail = ko.observable();
        this.TrackingGuid = ko.observable();

        this.ActivityList = ko.observableArray();

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

    function activity(ActivityId, Name, Description, Abbreviation, ActiveStatus, ActivityType, AlwaysOnTop, BeginDate, EndDate, FileEmbed, SortOrder, TaskDetail, TrackingGuid) {
        this.ActivityId = ko.observable(ActivityId);
        this.Name = ko.observable(Name);
        this.Description = ko.observable(Description);
        this.Abbreviation = ko.observable(Abbreviation);
        this.ActiveStatus = ko.observable(ActiveStatus);
        this.ActivityType = ko.observable(ActivityType);
        this.AlwaysOnTop = ko.observable(AlwaysOnTop);
        this.BeginDate = ko.observable(BeginDate);
        this.EndDate = ko.observable(EndDate);
        this.FileEmbed = ko.observable(FileEmbed);
        this.SortOrder = ko.observable(SortOrder);
        this.TaskDetail = ko.observable(TaskDetail);
        this.TrackingGuid = ko.observable(TrackingGuid);
    }

    function LoadFromServer(pageNum, url) {
        // blmsCommon.showLoading();
        console.log(url);
        courseService.getCourseLists(url,
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
                bindList(data[0]);

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

        return { "skip": skip, "top": top };
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
        console.log("bindList");
        console.log(data);
        var results = ko.observableArray();
        ko.mapping.fromJS(data, {}, results);
        item.ActivityList.removeAll();
        for (var i = 0; i < results().length; i++) {
            item.ActivityList.push(
                new activity(
                    results()[i].ActivityId(),
                    results()[i].Name(),
                    results()[i].Description() != null ? results()[i].Description().substring(0, 300) : "",
                    results()[i].Abbreviation(),
                    results()[i].ActiveStatus(),
                    results()[i].ActivityType(),
                    results()[i].AlwaysOnTop(),
                    results()[i].BeginDate() != null ? moment.utc(results()[i].BeginDate()).local().format('llll') : null,
                    results()[i].EndDate() != null ? moment.utc(results()[i].EndDate()).local().format('llll') : null,
                    results()[i].FileEmbed(),
                    results()[i].SortOrder(),
                    results()[i].TaskDetail() != null ? results()[i].TaskDetail().substring(0, 200) : "",
                    results()[i].TrackingGuid()
                )
            );
        }

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


        $('#btnCourseNewSubmit').click(function () {
            var activityName = $('#activityName').val();
            console.log("sdfsdf");
            console.log(activityName);
            if (activityName === "")
                $('#nameValidation').show();
            else
                $('#nameValidation').hide();


        });
    };

    pub.activityStatusReplace = function (el) {
        var actId = $(el).attr("data-Id");
        var actStatus = $(el).attr("data-status");

        $.ajax({
            url: "/Course/ActivityStatusReplace/",
            type: "POST",
            dataType: "json",
            data: { actId: actId, status: actStatus },
            success: function (res) {
                if (res == "success") {
                    alert("Aktivite statusu değiştirildi");
                } else {
                    console.log("Aktivite statusu değiştirilir iken bir hata ile karşılaşıldı.");
                }
            },
            error: function (res) {
                console.log("Error in process.");
                //blmsCommon.hideLoading();
            }
        });
    }

    pub.ActivityList = function () {
        return item.ActivityList();
    };

    return pub;
}(jQuery));