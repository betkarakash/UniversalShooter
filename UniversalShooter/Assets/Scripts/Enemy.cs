using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //Speed of the enemy
    [SerializeField]
    private float _enemySpeed = 4f;

    // Start is called before the first frame update
    void Start() {
        transform.position = new Vector3(0, 7, 0);
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y <= -6f) {
            transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 6, 0);
        }
    }
}
