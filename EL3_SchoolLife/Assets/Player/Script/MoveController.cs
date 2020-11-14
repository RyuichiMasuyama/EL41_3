using UnityEngine;

namespace nMoveController
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private float moveSpd = default;
        [SerializeField] private float x_sensi = default;
        [SerializeField] private float y_sensi = default;
        private float x_Rotation;
        private float y_Rotation;
        public new GameObject camera;
        private Vector3 headAngle;
        [SerializeField] private float limitXAxisAngle = default;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Move();
            CameraRot();
        }

        private void Move()
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") * moveSpd;
            transform.position += transform.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal") * moveSpd;
        }

        private void CameraRot()
        {
            y_Rotation = Input.GetAxis("Mouse X") * y_sensi;
            x_Rotation = Input.GetAxis("Mouse Y") * (-x_sensi);

            //横方向はキャラを回転させて合わせる
            transform.Rotate(0, y_Rotation, 0);

            //縦方向はカメラを回転させる
            var x = headAngle.x + x_Rotation;
            if (x >= -limitXAxisAngle && x <= limitXAxisAngle)
            {
                headAngle.x = x;
                camera.transform.Rotate(x_Rotation, 0, 0);
            }
        }
    }


}
