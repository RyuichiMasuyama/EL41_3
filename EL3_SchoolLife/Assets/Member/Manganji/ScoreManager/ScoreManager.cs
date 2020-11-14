using UnityEngine;

namespace nScoreManager
{
    public class ScoreManager : MonoBehaviour
    {
        public static float score { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            score = 0;
        }

        public static void AddScore(float add)
        {
            score += add;
            Debug.Log(score);
        }

    }

}
