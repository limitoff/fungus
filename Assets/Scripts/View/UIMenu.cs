using UnityEngine;

namespace FungusLibrary
{
    public class UIMenu : MonoBehaviour
    {
        [HideInInspector] public bool GameIsPause; //Стоит ли игра на паузе

        public GameObject PauseMenuUI;
        public GameObject WindowTryUI;
        public GameObject PanelTheEnd;
        public AudioManager AudioManager;

        public bool _isQuit;

        private void Start()
        {
            GameIsPause = true;
            _isQuit = false;
        }

        void Update()
        {
            if (!_isQuit)
            {
                if (Input.GetKeyDown(KeyCode.Escape)) //Если нажата кнопка Escape
                {
                    if (GameIsPause) Play(); //Если игра на паузе, то продолжить игру
                    else Pause(); //Иначе - поставаить игру на паузу
                }
            }
        }

        #region Методы

        /// <summary>
        /// Метод - Поставить игру на паузу
        /// </summary>
        private void Pause()
        {
            PauseMenuUI.SetActive(true);
            WindowTryUI.SetActive(false);
            Time.timeScale = 0f;
            GameIsPause = true;
        }
        
        /// <summary>
        /// Метод главного меню - Начать игру
        /// </summary>
        public void Play()
        {
            PauseMenuUI.SetActive(false);
            WindowTryUI.SetActive(true);
            Time.timeScale = 1f;
            GameIsPause = false;
        }

        /// <summary>
        /// Метод выхода из игры
        /// </summary>
        public void Quit()
        {
            _isQuit = true;
            PauseMenuUI.SetActive(false);
            PanelTheEnd.SetActive(true);
            AudioManager.Source.clip = AudioManager.Sounds[2];
            AudioManager.Source.Play();
            Time.timeScale = 1f;
            Invoke("TheEnd", 3);
        }

        public void TheEnd()
        {
            Application.Quit();
            //Debug.Log("TheEnd");
        }

        #endregion
    }
}
