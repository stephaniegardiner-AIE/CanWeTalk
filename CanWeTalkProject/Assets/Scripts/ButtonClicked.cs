using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicked : MonoBehaviour
{
    // Start is called before the first frame update
    public SceneStarter sceneStarter;
    public int decisionNumber;
    public int activityNumber;
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

        if (gameObject.tag == "Decision")
        {
            NewLineBlock();
        }
        
        if (gameObject.tag == "Activity")
        {
            RunActivity();
        }
    }

    public void NewLineBlock()
    {
        sceneStarter.currentLineBlock = sceneStarter.currentLineBlock.endDecisionBlock.decisions[decisionNumber].followingLineBlock;
        sceneStarter.lineNumber = 0;
        sceneStarter.currentVisibleSpeech.Remove(sceneStarter.currentVisibleSpeech[sceneStarter.currentVisibleSpeech.Count - 1]);
        //sceneStarter.previousLines += sceneStarter.currentLineBlock.lines.Length - sceneStarter.previousLines;
        sceneStarter.LineRunner();
        sceneStarter.decision = false;
        sceneStarter.ResizeContent(-transform.parent.transform.parent.gameObject.GetComponent<RectTransform>().sizeDelta.y);
        Destroy(transform.parent.transform.parent.gameObject);
    }

    public void RunActivity()
    {

        //sceneStarter.currentLineBlock = sceneStarter.currentLineBlock.endDecisionBlock.decisions[decisionNumber].followingLineBlock;
        sceneStarter.lineNumber = 0;
        sceneStarter.currentVisibleSpeech.Remove(sceneStarter.currentVisibleSpeech[sceneStarter.currentVisibleSpeech.Count - 1]);
        //sceneStarter.previousLines += sceneStarter.currentLineBlock.lines.Length - sceneStarter.previousLines;
        sceneStarter.CreateActivityResponse(activityNumber);
        sceneStarter.decision = false;
        sceneStarter.ResizeContent(-transform.parent.transform.parent.gameObject.GetComponent<RectTransform>().sizeDelta.y);
        Destroy(transform.parent.transform.parent.gameObject);

        //DO ATTITUDE CHANGE

    }
}
