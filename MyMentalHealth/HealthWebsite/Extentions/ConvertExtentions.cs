using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyMentalHealth.Interface;

namespace MyMentalHealth.Extentions
{
    public static class ConvertExtentions
    {
        public static List<SelectListItem> ConvertToSelectList<T>(this IEnumerable<T> collection, int selectedValue) where T : IProperties
        {
            return (from item in collection
                    select new SelectListItem
                    {
                        Text = item.Title,
                        Value = item.Id.ToString(),
                        Selected = (item.Id == selectedValue)
                    }).ToList();
        }

    }
}

