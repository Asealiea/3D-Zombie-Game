using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //get handle for controller
    //a lot of variable
    private CharacterController _charCont;
    private float _jumpForce = 20;
    [SerializeField] private float _yVelocity;
    [SerializeField] private Vector3 _velocity;
    private Vector3 _direction;
    private float _speed = 5;
    private float _gravity = 0.4f;
    [SerializeField] private bool _isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        _charCont = GetComponent<CharacterController>();
        if (_charCont == null) Debug.Log(transform.name + ":: CharacterController is null");
      
    }

    // Update is called once per frame
    void Update()
    {
        ///wsad keys,
        ///direction = vector to move 
        ///velocity = direction x speed
        ///adding in jump
        ///if grounded then add jump force to velocity y
        ///
        ///controller.move( velcoity * time.delta time)

        Movement();
        _isGrounded = IsGrounded();

    }

    private void Movement()
    {
        if (_charCont.isGrounded)
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");
            _direction = new Vector3(h, -0.1f, v);
            _velocity = _direction * _speed;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // _yVelocity = _jumpForce;
                _velocity.y = _jumpForce;
            }
        }
        if(!_charCont.isGrounded && _velocity.y > -15f)
        {
           // _yVelocity -= _gravity;
            _velocity.y -= _gravity;
        }
      
      //  _velocity.y = _yVelocity;
        _charCont.Move(_velocity * Time.deltaTime);

    }

    private bool IsGrounded()
    {
        return _charCont.isGrounded;
    }
  
}
