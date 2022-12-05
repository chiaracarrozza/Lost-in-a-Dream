using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KeySpawner : MonoBehaviour
{
    [SerializeField] private GameObject key;

    [SerializeField] private Transform[] spawningPoints;

    [SerializeField] private int maxKeys;

    [SerializeField] private float spawningInterval;

    public static int currentKeyCount;

    private float timeBetweenSpawns;

    public static bool keyPicked;
    // Start is called before the first frame update
    void Start()
    {
        keyPicked = false;
        currentKeyCount = 0;
        
        timeBetweenSpawns = spawningInterval;

    }

    // Update is called once per frame
    void Update()
    {
        if ((PhotonNetwork.IsMasterClient) && (PhotonNetwork.CurrentRoom.PlayerCount > 0))
        {
            if (timeBetweenSpawns <= 0.0)
            {
                if (currentKeyCount < maxKeys)
                {

                    int randomIndex = Random.Range(0, spawningPoints.Length);
                    Vector3 randomSpawnPos = spawningPoints[randomIndex].position;
                    PhotonNetwork.Instantiate(key.name, randomSpawnPos, Quaternion.identity);

                    currentKeyCount++;
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
