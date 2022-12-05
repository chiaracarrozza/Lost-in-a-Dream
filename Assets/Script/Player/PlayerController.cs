using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TMP_Text nametag;

    [SerializeField] private float speed;

    [SerializeField] private GameObject myCamera;

    [SerializeField] private float cameraSensitivity;

    [SerializeField] private GameObject rotatingBody;

    private float currentRBRotation;

    private PhotonView playerview;
    // Start is called before the first frame update
    void Start()
    {
        playerview= GetComponent<PhotonView>();
        nametag.text = playerview.Owner.NickName;

        if (!playerview.IsMine)
        {
            myCamera.GetComponent<Camera>().enabled = false;
            myCamera.GetComponent<AudioListener>().enabled = false;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerview.IsMine)
        {
            if (Input.GetKey("w"))
            {
                transform.position += transform.forward * speed * Time.deltaTime;

            }

            if (Input.GetKey("s"))
            {
                transform.position -= transform.forward * speed * Time.deltaTime;
            }

            if (Input.GetKey("a"))
            {
                transform.position -= transform.right * speed * Time.deltaTime;
            }

            if (Input.GetKey("d"))
            {
                transform.position += transform.right * speed * Time.deltaTime;
            }

            float cameraXRotation = Input.GetAxis("Mouse X");
            
            Vector3 rotationalVector = new Vector3(0.0f,cameraXRotation,0.0f) * cameraSensitivity;
            Vector3 currentCameraRotation = transform.rotation.eulerAngles;
            currentCameraRotation += rotationalVector;

            transform.rotation = Quaternion.Euler(currentCameraRotation);

            float cameraYRotation = Input.GetAxis("Mouse Y") * cameraSensitivity;
            currentRBRotation -= cameraYRotation;
            currentRBRotation = Mathf.Clamp(currentRBRotation, -45f,45f);
            rotatingBody.transform.localEulerAngles = new Vector3(currentRBRotation,0.0f,0.0f);

            //myCamera.transform.localEulerAngles = new Vector3(currentRBRotation, 0.0f, 0.0f);

            
        }
    }
}
