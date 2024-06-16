using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class QueuedPopup
{
    public BasePopup popup;
    public PopupInfo info;
    public bool popIt;

    public QueuedPopup(BasePopup popup, PopupInfo info, bool popIt)
    {
        this.popup = popup;
        this.info = info;
        this.popIt = popIt;
    }
}


public class UIManager : Singleton<UIManager>
{
    protected List<BasePopup> pseudoStack = new List<BasePopup>();
    protected Queue<QueuedPopup> queue = new Queue<QueuedPopup>();
    protected Dictionary<System.Type, BasePopup> cache = new Dictionary<Type, BasePopup>();

    private string currentScreen;

    GameObject canvasObject;

    [HideInInspector]
    public bool isScreenBusy = false;
    public const int DEFAULT_POPUP_ORDER = 10;
    public int popupOrder = DEFAULT_POPUP_ORDER;

    public string CurrentScreen { get => currentScreen; set => currentScreen = value; }

    /// <summary>
    /// Returns the top most screen on the screen stack. Will return null if the screen stack is empty.
    /// </summary>
    public BasePopup TopPopup()
    {
        if (this.pseudoStack != null && this.pseudoStack.Count > 0)
        {
            return this.pseudoStack[0];
        }
        else
        {
            return null;
        }
    }

    public BasePopup PendingTopScreen()
    {
        if (this.pseudoStack.Count > 1)
        {
            return this.pseudoStack[1];
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Returns the second top most screen on the screen stack. Will return null if the screen stack is empty.
    /// </summary>
    public BasePopup UnderTopScreen()
    {
        if (this.pseudoStack.Count > 1)
        {
            return this.pseudoStack[1];
        }
        else
        {
            return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Push next screen in queue if there is no screen that is busy
        if (!isScreenBusy && queue.Count > 0)
        {
            QueuedPopup queuedPopup = queue.Dequeue();
            if (queuedPopup != null)
            {
                if (queuedPopup.popIt)
                {
                    StartCoroutine(PopEnumerator(queuedPopup.popup));
                }
                else
                {
                    StartCoroutine(PushEnumerator(queuedPopup));
                }
            }
        }
    }

    protected IEnumerator PushEnumerator(QueuedPopup queuedPopup)
    {
        this.isScreenBusy = true;

        System.Type screenType = queuedPopup.popup.GetType();
        bool isCached = this.cache.ContainsKey(screenType);

        BasePopup topScreen = this.TopPopup();
        BasePopup popup = null;

        // Figure out if the screen is already on top of the stack or not.
        bool alreadyOnTop;
        if (topScreen != null && topScreen.GetType() == screenType)
        {
            alreadyOnTop = true;
        }
        else
        {
            alreadyOnTop = false;
        }

        if (isCached)
        {
            // Turn cached screen back on.
            popup = this.cache[screenType];

            // prevent multiple intro tween when screen is pushed multiple times
            if (!alreadyOnTop)
            {
                popup.gameObject.SetActive(true);

                // refresh screen content when bringing it back from cache, since it could be outdated
                popup.OnFocus();
            }
        }
        else
        {
            popup = BasePopup.Create(queuedPopup.popup);
            popup.transform.SetParent(canvasObject.transform);
            popup.transform.localScale = Vector3.one;
            popup.transform.position = Vector3.zero;
            if (popup.gameObject.GetComponent<Canvas>())
            {
                RectTransform rect = popup.gameObject.GetComponent<RectTransform>();
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.sizeDelta = Vector2.zero;
                rect.anchoredPosition = Vector2.zero;
                //rect.sizeDelta = new Vector2(canvasObject.GetComponent<RectTransform>().rect.width, canvasObject.GetComponent<RectTransform>().rect.height);

                rect.pivot = new Vector2(0.5f, 0.5f);

            }
            else
            {
                RectTransform rect = popup.gameObject.GetComponent<RectTransform>();
                rect.sizeDelta = Vector2.zero;
                rect.anchoredPosition = Vector2.zero;
                Debug.Log($"rect.sizeDelta {rect.sizeDelta}");
            }
            this.cache.Add(screenType, popup);
        }

        // Push the new screen on to the psuedo stack.
        if (!alreadyOnTop)
        {
            if (isCached)
            {
                this.pseudoStack.Remove(popup);
            }

            this.pseudoStack.Insert(0, popup);
        }
        if (popup.gameObject.GetComponent<Canvas>())
        {
            popup.gameObject.GetComponent<Canvas>().sortingLayerName = "UI";
            popup.gameObject.GetComponent<Canvas>().sortingOrder = popupOrder;
            popupOrder++;
            if (popupOrder == int.MaxValue) popupOrder = DEFAULT_POPUP_ORDER;
        }

        // Wait for screen's PushRoutine to return...
        yield return StartCoroutine(popup.PushRoutine(queuedPopup.info));

        this.isScreenBusy = false;

        yield return null;
    }

    protected IEnumerator PopEnumerator(BasePopup popup)
    {
        this.isScreenBusy = true;

        // Wait for the screen's PopRoutine to return...
        yield return StartCoroutine(popup.PopRoutine());

        if (popup.isDestroyWhenPop)
        {
            this.cache.Remove(popup.GetType());

            //In some cases, the gameObject has already been destroyed by this point.  So we'll double-check and make sure the object still exista before trying to destroy it.
            if ((popup != null) && (popup.gameObject != null))
            {
                Object.Destroy(popup.gameObject);
            }
        }
        else
        {
            popup.gameObject.SetActive(false);
        }

        // NOTE: The above command to Remove from the top is not correct, as there is a chance
        //  another screen could be added to the pseudoStack, from the time the 'then' top most screen was marked to be popped, to 'now', when it is actually popped
        // 'screen' variable is correctly the right screen that needs to be popped, so use that to find it in the pseudoStack.
        this.pseudoStack.Remove(popup);

        // Tell the screen on top of the stack that it has gained focus again.
        BasePopup topScreen = this.TopPopup();
        if (topScreen != null)
        {
            topScreen.OnFocus();
        }

        this.isScreenBusy = false;
        popupOrder--;
    }

    public List<BasePopup> GetUIScreens()
    {
        return this.pseudoStack;
    }

    public void ShowPopup(string name, PopupInfo info = null)
    {
        if (string.IsNullOrEmpty(name))
        {
            return;
        }
        //if (this.pseudoStack.Count > 0)
        //    HidePopup();
        Debug.Log($"ShowPopup: {name}");

        GameObject obj = ResourceManager.Instance.Load<GameObject>(name);
        if (obj == null)
        {
            Debug.LogError("UIManager - ShowPopup - obj null");
            return;
        }

        BasePopup popup = obj.GetComponent<BasePopup>();
        if (popup == null)
        {
            Debug.LogError("UIManager - ShowPopup - screen null");
            return;
        }

        QueuedPopup QueuedPopup = new QueuedPopup(popup, info, false);

        this.queue.Enqueue(QueuedPopup);
        currentScreen = name;
    }

    /// <summary>
    /// Pops the top most screen from the screen stack and adds it to the screen action queue.
    /// </summary>
    public BasePopup HidePopup()
    {
        BasePopup popup = this.GetNextUnpoppedPopup();

        if (popup == null)
        {
            return null;
        }

        this.queue.Enqueue(new QueuedPopup(popup, null, true));
        currentScreen = "";
        return popup;
    }

    /// <summary>
    /// Will continue to pop screens until a popped screen matches the specified type.
    /// </summary>
    public BasePopup HidePopup(System.Type type)
    {
        if (!this.cache.ContainsKey(type))
        {
            return null;
        }

        BasePopup screen = HidePopup();
        while (screen.GetType() != type)
        {
            screen = HidePopup();
        }

        return screen;
    }

    /// <summary>
    /// Will continue to pop screens until the screen on top of the stack matches the specified type.
    /// </summary>
    public void HidePopupToScreen(System.Type type)
    {
        if (!this.cache.ContainsKey(type))
        {
            return;
        }

        var screen = this.GetNextUnpoppedPopup();
        while (screen != null && screen.GetType() != type)
        {
            this.HidePopup();
            screen = this.GetNextUnpoppedPopup();
        }
    }

    public BasePopup HidePopupByName(string popName)
    {
        BasePopup screen = null;
        for (int i = 0; i < pseudoStack.Count; i++)
        {
            if (!string.IsNullOrEmpty(pseudoStack[i].name) && pseudoStack[i].name.Contains(popName))
            {
                screen = pseudoStack[i];
                continue;
            }
        }

        if (screen == null)
        {
            return null;
        }

        this.queue.Enqueue(new QueuedPopup(screen, null, true));

        return screen;
    }

    /// <summary>
    /// Since we push and pop lazily. We need to count how many screens are currently in the queue to be popped.
    /// </summary>

    protected BasePopup GetNextUnpoppedPopup()
    {
        int lazyIndex = 0;
        foreach (QueuedPopup popup in this.queue)
        {
            if (popup.popIt)
                lazyIndex++;
        }

        if (lazyIndex >= this.pseudoStack.Count)
            return null;

        return this.pseudoStack[lazyIndex];
    }

    public T GetScreen<T>() where T : BasePopup
    {
        System.Type type = typeof(T);

        if (this.cache.ContainsKey(type))
        {
            return this.cache[type] as T;
        }

        return null;
    }

    //Move it to ToastManager
    //public void ShowToastMessage(string message) {
    //  ScreenInfo info = new ScreenInfo();
    //  info.Add("Message", message);
    //    ShowPopup("AssetBundles/GameScene/Prefabs/windows/toast/ToastMessage", info);
    //}

    /// <summary>
    /// canvas contains uis
    /// </summary>
    public void SetCanvas(GameObject canvas)
    {
        this.canvasObject = canvas;
        this.cache.Clear();
    }

    public GameObject GetCanvas()
    {
        return this.canvasObject;
    }

    public void ClearStack()
    {
        this.pseudoStack.Clear();
    }

    UnityAction sceneLoadedCallback = null;
    bool useTransition = false;

    public void LoadScene(string sceneName, UnityAction callback = null, bool transition = false)
    {
        sceneLoadedCallback = callback;
        useTransition = transition;
        ClearStack();
        StartCoroutine(DoLoadScene(sceneName, useTransition));
    }

    public IEnumerator DoLoadScene(string sceneName, bool useTransition)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        if (useTransition)
        {
            asyncOperation.allowSceneActivation = false;
            //SceneTransition.Instance.ShowTransition();
            //yield return new WaitForSeconds(SceneTransition.Instance.transtiontionTime);
        }
        else
        {
            yield return null;
        }
        asyncOperation.allowSceneActivation = true;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        sceneLoadedCallback?.Invoke();
        //if (!useTransition)
        //{
        //    SceneTransition.Instance?.Hide();
        //}
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
