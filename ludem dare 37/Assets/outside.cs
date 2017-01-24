using UnityEngine;
using System.Collections;

public class outside : MonoBehaviour {

    public bool outsideNow;
    public string nextLevel;

    void Update() {
        if (Input.GetKeyDown(KeyCode.C) && outsideNow) {
            CameraBehavior.instance.roomChange(0);
            Invoke("jumpToEnding", 0.25f);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player"){
            outsideNow = true;
            } 
    }

    void OnTriggerExit(Collider other)  {
        if (other.gameObject.tag == "Player")
        {
            outsideNow = false;
        }
    }

    void jumpToEnding() {
        Application.LoadLevel(nextLevel);
    }
}
