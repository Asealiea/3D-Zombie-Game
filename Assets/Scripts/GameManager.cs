using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager:: Instance is null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        _invertedV = false;
        _invertedH = false;

        #region Don't Destroy on load
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion
    }
    #endregion


    public bool _invertedH { get; set; }
    public bool _invertedV { get; set; }

    public bool Controller { get; set; }


}
