using System;
using System.Collections.Generic;
using MigraDoc.DocumentObjectModel;

namespace Atena.Services.Interfaces;

public interface IAtenaReport
{
    public List<Document> getArraysToPdsFromData(string periodText, string groupName, Dictionary<string, List<Tuple<string, string>>> monthsNuaHours);

    public Document createArrayFromData(string periodText,string groupName,string month, List<Tuple<string, string>> nuasHours);

    public void savePdfs (string periodText,string groupName,  List<Document> pdfs);

}
