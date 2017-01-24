using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class canvasUI : MonoBehaviour {

    public GameObject initialRoom;
    public List<GameObject> allRooms;

    public static canvasUI instance;

    public Color onColor;
    public Color offColor;

    public AudioClip pop;

    void Start() {
        instance = this;
        allRooms.Add(initialRoom);
    }

    public void Update() {
        pickSelectedRoom();

        if (Input.GetKeyDown(KeyCode.Space)) {
            print("hoi");
            addRoom();
        }
    }

    public void pickSelectedRoom() {
        for (int i = 0; i < allRooms.Count; i++) {
            if (i == CameraBehavior.instance.roomIndex) {
                allRooms[i].GetComponent<Image>().color = onColor;
                allRooms[i].transform.localScale = Vector3.Lerp(allRooms[i].transform.localScale , new Vector3(0.5f, 0.4f), Time.deltaTime * 3);
            } else {
                allRooms[i].GetComponent<Image>().color = offColor;
                allRooms[i].transform.localScale = Vector3.Lerp(allRooms[i].transform.localScale, new Vector3(0.3f, 0.2f), Time.deltaTime * 3);
            }
        }
    }

    public void addRoom() {
        Vector3 initialPos = initialRoom.transform.position;
        GameObject newRoom = Instantiate(initialRoom, initialRoom.transform.localPosition, initialRoom.transform.rotation) as GameObject;
        newRoom.transform.SetParent(this.transform, true);
        Vector3 newPos = new Vector3(initialRoom.transform.localPosition.x, initialRoom.transform.localPosition.y + 30 * CameraBehavior.instance.maxDimensions, initialRoom.transform.localPosition.z);

        newRoom.transform.localPosition = newPos;
        newRoom.transform.localScale = new Vector3(0, 0, 0);
        audioManager.instance.Play(pop, 0.05f);
        allRooms.Add(newRoom);
    }
}
