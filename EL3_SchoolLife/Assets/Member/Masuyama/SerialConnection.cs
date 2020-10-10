// Unityのシステム使用
using UnityEngine;
using UnityEngine.UI;

// Singleton
using TeamProject.System;

//　ポート通信等に使用
using System.IO;
using System.IO.Ports;
using System.Threading;

public class SerialConnection : MonoBehaviour
{
    // Findが面倒なのでTextをそのままアタッチ
    [SerializeField]
    private Text comText;
    [SerializeField]
    private Text com;

    [SerializeField]
    private Text portText;
    [SerializeField]
    private Text port;

    [SerializeField]
    private Button button;

    private string portName = "COM3";
    private int baudRate = 9600;

    private void Start()
    {
        comText.text = portName;
        portText.text = baudRate.ToString();

        // 設定
        Serial.Instance.SetPortBaud(portName, baudRate);

        // instanceを生成するために一度ここで呼び出す
        Serial.Instance.SerialOpen();
    }

    private void Update()
    {

    }
    
    public void OnClick()
    {
        portName = com.text;
        baudRate = int.Parse(port.text); 

        // 設定
        Serial.Instance.SetPortBaud(portName, baudRate);

        // instanceを生成するために一度ここで呼び出す
        Serial.Instance.SerialOpen();
    }
}

public class Serial : Singleton<Serial>
{
    private string portName;
    private int baudRate;

    // 接続ができていない場合外部からポートの設定を行う
    public string PortName { set { if (!isRunning_) portName = value; } }
    // 接続ができていない場合外部からバウドの設定を行う
    public int BaudRate { set { if (!isRunning_) baudRate = value; } }

    private SerialPort serialPort_;
    private Thread thread_;
    private bool isRunning_ = false;

    private string message_;
    private bool isNewMessageReceived_ = false;

    private bool StopAlarm = false;
    private byte WakingLevel;


    public Serial()
    {
    }

    public void SerialOpen()
    {
        // ポートの生成とThreadの生成
        Debug.Log("Start");

        if (isRunning_) return;

        // ポートの開示
        var isOpen = Open();

        // ポート開示ができていればThreadの開示
        if (isOpen) ThreadOpen();
    }

    public void SetPortBaud(string _port, int _baud)
    {
        if (isRunning_) return;

        portName = _port;
        baudRate = _baud;
    }

    public void Update()
    {
        Debug.LogWarning("Serial-Update");
        //if (isNewMessageReceived_) {
        //    OnDataReceived(message_);
        //}
        if (isRunning_ && serialPort_ != null && serialPort_.IsOpen)
        {
            Debug.LogWarning("ReadOK");
            int Mes = serialPort_.ReadByte();

            if (Mes == 1)
            {
                StopAlarm = true;
            }
        }
    }

    public void SetWakingLevel(int val)
    {
        if (isRunning_ && serialPort_ != null && serialPort_.IsOpen)
        {
            Debug.LogWarning("WriteOK");
            byte[] buff = new byte[1];
            buff[0] = (byte)val;
            serialPort_.Write(buff, 0, 1);
        }
    }

    public bool GetStopAlarm()
    {
        return StopAlarm;
    }

    public void SetStopAlarm(bool val)
    {
        StopAlarm = val;
    }

    public void OnDestroy()
    {
        Debug.LogWarning("OnDestroy");
        Close();
    }

    private bool Open()
    {
        Debug.Log("エラー前");

        // ポートの生成
        serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);

        bool isOk = true;

        // 念のためポートが既にオープンされていないことを確認
        try
        {
            if (!serialPort_.IsOpen) serialPort_.Open();
            isOk = true;
        }
        catch(IOException e)
        {
            Debug.LogWarning("Comかポート番号が正常ではありません。再接続をお願いします。");
            isOk = false;
        }
        
        isRunning_ = isOk;

        return isOk;
    }

    private void ThreadOpen()
    {
        // リードThreadの起動
        thread_ = new Thread(Read);
        thread_.Start();
    }

    private void Read()
    {
        Debug.LogWarning("ReadPrev");
        while (isRunning_ && serialPort_ != null && serialPort_.IsOpen)
        {
            Debug.LogWarning("ReadOK");
            try
            {
                message_ = serialPort_.ReadLine();
                //                Debug.LogWarning(message_);
                isNewMessageReceived_ = true;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }

    private void Close()
    {
        isRunning_ = false;

        if (thread_ != null && thread_.IsAlive)
        {
            thread_.Join();
        }

        if (serialPort_ != null && serialPort_.IsOpen)
        {
            serialPort_.Close();
            serialPort_.Dispose();
        }
    }

    void OnDataReceived(string message)
    {
        Debug.LogWarning("OnDataReceived1");
        var data = message.Split(
                new string[] { "\t" }, System.StringSplitOptions.None);
        if (data.Length < 2) return;

        try
        {
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}
