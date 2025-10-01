using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Tooltip("Prefabs de jugadores, ordenados según los spawn points")]
    public List<GameObject> playerPrefabs = new List<GameObject>(); // 6 prefabs distintos

    [Tooltip("Puntos de aparición para los jugadores")]
    public List<Transform> spawnPoints = new List<Transform>();     // 6 puntos distintos

    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
        }
        else
        {
            Debug.LogError("No estás conectado a Photon.");
        }
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
}

