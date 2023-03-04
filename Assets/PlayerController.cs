using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] Rigidbody2D _rb2D { get => GetComponent<Rigidbody2D>(); }
    [SerializeField] float _jumpForce = 6f;
    [SerializeField] float _maxHoldJump = 0.5f;
    [SerializeField] float _holdingJumpTime = 0f;
    [SerializeField] bool _isExitSpaceButton;
    //Old Ground Check
    // [SerializeField] bool _isGround = true;


    [SerializeField] float _rayDistance = .1f;
    [SerializeField] LayerMask _groundCheckLayer;
    private void Start()
    {
        _rb2D.freezeRotation = true;
    }

    private void Update()
    {
        CharacterRun();
        CharacterJumpInput();
        IsGround();
    }
    void CharacterRun()
    {
        float direction = Input.GetAxis("Horizontal");
        transform.Translate(new Vector2(direction,0) * _speed * Time.deltaTime);
    }
    void CharacterJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGround())
        {
            CharacterJump();
            _holdingJumpTime = _maxHoldJump;
            _isExitSpaceButton = false;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isExitSpaceButton = true;
        }
        if (Input.GetKey(KeyCode.Space) && !IsGround() && _holdingJumpTime >0 && !_isExitSpaceButton)
        {
            CharacterJump();
            _holdingJumpTime -= Time.deltaTime;
        }
    }
    void CharacterJump()
    {
        _rb2D.velocity = Vector2.up * _jumpForce;
    }
    bool IsGround()
    {
        Vector3 rayOrigin = transform.position - Vector3.up * (transform.localScale.y / 2);
        //Debug.DrawRay(rayOrigin, Vector3.down,Color.red);
        RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, Vector2.down, _rayDistance, _groundCheckLayer);
        if (hit2D.collider != null)
            return true;

        return false;
    }
    #region Ground Check
    //Old Ground Check
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        _isGround = true;  
    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        _isGround = false;
    //    }
    //}
    #endregion

}
