using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorMode : MonoBehaviour
{
    public bool isSpectator;
    [SerializeField] float speed = 100f;
    float mouseSense = 1; 
    // Start is called before the first frame update
    void Start()
    {
        isSpectator = false;
    }

    // Update is called once per frame
    void Update()
    {
        float rotateX = Input.GetAxis("Mouse X") * mouseSense;
        float rotateY = Input.GetAxis("Mouse Y") * mouseSense;
        if(isSpectator)
        {
            Vector3 rotCamera = transform.rotation.eulerAngles;
            rotCamera.x -= rotateY;
            rotCamera.z = 0;
            rotCamera.y += rotateX;
            transform.rotation = Quaternion.Euler(rotCamera);
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 dir = transform.right * x + transform.forward * z;
            transform.position += dir * speed * Time.deltaTime;
        }
    }
}
