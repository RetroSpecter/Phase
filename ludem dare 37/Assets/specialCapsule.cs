using UnityEngine;
using System.Collections;

public class specialCapsule : MonoBehaviour {

    public textBox firstBox;
    public AudioClip sound;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            textBox.instance.StopAllCoroutines();
            textBox.instance.StartCoroutine(firstBox.autoMoveText(new int[] { 5 }, 5));
        }
    }

    public void playWoosh() {
        audioManager.instance.Play(sound, 0.25f);
    }
}
