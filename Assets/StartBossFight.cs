using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossFight : MonoBehaviour
{

    public GameObject door;
    public GameObject mainCamera;
    public GameObject bossCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            door.SetActive(true);
        }

        mainCamera.SetActive(false);
        mainCamera.tag = "Untagged";
        bossCamera.SetActive(true);
        bossCamera.tag = "MainCamera";
    }
}
