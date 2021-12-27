using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Create a speed variable to the player to manipulate its speed.
    //SerializedField is use to create variable as private but user can access them within unity inspector.
    [SerializeField]
    private float _Speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireTime = 0.5f;
    private float _nextFire = 0.0f;
    private int _lives = 3;

    // Start is called before the first frame update
    void Start() {
        //With start of the game rearrange the position of the player to the center of the screen.
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update() {
        //Calculate the player movement and apply the bound to screen.
        calculateMovement();

        //Offset to the laser object from the player object
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire){
            fireLaser();
        }
    }

    //Calculate player movement
    void calculateMovement() {
        //Now create the player movemenet.
        //Input is the unity function, where user can access the input from  alhpa keys as well as from arrow keys
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * _Speed * Time.deltaTime);

        //Now create a player bounds.
        if (transform.position.y >= 0) {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }else if (transform.position.y <= -4.8f) {
            transform.position = new Vector3(transform.position.x, -4.8f, 0);
        }

        if (transform.position.x >= 11.3f) {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }else if (transform.position.x <= -11.3f) {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    //Fire Laser
    void fireLaser() {
        //It will delay the fire mechanism by the _fireTime instead of continus fire.
        _nextFire = Time.time + _fireTime;
        //It will instantiate the clone of the prefab in the runtime, to simulate the laser or bullet behaviour.
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
    }

    //Damage Method
    public void Damage(){
        --_lives;

        //Check if lives are zero, then destroy the player
        if(_lives > 1){
            Destroy(this.gameObject);
        }
    }
}
