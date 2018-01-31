using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

using static DebugUtils;

public class ContentBundleWindow : ExtendedWindow
{
    public const string ContentBundleDirectoryKey = "Autrage.Omega.AssetBundleDirectory";
    public const string DefaultContentBundleDirectory = "Content";

    private TextField directoryTextField;

    protected override void Initialize()
    {
        StackPanel rootPanel = new StackPanel()
        {
            Orientation = Orientation.Vertical,
            Left = 8,
            Top = 8,
            Right = 8,
            Bottom = 8,
        };

        StackPanel directoryPanel = new StackPanel()
        {
            Orientation = Orientation.Horizontal,
            Top = 4,
        };

        ScrollViewer assetBundleScrollViewer = new ScrollViewer()
        {
            Style = EditorStyles.helpBox,
            Top = 4,
        };

        Button buildAllButton = new Button()
        {
            Text = "Build All",
            Top = 4,
        };

        Label directoryLabel = new Label()
        {
            Text = "Output Directory:",
            Options = new List<GUILayoutOption>() { GUILayout.ExpandWidth(false) }
        };

        Label titleLabel = new Label()
        {
            Text = "Content Bundle Builder"
        };

        directoryTextField = new TextField()
        {
            Options = new List<GUILayoutOption>() { GUILayout.ExpandWidth(true) }
        };

        ListView<string> assetBundleListView = new ListView<string>()
        {
            SelectItemContent = name => new GUIContent(name),
            ItemStyle = new GUIStyle(EditorStyles.boldLabel),
        };

        assetBundleListView.PreviewDrew += () => assetBundleListView.Items = AssetDatabase.GetAllAssetBundleNames().ToList();

        buildAllButton.Clicked += BuildAllAssetBundles;

        root = rootPanel;

        rootPanel.AddChild(titleLabel);
        rootPanel.AddChild(assetBundleScrollViewer);
        rootPanel.AddChild(directoryPanel);
        rootPanel.AddChild(buildAllButton);

        assetBundleScrollViewer.AddChild(assetBundleListView);

        directoryPanel.AddChild(directoryLabel);
        directoryPanel.AddChild(directoryTextField);
    }

    protected override void Restore()
    {
        if (directoryTextField != null)
        {
            directoryTextField.Value = EditorPrefs.GetString(ContentBundleDirectoryKey, DefaultContentBundleDirectory);
        }

    }

    protected override void Persist()
    {
        if (directoryTextField != null)
        {
            EditorPrefs.SetString(ContentBundleDirectoryKey, directoryTextField.Value);
        }
    }

    // Add submenu
    [MenuItem("Window/Content Bundle Builder")]
    public static void OpenWindow()
    {
        OpenWindow<ContentBundleWindow>("Content Bundles");
    }

    [MenuItem("Assets/Build Content Bundles %&b")]
    private static void BuildAllAssetBundles()
    {
        string contentBundleDirectory = EditorPrefs.GetString(ContentBundleDirectoryKey, DefaultContentBundleDirectory);

        Log($"Building Content Bundles...");

        if (!Directory.Exists(contentBundleDirectory))
        {
            Directory.CreateDirectory(contentBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles(contentBundleDirectory, BuildAssetBundleOptions.UncompressedAssetBundle, EditorUserBuildSettings.activeBuildTarget);

        if (File.Exists($"{contentBundleDirectory}/{contentBundleDirectory}"))
        {
            File.Delete($"{contentBundleDirectory}/{contentBundleDirectory}");
        }
        if (File.Exists($"{contentBundleDirectory}/{contentBundleDirectory}.manifest"))
        {
            File.Delete($"{contentBundleDirectory}/{contentBundleDirectory}.manifest");
        }

        Log($"Build finished!");
    }
}