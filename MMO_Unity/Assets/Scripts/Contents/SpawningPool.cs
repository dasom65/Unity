using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int _monsterCount = 0;
    int _reserveCount = 0;
    [SerializeField]
    int _totalMonster = 0;
   
    [SerializeField]
    Vector3 _spawnPos;
    [SerializeField]
    float _spawnRadius = 50.0f;
    [SerializeField]
    float _spawnTime = 10.0f;
    

    public void AddMonsterCount(int value) { _monsterCount += value; }
    public void MonsterCount(int count) { 
        _totalMonster = count;
        for (_monsterCount = 0; _monsterCount < _totalMonster; _monsterCount++)
        {
            //Spawn();
            StartCoroutine(ReserveSpawn());
        }
    }
    
    void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
        
    }

    
    void Update()
    {

        //while(_reserveCount+_monsterCount< _totalMonster)
        //{
        //    StartCoroutine(ReserveSpawn());
        //}
       
    }
    IEnumerator ReserveSpawn()
    {
        //_reserveCount++;
        //yield return new WaitForSeconds(Random.Range(0, _spawnTime));
        GameObject obj = Managers.Game.Spawn(Define.WorldObject.Monster, "Monster");

        NavMeshAgent nma = obj.GetOrAddComponent<NavMeshAgent>();

        Vector3 randPos;
        while (true)
        {
            Vector3 randDir = Random.insideUnitSphere * Random.Range(0, _spawnRadius);
            randDir.y = 0;
            randPos = _spawnPos + randDir;

            //갈수 있나?
            NavMeshPath path = new NavMeshPath();
            if (nma.CalculatePath(randPos, path))
                break;
            else
                yield return null;
        }
        obj.transform.position = randPos;

        //_reserveCount--;
    }
    

}
