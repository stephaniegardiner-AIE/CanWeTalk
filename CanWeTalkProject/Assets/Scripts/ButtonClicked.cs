using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicked : MonoBehaviour
{
    // Start is called before the first frame update
    public SceneStarter sceneStarter;
    public int decisionNumber;
    //public int decisionNumber;
    void Start()
    {
        //sceneStarter.DecisionRunner().decision.
        //sceneStarter.currentLineBlock.endDecisionBlock.decisions[decisionNumber];
        
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    public void Click()
    {
        sceneStarter = FindObjectOfType<SceneStarter>();
        NewLineBlock();
        
    }

    public void NewLineBlock()
    {
        sceneStarter.currentLineBlock = sceneStarter.currentLineBlock.endDecisionBlock.decisions[decisionNumber].followingLineBlock;
        sceneStarter.lineNumber = 0;
        sceneStarter.currentVisibleSpeech.Remove(sceneStarter.currentVisibleSpeech[sceneStarter.currentVisibleSpeech.Count - 1]);
        //sceneStarter.previousLines += sceneStarter.currentLineBlock.lines.Length - sceneStarter.previousLines;
        sceneStarter.LineRunner();
        Destroy(transform.parent.transform.parent.gameObject);
    }
}
