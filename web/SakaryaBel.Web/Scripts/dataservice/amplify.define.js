var amplifyjs_cacheDuration = 15000;


function amplifyJsClearRequestCache(resourceId) {
    var prefix = "request-" + resourceId,
        length = prefix.length,
        type = amplify.request.resources[resourceId]

    $.each(amplify.store(), function (key) {
        if (key.substring(0, length) === prefix) {
            amplify.store(key, null);
        }
    });
}
