using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KeyController : MonoBehaviour
{
    private PhotonView view;

    
   
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
        if(view==null)
            view = GetComponent<PhotonView>();
        if (view.IsMine)
        {
            if (KeySpawner.keyPicked == false)
            {
                if (collision.gameObject.tag == "Player")
                {
                    
                    KeySpawner.keyPicked = true;
                    KeySpawner.currentKeyCount--;
                    PhotonNetwork.Destroy(this.gameObject);
                }
            }
        }
    }
}
