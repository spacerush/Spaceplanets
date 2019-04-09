﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarvellousMarkovModels.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ModelBuilder m = new ModelBuilder(2);

            m.Teach("a");
        }

        [TestMethod]
        public void MethodName()
        {
            ModelBuilder m = new ModelBuilder(2);

            m.Teach("abcdefghijk");

            var mod = m.ToModel();

            Console.WriteLine(mod.Generate(new Random()));
        }

        [TestMethod]
        public void Stars()
        {
            List<string> starNames = new List<string> {
                "Acamar", "Achernar", "Achird", "Acrab",
                "Akrab", "Elakrab", "Graffias", "Acrux",
                "Acubens", "Adhafera", "Adhara", "Ain",
                "Aladfar", "Alamak", "Alathfar", "Alaraph",
                "Albaldah", "Albali", "Albireo", "Alchiba",
                "Alcor", "Alcyone", "Aldebaran", "Alderamin",
                "Aldhafera", "AlDhanab", "Aldhibah", "Aldib",
                "AlFawaris", "Alfecca", "Meridiana", "Alfirk",
                "Algedi", "AlGiedi", "Algenib", "Algieba",
                "Algol", "Algorab", "Alhajoth", "Alhena",
                "Alioth", "Alkaid", "AlKurud", "AlKalb", "AlRai",
                "Alkalurops", "AlKaphrah", "Alkes", "Alkurah", "Almach",
                "AlMinliar", "AlAsad", "AlNair", "Alnasl", "Alnilam",
                "Alnitak", "Alniyat", "Alphard", "Alphecca", "Alpheratz", "Alrai",
                "Alrakis", "Alrami", "Alrischa", "Alruccbah", "Alsafi",
                "Alsciaukat", "Alshain", "Alshat", "Altair", "Altais",
                "Altarf", "Alterf", "AlThalimain", "Aludra", "Alula",
                "Alwaid", "Alya", "Alzir", "Ancha", "Angetenar", "Ankaa",
                "Antares", "Arcturus","Arich","Arided", "Arkab", "Armus", "Arneb",
                "Arrakis","Ascella","Asellus","Ashlesha","Askella","Aspidiske",
                "Asterion","Asterope","Atik","Atlas","Atria","Auva", "Avior","Azaleh",
                "Azelfafage","Azha","Azimech","Azmidiske",

                "Baham", "Biham", "Baten", "Kaitos", "Becrux", "Beid", "Bellatrix",
                "Benetnasch", "Betelgeuse", "Botein", "Brachium",

                "Canopus", "Capella", "Caph", "Chaph", "Caphir", "Caput", "Medusae", "Castor",
                "Castula", "Cebalrai", "Celbalrai", "Ceginus", "Celaeno", "Chara", "Cheleb",
                "Chertan", "Chort", "Cor", "Caroli", "Hydrae", "Leonis", "Scorpii", "Serpentis",
                "Coxa", "Cujam", "Caiam", "Cursa", "Cynosura",

                "Dabih", "Decrux", "Deneb", "Denebola", "Dheneb", "Diadem", "Diphda",
                "Dnoces", "Dschubba", "Dubhe", "Duhr",

                "Edasich", "Electra", "Elmuthalleth", "Elnath", "Eltanin", "Enif",
                "Errai", "Etamin",

                "Fomalhaut", "FumalSamakah", "Furud",

                "Gacrux", "Garnet", "Gatria", "Gemma", "Gianfar", "Giedi", "GienahGurab", "Girtab",
                "Gomeisa", "Gorgonea", "Graffias", "Grafias", "Grassias", "Grumium",

                "Hadar", "Hadir", "Haedus", "Haldus", "Hamal", "Hassaleh", "Hydrus", "Heka", "Heze",
                "Hoedus", "Homam", "Hyadum", "Hydrobius",

                "Izar",

                "Jabbah", "Jih",

                "Kabdhilinan", "Kaffaljidhma", "Kajam", "Kastra", "Kaus", "Media", "Keid", "Kitalpha",
                "Kleeia", "Kochab", "Kornephoros", "Kraz", "Ksora", "Kullat", "Kuma",

                "Lanx", "Librae", "Superba", "Lesath", "Lucida",

                "Maasym", "Mahasim", "Maia", "Marfark", "Marfik", "Markab", "Matar", "Mebsuta", "Megrez",
                "Meissa", "Mekbuda", "Menchib", "Menkab", "Menkalinan", "Menkar", "Menkent", "Menkib",
                "Merak", "Merga", "Merope", "Mesarthim", "Miaplacidus", "Mimosa", "Minchir", "Minelava",
                "Minkar", "Mintaka", "Mira", "Mirach", "Miram", "Mirfak", "Mirzam", "Misam", "Mizar",
                "Mothallah", "Muliphein", "Muphrid", "Murzim", "Muscida", "Muscida", "Muscida",

                "Nair", "Naos", "Nash", "Nashira", "Navi", "Nekkar", "Nembus", "Neshmet", "Nihal", "Nunki",
                "Nusakan",

                "Okul",

                "Peacock", "Phact", "Phad", "Pherkad", "Pleione", "Polaris", "Pollux", "Porrima", "Praecipua",
                "Procyon", "Propus", "Proxi", "Pulcherrim",

                "Rana", "Ras", "Rasalas", "Rastaban", "Thaoum", "Regor", "Regulus", "Rigel", "Rigil", "Rijl",
                "Rotanev", "Ruchba", "Ruchbah", "Rukbat",

                "Sabik", "Sadachbia", "Sadalbari", "Sadalmelik", "Sadalsuud", "Sadatoni", "Sadira", "Sadr",
                "Sadlamulk", "Saiph", "Saiph", "Salm", "Sargas", "Sarin", "Sceptrum", "Scheat", "Scheddi",
                "Schedar", "Segin", "Seginus", "Sham", "Shaula", "Sheliak", "Sheratan", "Shurnarkabti", "Shashutu",
                "Sinistra", "Sirius", "Situla", "Skat", "Spica", "Sterope", "Sterope", "Sualocin", "Subra", "Suhail",
                "Suhel", "Sulafat", "Sol", "Syrma",

                "Tabit", "Talitha", "Tania", "Tarazet", "Tarazed", "Taygeta", "Tegmen", "Tegmine", "Terebellum", "Tejat",
                "Thabit", "Theemin", "Thuban", "Tien", "Toliman", "Torcularis", "Tseen", "Turais", "Tyl",

                "Unukalhai", "Unuk",

                "Vega", "Vindemiatrix",

                "Wasat", "Wei", "Wezen", "Wezn",

                "Yed", "Yildun",

                "Zaniah", "Zaurak", "Zavijava", "Zawiat", "Zedaron", "Zelphah", "Zibal", "Zosma", "Zuben", "Zubenelgenubi",
                "Zubeneschamali"
            };
            starNames = starNames.Select(a => a.ToLowerInvariant()).ToList();

            var builder = new ModelBuilder(3).Teach(starNames);

            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("{0},{1}", i, builder._productions.Count(a => a.Value.Count == i));
            }

            var model = builder.ToModel();
            Random r = new Random();

            int count = 100;
            while (count > 0)
            {
                var n = model.Generate(r).Trim();
                if (!starNames.Contains(n))
                {
                    Console.WriteLine(model.Generate(r));
                    count--;
                }
            }
        }
    }
}
