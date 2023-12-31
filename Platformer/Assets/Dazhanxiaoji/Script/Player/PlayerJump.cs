﻿using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour
{
    public float jumpPower = 10;
    public PlayerGroundDetecter groundDetecter;
    PlayerMovePosition _movePosition;
    bool _isFloating { get { return !groundDetecter.isGrounded; } }
    bool _isAttacking;

    PlayerAttackBehaviour _attack;
    PlayerHealthBehaviour _health;

    void Awake()
    {
        _movePosition = GetComponent<PlayerMovePosition>();
        _attack = GetComponent<PlayerAttackBehaviour>();
        _health = GetComponent<PlayerHealthBehaviour>();
    }

    void Update()
    {
        ReadInput();
    }

    public bool IsJumping { get { return _isFloating; } }

    void ReadInput()
    {
        if (_attack.isAttacking)
            return;
        if (_health.isDead)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
            TryJump();
    }

    void TryJump()
    {
        if (canNotJump)
            return;

        DoJump();
    }

    bool canNotJump { get { return _isFloating || _isAttacking || PlayerBehaviour.instance.health.isDead; } }

    void DoJump()
    {
        //_speedY = jumpPower;
        PlayerBehaviour.instance.animator.SetBool("walk", false);
        PlayerBehaviour.instance.animator.SetTrigger("jump");
        _movePosition.rb.AddForce(new Vector2(0, jumpPower));
    }

    private void FixedUpdate()
    {
        //  if (_speedY <= 0 && !_isFloating)
        //  {
        //      _speedY = 0;
        //      return;
        //  }

        //_movePosition.AddMovement(Vector3.up * _speedY);
        //_speedY -= gravity * Time.fixedDeltaTime;
    }

    public void OnGrounded()
    {
        //_speedY = 0;
        var v = _movePosition.rb.velocity;
        v.y = 0;
        _movePosition.rb.velocity = v;
        _movePosition.StopXMovement();

        PlayerBehaviour.instance.animator.SetBool("walk", false);
    }
}