using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    //Coroutine co;
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;
        //Managers.UI.ShowSceneUI<UI_Inven>();
        //Managers.UI.ShowPopupUI<UI_Button>();

        //co = StartCoroutine("ExplodeAfterSeconds", 4.0f);
        //StartCoroutine("CoStopExplode", 2.0f);
        Dictionary<int, Data.Stat> dict= Managers.Data.StatDict;

        gameObject.GetOrAddComponent<CursorController>();

        GameObject player=Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
        
        //GameObject monster1 = Managers.Game.Spawn(Define.WorldObject.Monster, "Monster");
        
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);
       
        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();

        pool.MonsterCount(5);

    }
    private void Update()
    {
       
            
    }
        
    //IEnumerator CoStopExplode(float seconds)
    //{
    //    Debug.Log("Stop Enter");
    //    yield return new WaitForSeconds(seconds);
    //    Debug.Log("Stop Execute");
    //    if (co != null)
    //    {
    //        StopCoroutine(co);
    //        co = null;
    //    }
    //}
    //IEnumerator ExplodeAfterSeconds(float seconds)
    //{
    //    Debug.Log("Explode Enter");
    //    yield return new WaitForSeconds(seconds);
    //    Debug.Log("Explode Execute");
    //    co = null;
    //}
    public override void Clear()
    { 

    }

    // Start is called before the first frame update
    

    // Update is called once per frame
    
}
