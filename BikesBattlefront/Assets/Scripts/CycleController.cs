using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CycleController : MonoBehaviour
{
    [SerializeField] public bool isAlive;
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;

    [SerializeField] GameObject trail;
    public float spawnInterval = 0.1f;
    private float timer = 0f;

    public enum teams { blue, orange }
    public teams Team;

    [SerializeField] Transform camara;

    [SerializeField] GameObject GameManager;
    // Start is called before the first frame update
    void Start()
    {
        isAlive = false;
        GameManager = GameObject.Find("GameManager");
        StartCoroutine(StartAfterDelay(3f));
    }
    IEnumerator StartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(3f);
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(0, -90, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(0, 90, 0);
        }

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnTrail();
            timer = 0f;
        }
    }

    void FixedUpdate()
    {
        if (!isAlive) return;
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }

    void SpawnTrail()
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        Instantiate(trail, pos, rot);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trail"))
        {
            transform.Find("Camera").GetComponent<SpectatorMode>().isSpectator = true;
            camara.transform.SetParent(null);
            GameManager.GetComponent<GameManager>().isDead = true;
            gameObject.SetActive(false);
            isAlive = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            transform.Find("Camera").GetComponent<SpectatorMode>().isSpectator = true;
            camara.transform.SetParent(null);
            GameManager.GetComponent<GameManager>().isDead = true;
            gameObject.SetActive(false);
            isAlive = false;
        }
    }
}