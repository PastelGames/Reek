using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossFight : MonoBehaviour
{

    public GameObject door;
    public GameObject mainCamera;
    public GameObject bossCamera;
    public GameObject boss;

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
            //block the player from returning
            door.SetActive(true);

            //swap out the cameras
            mainCamera.SetActive(false);
            mainCamera.tag = "Untagged";
            bossCamera.SetActive(true);
            bossCamera.tag = "MainCamera";

            //start the bosses movement
            boss.GetComponent<Animator>().SetTrigger("Boss Fight Started");
        }
    }
}
