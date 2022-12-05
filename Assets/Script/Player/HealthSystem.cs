using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HealthSystem : MonoBehaviourPunCallbacks,IPunObservable
{    
    private float Health;

    [SerializeField] private float startingHealth;

    [SerializeField] private Image healthBar;

    private PhotonView view;

    public static bool playerDead;

    // Start is called before the first frame update
    void Start()
    {
        playerDead = false;
        Health = startingHealth;
        healthBar.fillAmount = Health / startingHealth;
        view= GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Health / startingHealth;

    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        Health-=damage;
        healthBar.fillAmount = Health / startingHealth;

        if (Health <= 0.0f)
        {
            Death();
        }
    }

    public void Death()
    {
        playerDead = true;
        KeySpawner.keyPicked=false;
        Debug.Log("PlayerDead");
        if(view == null)
            view = GetComponent<PhotonView>();
        if (view.IsMine)
        {
            PhotonNetwork.LeaveRoom();
        }
    }
    //sync health
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(Health);
        }
        else
        {
            // Network player, receive data
            Health = (float)stream.ReceiveNext();
            
           
        }
        TakeDamage(0);
    }

}
