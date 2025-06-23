using UnityEngine;

public class Global : MonoBehaviour
{
    private static Global _instance = default;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
}
