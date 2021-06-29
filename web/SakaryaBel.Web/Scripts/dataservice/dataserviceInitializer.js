var serviceInitializer = (function ($) {
    var pub = {};

    pub.init = function () {
        // contactService.init();
        fileService.init();
        securityService.init();       
        userService.init();       
        helperValuesService.init();        
        accountService.init();                                       
        messageService.init();   
        userService.init();
        actionService.init();
        problemService.init();
    };


    return pub;
}(jQuery));


