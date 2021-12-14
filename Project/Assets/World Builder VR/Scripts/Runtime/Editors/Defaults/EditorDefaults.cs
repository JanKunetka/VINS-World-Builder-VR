using UnityEngine;

namespace WorldBuilderVR.Editors.Core.Defaults
{
    /// <summary>
    /// Contains all default values used by the editor.
    /// </summary>
    public static class EditorDefaults
    {
        //World
        public const string WorldTitle = "New World";
        public const string WorldDescription = "A world filled with possibilities!";
        public static readonly Sprite WorldIcon = Resources.Load<Sprite>("Defaults/tex_icon_DefaultPack");
        public const string Author = "NO_AUTHOR";
    }
}