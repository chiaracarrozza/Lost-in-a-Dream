using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;

    [SerializeField] private float shootingRate;

    [SerializeField] private Transform SpawningPoint;
    private float timeFromLastShoot;

    private float maxTimeBetweenShoots;

    private PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        maxTimeBetweenShoots= 1.0f / shootingRate;
        timeFromLastShoot = maxTimeBetweenShoots;
        view = this.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeFromLastShoot < maxTimeBetweenShoots)
        {
            timeFromLastShoot += Time.deltaTime;
        }
        if (view.IsMine)
        {
            if (Input.GetButton("Fire1"))
            {
                if(timeFromLastShoot >= maxTimeBetweenShoots)
                {
                   GameObject bullet = PhotonNetwork.Instantiate(Bullet.name,SpawningPoint.position,Quaternion.identity);

                    BulletController bulletContr= bullet.GetComponent<BulletController>();
                    bulletContr.SetDirection(SpawningPoint.forward);

                    timeFromLastShoot = 0.0f;
                }
            }

        }
        
    }
}
