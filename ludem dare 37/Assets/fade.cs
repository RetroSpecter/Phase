using UnityEngine;
using System.Collections;

public class fade : MonoBehaviour {

    public int index;
    SpriteRenderer renderer;
    public static fade instance;

    void Start() {
        instance = this;
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = new Color(0, 0, 0, 1);
        StartCoroutine(fadeOut(Color.black, 0.5f));
    }


    public IEnumerator fadeIn(Color c, float time) {
        float f = 0;

        while (f < 1) {
            f += Time.deltaTime * time;
            renderer.color = new Color(c.r, c.g, c.b, f);
            yield return null;
        }
        renderer.color = new Color(c.r, c.g, c.b, 1);
        lighting.instance.changeColor(CameraBehavior.instance.roomIndex);
        StartCoroutine(fadeOut(c, time));
    }

    IEnumerator fadeOut(Color c, float time) {
        float x = 0;
        while (x < 1) {
            x += Time.deltaTime * time;
            renderer.color = new Color(c.r, c.g, c.b, 1 - x);
            yield return null;
        }
        renderer.color = new Color(c.r, c.g, c.b, 0);
    }

}
