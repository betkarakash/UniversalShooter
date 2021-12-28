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

    // Start is called before the first frame update
    void Start() {
        _scoreText.text = "Kills : 0";
    }

    //Update the ScoreText value
    public void updateScoreText(int Score) {
        _scoreText.text = "Kills : " + Score;
    }

    //Update the lives of the player
    public void updateLives(int currentLives) {
        _liveSpriteImg.sprite = _liveSprites[currentLives];
    }
}
