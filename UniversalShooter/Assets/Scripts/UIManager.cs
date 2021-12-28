using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //Create a handle for Score component
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _liveSpriteImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOver;

    // Start is called before the first frame update
    void Start() {
        _scoreText.text = "Kills : 0";
        _gameOver.text = "";
        _gameOver.gameObject.SetActive(false);
    }

    //Update the ScoreText value
    public void updateScoreText(int Score) {
        _scoreText.text = "Kills : " + Score;
    }

    //Update the lives of the player
    public void updateLives(int currentLives) {
        _liveSpriteImg.sprite = _liveSprites[currentLives];
    }

    //Display GameOver sprite
    public void displayGameOver() {
        _gameOver.gameObject.SetActive(true);
        StartCoroutine(flikrGameOverRoutine());
    }

    // Coroutine to flikr the GameOver text
    IEnumerator flikrGameOverRoutine(){
        while (true){
            _gameOver.text = "You Faught Well";
            yield return new WaitForSeconds(0.5f);
            _gameOver.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }


}
