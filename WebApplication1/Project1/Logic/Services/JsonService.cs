using ReportApp.Common;
using App.Entities;
using ReportApp.Logic.Services.Interfacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReportApp.Logic.Services
{
    public class JsonService : IJsonService
    {
        IUnitOfWork uow;
        IManufactureService _manufactureService;
        IProductService _productService;

        public JsonService(IUnitOfWork uow, IManufactureService manufactureService, IProductService productService)
        {
            this.uow = uow;
            _manufactureService = manufactureService;
            _productService = productService;
        }

        public string IPAddress { get; set; }

        public void UpdateReportValues(DateTime? dateTime)
        {
            var jsonManufacturies = GetManufactoryList();
            List<Product> jsonProducts = new List<Product>();

            List<ProductQty> jsonProductQties = new List<ProductQty>();
            CheckForUpdate(jsonManufacturies);
            foreach (Manufacture item in jsonManufacturies)
            {
                Manufacture product = GetManufacturyDetail(item.Id);
                Manufacture productQty;
                if (dateTime != null)
                    productQty = GetManufacturyDetail(item.Id, Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd"));
                else
                    productQty = GetManufacturyDetail(item.Id, null);

                foreach (var prod in product.Products)
                {
                    prod.ManufactureId = item.Id;
                }
                _productService.Insert(product.Products);

                foreach (var prod in productQty.Products)
                {
                    foreach (var qty in prod.Qties)
                    {
                        qty.ProductId = prod.Id;
                        qty.Id = prod.Id;
                        qty.DateOfManufacture = dateTime == null ? new DateTime() : Convert.ToDateTime(dateTime);
                    }
                    _productService.Insert(prod.Qties);

                }

            }
        }

        private void CheckForUpdate(List<Manufacture> jsonManufacturies)
        {
            var dbManufacturies = uow.ManufactureRepository.GetEntities();
            /*if (dbManufacturies.Count == 0)
                foreach (var json in jsonManufacturies)
                {
                    _manufactureService.Insert(json);
                }
            else
                foreach (var json in jsonManufacturies)
                {
                    var dbManuf = dbManufacturies.FirstOrDefault(d => d.Id == json.Id);
                    if (dbManuf.Entered != json.Entered)
                    {
                        _manufactureService.DeleteFactory(dbManuf.Id);
                        _manufactureService.Insert(json);
                    }

                }*/

        }

        public List<Manufacture> GetManufactoryList()
        {
            Manufacture[] list;

            string values = EndpointService.GetRequestFromEndpoint("factories", new List<string>().ToArray()).Result + "";

            if (!string.IsNullOrEmpty(values))
            {
                list = JsonSerializer.Deserialize<Manufacture[]>(values);
                return list.ToList();
            }

            return new List<Manufacture>();
        }

        public Manufacture GetManufacturyDetail(int manufacturyId, string date = "")
        {
            Manufacture value = new Manufacture();

            string values = EndpointService.GetRequestFromEndpoint("factories", new List<string>() {
                manufacturyId + "", string.IsNullOrEmpty(date) ? null : date }.ToArray()).Result + "";

            if (!string.IsNullOrEmpty(values))
            {
                value = JsonSerializer.Deserialize<Manufacture>(values);
                return value;
            }

            return value;
        }


        //public void GetJsonData()
        //{
        //    var client = new RestClient
        //    string json = JsonSerializer.Serialize<Person>(tom);
        //    Console.WriteLine(json);
        //    Person restoredPerson = JsonSerializer.Deserialize<Person>(json);
        //    Console.WriteLine(restoredPerson.Name);
        //}
    }
}
