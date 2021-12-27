using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [SerializeField]
    private float _powerUpSpeed = 3f;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        //Moving down
        transform.Translate(Vector3.down * _powerUpSpeed * Time.deltaTime);

        //Destroy if object leave the screen without colliding with player
        if(transform.position.y <= -6f) {
            Destroy(this.gameObject);
        }
    }

    //OnTreiggerCollision for the player
    private void OnTriggerEnter2D(Collider2D collision) {
        //Check if the collided object is player or not.
        if (collision.tag == "Player") {
            //Now enable the _isTripleShotActive to true.
            Player player = collision.gameObject.GetComponent<Player>();
            if(player != null){
                player.activateTripleShot();
            }
            Destroy(this.gameObject);
        }
    }
}
