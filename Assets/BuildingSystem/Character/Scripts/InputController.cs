using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Player _character;
    private IControllable _controllableObject;
    private CharacterController _characterController;

    void Start()
    {
        _controllableObject = _character.GetComponent<IControllable>();
        _characterController = _character.GetComponent<CharacterController>();
    }

    void Update()
    {
        _controllableObject.Jump(Input.GetKey(KeyCode.Space) && _characterController.isGrounded);
        _controllableObject.Gravity(_characterController.isGrounded);
        _controllableObject.Run();
        _controllableObject.Sit(Input.GetKey(KeyCode.LeftControl));
        _controllableObject.Sprint(Input.GetKey(KeyCode.LeftShift));

    }


}
