using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text ScoreCountUI;
    public int score;
    [SerializeField] private Text TimeCountUI;
    public int time;

    private float OneSecondTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        ScoreCountUI.text = score.ToString();
        TimeCountUI.text = time.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreUpdate();
        TimeUpdate();
    }

    private void ScoreUpdate() {
        ScoreCountUI.text = score.ToString();
    }

    private void TimeUpdate() {
        OneSecondTime += Time.deltaTime;
        if(OneSecondTime >= 1.0f) {
            OneSecondTime -= 1.0f;
            time--;
        }
        TimeCountUI.text = time.ToString();
    }
}
