using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private GameController gameControllerObj;
    public GameObject[] hazard;
    public Vector3 spawnValues;

    public int hazardCount;
    public float spawnWait;
    public bool waveActive;


    void Start()
    {
        gameControllerObj = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public bool checkForEnemies()
    {
        bool enemysCleared = false;

        int countGroup1 = GameObject.FindGameObjectsWithTag("Asteroid").Length;
        int countFlaming = GameObject.FindGameObjectsWithTag("Asteroid2").Length;
        int countMines = GameObject.FindGameObjectsWithTag("Mine").Length;

        int enemyCount = countGroup1 + countFlaming + countMines;
        if (enemyCount == 0 && !waveActive) enemysCleared = true;

        return enemysCleared;
    }

    public IEnumerator SpawnEnemies()
    {
        waveActive = true;

        for (int i = 0; i < hazardCount; i++)
        {
            if (gameControllerObj.gameOver) break;

            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y), Random.Range(-spawnValues.z, spawnValues.z));

            int rndHazard = 0;
            int hazardRange = hazard.Length;

            if (gameControllerObj.wave < 3) hazardRange -= 2;
            if (gameControllerObj.wave < 5) hazardRange -= 1;

            while (true)
            {
                rndHazard = Random.Range(0, hazardRange);

                string hazardName = hazard[rndHazard].name;
                int flamingCount = GameObject.FindGameObjectsWithTag("Asteroid2").Length;
                int pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
                if (hazardName != "Flaming" || (pickupCount == 0 && flamingCount == 0)) break;
            }

            GameObject instantiatedPrefab = Instantiate(hazard[rndHazard], spawnPosition + transform.position, transform.rotation);

            if (instantiatedPrefab.tag != "Mine" && instantiatedPrefab.tag != "Asteroid2")
            {
                float rndSizeSetting = Random.Range(1.5f, 4.0f);
                instantiatedPrefab.transform.localScale = new Vector3(rndSizeSetting, rndSizeSetting, rndSizeSetting);
            }

            yield return new WaitForSeconds(spawnWait);
        }

        waveActive = false;
    }
}
