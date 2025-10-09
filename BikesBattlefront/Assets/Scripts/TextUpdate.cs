using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class TextUpdate : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text playerNickName;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            playerNickName.text = photonView.Controller.NickName;
            photonView.RPC("RotateName", RpcTarget.Others);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [PunRPC]
    public void RotateName()
    {
        playerNickName.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
    }
}
