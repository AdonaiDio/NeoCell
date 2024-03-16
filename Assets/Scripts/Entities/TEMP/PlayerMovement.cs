using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private Camera _Camera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private InputHandler _InputHandler;

    private Rigidbody _Rigidbody;
    private Transform body;

    private void Awake()
    {
        //_Input = new Controls();
        _Rigidbody = GetComponent<Rigidbody>();
        body = transform.Find("Body");
        _Camera = Camera.main;
    }

    private void OnMove()
    {
        //move
        Vector3 movement = new Vector3(_InputHandler.MoveInput.x, 0, _InputHandler.MoveInput.y);
        _Rigidbody.velocity = movement * speed;
    }
    private void OnAim()
    {
        //rotaciona o corpo do player para o cursor do mouse, relativo a sua posição no chão.
        Ray ray = _Camera.ScreenPointToRay(_InputHandler.AimInput);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
        {
            Vector3 mousePos = raycastHit.point;
            body.LookAt(new Vector3(mousePos.x, body.position.y, mousePos.z));
        }

    }

    private void FixedUpdate()
    {
        OnMove();
        OnAim();
    }
}
