using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void CambiarEscena()
    {
        PhotonNetwork.LoadLevel("Menu2");
    }
}
