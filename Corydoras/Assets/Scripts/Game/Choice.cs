using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefines;

public class Choice : MonoBehaviour {
    
    public List<GameObject> Choices = new List<GameObject>();
    public readonly Vector3[] itemPosition = new Vector3[] {
        new Vector3(0, 50, 0),
        new Vector3(50, 0 ,0),
        new Vector3(0, -50, 0),
        new Vector3(-50, 0, 0),
    };
    private int curLevel = 0;
    public GameDirector director;

	// Use this for initialization
	void Start () {
	   ClearChoices();
       director = GameObject.Find("MainCamera").GetComponent<GameDirector>();
       director.choice = this;
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowChoices(int level, KanaType type)
    {
        if(curLevel == level) 
        {
            List<int> choicesList = GamePlayMgr.Instance.GetChoiceItems();
            for(int i = 0; i < Choices.Count; ++i)
            {
                Choices[i].GetComponent<ChoiceItem>().Assemble(choicesList[i], type);
            }
        }
        else 
        {
            ClearChoices();   
            List<int> choicesList = GamePlayMgr.Instance.GetChoiceItems(); 
            for(int i = 0; i < level; i++)
            {
                GameObject item = Instantiate(Resources.Load("Prefabs/" + item) as GameObject);
                item.GetComponent<RectTransform>().position = itemPosition[i];
                item.transform.parent = this.gameObject.transform;
                item.GetComponent<ChoiceItem>().Assemble(choicesList[i], type);
                choicesList.Add(item);
            }
        }
        curLevel = level;
    }

    private void ClearChoices()
    {
        for(int i = 0; i < Choices.Count; ++i)
        {
            DestroyObject(Choices[i]);
        }
        Choices.Clear();
    }
}
