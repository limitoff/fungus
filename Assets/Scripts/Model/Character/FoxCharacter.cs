using UnityEngine;
using UnityEngine.UI;

namespace FungusLibrary
{
    public class FoxCharacter : MonoBehaviour
    {
        public GameObject Message;
        public Text MessageText;
        public Text CountOfTryText;
        public AudioManager AudioManager;
        
        [SerializeField] private Transform[] _moveSpots;
        private Animator _animator;
        private string[] _message;
        private int _currentSpot;
        private int _randomSpot;
        private int _countOfTry;

        public int CountOfTry
        {
            get
            {
                return _countOfTry;
            }
        }

        private void Start()
        {
            _countOfTry = 0;
            _randomSpot = 0;
            _currentSpot = _randomSpot;
            transform.position = _moveSpots[_randomSpot].position;
            _animator = GetComponent<Animator>();
            _message = new string[5];
            _message[0] = "Эй, это моя добыча!";
            _message[1] = "Ищи себе другого водопроводчика!";
            _message[2] = "Руки убрал! ...или что у тебя там";
            _message[3] = "Неплохая попытка (плохая)";
            _message[4] = "Ты не стараешься..";

            CountOfTryText.text = "Бессмысленных попыток отобрать водопроводчика: " + _countOfTry;
        }

        /// <summary>
        /// Метод коллизии Лисы с Грибком
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                while (_randomSpot == _currentSpot)
                {
                    _randomSpot = Random.Range(0, _moveSpots.Length);
                }

                int randomNumber = Random.Range(0, _message.Length);

                if(_randomSpot != _currentSpot)
                {
                    AudioManager.Source.clip = AudioManager.Sounds[1];
                    AudioManager.Source.Play();

                    TurnOffAction();
                    CancelInvoke("TurnOffAction");

                    _countOfTry++;
                    if (_countOfTry == 100)
                    {
                        _countOfTry = 0;
                    }
                    CountOfTryText.text = "Бессмысленных попыток отобрать водопроводчика: " + _countOfTry;
                    
                    if (randomNumber == 1)
                    {
                        ShowMessage();
                        Invoke("TurnOffAction", 3);
                    }
                    else if (randomNumber == 2)
                    {
                        Laughter();
                        Invoke("TurnOffAction", 3);
                    }
                
                    transform.position = _moveSpots[_randomSpot].position;
                }

                _currentSpot = _randomSpot;
            }
        }

        /// <summary>
        /// Метод вызова облочка диалога
        /// </summary>
        public void ShowMessage()
        {
            int random = Random.Range(0, 5);
            MessageText.text = _message[random];
            Message.SetActive(true);
        }

        /// <summary>
        /// Метод вызова смеха
        /// </summary>
        public void Laughter()
        {
            _animator.Play("Fox Animation");
            AudioManager.Source.clip = AudioManager.Sounds[0];
            AudioManager.Source.Play();
        }

        /// <summary>
        /// Метод выключения всех действий
        /// </summary>
        public void TurnOffAction()
        {
            _animator.Play("Fox Stands");
            Message.SetActive(false);
        }
    }
}
