using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    private bool ToggleCursorState;
    // Start is called before the first frame update
    void Start()
    {
        ToggleCursorState = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("c"))
        {
            ToggleCursorState = !ToggleCursorState;
        }
        if (ToggleCursorState)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState= CursorLockMode.None;
        }
            
        
    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        SceneManager.LoadScene("RoomManagement");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
    }
}
