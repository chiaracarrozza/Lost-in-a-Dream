using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameCleared : MonoBehaviour
{
    PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (view.IsMine)
        {
            if(collision.gameObject.tag == "Player")
            {
                
                if (KeySpawner.keyPicked == true)
                {
                    if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
                    {
                        SceneManager.LoadScene("GameCleared");
                        //PhotonNetwork.LeaveRoom();
                        
                    }
                }
            }
        }
    }
   
}
