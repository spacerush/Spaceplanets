using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsClient.Models
{
    public class MenuButtonMetadataItem
    {
        public Guid Id { get; set; }
        public string ButtonText { get; set; }
        public string ButtonType { get; set; }

        public MenuButtonMetadataItem()
        {

        }

        public MenuButtonMetadataItem(string text)
        {
            this.ButtonText = text;
        }

        public MenuButtonMetadataItem(Guid id, string text, string type)
        {
            Id = id;
            this.ButtonText = text;
            this.ButtonType = type;
        }

    }
}
