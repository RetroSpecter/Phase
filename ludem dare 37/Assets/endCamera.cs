using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endCamera : MonoBehaviour {

    public string levelName;

    public void endToTitle() {
        SceneManager.LoadScene(0);
    }
}
