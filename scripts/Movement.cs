using System.Windows.Input;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float mouseSpeed;
    public bool movingToTarget = false;
    public Vector3 moveTowards;
    public Quaternion rotateTowards;

    private float yaw = 0.0f;
    private float pitch = 0.0f;


    void Update()
    {
        if (movingToTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTowards, 100f * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTowards, 100f * Time.deltaTime);
            if (transform.position == moveTowards && transform.rotation == rotateTowards)
            {
                movingToTarget = false;
                pitch = transform.eulerAngles[0];
                yaw = transform.eulerAngles[1];
            }
        }
        else
        {
            if (Input.GetMouseButton(1))
            {
                Cursor.visible = false;
                yaw += mouseSpeed * Input.GetAxis("Mouse X");
                pitch += mouseSpeed * -Input.GetAxis("Mouse Y");

                if (pitch > 90) pitch = 90;
                if (pitch < -90) pitch = -90;

                transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            }
            else
            {
                Cursor.visible = true;
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.Space))
            {
                transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
            }
        }
    }
}
