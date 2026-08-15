using UnityEngine;

public class ReactorManager : MonoBehaviour
{
    //private float _coolantSupply;

    public static ReactorData ReactorData;

    public static event System.Action OnReactorStart;
    public event System.Action MeltingPointReached; // ???

    /// <summary>
    /// Sets the IsOnline property to true, and starts calling (2s) the TempController.TemperatureLoop().
    /// </summary>
    public void StartReactor()
    {
        ReactorData.IsOnline = true;
        SetValuesToDefault();

        /*// Setting every tempController values for the cold reactor start
        tempController.SetToDeafultValues();

        // calling continously the loops
        InvokeRepeating(nameof(_CallTemperatureLoop), 0f, 2f);*/

        OnReactorStart?.Invoke();
    }

    // submethods //

    private void SetValuesToDefault()
    {
        ReactorData.Temperature = ValueStorage.REACTOR_TMP_DEFAULT;
        ReactorData.Pressure = ValueStorage.REACTOR_PS_DEFAULT;
    }

    // built-in //

    private void Start()
    {
        ReactorData = new ReactorData();
    }
}

[System.Serializable]
public class ReactorData
{
    public bool IsOnline;
    public float Temperature
    {
        get => _temp;
        set
        {
            // meltdown is getting checked in TempController

            if (value > ValueStorage.REACTOR_TMP_MIN)
            {
                if (value < ValueStorage.REACTOR_TMP_MAX)
                {
                    _temp = value;
                }
                else
                {
                    _temp = ValueStorage.REACTOR_TMP_MAX - 1;
                }
            }
            else
            {
                _temp = ValueStorage.REACTOR_TMP_MIN + 1;
            }
        }
    }
    public float TemperatureIntensity;
    public float AllControlRodValue; // ?
    public float Pressure
    {
        get => _pressure;
        set
        {
            if (value > ValueStorage.REACTOR_PS_MIN)
            {
                if (value < ValueStorage.REACTOR_PS_MAX)
                {
                    _pressure = value;
                }
                else
                {
                    _pressure = ValueStorage.REACTOR_PS_MAX - 1;
                }
            }
            else
            {
                _pressure = ValueStorage.REACTOR_PS_MIN + 1;
            }
        }
    }
    public float MinimumPressure;
    public bool IsPressurized;
    public string ReactorStatus
    {
        get
        {
            if (_temp > ValueStorage.REACTOR_TMP_MELTINGPOINT || _temp < ValueStorage.REACTOR_TMP_MIN)
            {
                return "error";
            }
            else if (IsOnline)
            {
                return "online";
            }
            else
            {
                return "offline";
            }
        }
        set => ReactorStatus = value;
    }
    public bool IsError; // ?


    private float _temp = 20f;
    private float _pressure = 2f;


    public ReactorData()
    {
        Temperature = _temp;
        Pressure = _pressure;
    }
}