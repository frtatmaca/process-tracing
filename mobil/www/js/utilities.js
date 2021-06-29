angular.module('sakaryabel.utilities', [])
.factory('Auth', ['$ls', function ($ls) {
    this.host = window.Settings.defaultApiHost;
    this.logo = window.Settings.defaultLogo;
    return {

        getToken: function () {
            return $ls.get("AuthToken");
        },
        setToken: function (token) {
            $ls.set("AuthToken", token);
        },
        getUserName: function () {
            return $ls.get("userName");
        },
        setUserName: function (userName) {
            $ls.set("userName", userName);
        },
        getPassword: function (password) {
            return $ls.get("password");
        },
        setPassword: function (password) {
            $ls.set("password", password);
        },
        getLogo: function () {
            return window.Settings.defaultLogo;
        }        
    };


}])
.factory('httpRequestInterceptor', ['$q', '$injector', 'HttpCache', '$timeout', function ($q, $injector, HttpCache, $timeout) {
    var Auth = $injector.get("Auth");
    var interceptor = {
        request: function (config) {
            if (config.url.indexOf('{apihost}') > -1) {
                if (!config.timeout)
                    config.timeout = window.Settings.defaultRequestTimeout;
                if (!config.hasOwnProperty('spinner'))
                    config.spinner = true;
                config.url = config.url.replace('{apihost}', window.Settings.defaultApiHost + '/mobile/v' + window.Settings.defaultApiVersion);
                config.url = URI(config.url).addSearch({ 'token': Auth.getToken() }).toString();
                console.log(config.url);
                if (config.spinner) {
                    var $spinner = $injector.get('$spinner');
                    $spinner.show();
                }
            }
          
            if (config.cache == false) {
                if (window.Settings.logingEnabled)
                    console.log('Cache removed : ' + config.url);
                try {
                    HttpCache.remove(config.url);
                }
                catch (Ex) {
                    console.log(Ex);
                }
            }

            if (window.Settings.logingEnabled && config.url.indexOf('http') > -1)
                console.log('Api Request : ' + config.url);

            return config;
        },
        response: function (response) {

            if (response && response.config && response.config.url.indexOf('http') > -1) {
                var url = response.config.url;
                if (url.indexOf(window.Settings.defaultApiHost) > -1) {
                    if (window.Settings.logingEnabled) {
                        console.log('Api Response : ' + url);
                    }
                    $timeout(function () {
                        HttpCache.remove(url);
                        if (window.Settings.logingEnabled) console.log('Cache removed : ' + url);
                    }, window.Settings.cacheTime);

                    var $spinner = $injector.get('$spinner');
                    setTimeout(function () {
                        $spinner.hide();
                    }, 500);
                }
            }


            return response;
        },
        requestError: function (error) {
            var $spinner = $injector.get('$spinner');
            $spinner.forceHide(JSON.stringify(error));
            return error;
        },
        responseError: function (error) {
            var $spinner = $injector.get('$spinner');
            $spinner.forceHide(JSON.stringify(error));
            return error;
        }
    };
    return interceptor;
}])
.factory('HttpCache', function ($cacheFactory) {
    return $cacheFactory.get('$http');

    /*  istek yapılırken options parametresiyle cache:true değeri verilirse
    appSettings.js dosyasındaki cacheTime parametresinde belirtilen
    süre kadar(ms cinsinden)  ilgili veri cachte tutulur, süre dolunca cachten silinir.
    Not:Verilerin gösterildiği template'in cache değeri app.js dosyasındaki ilgili kısımdan false yapılmalı. Aksi takdirde
    templateden istek gelmediği için cache süresi dolmuş olsa dahi yeni veriler yüklenmez.
    */
})
.factory('$ls', ['$window', function ($window) {
    return {
        set: function (key, value) {
            var compressed = Settings.compressedStorage ? LZString.compressToUTF16(value) : value;
            $window.localStorage[key] = compressed;
        },
        get: function (key, defaultValue) {
            var value = $window.localStorage[key];
            if (value) value = Settings.compressedStorage ? LZString.decompressFromUTF16(value) : value;
            return value || defaultValue;
        },
        setObject: function (key, value) {
            var compressed = Settings.compressedStorage ? LZString.compressToUTF16(JSON.stringify(value)) : JSON.stringify(value);
            $window.localStorage[key] = compressed;
        },
        getObject: function (key) {
            var obj = null;
            var value = $window.localStorage[key];
            if (value)
                obj = Settings.compressedStorage ? JSON.parse(LZString.decompressFromUTF16(value)) : JSON.parse(value);
            return obj;
        },
        remove: function (key) {
            $window.localStorage.removeItem(key);
            return true;
        },
        removeAll: function () {
            $window.localStorage.clear();
            return true;
        }
    }
}])
.factory('AsyncTranslationLoader', function ($http, $q, $timeout, $ls) {

    return function (options) {
        var deferred = $q.defer(),
        translations;
        var languages = [];
        $http.get('content/translations.json').then(function (response) {
            if (response && response.data) {
                var datas = response.data;

                for (var i in datas) {
                    var selected = false;
                    if (datas[i].key == options.key) {
                        deferred.resolve(datas[i].data);
                        selected = true;
                    }
                    languages.push({ name: datas[i].name, key: datas[i].key, selected: selected });
                }
            }
            else {
                console.log("nonono");
            }

            $ls.setObject("Languages", languages);
        });

        return deferred.promise;
    };
})
.factory('$spinner', function ($ionicLoading) {
    var openCount = 0;
    return {
        show: function () {
            openCount++;
            var template = (window.Settings && window.Settings.hasOwnProperty('spinnerTemplate')) ? window.Settings.spinnerTemplate : '<ion-spinner icon="lines" class="spinner-calm"></ion-spinner>';
            $ionicLoading.show({
                template: template
            });
            return true;
        },
        hide: function () {
            openCount--;
            if (openCount <= 0) {
                openCount = 0;
                $ionicLoading.hide();
            }
            return true;
        },
        forceHide: function (message) {
            swal({ title: message, text: message, timer: 5000, type: 'warning' });
            openCount = 0;
            $ionicLoading.hide();
            return true;
        }
    };
})
.factory('Resource', function ($cordovaCamera, $cordovaCapture, $cordovaFileTransfer, $sce, Auth) {
    return {

        GetImage: function (fromCamera) {

            var options = {
                quality: 50, //0-100
                destinationType: Camera.DestinationType.FILE_URI, //DATA_URL (returns base 64) or FILE_URI (returns image path)
                allowEdit: true, //allow cropping
                encodingType: Camera.EncodingType.JPEG,
                popoverOptions: CameraPopoverOptions,
                saveToPhotoAlbum: false
            };
            var resource = { Url: "", Type: 0, Name: "", MimeType: "" };

            if (fromCamera == 0)
                options.sourceType = Camera.PictureSourceType.PHOTOLIBRARY;
            else if (fromCamera == 1)
                options.sourceType = Camera.PictureSourceType.CAMERA;

            var func = $cordovaCamera.getPicture(options).then(function (imageUrl) {
                resource.nativeUrl = imageUrl;
                resource.Type = 0;
                resource.Url = $sce.trustAsResourceUrl(imageUrl);
                resource.Name = imageUrl.substr(imageUrl.lastIndexOf('/') + 1);
                resource.MimeType = "image/jpeg";
                console.log("eddd ");
                console.log(resource);
                return resource;
            });
            return func;
        },
        GetVideo: function () {
            var resource = { Url: "", Type: 0, Name: "", MimeType: "" };
            var func = $cordovaCapture.captureVideo({ limit: 1 }).then(function (data) {
                resource.nativeUrl = data[0].fullPath;
                resource.Type = 1;
                resource.Url = $sce.trustAsResourceUrl(resource.nativeUrl);
                resource.Name = data[0].name;
                resource.MimeType = data[0].type;
                return resource;
            });
            return func;
        },
        Upload: function (resource) {
            var options = {
                fileKey: "avatar",
                fileName: resource.Name,
                chunkedMode: false,
                mimeType: resource.MimeType,
                headers: { Connection: "close" }
            }
            var target = "http://" + Settings.defaultApiHost + "/mobile/v1/PostFile?token=" + Auth.token + "&type=" + resource.Type;
            var func = $cordovaFileTransfer.upload(target, resource.nativeUrl, options).then(function (result) {
                console.log(result);
                return result;
            }, function (err) {

            }, function (progress) {
                return progress.loaded / progress.total * 100;
            });
            return func;
        }
    }
})
;
