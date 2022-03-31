using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        HandleInput();
        Look();
    }

    void Look()
    {
        float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * Time.fixedDeltaTime * InputManager.Instance.sensitivity * 10;
        float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * Time.fixedDeltaTime * InputManager.Instance.sensitivity * 10;
        transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
    }

    void HandleInput()
    {
        if (Input.GetKey(InputManager.Instance.Forward))
            transform.position = transform.position + (transform.forward * speed * Time.deltaTime);
        if (Input.GetKey(InputManager.Instance.Backward))
            transform.position = transform.position + (-transform.forward * speed * Time.deltaTime);

        if (Input.GetKey(InputManager.Instance.Right))
            transform.position = transform.position + (transform.right * speed * Time.deltaTime);
        if (Input.GetKey(InputManager.Instance.Left))
            transform.position = transform.position + (-transform.right * speed * Time.deltaTime);
    }
}
