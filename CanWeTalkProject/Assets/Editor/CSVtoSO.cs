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
            line.name = "S" + splitData[0] + "LB" + splitData[1] + "L" + splitData[2];

            line.character = Line.Character.Parse<Line.Character>(splitData[3]);

            line.dialog = splitData[4];

            if (splitData[5] == "")
            {
                Debug.Log("no attitude change");
            }
            else
            {
                line.attitudeArrayLength = int.Parse(splitData[5]);
            }


            for (int i = 0; i < line.attitudeArrayLength; i++)
            {
                //create new data object
                var tmp = new Line.AttitudeEffects();

                tmp.attitudeChangeEffects = Line.AttitudeEffects.AttitudesCharacter.Parse<Line.AttitudeEffects.AttitudesCharacter>(splitData[6]);
                tmp.attitudeChangeAmount = int.Parse(splitData[7]);
                line.name = line.name + "A";

                if (i == 1)
                {
                    tmp.attitudeChangeEffects = Line.AttitudeEffects.AttitudesCharacter.Parse<Line.AttitudeEffects.AttitudesCharacter>(splitData[8]);
                    tmp.attitudeChangeAmount = int.Parse(splitData[9]);
                }
                if (i == 2)
                {
                    tmp.attitudeChangeEffects = Line.AttitudeEffects.AttitudesCharacter.Parse<Line.AttitudeEffects.AttitudesCharacter>(splitData[10]);
                    tmp.attitudeChangeAmount = int.Parse(splitData[11]);
                }

                Debug.Log(line.name + " 1 attitude change");

                //store the Data object in our dataArray
                line.attitudeArray.Add(tmp);

            }
            //enum.Parse<Line.Character>(splitData[4]);

            if (splitData[14] == "")
            {
                Debug.Log("no action change");
            }
            else
            {
                line.action = LineBlock.Actions.Parse<LineBlock.Actions>(splitData[14]);
                line.hasAction = true;
            }

            

            AssetDatabase.CreateAsset(line, $"Assets/Lines/{line.name}.asset");
            


        }

        AssetDatabase.SaveAssets();
    }
}
