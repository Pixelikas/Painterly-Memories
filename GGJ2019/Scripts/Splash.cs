using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2019
{
    public class Splash : MonoBehaviour
    {
        public ScreenFader fader;

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                fader.GoToScene("intro");
            }
        }
    }
}