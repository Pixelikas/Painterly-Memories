using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2019
{
    public class Cutscene : MonoBehaviour
    {
        public float duration;
        public string scene;
        public ScreenFader fader;

        private void Start()
        {
            Invoke("ChangeScreen", duration);
        }

        private void ChangeScreen()
        {
            fader.GoToScene(scene);
        }
    }
}