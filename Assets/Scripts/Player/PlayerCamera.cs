using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float sens;
    [SerializeField] private Transform orientation, player;
    
    private float mouseX, mouseY, rotationX, rotationY;
    
    public void onMouseInput(InputAction.CallbackContext context)
    {
        mouseX = context.ReadValue<Vector2>().x;
        mouseY = context.ReadValue<Vector2>().y;
    }
    private void Start()
    { 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        rotationY += mouseX * Time.deltaTime * sens;
        rotationX -= mouseY * Time.deltaTime * sens;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        orientation.rotation = Quaternion.Euler(0, rotationY + 90f, 0);
        player.rotation = Quaternion.Euler(0, rotationY, 0);
    }
}