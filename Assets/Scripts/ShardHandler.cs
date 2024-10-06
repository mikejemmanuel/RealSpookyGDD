using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardHandler : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Put all Shard objects here")]
    private GameObject[] shards;
    [SerializeField]
    [Tooltip("Put all shard intended locations here")]
    private Vector2[] shardSpawns;
    private int collectedShards;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < shards.Length; i++) {
            shards[i].transform.position = this.GetComponent<MapHandler>().GetTileCenter((int)shardSpawns[i].x, (int)shardSpawns[i].y);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Delete Shard Functions
    public void delete1_1() {
        shards[0].SetActive(false);
        if (collectedShards < 3) {
        collectedShards += 1;
        }
    }
    public void delete5_5() {
        shards[1].SetActive(false);
        if (collectedShards < 3) {
        collectedShards += 1;
        }
    }
    public void delete5_1() {
        shards[2].SetActive(false);
        if (collectedShards < 3) {
        collectedShards += 1;
        }
    }
}
