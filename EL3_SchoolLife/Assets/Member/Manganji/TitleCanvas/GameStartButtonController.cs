using UnityEngine;
using UnityEngine.SceneManagement;

namespace nGameStartButtonController
{
    public class GameStartButtonController : MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadScene("EnemyTEST");
        }

    }
}
