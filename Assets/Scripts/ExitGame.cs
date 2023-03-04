using UnityEditor;
using UnityEngine;

public static class ExitGame
{
    #region Public methods

    public static void ExitButtonClicked()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    #endregion
}