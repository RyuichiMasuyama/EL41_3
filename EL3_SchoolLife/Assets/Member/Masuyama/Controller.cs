using UnityEngine;

public class Controller : MonoBehaviour
{
    public enum Shoulder : int 
    {
        Right,
        Left,
        Max,
    }
    
    private SerialConnection serial;

    private float[] data;
    public   float[] Data { get { return data; } }

    [SerializeField]
    private string com = "COM3";

    [SerializeField]
    private int port = 9600;

    // Start is called before the first frame update
    void Start()
    {
        // コントローラに必要なので追加
        serial = gameObject.AddComponent<SerialConnection>();
        serial.SetSize(sizeof(float) * (int)Shoulder.Max);
        data  = new float[(int)Shoulder.Max];
        DontDestroyOnLoad(gameObject);
    }

    public void ReConection()
    {
        serial.PortName = com;
        serial.BaudRate = port;

        serial.OnClick();
    }

    // Update is called once per frame
    void Update()
    {
        var dataStr = serial.GetData();
        if (dataStr == null) return;
        var aaaa = dataStr.Split('a');
        Debug.Log(aaaa);

        data[0] = float.Parse(aaaa[0]);
        data[1] = float.Parse(aaaa[1]);
    }
}
