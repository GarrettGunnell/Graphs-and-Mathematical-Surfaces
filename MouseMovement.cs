using UnityEngine;

public class MouseMovement : MonoBehaviour {
    public float Vspeed = 2f;
    public float Hspeed = 2f;
    private float verticalChange = 0.0f;
    private float horizontalChange = 0.0f;

    void Update() {
        verticalChange -= Vspeed * Input.GetAxis("Mouse Y");
        horizontalChange += Hspeed * Input.GetAxis("Mouse X");

        transform.eulerAngles = new Vector3(verticalChange, horizontalChange, 0.0f);
    }
}
