using System;
using TT_Lab.Assets;
using TT_Lab.ViewModels.Interfaces;

namespace TT_Lab.Project.Messages
{
    public class CreateEditorMessage<T> where T : IEditorViewModel
    {
        public CreateEditorMessage(LabURI resourceUri, Type editorType)
        {
            ResourceURI = resourceUri;
            EditorType = editorType;
        }

        public LabURI ResourceURI { get; set; } = LabURI.Empty;
        public Type EditorType { get; private set; }
    }
}
