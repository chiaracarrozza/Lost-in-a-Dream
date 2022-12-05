using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    [SerializeField] private Transform[] spawningPoints;

    [SerializeField] private int maxEnemy;

    [SerializeField] private float spawningInterval;

    public static int currentEnemyCount;

    private float timeBetweenSpawns;

    // Start is called before the first frame update
    void Start()
    {
        currentEnemyCount = 0;
        timeBetweenSpawns = spawningInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if ((PhotonNetwork.IsMasterClient) && (PhotonNetwork.CurrentRoom.PlayerCount > 1))
        {
            if(timeBetweenSpawns <= 0.0)
            {
                if(currentEnemyCount < maxEnemy)
                {
                    int randomIndex = Random.Range(0,spawningPoints.Length);
                    Vector3 randomSpawnPos = spawningPoints[randomIndex].position;
                    PhotonNetwork.Instantiate(enemy.name,randomSpawnPos, Quaternion.identity);

                    currentEnemyCount++;
                    timeBetweenSpawns = spawningInterval;
                }

            }
            else
            {
                timeBetweenSpawns -= Time.deltaTime;
            }

        }
        
    }
}
