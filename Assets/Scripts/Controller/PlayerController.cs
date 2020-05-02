using UnityEngine;

namespace FungusLibrary
{
    public class PlayerController : MonoBehaviour
    {
        public Transform _groundCheck; //Проверка находиться ли герой на земле
        public AudioManager AudioManager;
        public UIMenu UIMenu;

        [SerializeField] private float _maxSpeed = 5; //Максимальная скорость движения героя
        [SerializeField] private LayerMask _whatIsGround; //Что является землёй

        private bool IsGrounded; //Находиться ли герой на земле
        private bool IsForward = true; //Смотрит ли герой вперёд
        private float _jumpForce = 17; //Максимальная сила прыжка героя
        private float _curSpeed; //Текущая скорость
        private readonly float _grountDis = 0.1f;
        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        
        public void Update()
        {
            _curSpeed = Input.GetAxisRaw("Horizontal");

            if (!UIMenu.GameIsPause)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    JumpPhysics();
                    if (IsGrounded)
                    {
                        AudioManager.Source.clip = AudioManager.Sounds[0];
                        AudioManager.Source.Play();
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            if (!UIMenu.GameIsPause)
            {
                Run(_curSpeed);
                Flip(_curSpeed);
            }
            //RunPhysics(curSpeed);
            PlayerPosition();
            CheckGround();
        }

        #region Методы

        /// <summary>
        /// Метод бега
        /// </summary>
        /// <param name="curSpeed"></param>
        private void Run(float curSpeed)
        {
            if (curSpeed != 0) transform.position += Vector3.right * curSpeed * _maxSpeed * Time.deltaTime;
        }

        /// <summary>
        /// Метод бега основанный на физике
        /// </summary>
        /// <param name="curSpeed">Текущая скорость</param>
        //private void RunPhysics(float curSpeed)
        //{
        //    if (Mathf.Abs(_rb.velocity.x) < _maxSpeed)
        //    {
        //        _rb.AddForce(Vector2.right * curSpeed, ForceMode2D.Impulse);
        //    }
        //    else if (Mathf.Abs(_rb.velocity.x) > _maxSpeed)
        //    {
        //        var currSpeed = _rb.velocity;
        //        currSpeed.x = Mathf.Sign(_rb.velocity.x) * _maxSpeed;
        //        _rb.velocity = currSpeed;
        //    }
        //    else if (curSpeed == 0) _rb.velocity = Vector2.zero;
        //}

        /// <summary>
        /// Метод поворота
        /// </summary>
        /// <param name="curSpeed">Текущая скорость</param>
        private void Flip(float curSpeed)
        {
            if ((curSpeed > 0 && !IsForward) || (curSpeed < 0 && IsForward))
            {
                IsForward = !IsForward;
                Vector3 curRot = transform.rotation.eulerAngles;
                curRot.y += 180f;
                transform.rotation = Quaternion.Euler(curRot);
            }
        }
        
        /// <summary>
        /// Метод прыжка основанный на физике
        /// </summary>
        private void JumpPhysics()
        {
            if (IsGrounded) _rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
        }

        /// <summary>
        /// Метод проверяющий находится ли герой на земле
        /// </summary>
        private void CheckGround()
        {
            RaycastHit2D hit = Physics2D.Raycast(_groundCheck.position, Vector2.down, _grountDis, _whatIsGround);

            if (hit != false) IsGrounded = true;
            else IsGrounded = false;
        }
    
        private void PlayerPosition()
        {
            var playerPos = transform.position;
            if (playerPos.x < -9.5) playerPos.x = 9.5f;
            if (playerPos.x > 9.5) playerPos.x = -9.5f;
            transform.position = playerPos;
        }

        #endregion

    }
}
