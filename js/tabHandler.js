function tabHandler(event, tabID) 
{
  var i;
  var tabcontent = document.getElementsByClassName("tab-content");
  for (i = 0; i < tabcontent.length; i++) 
  {
    tabcontent[i].style.display = "none";
  }
  document.getElementById(tabID).style.display = "block";
  
  var tabUnderline = document.getElementsByClassName("tab-underline");
  for (i = 0; i < tabUnderline.length; i++) 
  {
    tabUnderline[i].className = tabUnderline[i].className.replace(" active", "");
  }               
  event.currentTarget.className += " active";
}