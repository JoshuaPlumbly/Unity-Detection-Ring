using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _normalMoveSpeed;

    CharacterController _character;
    Transform _camera;

    private void Awake()
    {
        _character = GetComponent<CharacterController>();
        _camera = Camera.main.transform;
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Move(new Vector2(h, v));
    }

    private void Move(Vector2 direction)
    {
        var movement = _camera.forward * direction.y;
        movement += _camera.right * direction.x;
        movement.Normalize();
        movement.y = 0;
        movement *= _normalMoveSpeed * Time.deltaTime;

        _character.Move(movement);
    }
}