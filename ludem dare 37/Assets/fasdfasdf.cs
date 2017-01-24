using UnityEngine;
using System.Collections;

public class fasdfasdf : MonoBehaviour {

    public string nextLevel;
    public SpriteRenderer renderer;

	void Update () {
        if (Input.GetKeyDown(KeyCode.X)) {
            StartCoroutine(fadeI(Color.black));
        }
	}

    IEnumerator fadeI(Color c) {
        float x = 0;
        float f = 0;
        while (f < 1) {
            f += Time.deltaTime * 2;
            renderer.color = new Color(c.r, c.g, c.b, f);
            yield return null;
        }
        renderer.color = new Color(c.r, c.g, c.b, 255);
        Application.LoadLevel(nextLevel);
    }
}
