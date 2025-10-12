using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip music1;
    [SerializeField] private AudioClip music2;

    [SerializeField] private string stopMusicInScene = "Menu2"; // Cambia esto

    public AudioSource audioSource;

    private void Awake()
    {
        // Asegúrate de que solo haya una instancia
        if (FindObjectsOfType<GameMusicManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        int randomNumber = Random.Range(1, 3); // 1 o 2
        audioSource.clip = (randomNumber == 1) ? music1 : music2;
        audioSource.loop = true;
        audioSource.Play();

        // Escuchar cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == stopMusicInScene)
        {
            Destroy(gameObject); // Detiene la música y elimina el GameObject
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Limpieza de eventos
    }
}
