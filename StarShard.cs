using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarShard : MonoBehaviour
{
    public GameObject starShardPrefab;
    public GameObject starShard;

    public void CreateShard(){
        Vector2 randomSpawnPosition = new Vector2(Random.Range(-3,4),Random.Range(-6,7));
        starShard = Instantiate(starShardPrefab,randomSpawnPosition,Quaternion.Euler(new Vector3(0, 0, 90)));
    }
    public void DestroyShard(){
        Destroy(starShard);
    }

}
