using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //Speed of the enemy
    [SerializeField]
    private float _enemySpeed = 4f;
    private Player _player;
    private Animator _enemyDestroy;
    private AudioSource _destroyClip;
    
    // Start called at the initilize
    private void Start() {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null) {
            Debug.Log("Player is null");
        }
        _enemyDestroy = GetComponent<Animator>();
        if (_enemyDestroy == null) {
            Debug.Log("Animation is null");
        }
        _destroyClip = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y <= -6f) {
            transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 6, 0);
        }
    }

    //On Trigger Event
    private void OnTriggerEnter2D(Collider2D collidedObject) {
        //If laser hit the enemy then,
        if (collidedObject.tag == "Player") {
            //Destroy the lives of the object.
            Player playerComponent = collidedObject.transform.GetComponent<Player>();
            if (playerComponent != null) {
                _enemyDestroy.SetTrigger("OnTriggerDestroy");
                playerComponent.Damage();
            }
            //And destroy the enemy object.
            _enemySpeed = 0;
            _destroyClip.Play();
            Destroy(this.gameObject, 2.5f);
        }else if (collidedObject.tag == "Laser"){
            if(_player != null) {
                _player.addScoreToPlayer();
            }
            _enemyDestroy.SetTrigger("OnTriggerDestroy");
            _enemySpeed = 0;
            _destroyClip.Play();

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);
            Destroy(collidedObject.gameObject);
        }
    }

}
