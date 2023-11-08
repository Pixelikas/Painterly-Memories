using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2019.Bird
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;
        public ScreenFader fader;
        public int birdCount;

        private int birdFeeded = 0;

        [SerializeField]
        private bool[] occupiedSpawns;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            SoundManager.PlaySound("theme", true, .5f);
        }

        public void FeedBird()
        {
            birdFeeded++;
            if (birdFeeded >= birdCount)
                Invoke("Success", 1f);
        }

        public void Success()
        {
            if (PlayerPrefs.GetInt("STAGE") < 3)
                PlayerPrefs.SetInt("STAGE", 3);

            SoundManager.PlaySound("success");
            fader.Color = Color.white;
            StartCoroutine(WaitForTransition());
        }

        public void GameOver()
        {
            SoundManager.PlaySound("failure");
            fader.Color = Color.black;
            StartCoroutine(WaitForTransition());
        }

        private IEnumerator WaitForTransition()
        {
            yield return fader.GoToMenu();
        }

        public void OccupyPos(int pos)
        {
            occupiedSpawns[pos] = true;
        }

        public void FreePos(int pos)
        {
            occupiedSpawns[pos] = false;
        }

        public bool CheckPos(int pos)
        {
            return occupiedSpawns[pos];
        }
    }
}