using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletController : MonoBehaviour
{
    private Vector3 direction;

    private Vector3 startingPos;

    [SerializeField] private float speed;

    private PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (view.IsMine)
        {
            if (Vector3.Distance(transform.position, startingPos) >=20.0f)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }

        }
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
        startingPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (view == null)
            view = GetComponent<PhotonView>();
        if (view.IsMine)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("Player");
                collision.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 20.0f);
                PhotonNetwork.Destroy(this.gameObject);
            }

            if(collision.gameObject.tag == "Enemy")
            {
                Debug.Log("Enemy");
                collision.gameObject.GetComponent<PhotonView>().RPC("Death", RpcTarget.AllBuffered);
                PhotonNetwork.Destroy(this.gameObject);
            }


           
        }
    }
}
