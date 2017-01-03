Errors

Error: Cannot find module './wwwroot/dist/vendor-manifest.json'
at Function.Module._resolveFilename (module.js:438:15)
at Function.Module._load (module.js:386:25)....

=> execute the command below under the UrlCatalog project folder:

webpack --config webpack.config.vendor.js

https://github.com/aspnet/JavaScriptServices/issues/99#issuecomment-221802504