using nWeightManager;
using UnityEngine;

namespace nBillbord
{

    public class Billbord : MonoBehaviour
    {
        //[SerializeField] private GameObject targetCam = default;
        private WeightManager weightManager;
        

        // Start is called before the first frame update
        void Start()
        {
            weightManager = gameObject.GetComponent<WeightManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!weightManager.inpactFlg)
                BillboardToCamera();
        }

        private void BillboardToCamera()
        {
            Vector3 p = Camera.main.transform.position;
            p.y = transform.position.y;
            transform.LookAt(p);
        }

    }

}
