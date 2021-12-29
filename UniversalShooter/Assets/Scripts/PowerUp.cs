using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [SerializeField]
    private float _powerUpSpeed = 3f;
    [SerializeField]
    private int powerUpID;
    [SerializeField]
    private AudioClip _powerUpClip;

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
            //Now enable the power up to true.
            AudioSource.PlayClipAtPoint(_powerUpClip, transform.position);
            Player player = collision.gameObject.GetComponent<Player>();
            if(player != null){
                player.activatePowerUp(powerUpID);
            }
            Destroy(this.gameObject, 3f);
        }
    }
}
