using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;//스테틱사용하여 유일성 보장
    static Managers Instance{get{ Init(); return s_instance; } }//유일한 매니져를 가져온다
                                                                // Start is called before the first frame update

    #region Contents
    GameManager _game = new GameManager();
    public static GameManager Game { get { return Instance._game; } }
    #endregion
    #region Core
    DataManager _data = new DataManager();
    InputManagers _input = new InputManagers();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UI_Manager _ui = new UI_Manager();



    public static DataManager Data { get { return Instance._data; } }
    public static InputManagers Input { get { return Instance._input; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return  Instance._sound; } }
    public static UI_Manager UI { get { return Instance._ui; } }
    #endregion
    void Start()
    {
        //초기화

        Init();
    }

    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go==null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            
            s_instance = go.GetComponent<Managers>();
            s_instance._data.Init();
            s_instance._pool.Init();
            s_instance._sound.Init();
        }
    }
    public static void Clear()
    {
        Sound.Clear();
        Input.Clear();
        UI.Clear();
        Scene.Clear();
        Pool.Clear();
    }
}
