function doPost(e)
{
  var parameter = e.parameter;
  var table = getJson(parameter.id, parameter.name);

  return ContentService.createTextOutput(table);
}
function getJson(id, name)
{
  var app = SpreadsheetApp.openById(id);
  var sheet = app.getSheetByName(name);

  var datas = sheet.getDataRange();

  var table = "";
  var headers = datas.getValues()[0];
  // Logger.log(headers);    
  for(var row = 1; row < datas.getNumRows(); row++)
  {
    var items = datas.getValues()[row];
    var fields = "";

    for(var column = 0; column < datas.getNumColumns(); column++)
    {
      if(column > 0) fields += ",";
      fields += `"${headers[column]}":"${items[column]}"`;
    }

    if(row > 1) table += ",";
    table += `{${fields}}`;
  }

  // Logger.log(table);

  return `[${table}]`;
}