using UnityEditor;
#if UNITY_EDITOR
using System.Reflection;
#endif

namespace Utility
{
    public static class GameViewUtils
    {
#if UNITY_EDITOR

         static object gameViewSizesInstance;
         static MethodInfo getGroup;
         private static int screenIndex = 16;
         private static int gameViewProfilesCount;
     
         static GameViewUtils()
         {
             var sizesType = typeof(EditorGUI).Assembly.GetType("UnityEditor.GameViewSizes");
             var singleType = typeof(ScriptableSingleton<>).MakeGenericType(sizesType);
             var instanceProp = singleType.GetProperty("instance");
             getGroup = sizesType.GetMethod("GetGroup");
             if (instanceProp != null) gameViewSizesInstance = instanceProp.GetValue(null, null);
         }
         
         public static void SetSize(int index)
         {
             var gvWndType = typeof(EditorGUI).Assembly.GetType("UnityEditor.GameView");
             var gvWnd = EditorWindow.GetWindow(gvWndType);
             var sizeSelectionCallback = gvWndType.GetMethod("SizeSelectionCallback",
                 BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
             if (sizeSelectionCallback != null) sizeSelectionCallback.Invoke(gvWnd, new object[] { index, null });
         }
     
         static object GetGroup(GameViewSizeGroupType type)
         {
             return getGroup.Invoke(gameViewSizesInstance, new object[] { (int)type });
         }
         
         private static void SetPrevious()
         {
             GetViewListSize();
             if (screenIndex - 1 >= 16)
             {
                 screenIndex -= 1;
             }
             else
             {
                 screenIndex = gameViewProfilesCount - 1;
             }
     
             SetSize(screenIndex);
         }
         
         private static void SetNext()
         {
             GetViewListSize();
             if (screenIndex + 1 < gameViewProfilesCount)
             {
                 screenIndex += 1;
             }
             else
             {
                 screenIndex = 16;
             }
     
             SetSize(screenIndex);
         }
     
         private static void GetViewListSize()
         {
             var group = GetGroup(GameViewSizeGroupType.Android);
             var getDisplayTexts = group.GetType().GetMethod("GetDisplayTexts");
             if (getDisplayTexts != null)
                 gameViewProfilesCount = ((string[])getDisplayTexts.Invoke(group, null)).Length;
         }
#endif
    }
}
