using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 50f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        //translate the laser object into upside motion
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);
    }
}
