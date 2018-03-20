using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace tuwerkplekkenzoeker
{
    public class Program
    {
        public static List<Workplace> Main()
        {
            return Enumerable.Range(0, 100)
                 .AsParallel()
                 .Select(JsonHelper.Load)
                 .Where(a => a != null)
                 .SelectMany(a => a)
                 .ToList();

        }
    }

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
                    RootObject ff = JsonConvert.DeserializeObject<RootObject>(jsonFromApi);

                    return ff?.getWerkplekBeschikbaarheidByLocatieCodeResponse?
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
                    Console.WriteLine(id);
                    return null;
                }


            };



        }
    }
}

public class Workplace
{

    public string Location { get; set; }
    public int NumberOffComputer { get; set; }

    public int NumberOfAvailableComputers { get; set; }
}


public class BinnenlandsAdres
{
    public string straat { get; set; }
    public string huisnummer { get; set; }
    public string postcode { get; set; }
    public string plaats { get; set; }
}

public class FysiekAdres
{
    public BinnenlandsAdres binnenlandsAdres { get; set; }
}



public class RuimteLocatie
{
    public string locatieCode { get; set; }
    public FysiekAdres fysiekAdres { get; set; }
    public string naamNL { get; set; }
    public string naamEN { get; set; }
    public string lifecycleFase { get; set; }
}

public class Ruimte
{
    public string ruimteId { get; set; }
    public string naamNL { get; set; }
    public string naamEN { get; set; }
    public RuimteLocatie ruimteLocatie { get; set; }
}

public class Ruimte2
{
    public string ruimteId { get; set; }
}

public class ComputerGebruik
{
    public Ruimte2 ruimte { get; set; }
    public string aantalBeschikbaar { get; set; }
    public string aantalInGebruik { get; set; }
    public DateTime momentopnameDatumTijd { get; set; }
}

public class ComputerRuimteInformatie
{
    public Ruimte ruimte { get; set; }
    public ComputerGebruik computerGebruik { get; set; }
    public object evenementLijst { get; set; }
}

public class ComputerRuimteInformatieLijst
{
    [JsonConverter(typeof(SingleOrArrayConverter<ComputerRuimteInformatie>))]
    public List<ComputerRuimteInformatie> computerRuimteInformatie { get; set; }
}

public class GetWerkplekBeschikbaarheidByLocatieCodeResponse
{
    public ComputerRuimteInformatieLijst computerRuimteInformatieLijst { get; set; }
}

public class RootObject
{
    public GetWerkplekBeschikbaarheidByLocatieCodeResponse getWerkplekBeschikbaarheidByLocatieCodeResponse { get; set; }
}

class SingleOrArrayConverter<T> : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return (objectType == typeof(List<T>));
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JToken token = JToken.Load(reader);
        if (token.Type == JTokenType.Array)
        {
            return token.ToObject<List<T>>();
        }
        return new List<T> { token.ToObject<T>() };
    }

    public override bool CanWrite
    {
        get { return false; }
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}


