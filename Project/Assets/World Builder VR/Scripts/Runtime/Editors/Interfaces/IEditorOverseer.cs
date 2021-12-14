namespace WorldBuilderVR.Editors.Core
{
    public interface IEditorOverseer
    {
        /// <summary>
        /// Finish up editing current asset and send it to a higher overseer.
        /// </summary>
        public void CompleteEditing();
    }
}