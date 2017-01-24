using UnityEngine;
using System.Collections;

public class death : MonoBehaviour {

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            CameraBehavior.instance.dying();
        }
    }
}
