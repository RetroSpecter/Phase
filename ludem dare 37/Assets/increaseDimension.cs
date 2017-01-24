using UnityEngine;
using System.Collections;

public class increaseDimension : MonoBehaviour {

    bool triggered;

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player" && playerControllerv2.instance.active == true && !triggered){
            triggered = true;
            GetComponent<Animator>().Play("roomCollect");
        }
    }

    public void destroyDude() {
        CameraBehavior.instance.maxDimensions++;
        canvasUI.instance.addRoom();
        Destroy(this.gameObject);
    }
}
