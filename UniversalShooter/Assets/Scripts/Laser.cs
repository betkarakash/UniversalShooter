using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 50f;

    // Update is called once per frame
    void Update() {
        //translate the laser object into upside motion
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        //Once the laser object move away from screen, then destroy object,
        //so it will not exist without a reason in the game play
        if (transform.position.y >= 7) {
            //gameObject is the value for the current istance of the object.
            Destroy(this.gameObject);
        }
    }
}
