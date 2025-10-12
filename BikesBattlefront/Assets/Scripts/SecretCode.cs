using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretCode : MonoBehaviour
{
    [SerializeField] GameObject easterEgg;

    private List<KeyCode> secretCode = new List<KeyCode> {
        KeyCode.T,
        KeyCode.R,
        KeyCode.O,
        KeyCode.N
    };

    private int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            if(Input.GetKeyDown(secretCode[currentIndex]))
            {
                currentIndex++;
                if (currentIndex == secretCode.Count)
                {
                    StartCoroutine(Secret());
                    currentIndex = 0;
                }
                else if (Input.anyKeyDown)
                {
                    currentIndex = 0;
                }
            }
        }
    }
    IEnumerator Secret()
    {
        easterEgg.SetActive(true);
        yield return new WaitForSeconds(3f);
        easterEgg.SetActive(false);
    }
}
