using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField playerNameInput;
    [SerializeField] TMP_InputField roomNameInput;
    [SerializeField] TMP_InputField maxPlayersInput;
    [SerializeField] TMP_InputField existingRoomInput;

    private byte MaxPlayers;

    private static byte MAX_PLAYERS = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //********** On Click *********
   public void CreateRoom()
    {
        if(InputIsValid(playerNameInput) && (InputIsValid(roomNameInput) && MaxPlayersIsValid()))
        {
            PhotonNetwork.NickName = playerNameInput.text;
            RoomOptions rOptions= new RoomOptions();
            rOptions.IsOpen = true; 
            rOptions.IsVisible = true;
            rOptions.MaxPlayers = MaxPlayers;

            PhotonNetwork.CreateRoom(roomNameInput.text, rOptions);
        }
        else
        {
            Debug.Log("Invalid input");
        }

    }
    public void CheckControls()
    {
        SceneManager.LoadScene("Controls");
    }
    // ************ Callbacks *************
    public void JoinRoom()
    {
        if(InputIsValid(playerNameInput) && InputIsValid(existingRoomInput))
        {
            PhotonNetwork.NickName=playerNameInput.text;
            PhotonNetwork.JoinRoom(existingRoomInput.text);
        }
        else
        {

            Debug.Log("Invalid input");
        }

    }

    public void JoinRandomRoom()
    {
        if (InputIsValid(playerNameInput))
        {
            PhotonNetwork.NickName = playerNameInput.text;
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            Debug.Log("Invalid input");
        }
    }
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber);
        if(PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            SceneManager.LoadScene("WaitingRoom");
        }
        else
        {
            PhotonNetwork.LoadLevel("MoodyNight");
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("ERROR CODE " + returnCode + "-" + message);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("ERROR CODE " + returnCode + "-" + message);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("ERROR CODE " + returnCode + "-" + message);

        RoomOptions rOptions = new RoomOptions();
        rOptions.IsOpen = true;
        rOptions.IsVisible = true;
        rOptions.MaxPlayers =(byte)Random.Range(2,MAX_PLAYERS);
        string roomName = "Room" + Random.Range(0, 1000);
       
        PhotonNetwork.CreateRoom(roomName, rOptions);
    }

    public bool InputIsValid(TMP_InputField inputField)
    {

        string input = inputField.text;
        if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private bool MaxPlayersIsValid()
    {
        string maxPlayerString = maxPlayersInput.text;
        if(byte.TryParse(maxPlayerString, out MaxPlayers))
        {
            if (MaxPlayers > MAX_PLAYERS)
                MaxPlayers = MAX_PLAYERS;
            else if(MaxPlayers < 2)
                    MaxPlayers = 2;

            return true;
        }
        else
        {
            return false;
        }
    }
}
