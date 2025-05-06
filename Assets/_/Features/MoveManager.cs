using UnityEngine;
using UnityEngine.InputSystem;

public class MoveManager : MonoBehaviour
{
    private Vector2 _InputDirection;
    [SerializeField]private float _moveSpeed;

    [SerializeField] private float margineToScreen;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    private void Start()
    {
        _InputDirection = Vector2.zero;
        Camera cam = Camera.main;
        float vertExtent = cam.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;
        
        minBounds = new Vector2(cam.transform.position.x - horzExtent + margineToScreen, cam.transform.position.y - vertExtent + margineToScreen);
        maxBounds = new Vector2(cam.transform.position.x + horzExtent - margineToScreen, cam.transform.position.y + vertExtent - margineToScreen);
    }

    private void Update()
    {
        Vector3 move = (Vector3)_InputDirection * _moveSpeed * Time.deltaTime;
        Vector3 newPos = transform.position + move;
        
        newPos.x = Mathf.Clamp(newPos.x, minBounds.x, maxBounds.x);
        newPos.y = Mathf.Clamp(newPos.y, minBounds.y, maxBounds.y);
        transform.position = newPos;
    }
    public void Move2D(InputAction.CallbackContext context)
    {
        _InputDirection = context.ReadValue<Vector2>();
    }
}
