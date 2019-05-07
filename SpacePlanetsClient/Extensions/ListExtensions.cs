using SpacePlanets.SharedModels.ServerToClient;
using SpacePlanetsClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsClient.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Iterate through a list of strings and return how many characters the longest item is.
        /// </summary>
        /// <param name="list">A list of strings</param>
        /// <returns>An integer representing how many characters the longest string is.</returns>
        public static int GetLengthOfLongestItem(this List<string> list)
        {
            int longest = 0;
            foreach (var item in list)
            {
                if (item.Length > longest)
                {
                    longest = item.Length;
                }
            }
            return longest;
            //return list.OrderByDescending(o => o.Length).Take(1).Single().Length;
        }

        /// <summary>
        /// ITerate through a list of button metadata items and return how many characters the longest ButtonText property is in the list.
        /// </summary>
        /// <param name="list">A list of MenuButtonMetadataItems</param>
        /// <returns>An integer representing the longest element in the list.</returns>
        public static int GetLengthOfLongestItem(this List<MenuButtonMetadataItem> list)
        {
            int longest = 0;
            foreach (var item in list)
            {
                if (item.ButtonText.Length > longest)
                {
                    longest = item.ButtonText.Length;
                }
            }
            return longest;
            //return list.OrderByDescending(o => o.Length).Take(1).Single().Length;
        }

        /// <summary>
        /// ITerate through a list of gernic picklist dto items and return how many characters the longest one is long.
        /// </summary>
        /// <param name="list">A list of GenericItemForPicklist</param>
        /// <returns>An integer representing the longest element in the list.</returns>
        public static int GetLengthOfLongestItem(this List<GenericItemForPicklist> list)
        {
            int longest = 0;
            foreach (var item in list)
            {
                if (item.Name.Length > longest)
                {
                    longest = item.Name.Length;
                }
            }
            return longest;
        }
    }
}
