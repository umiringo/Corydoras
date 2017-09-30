using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefines;

public class Choice : MonoBehaviour {
    
    public List<GameObject> choicesObjList = new List<GameObject>();
    private  Vector2[] itemPosition = new Vector2[] {
        new Vector2(0, 400),
        new Vector2(400, 0),
        new Vector2(0, -400),
        new Vector2(-400, 0),
    };
    private int curLevel = 0;
    public GameDirector director;

    // Use this for initialization
    void Start()
    {
        ClearChoices();
        director = GameObject.Find("MainCamera").GetComponent<GameDirector>();
        director.choice = this;
    }
	// Update is called once per frame
	void Update () 
    {
		
	}

    public void ShowChoices(int level, KanaType type)
    {
        if(curLevel == level) 
        {
            List<int> choicesList = GamePlayMgr.Instance.GetChoiceItems();
            for(int i = 0; i < choicesObjList.Count; ++i)
            {
                choicesObjList[i].GetComponent<ChoiceItem>().Assemble(choicesList[i], type);
            }
        }
        else 
        {
            ClearChoices();   
            List<int> choicesList = GamePlayMgr.Instance.GetChoiceItems(); 
            for(int i = 0; i < level; i++)
            {
                GameObject item = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
                item.transform.SetParent(transform);
				item.GetComponent<RectTransform>().anchoredPosition = itemPosition[i];
                item.GetComponent<ChoiceItem>().Assemble(choicesList[i], type);
                item.name = "Item" + i; 
                choicesObjList.Add(item);
            }
        }
        curLevel = level;
    }

    private void ClearChoices()
    {
        for(int i = 0; i < choicesObjList.Count; ++i)
        {
            DestroyObject(choicesObjList[i]);
        }
        choicesObjList.Clear();
    }
}
