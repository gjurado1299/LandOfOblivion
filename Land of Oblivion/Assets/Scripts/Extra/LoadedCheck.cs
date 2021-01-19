using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadedCheck : MonoBehaviour
{
    public bool loaded = false;
    public bool died = false;
    public bool skipDialogue = false;

    #region Singleton
    public static LoadedCheck instance;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

}
