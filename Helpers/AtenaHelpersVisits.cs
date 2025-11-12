using System;
using System.Collections.Generic;
using System.Linq;
using DataBaseContext;
using Humanizer;

namespace Atena.Helpers;

public static  class AtenaHelpersVisits
{

    public static Dictionary<string, string> countAndCoumputeVisits(Dictionary<string, List<IGrouping<int, Visit>>> input)
    {
        string stringOfTotalVisits = "";
        double finalHours = 0;
        Dictionary<string, string> outPut = new Dictionary<string, string>();

        foreach (var key in input.Keys)
        {
            var listOfGrupedVisits = input[key];

            stringOfTotalVisits = "";
            finalHours = 0;
            foreach (var groupedVisits in listOfGrupedVisits)
            {
                long studentElapsedByGroup = 0;
                groupedVisits.ToList().ForEach(v =>
                {
                    studentElapsedByGroup += (long)(v.End - v.Start).Value.TotalMilliseconds;
                });
                var totalHoursByMonthByStudent = Math.Round(TimeSpan.FromMilliseconds(studentElapsedByGroup).TotalHours);
                finalHours += totalHoursByMonthByStudent;
                stringOfTotalVisits += $"{groupedVisits.Key}:{totalHoursByMonthByStudent},";
            }
            stringOfTotalVisits += $"0:{finalHours}";
            outPut[key] = stringOfTotalVisits;
        }

        return outPut;
    }
    

    public static Dictionary<string, string> CorrectVistsFromComputed(Dictionary<string, string> input)
    {
        // 2:8, 4:8, 3:5, 5:6, 0:27
        List<string> monthKeys = new List<string>();

        foreach (var nua in input.Keys)
        {
            var visitsPerMonths = input[nua].Split(',');

            foreach (var visitPerMonth in visitsPerMonths)
            {
                var month = visitPerMonth.Split(':')[0];

                if (!monthKeys.Contains(month))
                    monthKeys.Add(month);
            }
        }

        foreach (var nua in input.Keys)
        {
            var visitsPerMonths = input[nua].Split(',');

            foreach (var monthKey in monthKeys)
            {
                bool isPressent = false;
                foreach (var visitPerMonth in visitsPerMonths)
                {
                    var month = visitPerMonth.Split(':')[0];


                    if(month==monthKey)
                        isPressent = true;
                }

                if (!isPressent)
                    input[nua] += $",{monthKey}:0";
            }
        }
        
        return input;
    }

}
