mergeInto(LibraryManager.library,{

    GetCookie: function(str)
    {
        var _v = UTF8ToString(str);
        var cookieList = document.cookie.split(";");

        for (var i=0; i<cookieList.length; i++)
        {
            var _cookieIndexValue = cookieList[i].trim();
            if (_cookieIndexValue.indexOf(_v+"=")==0) return _cookieIndexValue.substring((_v+"=").length,c.length);
        }
        return "";
    },

    SetCookie: function(_index,_value)
    {
        document.cookie = UTF8ToString(_index) + "=" + UTF8ToString(_value);
    }

}
)