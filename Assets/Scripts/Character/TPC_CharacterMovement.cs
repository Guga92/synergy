using UnityEngine;

namespace ThirdPersonController
{
    [RequireComponent(typeof(CharacterController))]
    public class TPC_CharacterMovement : MonoBehaviour
    {
        [Header("MOVEMENT PARAMS")]
        [SerializeField, Range(1, 6)] private float walkSpeed = 5f;
        [SerializeField, Range(6, 10)] private float runSpeed = 7f;
        [SerializeField, Range(0, 1)] private float smoothingTurnTime = 0.1f;

        [Header("CAMERA PARAMS")]
        [SerializeField] private Camera playerCamera;

        [Header("GROUND CHECKER PARAMS")]
        [SerializeField] private Transform checkerTransform;
        [SerializeField, Range(0, 1)] private float distance = 0.1f;
        [SerializeField] private LayerMask groundMask;

        [Header("PARTICLE PARAMS")]
        [SerializeField] private ParticleSystem moveParticle;

        private Vector3 _velocity;
        private Vector3 _direction;
        private float _turnVelocity;
        private float _currentSpeed;
        private bool _onGround;

        private CharacterController _controller;
        private Animator _animator;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Move();
            SpeedHandler();
            GroundChecker();
            Animator();
        }

        private void Move()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            _direction = new Vector3(horizontal, 0, vertical).normalized;

            if (_direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + playerCamera.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnVelocity, smoothingTurnTime);

                transform.rotation = Quaternion.Euler(0, angle, 0);

                Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

                _controller.Move(moveDirection * _currentSpeed * Time.deltaTime);
            }

            _velocity.y += _onGround ? 0 : Physics.gravity.y * Time.deltaTime;

            moveParticle?.gameObject.SetActive(_onGround);

            if (_onGround)
                _velocity.y = 0;

            _controller.Move(_velocity * Time.deltaTime);
        }

        private void GroundChecker()
        {
            Ray ray = new Ray(checkerTransform.position, Vector3.down);

            _onGround = Physics.Raycast(ray, distance, groundMask);
        }

        private void SpeedHandler()
        {
            _currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        }

        private void Animator()
        {
            _animator.SetFloat("Speed", _direction.magnitude * _currentSpeed);
            _animator.SetBool("InAir", !_onGround);
        }
    }
}
