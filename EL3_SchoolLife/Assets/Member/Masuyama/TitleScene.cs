using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SerialConnection.Instance.SerialOpen();
    }

    // Update is called once per frame
    void Update()
    {
        // SceneManager.LoadScene("GameSce");
    }
}
