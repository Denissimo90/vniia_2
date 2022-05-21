﻿using AuthService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Logic.Services.Interfacies
{
    public interface IManufactureService
    {
        void Insert(Manufacture facture);
        void DeleteFactory(int id);
        List<Manufacture> GetFactyries();
        Manufacture GetFactoryById(int id);
    }
}
