using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupInfo
{
    private Dictionary<string, object> dict;

    public PopupInfo()
    {
        this.dict = new Dictionary<string, object>();
    }

    public void Add(string key, object obj)
    {
        if (!this.dict.ContainsKey(key))
        {
            this.dict.Add(key, obj);
        }
        else
        {
            throw new System.Exception("PopupInfo already contains an entry with the key: " + key);
        }
    }

    public void Replace(string key, object obj)
    {
        if (this.dict.ContainsKey(key))
        {
            this.dict[key] = obj;
        }
        else
        {
            throw new System.Exception("PopupInfo does not contain an entry with the key: " + key);
        }
    }

    public bool KeyExists(string key)
    {
        return this.dict.ContainsKey(key);
    }

    public T TryGet<T>(string key)
    {
        return this.Get<T>(key, false);
    }

    public T Get<T>(string key)
    {
        return this.Get<T>(key, true);
    }

    private T Get<T>(string key, bool assert)
    {
        if (this.dict.ContainsKey(key))
            return (T)this.dict[key];
        else
        {
            if (assert)
                throw new System.Exception("PopupInfo does not contain an entry for key: " + key);

            return default(T);
        }
    }
}
public class BasePopup : MonoBehaviour
{
    public string popupName;
    public bool isDestroyWhenPop;
    public bool hideOnFocusLost = false;

    [HideInInspector]
    public bool hasBeenSetup;

    // Update is called once per frame
    void Update() { }


    public static BasePopup Create(BasePopup popup, bool deactivate = false)
    {
        GameObject obj = Instantiate(popup.gameObject);
        BasePopup newPopup = obj.GetComponent<BasePopup>();

        //Canvas sceenCanvas = newPopup.gameObject.GetComponent<Canvas>();
        //if (sceenCanvas != null && UICameraManager.Instance != null)
        //{
        //    sceenCanvas.worldCamera = UICameraManager.Instance.HomeUICamera;
        //    if (sceenCanvas.worldCamera)
        //    {
        //        if (sceenCanvas.gameObject.layer != UICameraManager.Instance.HomeUICamera.gameObject.layer)
        //        {
        //            sceenCanvas.gameObject.ChangeLayer(UICameraManager.Instance.HomeUICamera.gameObject.layer);
        //        }
        //    }
        //    sceenCanvas.sortingLayerName = "UI";
        //}

        if (!deactivate)
        {
            newPopup.Setup();
            newPopup.hasBeenSetup = true;
        }
        else
        {
            newPopup.hasBeenSetup = false;
        }

        return newPopup;
    }

    /// <summary>
    /// Use this to run inital setup on the screen. This is only ever called once when the screen is created.
    /// If the screen was cached and is being reactivated, this is not called again... use PushRoutine
    /// to setup cached screens if that is necessary.
    /// </summary>
    virtual public void Setup()
    {
        //Call only once
    }

    /// <summary>
    /// This is called after the UIManager activates or creates the screen during the pushing process.
    /// Note that this is called AFTER Setup during the instantiation process.
    /// Use this to run anything like animations or other scripted sequences.
    /// </summary>
    virtual public IEnumerator PushRoutine(PopupInfo info)
    {
        yield return null;
    }

    /// <summary>
    /// This is called just before the UIManager deactivates or destroys the screen during the popping process.
    /// Use this to run anything like animations or other scripted sequences before the UIManager deals with it.
    /// </summary>
    virtual public IEnumerator PopRoutine()
    {
        yield return null;
    }

    virtual public void CloseScreen()
    {
        UIManager.Instance.HidePopup();
    }

    /// <summary>
    /// This is called when the screen is back on the top of the screen stack.
    /// </summary>
    virtual public void OnFocus()
    {
        //TODO update tween here
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// This is called when the screen is pushed down the screen stack by another screen.
    /// </summary>
    virtual public void OnLostFocus()
    {
        // outro tween is played in UIManager::QueuePush
        // this is called by UIManager::Update > PushEnumerator after outro tween finishes
        if (this.hideOnFocusLost)
        {
            //TODO add tween here later
            Debug.Log($"OnLostFocus 1: {gameObject.name}");
            gameObject.SetActive(false);
        }
    }

    public void Close()
    {
        //AudioManager.OnClickSound();
        UIManager.Instance.HidePopup();
    }
}
