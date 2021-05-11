
if(localStorage.getItem("language") == null)
{
  localStorage.setItem("language", (navigator.language || navigator.userLanguage).toLowerCase());
}

var language = localStorage.getItem("language");
var current = window.location.pathname.replaceAll("/", "");

switch (language)
{
  case "zh-tw":
  break;
  default :
    language = "en";
}

if(current != language)
{
  var navigation = window.location.origin +"/"+ language;
  window.location.href = navigation;
}
