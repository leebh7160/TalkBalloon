using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CSVData
{
    private Dictionary<int, string> DIC_csvData = new Dictionary<int, string>();


    internal void CSVDataLoadStart()
    {
        CSVDataLoad();//코루틴 생각해볼것
    }

    private void CSVDataLoad()//CSVData Class 로 이동
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/" + "NPCData" + "/" + "NPC.csv");

        bool endoffile = false;
        while (!endoffile)
        {
            string data_String = sr.ReadLine();
            if (data_String == null)
            {
                endoffile = true;
                break;
            }
            var data_values = data_String.Split(",");
            DIC_csvData.Add(int.Parse(data_values[0]), data_values[1]);
        }

    }

    internal Dictionary<int, string> PlayData_GetData(Dictionary<int, int> dic_playdata)
    {
        if (DIC_csvData == null)
            Debug.Log("에러 확인");
        Dictionary<int, string> returnData = new Dictionary<int, string>();
        Dictionary<int, string> instantDIC = new Dictionary<int, string>();
        instantDIC = DIC_csvData;

        int startKey = dic_playdata.Keys.First();
        int startValue = dic_playdata.Values.First();

        int i = startKey;

        foreach (KeyValuePair<int, string> item in instantDIC)
        {

            if (item.Key == i + startValue)
            {
                returnData.Add(item.Key, item.Value);
                i++;
            }
            else
                continue;
        }

        return returnData;
    }
}
