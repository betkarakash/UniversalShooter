using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    [SerializeField]
    private float _rotateSpeed = 5.0f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;

    //Start  method
    private void Start() {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    //On Trigger generate the explosion
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Laser") {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
            _spawnManager.startSpawning();
        }
    }
}
