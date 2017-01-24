using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

    public Vector3 direction;
    public float velocity;
    public int damage = 1;

    void Start() {
        direction = Vector3.Normalize(direction);
        Destroy(this.gameObject, 3f);
    }

    void Update() {
        transform.position = new Vector3(transform.position.x + direction.x * velocity, transform.position.y, transform.position.z + direction.z * velocity);
    }

    void OnCollisionEnter(Collision other) {
        Destroy(this.gameObject);
    }
}
