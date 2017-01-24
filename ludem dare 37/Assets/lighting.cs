using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lighting : MonoBehaviour {

    public Color[] colors;
    public static lighting instance;

    void Start() {
        instance = this;
    }

    public void changeColor(int i) {
        GetComponent<Light>().color = colors[i];
    }
}
