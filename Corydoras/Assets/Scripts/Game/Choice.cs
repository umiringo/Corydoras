using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefines;

public class Choice : MonoBehaviour {
    
    public List<GameObject> choicesObjList = new List<GameObject>();
	private Vector2[] itemPosition2 = new Vector2[] {
		new Vector2(0, 400),
		new Vector2(0, -400),
	};
	private Vector2[] itemPosition3 = new Vector2[] {
		new Vector2(0, 400),
		new Vector2(346, -200),
		new Vector2(-346, -200),
	};
    private  Vector2[] itemPosition4 = new Vector2[] {
        new Vector2(0, 400),
        new Vector2(400, 0),
        new Vector2(0, -400),
        new Vector2(-400, 0),
    };
	private Vector2[] itemPosition5 = new Vector2[] {
		new Vector2(400, 160),
		new Vector2(0, 400),
		new Vector2(-400, 160),
		new Vector2(-240, -320),
        new Vector2(240, -320),
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
            if(level == 1)
            {
				GameObject item = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
				item.transform.SetParent(transform);
                item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, 400.0f);
				item.GetComponent<ChoiceItem>().Assemble(choicesList[0], type);
				item.name = "Item" + 0;
				choicesObjList.Add(item);
            }
            else if(level == 2)
            {
				for (int i = 0; i < level; i++) {
					GameObject item = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
					item.transform.SetParent(transform);
					item.GetComponent<RectTransform>().anchoredPosition = itemPosition2[i];
					item.GetComponent<ChoiceItem>().Assemble(choicesList[i], type);
					item.name = "Item" + i;
					choicesObjList.Add(item);
				}
            }
            else if(level == 3)
            {
				for (int i = 0; i < level; i++) {
					GameObject item = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
					item.transform.SetParent(transform);
					item.GetComponent<RectTransform>().anchoredPosition = itemPosition3[i];
					item.GetComponent<ChoiceItem>().Assemble(choicesList[i], type);
					item.name = "Item" + i;
					choicesObjList.Add(item);
				}
            }
            else if(level == 4)
            {
				for (int i = 0; i < level; i++) {
					GameObject item = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
					item.transform.SetParent(transform);
					item.GetComponent<RectTransform>().anchoredPosition = itemPosition4[i];
					item.GetComponent<ChoiceItem>().Assemble(choicesList[i], type);
					item.name = "Item" + i;
					choicesObjList.Add(item);
				}
            }
            else if(level == 5)
            {
				for (int i = 0; i < level; i++) {
					GameObject item = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
					item.transform.SetParent(transform);
					item.GetComponent<RectTransform>().anchoredPosition = itemPosition5[i];
					item.GetComponent<ChoiceItem>().Assemble(choicesList[i], type);
					item.name = "Item" + i;
					choicesObjList.Add(item);
				}
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
