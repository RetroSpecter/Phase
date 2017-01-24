using UnityEngine;
using System.Collections;

public class door : MonoBehaviour {

    public bool doored;
    public textBox firstBox;
    public AudioClip knock;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && !doored) {
            doored = true;
            firstBox.justStop();
            Invoke("playKnock", 0.5f);
        }
    }

    void playKnock() {
        audioManager.instance.Play(knock, 0.25f);
    }
}
