using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IControllable
{

    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _speedSprint;
    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpPower;
    private float _speed;
    private CharacterController _characterController;
    private Vector3 velocity;

    private void Start()
    {
        _speed = _normalSpeed;
        _characterController = GetComponent<CharacterController>();
    }

    public void Jump(bool isPossibleToJump)
    {
        if (isPossibleToJump)
            velocity.y = _jumpPower;              
    }

    public void Run()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        _characterController.Move(move * _speed * Time.deltaTime);
        _characterController.Move(velocity * Time.deltaTime);
    }

    public void Sit(bool isPressedBttn)
    {
        _characterController.height = isPressedBttn ? 1f : 2f;
    }

    public void Sprint(bool isPressedBttn)
    {
        _speed = isPressedBttn ? _speedSprint : _normalSpeed;
    }

    public void Gravity(bool isGround)
    {
        if (isGround && velocity.y < 0)
            velocity.y = -0.1f;
        velocity.y -= _gravity * Time.deltaTime;
    }
}
