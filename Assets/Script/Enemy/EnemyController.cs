using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed;

    private PlayerController[] players;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        players = FindObjectsOfType<PlayerController>();

        PlayerController nearestPlayer = null;
        float mindistance = float.MaxValue;
        foreach(PlayerController player in players)
        {

            float distance = Vector3.Distance(transform.position,player.transform.position);
            if(distance < mindistance)
            {
                mindistance = distance;
                nearestPlayer = player;
            }

        }

        if(nearestPlayer != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, nearestPlayer.transform.position,speed * Time.deltaTime);
            transform.LookAt(nearestPlayer.transform.position);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;

        if (hit.CompareTag("Player"))
        {
            hit.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 10.0f);
            Death();
        }
        
    }

    [PunRPC]
    public void Death()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            EnemySpawner.currentEnemyCount--;
            PhotonNetwork.Destroy(this.gameObject);     
        }
    }
}
