using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TuDelft.Api
{
    public class JsonHelper
    {
        public static List<Workplace> Load(int id)
        {
            string url = $"https://api.tudelft.nl/v0/werkplekken/{id}";
            using (var webClient = new System.Net.WebClient())
            {
                string jsonFromApi = webClient.DownloadString(url);
                try
                {
                    RootObject json = JsonConvert.DeserializeObject<RootObject>(jsonFromApi);

                    return json?.getWerkplekBeschikbaarheidByLocatieCodeResponse?
                       .computerRuimteInformatieLijst?
                       .computerRuimteInformatie.Select(a =>
                    new Workplace
                    {
                        Location = a.ruimte.ruimteLocatie.naamEN + " - " + a.ruimte.naamEN,
                        NumberOfAvailableComputers = Convert.ToInt32(a.computerGebruik.aantalBeschikbaar),
                        NumberOffComputer = Convert.ToInt32(a.computerGebruik.aantalInGebruik),
                    }).ToList();

                }
                catch
                {
                    return null;
                }


            };



        }
    }
}


