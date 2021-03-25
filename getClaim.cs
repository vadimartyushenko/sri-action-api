p_result = "";
p_debug_xtask = "";

var task = datumnode.ExecuteQuery("*.*.ossTechClaim.api_esb.action_api.getOrderList", new Dictionary<string, object>() {
{"PublicOrderId", p_public_order_id}
});

var message = Convert.ToString(task["message"]);

if ( !String.IsNullOrEmpty(message) ) {
  throw new Exception("test");
}

var xTask = XDocument.Parse(task.ResultSet).Root.Elements("Entity").First();


var p_prev_parameters = xTask.Element("parameter_prefix")  != null ?  xTask.Element("parameter_prefix") .Value : "";

var model  = new DatumNode.OssTechClaim.Api_esb.Action_api.EsbOrderModel();

if( xTask.Element("PublicOrderId") != null  ) { model.PublicOrderId = xTask.Element("PublicOrderId").Value; }
if( xTask.Element("OrderId") != null  ) { model.OrderId = xTask.Element("OrderId").Value; }
if( xTask.Element("ResourceType") != null  ) { model.ResourceType = xTask.Element("ResourceType").Value; }

if( xTask.Element("Resource_") != null  ) { model.Resource_ = xTask.Element("Resource_").Value; }
if( xTask.Element("DateCreate") != null  ) { model.DateCreate = xTask.Element("DateCreate").Value; }
if( xTask.Element("ReasonType") != null  ) { model.ReasonType = xTask.Element("ReasonType").Value; }

if( xTask.Element("DateExpected") != null  ) { model.DateExpected = xTask.Element("DateExpected").Value; }
if( xTask.Element("OrderStatus") != null  ) { model.OrderStatus = xTask.Element("OrderStatus").Value; }
if( xTask.Element("DateClose") != null  ) { model.DateClose = xTask.Element("DateClose").Value; }

if( xTask.Element("ReasonTypeCloseOrder") != null  ) { model.ReasonTypeCloseOrder = xTask.Element("ReasonTypeCloseOrder").Value; }
if( xTask.Element("parameter_prefix") != null  ) { model.parameter_prefix = xTask.Element("parameter_prefix").Value; }

var req_parameters = new DatumNode.JsonSerializableDictionary();

//req_parameters[p_prev_parameters + "." + "customer_line_number"].Value = (xTask.Element("customer_line_number") != null ? xTask.Element("customer_line_number").Value : "");

var request= datumnode.ToJsonDictionary(model);

var orderList = new DatumNode.OssTechClaim.Api_esb.Action_api.EsbOrdersList();
var request1 = datumnode.ToJsonDictionary(orderList);

request1["OrdersLists"].Value = request;

p_result = datumnode.JsonSerialize(request1);
