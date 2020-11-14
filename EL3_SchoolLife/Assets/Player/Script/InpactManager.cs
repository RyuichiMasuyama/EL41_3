using UnityEngine;


namespace nInpactManager
{
    public class InpactManager : MonoBehaviour
    {
        [SerializeField] public float inpacet;

        Controller controller;

        // Start is called before the first frame update
        void Start()
        {
            controller = GameObject.Find("Controller").GetComponent<Controller>();

        }

        // Update is called once per frame
        void Update()
        {
            inpacet = controller.Data[0] + controller.Data[1];
        }
    }


}