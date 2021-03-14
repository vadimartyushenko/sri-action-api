#define EXTENDED_DATUM_NODE_MODE
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Sri;

namespace DatumNode
{
 public static class Script
 {
  public static void Run(DatumNodeService datumnode, Sri.SriActionRequest request, out string response)
  {
   if (request == null)
    throw new FaultException("request is empty");

   if (!request.Items.Any())
    throw new FaultException("items is empty");

   Func<string, string> createFailResponse = x =>
   {
    var r = new SriActionResponse() { CheckSuccess = false, CheckMessage = x };
    var xml = datumnode.Serialize(r);
    return xml;
   };

    foreach (var i in request.Items)
    {
        if (i.SpecId != "cfs_line_access")
          throw new FaultException("Нормализация технологии доступна только для сервиса Линия (cfs_line_access)");
    }

   var orderResult = datumnode.Execute("*.*.oss_api.sri.action_api.normalize_sbms_tech", new Dictionary<string, object>()
   {
    { "cfs_public_ids", string.Join(";", request.Items.Select(x => x.ExtId))},
    { "note", "Нормализация технологии" }
   });

   response = string.Format(" Для CFS - {0} успешно нормализована технология", string.Join("; ", request.Items.Select(x => x.ExtId)));
  }
 }
}
