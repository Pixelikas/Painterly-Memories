using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2019.Bike
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;
        public ScreenFader fader;

        private bool transitioning = false;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            SoundManager.PlaySound("theme", true, .5f);
        }

        public void Success()
        {
            if (transitioning)
                return;

            if (PlayerPrefs.GetInt("STAGE") < 1)
                PlayerPrefs.SetInt("STAGE", 1);

            SoundManager.PlaySound("success");

            transitioning = true;

            fader.Color = Color.white;
            StartCoroutine(WaitForTransition());
        }

        public void GameOver()
        {
            if (transitioning)
                return;

            SoundManager.PlaySound("failure");

            transitioning = true;

            fader.Color = Color.black;
            StartCoroutine(WaitForTransition());
        }

        private IEnumerator WaitForTransition()
        {
            yield return fader.GoToMenu();
        }
    }
}