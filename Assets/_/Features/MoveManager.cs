using UnityEngine;
using UnityEngine.InputSystem;

public class MoveManager : MonoBehaviour
{
    private Vector2 _myPosition;
    [SerializeField]private float _moveSpeed;

    private void Start()
    {
        _myPosition = transform.position;
    }

    private void Update()
    {
        Vector2 move = new Vector3(_myPosition.x, _myPosition.y);
        transform.position += (Vector3)move * _moveSpeed * Time.deltaTime;
    }
    public void Move2D(InputAction.CallbackContext context)
    {
        _myPosition = context.ReadValue<Vector2>();
    }
}
