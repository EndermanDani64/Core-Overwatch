using UnityEngine;

public class ReactorManager : MonoBehaviour
{
    private bool _online = false;
    private float _temp = 20f;
    private float _pressure = 2f;
    //private float _coolantSupply;

    /// <summary>
    /// Read only.
    /// </summary>
    public static bool IsOnline
    {
        get => _online;
    }
    public static float Temp
    {
        get => _temp;
        set
        {
            if (value >= ValueStorage.REACTOR_TMP_MELTINGPOINT)
            {
                MeltingPointReached?.Invoke();
            }

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
    public static float Pressure
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

    public event System.Action ReactorStart;
    public event System.Action MeltingPointReached;

    /// <summary>
    /// Sets the IsOnline property to true, and starts calling (2s) the TempController.TemperatureLoop().
    /// </summary>
    public void StartReactor()
    {
        _online = true;
        SetValuesToDefault();

        // calling continously 
        InvokeRepeating(nameof(CallTempLoop), 0f, 2f);
        InvokeRepeating(nameof(CallPressureLoop), 0f, 2.2f);
    }

    // submethods //

    private void SetValuesToDefault()
    {
        _temp = ValueStorage.REACTOR_TMP_DEFAULT;
        _pressure = ValueStorage.REACTOR_PS_DEFAULT;
    }

    private void CallTempLoop()
    {
        tempController.TemperatureLoop();
    }

    private void CallPressureLoop()
    {
        if (!_online) return;   
        tempController.TemperatureLoop();
    }

    // references //

    [SerializeField] TempController tempController;
    [SerializeField] PressureControl pressureController;
}

