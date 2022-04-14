using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] obstaclePrefabs;

    Vector3 spawnPos = new Vector3(25, 0, 0);

    private float startDelay = 2f;

    [HideInInspector]
    public float repeatRate = 3f;
    
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    
    private void SpawnObstacle()
    {
        if (playerControllerScript.isGameOver == false)
        {
            Instantiate(obstaclePrefabs[Random.Range(0,3)], spawnPos, Quaternion.identity);
        }
    }
}
