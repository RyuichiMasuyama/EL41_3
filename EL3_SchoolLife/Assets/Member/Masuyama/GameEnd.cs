using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;



public class GameEnd : SingletonMonoBehaviour<GameEnd>
{
    // Update is called once per frame
    static void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }

    }

    static private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }
}