var coderAccess = false;
var controllerAccess = false;
var oldLink = null;

function invokeAction(controller, action, params, callback) {
    var url = "http://" + window.location.host + '/' + controller + '/' + action + "?" + params;
    var wRequest = new Sys.Net.WebRequest();
    wRequest.set_url(url);
    wRequest.set_httpVerb('GET');
    wRequest.add_completed(callback);
    wRequest.invoke();
}

function sourceodeCallback(result) {
    if (result.get_statusCode() != '404') {
        var response = result.get_responseData();

        var path = "http://" + window.location.host + '/Resources/SourceCode/' + response + '.htm';
        var newWindow = window.open(path, '');
        newWindow.focus();
    }
}