using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ViewWindows : EditorWindow
{
    [MenuItem("Window/View Window")]
    public static void ShowWindow()
    {
        GetWindow(typeof(ViewWindows));
    }

    private const int PixelCount = 1;
    private const int ScrollContHeight = 660;
    private const int ScrollPos = 0;
    private const int ScrollPadding = 2;
    private const int Width = 400;
    private const int BoxInitialPos = 0;
    private const int BoxHeight = 100;
    private const int BoxSpace = 5;
    private const int BoxSize = BoxHeight + BoxSpace;
    private const int ButtonHieght = 50;
    private const string CreateButtonName = "Create New";

    private Container _container;

    private List<bool> _nodes;

    Vector2 _scrollPosition;


    private Texture2D _textureGreen;
    private Texture2D _textureRed;

    private GUIStyle _styleGreen;
    private GUIStyle _styleRed;

    void OnEnable()
    {
        _nodes = new List<bool>();
        _scrollPosition = new Vector2(ScrollPos, ScrollPadding);
        InitGUIStyle();
        IninContainer();
    }

    private void IninContainer()
    {
        CreateNewContainer();
        GetTheNodes();
    }

    private void CreateNewContainer()
    {
        _container = new Container();
    }

    void OnGUI()
    {
        _scrollPosition = GUI.BeginScrollView(new Rect(ScrollPos, ScrollPos, Width, ScrollContHeight), _scrollPosition,
            new Rect(ScrollPos, ScrollPos, ScrollPos, _nodes.Count * BoxSize), false, false, GUIStyle.none,
            GUIStyle.none);

        int nodesCount = _nodes.Count;
        for (int i = 0; i < nodesCount; i++)
        {
            Rect rectBox = new Rect(BoxInitialPos, i * BoxSize, Width, BoxHeight);
            bool value = _nodes[i];
            GUI.Box(rectBox, value.ToString(), value ? _styleGreen : _styleRed);
        }

        GUI.EndScrollView();

        if (_scrollPosition.y >= (nodesCount * BoxSize - ScrollContHeight))
        {
            _scrollPosition = new Vector2(ScrollPos, ScrollPadding);
        }

        if (0.1f >= _scrollPosition.y)
        {
            _scrollPosition = new Vector2(ScrollPos, nodesCount * BoxSize - ScrollContHeight - ScrollPadding);
        }

        if (GUI.Button(new Rect(ScrollPos, ScrollContHeight, Width, ButtonHieght), CreateButtonName))
        {
            IninContainer();
        }
    }

    private void GetTheNodes()
    {
        _nodes.Clear();
        _container.MoveBackward();
        bool lastValue = _container.Value;
        _container.MoveForward();

        while (true)
        {
            _nodes.Add(_container.Value);
            int count = _nodes.Count;
            MoveBackward(count);

            if (!_nodes[count - 1].Equals(_container.Value))
            {
                MoveForward(count + 1);
                continue;
            }

            bool tmp = _container.Value;
            _container.Value = !_container.Value;
            MoveForward(count);

            if (!tmp.Equals(_container.Value))
            {
                _nodes[count - 1] = lastValue;
                break;
            }

            _container.Value = !_container.Value;
            MoveForward(1);
        }
    }

    private void MoveForward(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            _container.MoveForward();
        }
    }

    private void MoveBackward(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            _container.MoveBackward();
        }
    }

    private void InitGUIStyle()
    {
        _styleGreen = new GUIStyle();
        _styleRed = new GUIStyle();

        _textureGreen = new Texture2D(PixelCount, PixelCount);
        _textureRed = new Texture2D(PixelCount, PixelCount);

        for (int y = 0; y < PixelCount; ++y)
        {
            for (int x = 0; x < PixelCount; ++x)
            {
                _textureGreen.SetPixel(x, y, Color.green);
                _textureRed.SetPixel(x, y, Color.red);
            }
        }

        _textureGreen.Apply();
        _textureRed.Apply();

        _styleGreen.normal.background = _textureGreen;
        _styleGreen.alignment = TextAnchor.MiddleCenter;

        _styleRed.normal.background = _textureRed;
        _styleRed.alignment = TextAnchor.MiddleCenter;
    }
}