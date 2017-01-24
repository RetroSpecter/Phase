using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

    public Vector3[] positions;
    public Vector3 initialPosition;
    public bool comareX = true;
    public float speed = 0.05f;

    void Start() {
        initialPosition = transform.localPosition;
    }

    int selectedPosition = 0;
    void Update() {
        if (comareX) {
            if (playerControllerv2.instance.transform.localPosition.x + (selectedPosition == 1 ? -0.15f : 0.15f) > initialPosition.x) {
                selectedPosition = 0;
            } else{
                selectedPosition = 1;
            }
        } else {
            if (playerControllerv2.instance.transform.localPosition.z < initialPosition.z + 0.17f) {
                selectedPosition = 0;
            } else {
                selectedPosition = 1;
            }
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, positions[selectedPosition], speed);
    }
}
