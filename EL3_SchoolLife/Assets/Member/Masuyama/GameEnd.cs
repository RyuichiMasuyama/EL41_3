using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using System.Windows.Forms;



public class GameEnd : SingletonMonoBehaviour<GameEnd>
{
    // Update is called once per frame
    static void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {//メッセージボックスを表示する
            MessageBox.Show("正しい値を入力してください。",
                "エラー",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

    }
}
