using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 30.0f;
    private Camera _Camera;
    [SerializeField] private LayerMask _layerMask;
    private InputHandler _InputHandler;

    private Rigidbody _Rigidbody;
    private Transform body;

    [SerializeField] private float footstepFrequency = 1f;
    private float cur_time;

    private void Awake() {
        //_Input = new Controls();
        _Rigidbody = GetComponent<Rigidbody>();
        _InputHandler = FindFirstObjectByType<InputHandler>();
        body = transform.Find("Body");
        _Camera = Camera.main;
    }

    private void OnMove() {
        //move
        Vector3 movement = new Vector3(_InputHandler.MoveInput.x, 0, _InputHandler.MoveInput.y);
        _Rigidbody.velocity = movement * speed;
        PlayFootStepSFX();
    }
    private void OnAim() {
        //rotaciona o corpo do player para o cursor do mouse, relativo a sua posi��o no ch�o.
        Ray ray = _Camera.ScreenPointToRay(_InputHandler.AimInput);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask)) {
            Vector3 mousePos = raycastHit.point;
            body.LookAt(new Vector3(mousePos.x, body.position.y, mousePos.z));
        }
    }
    private void PlayFootStepSFX()
    {
        cur_time += Time.deltaTime;
        if (_InputHandler.MoveInput.x != 0 || _InputHandler.MoveInput.y != 0)
        {
            if (cur_time >= footstepFrequency)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.sfx_gameplay_char_step, transform.position);
                cur_time = 0;
            }
        }
    }
    private void FixedUpdate()
    {
        OnMove();
        OnAim();
    }
}
