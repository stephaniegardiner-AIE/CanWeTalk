using UnityEngine;
using UnityEditor;
using System.IO;


public class CSVtoSO
{
    private static string lineCSVPath = "/Editor/CSVs/LinesCSV.csv";
    [MenuItem("Utilities/Generate Lines")]
    public static void GenerateLines()
    {
        string[] allRows = File.ReadAllLines(Application.dataPath + lineCSVPath);

        foreach(string s in allRows)
        {
            string[] splitData = s.Split(",");

            Line line = ScriptableObject.CreateInstance<Line>();
            line.name = "S" + splitData[0] + "LB" + splitData[1] + "L " + splitData[2];

            AssetDatabase.CreateAsset(line, $"Assets/Lines/{line.name}.asset");
            //line.character = enum.Parse(splitData[4]);


        }

        AssetDatabase.SaveAssets();
    }
}
