using Sirenix.OdinInspector.Editor.Examples;
using UnityEngine;

#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos
{
    using UnityEditor;
    using Sirenix.OdinInspector.Editor;
    using Sirenix.OdinInspector;
    using Sirenix.Utilities.Editor;
    using Sirenix.Utilities;

    public class BasicOdinEditorExampleWindow : OdinEditorWindow
    {
        [MenuItem("Tools/Odin/Demos/Odin Editor Window Demos/Basic Odin Editor Window")]
        private static void OpenWindow()
        {
            var window = GetWindow<BasicOdinEditorExampleWindow>();

            // Nifty little trick to quickly position the window in the middle of the editor.
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 700);
        }

        [EnumToggleButtons]
        [InfoBox("Inherit from OdinEditorWindow instead of EditorWindow in order to create editor windows like you would inspectors - by exposing members and using attributes.")]
        public ViewTool SomeField;
        
        
        [TableMatrix(HorizontalTitle = "Square Celled Matrix", SquareCells = true)]
        public Texture2D[,] SquareCelledMatrix;

        [TableMatrix(SquareCells = true)]
        public Mesh[,] PrefabMatrix;

        [OnInspectorInit]
        private void CreateData()
        {
            SquareCelledMatrix = new Texture2D[8, 4]
            {
                {ExampleHelper.GetTexture(), null, null, null},
                {null, ExampleHelper.GetTexture(), null, null},
                {null, null, ExampleHelper.GetTexture(), null},
                {null, null, null, ExampleHelper.GetTexture()},
                {ExampleHelper.GetTexture(), null, null, null},
                {null, ExampleHelper.GetTexture(), null, null},
                {null, null, ExampleHelper.GetTexture(), null},
                {null, null, null, ExampleHelper.GetTexture()},
            };
        }
    }
}
#endif
