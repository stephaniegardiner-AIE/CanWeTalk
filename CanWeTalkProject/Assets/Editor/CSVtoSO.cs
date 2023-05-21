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

            line.character = Line.Character.Parse<Line.Character>(splitData[3]);

            line.dialog = splitData[4];

            if (splitData[5] == "")
            {
                Debug.Log("ain't nothing here");
            }
            else
            {
                line.attitudeArrayLength = int.Parse(splitData[5]);
            }

            line.attitudeArray = new Line.AttitudeArray[line.attitudeArrayLength];


            for (int i = 0; i < line.attitudeArrayLength; i++)
            {
                //create new data object
                var tmp = new Line.AttitudeArray();



                

                   /* tmp.attitudeChangeEffects = Line.AttitudeArray.Attitudes.Parse<Line.AttitudeArray.Attitudes>(splitData[8]);
                    tmp.attitudeChangeAmount = int.Parse(splitData[9]); */

                if (i == 0)
                {

                    tmp.attitudeChangeEffects = Line.AttitudeArray.Attitudes.Parse<Line.AttitudeArray.Attitudes>(splitData[6]);
                    tmp.attitudeChangeAmount = int.Parse(splitData[7]);
                }
                else
                {
                    Debug.Log(line.name);
                }

             /*   if (i == 1)
                {
                    tmp.attitudeChangeEffects = Line.AttitudeArray.Attitudes.Parse<Line.AttitudeArray.Attitudes>(splitData[6 + 2]);
                    tmp.attitudeChangeAmount = int.Parse(splitData[7 + 2]);
                } */

                line.attitudeArray[i] = tmp;


                //store the Data object in our dataArray


            }
            //enum.Parse<Line.Character>(splitData[4]);


            //Line.Character.Parse(splitData[4]);

            AssetDatabase.CreateAsset(line, $"Assets/Lines/{line.name}.asset");
            


        }

        AssetDatabase.SaveAssets();
    }
}
