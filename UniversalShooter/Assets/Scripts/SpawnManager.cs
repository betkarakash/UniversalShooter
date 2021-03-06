using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] _powerUps;

    // StartSpawning is called after asteroid is destroyed
    public void startSpawning() {
        //Start the coroutine
        StartCoroutine(spawnEnemyRoutine());
        StartCoroutine(spawnPowerUpRoutine());
    }

    //IENumerator for the Coroutine. Coruotine helps user to pause game for sometimme to acheive mutlitasking.
    IEnumerator spawnEnemyRoutine() {
        yield return new WaitForSeconds(3.0f);

        //Instantiate the enemy object to fall from the screen in interval of 5 sec.
        while (_stopSpawning == false) {
            Vector3 spawnTransform = new Vector3(Random.Range(-10.0f, 10.0f), 7, 0);
            //Just to collect the enemy clones within a container we can instatiate the enemy object and then transform the enemy clone to parent object
            GameObject enemies = Instantiate(_enemyPrefab, spawnTransform, Quaternion.identity);
            enemies.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    //Spawn the power up routine
    IEnumerator spawnPowerUpRoutine() {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false) {
            Vector3 spawnPowerUpTransform = new Vector3(Random.Range(-10.0f, 10.0f), 7, 0);
            Instantiate(_powerUps[Random.Range(0,3)], spawnPowerUpTransform, Quaternion.identity);
            //For every 3-7 seconds it will spawn the power up
            yield return new WaitForSeconds(Random.Range(3, 15));
        }
    }

    //Spawn the asteroids eoutine
    IEnumerator spawnAsteroidRoutine() {
        while (_stopSpawning == false) {
            Vector3 spawnPowerUpTransform = new Vector3(Random.Range(-10.0f, 10.0f), 7, 0);
            Instantiate(_powerUps[Random.Range(0, 3)], spawnPowerUpTransform, Quaternion.identity);
            //For every 3-7 seconds it will spawn the power up
            yield return new WaitForSeconds(Random.Range(3, 15));
        }
    }

    //When player dies then enemies stop comming
    public void OnPlayerDied(){
        _stopSpawning = true;
    }

}
