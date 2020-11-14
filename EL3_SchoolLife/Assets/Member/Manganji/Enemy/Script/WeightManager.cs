using nInpactManager;
using nScoreManager;
using System;
using UnityEngine;


namespace nWeightManager
{
    public class WeightManager : MonoBehaviour
    {
        private float weight;
        [SerializeField] private float bottomInpactForce = default;
        [SerializeField] private float coefficientInpact = default;
        [SerializeField] private float score = default;
        [SerializeField] private GameObject player = default;
        [SerializeField] private new Collider collider = default;
        private InpactManager inpactManager;
        [SerializeField] private string[] exclusionTags = default;
        private float inpactForce;
        public bool inpactFlg { get; private set; } = false;
        private Vector3 toVec = default;
        private Vector3 rollVec = default;
        private new Renderer renderer = default;


        // Start is called before the first frame update
        void Start()
        {
            weight = 10.0f;
            inpactForce = 0.0f;
            inpactManager = player.GetComponent<InpactManager>();
            renderer = this.GetComponent<Renderer>();
        }

        // Update is called once per frame
        void Update()
        {
            RollObject();

            if (this.transform.position.y <  -5.0f)
                Destroy(this.gameObject);
        }

        private void OnBecameInvisible()
        {
            Destroy(this.gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            //特定のタグを無視する
            foreach (var tag in exclusionTags)
            {
                if (collision.collider.tag == tag)
                    return;
            }

            //吹き飛ばし処理
            InpacetThisObject();
        }


        void InpacetThisObject()
        {
            //プレイヤーから衝撃力を取得
            //衝撃力が一定以上なら設定した衝撃力を出す
            inpactForce = inpactManager.inpacet;

            if (inpactForce < bottomInpactForce)
                inpactForce = 15.0f;
            else
            {
                inpactForce = coefficientInpact;
                ScoreManager.AddScore(score);   //ついでにスコア加算
            }

            
            //吹っ飛ばす力と重さから最終的な衝撃を出す(bottomForceの値分は保証する)
            const float bottomForce = 2.5f;
            Func<float, float> CalcInpact = (inpactForce) =>
            {
                float force = inpactForce - weight;
                if (force < bottomForce)
                    force = bottomForce;
                return force;
            };
            inpactForce = CalcInpact(inpactForce);

            //吹き飛ばす方向を設定
            toVec = rollVec = GetAngleVec(player, this.gameObject);

            toVec.x += Mathf.Sign(toVec.x) * UnityEngine.Random.Range(0.35f, 0.65f);
            toVec.y += 0.8f;
            toVec.z += 0.5f;

            //設定した方向に吹き飛ばす
            this.GetComponent<Rigidbody>().AddForce(toVec * inpactForce, ForceMode.Impulse);
            

            inpactFlg = true;
        }


        private void RollObject()
        {
            if (!inpactFlg)
                return;

            //回転方向の設定
            float rollSpd = 10.0f;
            if (inpactManager.inpacet < bottomInpactForce)
                rollSpd = 1.0f;

            transform.Rotate(new Vector3(0.0f, 0.0f, rollSpd));

            if (!collider.isTrigger)
                collider.isTrigger = true;
        }


        Vector3 GetAngleVec(GameObject _from, GameObject _to)
        {
            //高さの概念を入れないベクトルを作る
            Vector3 fromVec = new Vector3(_from.transform.position.x, 0, _from.transform.position.z);
            Vector3 toVec = new Vector3(_to.transform.position.x, 0, _to.transform.position.z);

            return Vector3.Normalize(toVec - fromVec);
        }


    }


}
