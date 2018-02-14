using System;
using UnityEditor;

public abstract class ExtendedWindow : EditorWindow
{
    public event Action PreviewDrew;
    public event Action Drew;

    protected UIElement root;
    private bool initialized = false;

    public static T OpenWindow<T>(string title, bool utility = false) where T : EditorWindow
    {
        // Get existing open window or if none, make a new one:
        T window = GetWindow<T>(utility, title, true);
        window.autoRepaintOnSceneChange = true;
        window.Show();
        return window;
    }

    private void OnEnable()
    {
        initialized = false;
    }

    private void OnDisable()
    {
        Persist();
    }

    private void OnGUI()
    {
        if (!initialized)
        {
            Start();
        }

        PreviewDrew?.Invoke();
        root.Draw();
        Drew?.Invoke();
    }

    private void Start()
    {
        if (!initialized)
        {
            Initialize();
            initialized = true;
            Restore();
        }
    }

    protected virtual void Initialize() { }
    protected virtual void Restore() { }
    protected virtual void Persist() { }
}