using UnityEngine;
using System.Collections;

public class moveableObject : MonoBehaviour {

    public bool pickedUp;

    bool touching;

    void Update() {
        if (Input.GetKeyDown(KeyCode.X)) {
            if (touching || pickedUp) {
                pickUp();
            }
        }
    }

    void pickUp() {
        if (!pickedUp) {
            pickedUp = true;
            this.transform.forward = playerControllerv2.instance.transform.forward;
            transform.parent = playerControllerv2.instance.transform;
            transform.position = new Vector3(playerControllerv2.instance.transform.position.x, playerControllerv2.instance.transform.position.y + 1.5f,
                playerControllerv2.instance.transform.position.z);
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
        } else {
            pickedUp = false;
            transform.parent = playerControllerv2.instance.transform.parent;
            Vector3 newPos = playerControllerv2.instance.transform.position + playerControllerv2.instance.transform.forward * 1;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
            transform.position = newPos;
        }
    }

    void OnCollisionStay(Collision other) {
        if (other.gameObject.tag == "Player" && !pickedUp) {
            touching = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player") {
            touching = false;
        }
    }
}
