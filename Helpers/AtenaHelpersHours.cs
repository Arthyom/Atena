using System;
using System.Collections.Generic;
using System.Linq;

namespace Atena.Helpers;

public static class AtenaHelpersHours
{
    public static Dictionary<string,List<Tuple< string,string>>> switchNuaByMonthAsKey( Dictionary<string, string> input)
    {
        Dictionary<string,List<Tuple< string,string>>> monthsXnuaXHours = new Dictionary<string,List< Tuple<string, string>>>();
        foreach (var nuaKey in input.Keys)
        {
            var monthsXHours = input[nuaKey].Split(',');
            foreach (var itemM in monthsXHours)
            {
                var splitedMontHour = itemM.Split(':');
                if (splitedMontHour.Length >= 2)
                {
                    var month = splitedMontHour[0];
                    var hour = splitedMontHour[1];
                 
                
                    if (monthsXnuaXHours.ContainsKey(month))
                    {
                        monthsXnuaXHours[month].Add(new Tuple<string, string>(nuaKey, hour));
                    }
                    else
                    {
                        monthsXnuaXHours[month] = new List<Tuple<string, string>>()
                        {
                            new Tuple<string, string>(nuaKey, hour)
                        };
                    }
                }
            }
        }
        
        return monthsXnuaXHours;
    }
}
