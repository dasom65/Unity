using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_MonsterCounter : MonoBehaviour
{
    
    GameObject[] monstercount;
    public Text monstercounttext;
    

    void Update()
    {
        monstercount = GameObject.FindGameObjectsWithTag("Monster");
        monstercounttext.text = $"남은몬스터수:{monstercount.Length.ToString()}/5";
    }
}
