using UnityEngine;

namespace nGameExitButtonController
{
    public class GameExitButtonController : MonoBehaviour
    {

        public void OnClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
        }

    }
}
