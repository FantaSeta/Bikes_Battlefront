using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class Menu2Manager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text logText;
    [SerializeField] TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.NickName = "User" + Random.Range(1, 9999);
        Log("Player Name: " + PhotonNetwork.NickName);
        // Configurando el juego
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Log(string message)
    {
        //Moving text to the next line
        logText.text += "\n";
        //Adding a message
        logText.text += message;
    }
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 6 });
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnConnectedToMaster()
    {
        Log("Connected to the server");
    }
    public override void OnJoinedRoom()
    {
        Log("Joined the lobby");
        PhotonNetwork.LoadLevel("Lobby");
    }
    public void ChangeName()
    {
        //Leer el texto que el jugador ha escrito en el InputField
        PhotonNetwork.NickName = inputField.text;
        //Generando el nuevo apodo
        Log("New Player name: " + PhotonNetwork.NickName);
    }
}
