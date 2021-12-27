using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //Speed of the enemy
    [SerializeField]
    private float _enemySpeed = 4f;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y <= -6f) {
            transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 6, 0);
        }
    }

    //On Trigger Event
    private void OnTriggerEnter(Collider collidedObject) {
        //If laser hit the enemy then,
        if (collidedObject.tag == "Player") {
            //Destroy the lives of the object.
            Player playerComponent = collidedObject.transform.GetComponent<Player>();
            if (playerComponent != null) {
                playerComponent.Damage();
            }
            //And destroy the enemy object.
            Destroy(this.gameObject);
        }else if (collidedObject.tag == "Laser"){
            Destroy(this.gameObject);
            Destroy(collidedObject.gameObject);
        }
    }

}
