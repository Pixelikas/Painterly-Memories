using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;


namespace GGJ2019.Bird
{
    [RequireComponent(typeof(CharacterController2D))]
    public class PlayerMovement : MonoBehaviour
    {
        // movement config
        public float gravity = -25f;
        public float runSpeed = 8f;
        public float groundDamping = 20f; // how fast do we change direction? higher means faster
        public float inAirDamping = 5f;
        public float jumpHeight = 3f;

        [HideInInspector]
        private float normalizedHorizontalSpeed = 0;
        private float normalizedVerticalSpeed = 0;

        private Animator _animator;
        private CharacterController2D _controller;
        private RaycastHit2D _lastControllerColliderHit;
        private Vector3 _velocity;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _controller = GetComponent<CharacterController2D>();
        }

        // the Update loop contains a very simple example of moving the character around and controlling the animation
        private void Update()
        {
            if (_controller.isGrounded)
                _velocity.y = 0;

            normalizedHorizontalSpeed = Input.GetAxisRaw("Horizontal");
            normalizedVerticalSpeed = Input.GetAxisRaw("Vertical");

            if (transform.localScale.x < 0f && normalizedHorizontalSpeed > 0f)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            if (transform.localScale.x > 0f && normalizedHorizontalSpeed < 0f)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

            _animator.SetBool("grounded", _controller.isGrounded);
            _animator.SetBool("walking", Mathf.Abs(normalizedHorizontalSpeed) > 0f);

            // we can only jump whilst grounded
            if (_controller.isGrounded && normalizedVerticalSpeed > 0f)
            {
                _velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
                _animator.SetTrigger("jump");

                SoundManager.PlaySound("woosh", false, .5f, 0.9f);
            }

            // apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
            var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
            _velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor);

            // apply gravity before moving
            _velocity.y += gravity * Time.deltaTime;

            // if holding down bump up our movement amount and turn off one way platform detection for a frame.
            // this lets us jump down through one way platforms
            if (_controller.isGrounded && normalizedVerticalSpeed < 0f)
            {
                _velocity.y *= 3f;
                _controller.ignoreOneWayPlatformsThisFrame = true;
            }

            _controller.move(_velocity * Time.deltaTime);

            // grab our current _velocity to use as a base for all calculations
            _velocity = _controller.velocity;
        }
    }
}