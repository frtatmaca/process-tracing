var examService = (function ($) {
    var pub = {};

    pub.init = function () {

        amplify.request.define("exam.searchGrading", "ajax", {
            url: '/Exam/SearchGrading',
            dataType: "html",
            type: "POST"
        });

        amplify.request.define("exam.saveGrades", "ajax", {
            url: '/Exam/SaveGrades',
            dataType: "JSON",
            type: "POST"
        });

        amplify.request.define("examKeyword.values", "ajax", {
            url: '/Exam/GetQuestionsByKeywords',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("examCategory.values", "ajax", {
            url: '/Exam/GetQuestionsByCategories',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("examTopic.values", "ajax", {
            url: '/Exam/GetQuestionsByTopics',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("examMasterCourse.values", "ajax", {
            url: '/Exam/GetQuestionsByMasterCourse',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("examDifficultytWithQuestionCount.values", "ajax", {
            url: '/Exam/GetDifficultyWithQuestionCount',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("examQuestions.values", "ajax", {
            url: '/Exam/GetExamQuestions',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("examQuestionsBySession.values", "ajax", {
            url: '/Exam/GetExamQuestionsBySession',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("exam.publish", "ajax", {
            url: '/Exam/Publish',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("exam.unpublish", "ajax", {
            url: '/Exam/Unpublish',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("exam.preview", "ajax", {
            url: '/Exam/Preview',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("exam.getExams", "ajax", {
            url: '/Exam/Search',
            dataType: "json",
            type: "POST",
            cache: {
                type: "persist",
                expires: 60000
            }
        });

        amplify.request.define("exam.userstakenexam", "ajax", {
            url: '/Exam/GetUsersAssignedExams',
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8"
        });

        amplify.request.define("exam.getDashboardQuantityResultItems", "ajax", {
            url: '/Exam/GetDashboardQuantityResultItems',
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8"
        });

        amplify.request.define("exam.getExamFeed", "ajax", {
            url: '/Exam/GetExamFeed',
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8"
        });

        amplify.request.define("exam.getActualExamSessions", "ajax", {
            url: '/Exam/GetActualExamSessions',
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8"
        });

        amplify.request.define("exam.getHelpDeskList", "ajax", {
            url: '/Exam/GetHelpDeskList',
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8"
        });
    };

    pub.getHelpDeskList = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "exam.getHelpDeskList",
            data: JSON.stringify(data),
            contentType: "application/json",
            success: function (data) {
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.getActualExamSessions = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "exam.getActualExamSessions",
            data: JSON.stringify(data),
            contentType: "application/json",
            success: function (data) {
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.getDashboardQuantityResultItems = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "exam.getDashboardQuantityResultItems",
            data: JSON.stringify(data),
            contentType: "application/json",
            success: function (data) {
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.getExamFeed = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "exam.getExamFeed",
            data: JSON.stringify(data),
            contentType: "application/json",
            success: function (data) {
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.usersTakenExam = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "exam.userstakenexam",
            data: JSON.stringify(data),
            contentType: "application/json",
            success: function (data) {
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.searchGrading = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "exam.searchGrading",
            data: data,
            success: function (data) {
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.saveGrades = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "exam.saveGrades",
            data: data,
            success: function (data) {
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.getQuestionsByKeywords = function (data, successCallBack, errorCallBack) {

        amplify.request({
            resourceId: "examKeyword.values",
            data: data,
            success: function (data) {
                //parent.location.reload(true);
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.getQuestionsByCategories = function (data, successCallBack, errorCallBack) {

        amplify.request({
            resourceId: "examCategory.values",
            data: data,
            success: function (data) {
                //parent.location.reload(true);
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.getQuestionsByTopics = function (data, successCallBack, errorCallBack) {

        amplify.request({
            resourceId: "examTopic.values",
            data: data,
            success: function (data) {
                //parent.location.reload(true);
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.getQuestionsByMasterCourse = function (data, successCallBack, errorCallBack) {

        amplify.request({
            resourceId: "examMasterCourse.values",
            data: data,
            success: function (data) {
                //parent.location.reload(true);
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.getDifficultyWithQuestion = function (data, successCallBack, errorCallBack) {

        amplify.request({
            resourceId: "examDifficultytWithQuestionCount.values",
            data: data,
            success: function (data) {
                //parent.location.reload(true);
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.getExamQuestions = function (data, successCallBack, errorCallBack) {

        amplify.request({
            resourceId: "examQuestions.values",
            data: data,
            success: function (data) {
                //parent.location.reload(true);
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.getExamQuestionsBySession = function (data, successCallBack, errorCallBack) {

        amplify.request({
            resourceId: "examQuestionsBySession.values",
            data: data,
            success: function (data) {
                //parent.location.reload(true);
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.publish = function (data, successCallBack, errorCallBack) {

        amplify.request({
            resourceId: "exam.publish",
            data: data,
            success: function (data) {
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.unpublish = function (data, successCallBack, errorCallBack) {

        amplify.request({
            resourceId: "exam.unpublish",
            data: data,
            success: function (data) {
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.preview = function (data, successCallBack, errorCallBack) {

        amplify.request({
            resourceId: "exam.preview",
            data: data,
            success: function (data) {
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.getExams = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "exam.getExams",
            data: data,
            success: function (data) {
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    return pub;
}(jQuery));