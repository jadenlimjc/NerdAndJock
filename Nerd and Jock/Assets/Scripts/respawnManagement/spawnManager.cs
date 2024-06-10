using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemyPrefabs;

    public float leftBound;
    public float rightBound;
    public float spawnPosY;
    private float checkInterval = 1.0f;
    void Start()
    {
        StartCoroutine(CheckAndSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CheckAndSpawn() {
        while (true) {
            yield return new WaitForSeconds(checkInterval);
            if (GameObject.FindGameObjectWithTag("enemy") == null)
            {
                SpawnCharacter();
            }
        }
    }

    void SpawnCharacter() {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Vector2 spawnPos = new Vector2(Random.Range(leftBound, rightBound), spawnPosY);
        Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation);
    }
}   
