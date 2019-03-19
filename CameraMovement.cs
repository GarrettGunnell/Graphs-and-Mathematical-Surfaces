using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float speed = 2f;

    void Update() {
        Vector3 position = transform.position;
        if (Input.GetKey("w")) {
            position.z += speed * Time.deltaTime;
        }
        if (Input.GetKey("s")) {
            position.z -= speed * Time.deltaTime;
        }
        if (Input.GetKey("a")) {
            position.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey("d")) {
            position.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("space")) {
            position.y += speed * Time.deltaTime;
        }
        if (Input.GetKey("left shift")) {
            position.y -= speed * Time.deltaTime;
        }

        transform.position = position;
    }
}
