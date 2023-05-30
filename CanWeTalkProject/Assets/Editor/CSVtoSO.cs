using UnityEngine;
using UnityEditor;
using System.Collections;
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
            line.name = "S" + splitData[1] + "LB" + splitData[2] + "L" + splitData[3];

            line.character = Line.Character.Parse<Line.Character>(splitData[4]);

            line.dialog = splitData[5];

            if (splitData[6] == "")
            {
                Debug.Log("no attitude change");
            }
            else
            {
                line.attitudeArrayLength = int.Parse(splitData[6]);
            }

           // line.attitudeArray = new List<Line.AttitudeEffects>(new Line.AttitudeEffects[line.attitudeArrayLength]);

            for (int i = 0; i < line.attitudeArrayLength; i++)
            {
                //create new data object
                var tmp = new Line.AttitudeEffects();

                /*for (int a = 0; a == line.attitudeArrayLength;)
                {
                    tmp.attitudeCharacter = Line.AttitudeEffects.AttitudesCharacter.Parse<Line.AttitudeEffects.AttitudesCharacter>(splitData[7]);
                    tmp.attitudeChangeAmount = Line.AttitudeEffects.AttitudeChange.Parse<Line.AttitudeEffects.AttitudeChange>(splitData[8]);
                    Debug.Log(line.name + " 1 attitude change");
                } */
                

             if (i == 0)
                {
                    tmp.attitudeCharacter = Line.AttitudeEffects.AttitudesCharacter.Parse<Line.AttitudeEffects.AttitudesCharacter>(splitData[7]);
                    tmp.attitudeChangeAmount = Line.AttitudeEffects.AttitudeChange.Parse<Line.AttitudeEffects.AttitudeChange>(splitData[8]);
                    Debug.Log(line.name + " 1 attitude change");
                }   


              if (i == 1)
                {
                    Debug.Log("2 attitude cahnges");
                    tmp.attitudeCharacter = Line.AttitudeEffects.AttitudesCharacter.Parse<Line.AttitudeEffects.AttitudesCharacter>(splitData[9]);
                    tmp.attitudeChangeAmount = Line.AttitudeEffects.AttitudeChange.Parse<Line.AttitudeEffects.AttitudeChange>(splitData[10]);
                }
                
               /* if (i == 1)
                {

                    Debug.Log(line.name + " 2 attitude change");
                } */
               
                if (line.attitudeArrayLength == 3)
                {
                    tmp.attitudeCharacter = Line.AttitudeEffects.AttitudesCharacter.Parse<Line.AttitudeEffects.AttitudesCharacter>(splitData[11]);
                    tmp.attitudeChangeAmount = Line.AttitudeEffects.AttitudeChange.Parse<Line.AttitudeEffects.AttitudeChange>(splitData[12]);
                    Debug.Log(line.name + " 3 attitude changes");
                }

                if (line.attitudeArrayLength == 4)
                {
                    tmp.attitudeCharacter = Line.AttitudeEffects.AttitudesCharacter.Parse<Line.AttitudeEffects.AttitudesCharacter>(splitData[13]);
                    tmp.attitudeChangeAmount = Line.AttitudeEffects.AttitudeChange.Parse<Line.AttitudeEffects.AttitudeChange>(splitData[14]);
                    Debug.Log(line.name + " 4 attitude changes");
                }

                line.name = line.name + "A";

                //store the Data object in our dataArray
                line.attitudeArray.Add(tmp);

            }
            //enum.Parse<Line.Character>(splitData[4]);

           /* if (splitData[14] == "")
            {
                Debug.Log("no action change");
            }
            else
            {
                line.action = LineBlock.Actions.Parse<LineBlock.Actions>(splitData[15]);
                line.hasAction = true;
            } */

            if (!Directory.Exists("Assets/Lines/Day" + splitData[0]))
            {
                AssetDatabase.CreateFolder("Assets/Lines", "Day" + splitData[0]);
            }
            if (!Directory.Exists($"Assets/Lines/Day{splitData[0]}/Scene" + splitData[1]))
            {
                AssetDatabase.CreateFolder($"Assets/Lines/Day{splitData[0]}", "Scene" + splitData[1]);
            }

            if (!Directory.Exists($"Assets/Lines/Day{splitData[0]}/Scene{splitData[1]}/LineBlock" + splitData[2]))
            {
                AssetDatabase.CreateFolder($"Assets/Lines/Day{splitData[0]}/Scene{splitData[1]}", "LineBlock" + splitData[2]);

            }
            
            //string guid = AssetDatabase.CreateFolder("Assets/Lines")

            AssetDatabase.CreateAsset(line, $"Assets/Lines/Day{splitData[0]}/Scene{splitData[1]}/LineBlock{splitData[2]}/{line.name}.asset");
            
            

        }

        AssetDatabase.SaveAssets();
    }


}
