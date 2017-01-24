using UnityEngine;
using System.Collections;

public class portal : MonoBehaviour {

    public int index = 0;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && index == CameraBehavior.instance.roomIndex) {
            CameraBehavior.instance.roomChange(CameraBehavior.instance.roomIndex + 1);
        }
    }
}
