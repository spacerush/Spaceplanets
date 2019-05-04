using SpacePlanetsMvc.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Models.WebViewModels
{
    public class AccountIndexViewModel
    {
        public string Message { get; set; }
        public GetCharactersByPlayerIdResponse GetCharactersByPlayerIdResponse { get; set; }
    }
}
