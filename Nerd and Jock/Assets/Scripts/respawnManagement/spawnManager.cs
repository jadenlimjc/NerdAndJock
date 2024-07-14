using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemyPrefabs;

    public float leftSpawn;
    public float rightSpawn;
    public float spawnPosY;
    public float checkInterval = 5.0f;
    public float leftBound = -10f;
    public float rightBound = 10f;

    private GameObject currentEnemy = null; // current spawned enemy
    
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
            if (currentEnemy == null)
            {
                SpawnCharacter();
            }
        }
    }

    void SpawnCharacter() {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Vector2 spawnPos = new Vector2(Random.Range(leftSpawn, rightSpawn), spawnPosY);
        currentEnemy = Instantiate(enemyPrefabs[enemyIndex], spawnPos, Quaternion.identity);
        IEnemy enemyController = currentEnemy.GetComponent<IEnemy>();
        if (enemyController != null)
        {
            enemyController.SetMovementBounds(leftBound,rightBound);
        }
    }
}   
