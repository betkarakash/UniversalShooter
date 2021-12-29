using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Create a speed variable to the player to manipulate its speed.
    //SerializedField is use to create variable as private but user can access them within unity inspector.
    [SerializeField]
    private float _Speed = 5f;
    private float _SpeedBoost = 2f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireTime = 0.5f;
    private float _nextFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private bool _isSpeedBoostActive = false;
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private int _Score = 0;
    private UIManager _UIManager;
    [SerializeField]
    private GameObject _firstHit, _secondHit;
    [SerializeField]
    private AudioClip _laserSound;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start() {
        //With start of the game rearrange the position of the player to the center of the screen.
        transform.position = new Vector3(0, -3, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null){
            Debug.Log("SpawnManager is empty.");
        }
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_UIManager == null) {
            Debug.Log("UI Manager is empty.");
        }
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null) {
            Debug.Log("Audio Source is empty.");
        }
        else {
            _audioSource.clip = _laserSound;
        }
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
        if (_isSpeedBoostActive == false) {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * _Speed * Time.deltaTime);
        }else {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * (_Speed * _SpeedBoost) * Time.deltaTime);
        }

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
        //If space key is pressed and isTripleShotActive is enabled then it will fire 3 laser else will fire single laser
        if (_isTripleShotActive == true) {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }else {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        }

        //Play the sound of laser
        _audioSource.Play();
    }

    //Damage Method
    public void Damage(){
        //Check the Shield Activ status
        if(_isShieldActive == true) {
            _isShieldActive = false;
            return;
        }
        --_lives;
        updateLivesOnUI(_lives);

        //Visualise damage once hit.
        if(_lives == 2) {
            _firstHit.gameObject.SetActive(true);
        }else if(_lives == 1) {
            _secondHit.gameObject.SetActive(true);
        }

        //Check if lives are zero, then destroy the player
        if (_lives < 1){
            //Call the spawwnManager function to stop enemies once the player is destroyed.
            _spawnManager.OnPlayerDied();
            Destroy(this.gameObject);

            //Display the GameOver text
            showGameOver();
        }
    }

    //Activate the TripleShot power up
    public void activatePowerUp(int powerUpID){
        //Activate the power up according to the id
        switch (powerUpID) {
            case 0:
                _isTripleShotActive = true;
                break;
            case 1:
                _isSpeedBoostActive = true;
                break;
            case 2:
                _isShieldActive = true;
                activateShieldVisualizer(_isShieldActive);
                break;
            default:
                Debug.Log("No PowerUp");
                break;
        }

        //Call the power down routine.
        StartCoroutine(deactivatePowerUp(powerUpID));
    }

    //Couroutine to start the power down
    IEnumerator deactivatePowerUp(int powerUpID) {
        yield return new WaitForSeconds(5.0f);

        //Back to the previous state.
        switch (powerUpID){
            case 0:
                _isTripleShotActive = false;
                break;
            case 1:
                _isSpeedBoostActive = false;
                break;
            case 2:
                _isShieldActive = false;
                activateShieldVisualizer(_isShieldActive);
                break;
            default:
                Debug.Log("No PowerUp");
                break;
        }
    }

    //Activate the Shield Visualizer
    void activateShieldVisualizer(bool _isShieldActive) {
        if(transform.GetChild(0).gameObject != null) {
            transform.GetChild(0).gameObject.SetActive(_isShieldActive);
        }
    }

    //Add the score.
    public void addScoreToPlayer() {
        _Score += 1;
        if(_UIManager != null) {
            _UIManager.updateScoreText(_Score);
        }
    }

    //Update the lives
    public void updateLivesOnUI(int lives) {
        if (_UIManager != null) {
            _UIManager.updateLives(lives);
        }
    }

    //Show the GameOver object
    public void showGameOver() {
        if(_UIManager != null) {
            _UIManager.displayGameOver();
        }
    }
}
