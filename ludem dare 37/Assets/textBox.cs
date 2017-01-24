using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class textBox : MonoBehaviour {

    public Text dialogueText;
    public string[] opening;
    public string[] cycle;
    public static textBox instance;

    public GameObject capsule;
    public AudioClip textTick;

    void Start () {
        transform.parent.GetComponent<Image>().enabled = false;
        GetComponent<Text>().enabled = false;
        capsule.SetActive(false);
        instance = this;
        dialogueText = GetComponent<Text>();
        Invoke("lazy", 1);
        dialogueText.text = "";

    }

    void lazy() {
        transform.parent.transform.parent.GetComponent<Animator>().Play("textBoxTurnOn");
        transform.parent.GetComponent<Image>().enabled = true;
        GetComponent<Text>().enabled = true;
        StartCoroutine(autoMoveText(new int[2] { 0, 1 }, 5));
    }

    public bool jumpedZ = false;
    public bool jumpedC = false;
    void Update() {
        if (Input.GetKeyDown(KeyCode.Z) && !jumpedZ && capsule == null) {
            StopCoroutine("autoMoveText");
            StopAllCoroutines();
            dialogueText.text = "";
            StartCoroutine(autoMoveText(new int[2] {6, 7}, 3));
            jumpedZ = true;
        }

        if (Input.GetKeyDown(KeyCode.C) && jumpedZ && !jumpedC && capsule == null) {
            StopCoroutine("autoMoveText");
            StopAllCoroutines();
            dialogueText.text = "";
            StartCoroutine(autoMoveText(new int[3] {8,9,10}, 3));
            jumpedC = true;
        }
    }

    public void justStop() {
        StopAllCoroutines();
        StartCoroutine(autoMoveText(new int[] { 2, 3, 4 }, 3));
    }

    public IEnumerator autoMoveText(int[] array, float sentencePause) {
        dialogueText.text = "";
        yield return new WaitForSeconds(1);
        for (int i = 0; i < array.Length; i++) {
            StartCoroutine(autoTypeText(opening[array[i]], 0.03f));

            if(array[i] == 4){
                capsule.SetActive(true);
            }    
            yield return new WaitForSeconds(sentencePause);
            if (array[i] == 10) {
                transform.parent.GetComponent<Image>().enabled = false;
                GetComponent<Text>().enabled = false;
                yield return new WaitForSeconds(30);   
                StartCoroutine(autoMoveTextII(30));
                StopCoroutine("autoMoveText");
            }
        }
    }

    public IEnumerator autoMoveTextII(float sentencePause) {
        dialogueText.text = "";
        for (int i = 0; i < cycle.Length; i++) {
            transform.parent.transform.parent.GetComponent<Animator>().Play("textBoxTurnOn");
            transform.parent.GetComponent<Image>().enabled = true;
            GetComponent<Text>().enabled = true;
            StartCoroutine(autoTypeText(cycle[i], 0.03f));

            if (i == cycle.Length - 1) {
                i = 0;
            }

            yield return new WaitForSeconds(sentencePause);
            transform.parent.GetComponent<Image>().enabled = false;
            GetComponent<Text>().enabled = false;
            yield return new WaitForSeconds(sentencePause / 4);
        }

    }

    IEnumerator autoTypeText(string text, float letterPause) {
        int play = 0;
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray()) {
            dialogueText.text += letter;
            if (Mathf.Abs(this.transform.position.y - playerControllerv2.instance.transform.position.y) < 10) {
                print("");
                audioManager.instance.Play(textTick, 0.05f);
            }
            play++;
            yield return new WaitForSeconds(letterPause);
        }
        dialogueText.text = text;
    }
}
