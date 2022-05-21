using AuthService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Logic.Services.Interfacies
{
    public interface IJsonService
    {
        string IPAddress { get; set; }
        void UpdateReportValues(DateTime? dateTime);
        List<Manufacture> GetManufactoryList();
        Manufacture GetManufacturyDetail(int manufacturyId, string date = "");
    }
}
