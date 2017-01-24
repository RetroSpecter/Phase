using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

    public float interpVelocity;
    public int roomIndex;
    public Vector3 offset;
    Vector3 targetPos;
	//Used to Disable Camera
	public bool isFollowing;
    public static CameraBehavior instance;
    public int maxDimensions;

    public GameObject[] rooms;

    public AudioClip teleportUp;
    public AudioClip teleportDown;

    void Start() {
        instance = this;
        targetPos = transform.position;
    }

    void Update() {
        Transform target = rooms[roomIndex].transform;
        if (target != null && isFollowing) {
            Vector3 posnoZ = transform.position;
			posnoZ.z = target.transform.position.z;

            Vector3 targetDirection = (target.transform.position - posnoZ);

            interpVelocity = targetDirection.magnitude * 100f;
            targetPos = target.transform.position + target.transform.forward * 0;
            transform.position = Vector3.Lerp(transform.position, targetPos + offset, Time.deltaTime * 4);
        }

        if (Vector3.Distance(targetPos + offset, this.transform.position) < 1f && playerControllerv2.instance.active) {
            if (Input.GetKeyDown(KeyCode.Z)) {
                roomChange(roomIndex + 1);
            }

            if (Input.GetKeyDown(KeyCode.C)) {
                roomChange(roomIndex - 1);
            }
        }

        checkPlayer();
    }

    public void checkPlayer() {
        if (this.transform.position.y - playerControllerv2.instance.transform.position.y > 500) {
            GameObject room = rooms[roomIndex];
            playerControllerv2.instance.transform.position = new Vector3(room.transform.position.x, room.transform.position.y + 0.75f, room.transform.position.z);
        }
    }

    public void roomChange(int index) {
        if (index >= 0 && index < rooms.Length && index <= maxDimensions) {
            audioManager.instance.Play((roomIndex < index) ? teleportUp : teleportDown, 0.05f);

            roomIndex = index;
            for (int i = 0; i < rooms.Length; i++) {
                if (i == roomIndex && i >= 0) {
                    Vector3 localPos = playerControllerv2.instance.transform.localPosition;
                    playerControllerv2.instance.transform.SetParent(rooms[i].transform);
                    StartCoroutine("pauseAction");
                    localPos.y += 1;
                    playerControllerv2.instance.transform.localPosition = localPos;
                    StartCoroutine(fade.instance.fadeIn(Color.black, 3.5f));
                }
            }
        }      
    }

    IEnumerator pauseAction() {
        playerControllerv2.instance.active = false;
        playerControllerv2.instance.GetComponent<Rigidbody>().velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        playerControllerv2.instance.active = true;
    }

    public void dying() {
        roomChange(0);
    }
}