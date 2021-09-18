using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    //get handle for controller
    //a lot of variable
    private CharacterController _charCont;
    [Header("Movement")]
    [SerializeField] private float _jumpForce = 20;
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _gravity = 0.4f;
    [Header("Camera")]
    [SerializeField] private Camera _camera;

    private float _invertH = -1f, _invertV = 1f;
    [Range(1,10)]
    [SerializeField] private float _mouseSensitivityH = 1, _mouseSensitivityV = 1;



    // Start is called before the first frame update
    void Start()
    {
        _charCont = GetComponent<CharacterController>();
        if (_charCont == null) Debug.Log(transform.name + ":: CharacterController is null");

        _camera = Camera.main;
        if (_camera == null) Debug.LogError(transform.name + ":Child: Camera is null");

        CheckInverted();
        GameManager.Instance.Controller = false;

        #region hides and locks the cursor when game starts
        //*
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //*/
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region hides and locks the cursor in playmode
        //*
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        //*/
        #endregion

        Movement();
        if (!GameManager.Instance.Controller)
        {
            CameraMovement();
        }
        else
        {
            CameraMovementController();
        }


    }

    public void CheckInverted() // after unpausing/changing the settings run this to recheck if controls are inverted
    {
        #region InvertedControls
        if (GameManager.Instance._invertedV == true)
            _invertH = 1f;
        if (GameManager.Instance._invertedH == true)
            _invertV = -1f;
        #endregion
    }

    private void CameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        #region look left and right
        // look left and right
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += (mouseX * _mouseSensitivityH) * _invertV;
        transform.rotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);
        #endregion

        #region Look up and down
        //look up and down
        Vector3 cameraRotation = _camera.gameObject.transform.localEulerAngles;
        cameraRotation.x += (mouseY * (_mouseSensitivityV / 10f)) * _invertH;
        _camera.gameObject.transform.localRotation = Quaternion.AngleAxis(Mathf.Clamp(cameraRotation.x, 5, 25), Vector3.right);
        #endregion
    }

    private void CameraMovementController()
    {
        float controllerX = Input.GetAxis("RV");
        float controllerY = Input.GetAxis("RH");

        #region look left and right
        //left and right
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += (controllerX * _mouseSensitivityH * _invertV);
        transform.rotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);
        #endregion

        #region look up and down
        //up and down
        Vector3 cameraRotation = _camera.gameObject.transform.localEulerAngles;
        cameraRotation.x += (controllerY * (_mouseSensitivityV / 10f)) * _invertH;
        _camera.gameObject.transform.localRotation = Quaternion.AngleAxis(Mathf.Clamp(cameraRotation.x, 7, 25), Vector3.right);
        #endregion
    }

    private void Movement()
    {
        if (_charCont.isGrounded)
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");
            _direction = new Vector3(h, -0.1f, v);
            _velocity = _direction * _speed;
        //changes velocity from world space to local space. We go where we look.
            _velocity = transform.TransformDirection(_velocity); 

            //lets us use a controller to jump as well
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
            {
                _velocity.y = _jumpForce;
            }
        }
        //limits the fall speed of the character
        if(!_charCont.isGrounded && _velocity.y > -15f)
        {
            _velocity.y -= _gravity;
        }
        _charCont.Move( _velocity * Time.deltaTime);
    }

}
