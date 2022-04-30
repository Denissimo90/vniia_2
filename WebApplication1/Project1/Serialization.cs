using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReportApp
{
    [Serializable]
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Company Company { get; set; }

        public Person()
        { }

        public Person(string name, int age, Company comp)
        {
            Name = name;
            Age = age;
            Company = comp;
        }
    }

    [Serializable]
    public class Company
    {
        public string Name { get; set; }

        // стандартный конструктор без параметров
        public Company() { }

        public Company(string name)
        {
            Name = name;
        }
    }
    /*
    <? xml version="1.0"?>
<ArrayOfPerson xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Person>
    <Name>Tom</Name>
    <Age>29</Age>
    <Company>
      <Name>Microsoft</Name>
    </Company>
  </Person>
  <Person>
    <Name>Bill</Name>
    <Age>25</Age>
    <Company>
      <Name>Apple</Name>
    </Company>
  </Person>
</ArrayOfPerson>
    */

    public class Serialization
    {
        static void Main2(string[] args)
        {
            Person person1 = new Person("Tom", 29, new Company("Microsoft"));
            Person person2 = new Person("Bill", 25, new Company("Apple"));
            Person[] people = new Person[] { person1, person2 };

            XmlSerializer formatter = new XmlSerializer(typeof(Person[]));

            using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, people);
            }

            using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
            {
                Person[] newpeople = (Person[])formatter.Deserialize(fs);

                foreach (Person p in newpeople)
                {
                    Console.WriteLine($"Имя: {p.Name} --- Возраст: {p.Age} --- Компания: {p.Company.Name}");
                }
            }
            Console.ReadLine();
        }
    }

    /*
     <?xml version="1.0" encoding="utf-8"?>
<phones>
  <phone name="iPhone 6">
    <company>Apple</company>
    <price>40000</price>
  </phone>
  <phone name="Samsung Galaxy S5">
    <company>Samsung</company>
    <price>33000</price>
  </phone>
</phones>

    XDocument xdoc = XDocument.Load("phones.xml");
foreach (XElement phoneElement in xdoc.Element("phones").Elements("phone"))
{
    XAttribute nameAttribute = phoneElement.Attribute("name");
    XElement companyElement = phoneElement.Element("company");
    XElement priceElement = phoneElement.Element("price");
     
    if (nameAttribute != null && companyElement!=null && priceElement!=null)
    {
        Console.WriteLine($"Смартфон: {nameAttribute.Value}");
        Console.WriteLine($"Компания: {companyElement.Value}");
        Console.WriteLine($"Цена: {priceElement.Value}");
    }
    Console.WriteLine();

    var items = from xe in xdoc.Element("phones").Elements("phone")
            where xe.Element("company").Value=="Samsung"
            select new Phone 
            { 
                Name = xe.Attribute("name").Value, 
                Price = xe.Element("price").Value 
            };
}
    */
    class Person2
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        public int Age { get; set; }
        [JsonIgnore]
        public int Age2 { get; set; }
    }
    class JSONApp
    {
        static void Main2(string[] args)
        {
            Person tom = new Person { Name = "Tom", Age = 35 };
            string json = JsonSerializer.Serialize<Person>(tom);
            Console.WriteLine(json);
            Person restoredPerson = JsonSerializer.Deserialize<Person>(json);
            Console.WriteLine(restoredPerson.Name);
        }
    }

}
