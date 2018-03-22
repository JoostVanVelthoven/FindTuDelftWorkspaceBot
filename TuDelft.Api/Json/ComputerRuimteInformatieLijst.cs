using Newtonsoft.Json;
using System.Collections.Generic;

public class ComputerRuimteInformatieLijst
{
    [JsonConverter(typeof(SingleOrArrayConverter<ComputerRuimteInformatie>))]
    public List<ComputerRuimteInformatie> computerRuimteInformatie { get; set; }
}


