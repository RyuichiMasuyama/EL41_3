using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : SingletonMonoBehaviour<GameEnd>
{
    // Update is called once per frame
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("sfewf");
            SceneManager.LoadScene("MasuyamaDebug");
            // Quit();
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