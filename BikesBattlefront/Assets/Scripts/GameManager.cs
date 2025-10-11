using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviourPunCallbacks
{
    [Tooltip("Prefabs de jugadores, ordenados según los spawn points")]
    public List<GameObject> playerPrefabs = new List<GameObject>(); // 6 prefabs distintos

    [Tooltip("Puntos de aparición para los jugadores")]
    public List<Transform> spawnPoints = new List<Transform>();     // 6 puntos distintos

    [SerializeField] GameObject readyImage;
    [SerializeField] GameObject setImage;
    [SerializeField] GameObject goImage;
    [SerializeField] public GameObject youLostImage;

    GameObject[] players;
    List<string> activePlayers = new List<string>();
    int checkPlayers = 0;
    [SerializeField] public TMP_Text playersText;
    private int previousPlayerCount;

    public bool isDead;
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
            StartCoroutine(ShowCountdown());
        }
        else
        {
            Debug.LogError("No estás conectado a Photon.");
        }
        isDead = false;
    }
    void Update()
    {
        if (PhotonNetwork.PlayerList.Length < previousPlayerCount)
        {
            ChangePlayersList();
        }
        previousPlayerCount = PhotonNetwork.PlayerList.Length;

        if(isDead)
        {
            youLostImage.SetActive(true);
        }
    }
    [PunRPC]
    public void PlayerList()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        activePlayers.Clear();
        foreach(GameObject player in players)
        {
            // If the player is alive, then
            if(player.GetComponent<CycleController>().isAlive == true)
            {
                // Adding their nicknames to the activePlayers list
                activePlayers.Add(player.GetComponent<PhotonView>().Owner.NickName);
            }
        }
        playersText.text = "Users in game : " + activePlayers.Count.ToString();
        if (activePlayers.Count <= 1 && checkPlayers > 0)
        {
            Invoke("EndGame", 5f);
        }

        checkPlayers++;
    }
    [PunRPC]
    public void ChangePlayersList()
    {
        photonView.RPC("PlayerList", RpcTarget.All);   
    }
    IEnumerator ShowCountdown()
    {
        readyImage.SetActive(true);
        yield return new WaitForSeconds(1f);
        readyImage.SetActive(false);

        setImage.SetActive(true);
        yield return new WaitForSeconds(1f);
        setImage.SetActive(false);

        goImage.SetActive(true);
        yield return new WaitForSeconds(1f);
        goImage.SetActive(false);
    }
    void SpawnPlayer()
    {
        int playerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;

        if (playerIndex >= spawnPoints.Count || playerIndex >= playerPrefabs.Count)
        {
            Debug.LogWarning("Hay más jugadores que spawn points o prefabs. Usando valores aleatorios.");
            playerIndex = Random.Range(0, Mathf.Min(spawnPoints.Count, playerPrefabs.Count));
        }

        Transform spawnPoint = spawnPoints[playerIndex];
        GameObject prefabToSpawn = playerPrefabs[playerIndex];

        PhotonNetwork.Instantiate(prefabToSpawn.name, spawnPoint.position, spawnPoint.rotation);
    }
    void EndGame()
    {
        // Regresando al Lobby
        PhotonNetwork.LoadLevel("Lobby");
    }
    public void ExitGame()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
}

