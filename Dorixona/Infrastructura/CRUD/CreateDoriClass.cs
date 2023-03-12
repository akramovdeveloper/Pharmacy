﻿using Domain.Core.Interfaces;
using Domain.Core.Models;
using Infrastructura.Paths_DB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructura.CRUD
{
    public class CreateDoriClass : ICreateDori
    {   
        static List<Dori>? doriList = new List<Dori>();
        public bool AddDori(Dori dori)
        {
            
            if (!File.Exists(Path_DB.path_Dori_DB))
            {
                File.Create(Path_DB.path_Dori_DB).Dispose();
            }
            else
            {
                doriList = JsonConvert.DeserializeObject<List<Dori>>(File.ReadAllText(Path_DB.path_Dori_DB));
            }
            var hasDori = doriList?.FirstOrDefault(x => x.Id.Equals(dori.Id));
            if (hasDori != null)
            {
                hasDori.Soni += dori.Soni;
                string stringToList = JsonConvert.SerializeObject(doriList, Formatting.Indented);
                File.WriteAllText(Path_DB.path_Dori_DB, stringToList);
                return true;
            }
            else if (hasDori == null)
            {
                doriList?.Add(dori);
                string stringToList = JsonConvert.SerializeObject(doriList, Formatting.Indented);
                File.WriteAllText(Path_DB.path_Dori_DB, stringToList);
                return true;
            }
            else
                return false;
        }
    }
}   
