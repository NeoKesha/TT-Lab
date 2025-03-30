using System;
using System.ComponentModel;
using System.Linq;
using GlmSharp;
using org.ogre;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Code;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.ViewModels.Composite;
using TT_Lab.ViewModels.Editors.Instance;
using TT_Lab.ViewModels.ResourceTree;
using TT_Lab.Views.Composite;

namespace TT_Lab.Rendering.Objects.SceneInstances
{
    public abstract class SceneInstance
    {
        protected EditableObject AttachedEditableObject;
        protected readonly EditingContext EditingContext;
        protected readonly ResourceTreeElementViewModel AttachedViewModel;
        
        protected vec3 Position;
        protected vec3 Rotation;
        protected vec3 Offset;
        protected vec3 Size;
        protected Boolean IsSelected;

        protected SceneInstance(EditingContext editingContext, ResourceTreeElementViewModel attachedViewModel)
        {
            AttachedViewModel = attachedViewModel;
            EditingContext = editingContext;
        }

        public ResourceTreeElementViewModel GetViewModel()
        {
            return AttachedViewModel;
        }

        public virtual void SetPositionRotation(vec3 position, dvec3 rotation)
        {
            if (!IsSelected)
            {
                return;
            }
            
            var editor = (ViewportEditableInstanceViewModel)EditingContext.GetCurrentEditor()!;
            editor.Position.X = -position.x;
            editor.Position.Y = position.y;
            editor.Position.Z = position.z;
            editor.Rotation.X = (float)rotation.x;
            editor.Rotation.Y = (float)-rotation.y;
            editor.Rotation.Z = (float)-rotation.z;
        }

        public void LinkChangesToViewModel(ViewportEditableInstanceViewModel viewModel)
        {
            viewModel.Position.PropertyChanged += PositionOnPropertyChanged;
            viewModel.Rotation.PropertyChanged += RotationOnPropertyChanged;
            viewModel.Scale.PropertyChanged += ScaleOnPropertyChanged;
        }

        private void ScaleOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsDirty")
            {
                return;
            }

            var viewModel = (Vector3ViewModel)sender!;
            var sceneNode = AttachedEditableObject.getParentSceneNode();
            sceneNode.setScale(viewModel.X, viewModel.Y, viewModel.Z);
        }

        private void RotationOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsDirty")
            {
                return;
            }
            
            var viewModel = (Vector3ViewModel)sender!;
            var sceneNode = AttachedEditableObject.getParentSceneNode();
            sceneNode.setOrientation(sceneNode.getInitialOrientation());
            sceneNode.pitch(new Radian(new Degree(viewModel.X)));
            sceneNode.yaw(new Radian(new Degree(-viewModel.Y)));
            sceneNode.roll(new Radian(new Degree(-viewModel.Z)));
        }

        private void PositionOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsDirty")
            {
                return;
            }

            var viewModel = (Vector4ViewModel)sender!;
            var sceneNode = AttachedEditableObject.getParentSceneNode();
            sceneNode.setPosition(-viewModel.X, viewModel.Y, viewModel.Z);
        }

        public void UnlinkChangesToViewModel(ViewportEditableInstanceViewModel viewModel)
        {
            viewModel.Position.PropertyChanged -= PositionOnPropertyChanged;
            viewModel.Rotation.PropertyChanged -= RotationOnPropertyChanged;
            viewModel.Scale.PropertyChanged -= ScaleOnPropertyChanged;
        }

        public void Select()
        {
            AttachedEditableObject.Select();
            IsSelected = true;
        }

        public void Deselect()
        {
            IsSelected = false;
            AttachedEditableObject.Deselect();
        }

        public EditableObject GetEditableObject()
        {
            return AttachedEditableObject;
        }

        public vec3 GetPosition()
        {
            return AttachedEditableObject.GetPosition();
        }

        public vec3 GetOffset()
        {
            return Offset;
        }
        
        public vec3 GetSize()
        {
            return Size;
        }
        
        public vec3 GetRotation()
        {
            return AttachedEditableObject.GetRotation();
        }

        public mat4 GetTransform()
        {
            return AttachedEditableObject.GetTransform();
        }
    }
}
