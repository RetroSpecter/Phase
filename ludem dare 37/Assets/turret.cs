using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class turret : MonoBehaviour {

    public GameObject bullet;

    public float playerAttatchRadius = 1;
    public float targetingRadius = 5;


    void Awake() {
        Invoke("autoFire", 0);
    }

    public void autoFire() {
        InvokeRepeating("shoot", 1, 1);
    }

    public void shoot() {
        GameObject shot = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        shot.GetComponent<bullet>().direction = this.transform.forward;
    }
}
