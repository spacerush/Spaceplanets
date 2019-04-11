using CasualGodComplex;
using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpLib.Objects
{
    public class GalaxyContainer : Document
    {
        Galaxy Galaxy { get; set; }
    }
}
