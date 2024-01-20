using GlmSharp;
using MS.WindowsAPICodePack.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
using TT_Lab.Rendering.Objects;
using TT_Lab.Util;

namespace TT_Lab.Rendering
{
    public class EditingContext
    {
        public SceneInstance? selectedInstance;
        public TransformSpace transformSpace = TransformSpace.LOCAL;
        public TransformMode transformMode = TransformMode.SELECTION;
        public TransformAxis transformAxis = TransformAxis.NONE;
        private EditorCursor cursor;
        private SceneInstance[] palette = new SceneInstance[9];
        private int currentPaletteIndex = 0;
        private Scene root;

        public EditingContext(Scene root)
        {
            this.root = root;
            cursor = new EditorCursor(root);
            root.AddChild(cursor);
            root.AddRender(cursor);
        }

        public void Deselect()
        {
            selectedInstance?.Deselect();
            selectedInstance = null;
        }

        public void Select(SceneInstance instance)
        {
            Deselect();
            selectedInstance = instance;
            selectedInstance?.Select();
        }

        public bool IsInstanceSelected()
        {
            return selectedInstance != null;
        }

        public void SetCursorCoordinates(vec3 pos)
        {
            cursor.SetPosition(pos);
        }

        public void SetPalette(SceneInstance instance)
        {
            palette[currentPaletteIndex] = instance;
        }

        public void SpawnAtCursor()
        {
            if (palette[currentPaletteIndex] == null)
            {
                return;
            }

            var newInstanceData = CloneUtils.Clone(palette[currentPaletteIndex].GetData());
            var cursorPosition = cursor.GetPosition();
            newInstanceData.Position = new Twinsanity.TwinsanityInterchange.Common.Vector4(-cursorPosition.x, cursorPosition.y, cursorPosition.z, 1.0f);
            var newInstance = root.NewSceneInstance(newInstanceData);
            Select(newInstance);
            transformMode = TransformMode.ROTATE;
            transformAxis = TransformAxis.NONE;
        }

        public void StartTransform(float x, float y)
        {
            if (selectedInstance == null)
            {
                transforming = false;
                return;
            }
            if (transforming)
            {
                return;
            }
            startPos = new vec2(x, y);
            startTransform = selectedInstance.GetRenderable().LocalTransform;
            transforming = true;
        }

        public void EndTransform(float x, float y) {
            if (selectedInstance == null)
            {
                transforming = false;
                return;
            }
            if (!transforming)
            {
                return;
            }
            UpdateTransform(x, y);
            transforming = false;
            var data = selectedInstance.GetData();
            var pos = selectedInstance.GetRenderable().WorldTransform.Column3.xyz;
            var rot = selectedInstance.GetRenderable().WorldTransform.ToQuaternion.EulerAngles * 180.0f / 3.14f;
            data.Position.X = -pos.x;
            data.Position.Y = pos.y;
            data.Position.Z = pos.z;
            data.RotationX.SetRotation((float)rot.x);
            data.RotationY.SetRotation((float)rot.y);
            data.RotationZ.SetRotation((float)rot.z);
        }

        public void UpdateTransform(float x, float y)
        {
            if (selectedInstance == null)
            {
                return;
            }
            endPos = new vec2(x, y);
            var delta = (endPos.x - startPos.x) + (startPos.y - endPos.y);
            
            if (transformMode == TransformMode.TRANSLATE)
            {
                var k = 0.05f;
                var axis = new vec3();
                if (transformAxis == TransformAxis.X)
                {
                    axis.x = 1.0f;
                } else if (transformAxis == TransformAxis.Y)
                {
                    axis.y = 1.0f;
                } else if (transformAxis == TransformAxis.Z)
                {
                    axis.z = 1.0f;
                }
                Translate(axis * k * delta);
            }
            if (transformMode == TransformMode.ROTATE)
            {
                var k = 0.2f;
                if (transformAxis == TransformAxis.X)
                {
                    RotateX(k * delta);
                }
                else if (transformAxis == TransformAxis.Y)
                {
                    RotateY(k * delta);
                }
                else if (transformAxis == TransformAxis.Z)
                {
                    RotateZ(k * delta);
                }
            }
        }

        public void ToggleTranslate()
        {
            if (transforming) {
                return;
            }
            if (transformMode != TransformMode.TRANSLATE)
            {
                transformMode = TransformMode.TRANSLATE;
            }
            else
            {
                transformMode = TransformMode.SELECTION;
            }
            transformAxis = TransformAxis.NONE;
        }

        public void ToggleRotate()
        {
            if (transforming)
            {
                return;
            }
            if (transformMode != TransformMode.ROTATE)
            {
                transformMode = TransformMode.ROTATE;
            }
            else
            {
                transformMode = TransformMode.SELECTION;
            }
            transformAxis = TransformAxis.NONE;
        }

        public void SetTransformAxis(TransformAxis axis)
        {
            if (transformAxis == axis)
            {
                transformAxis = TransformAxis.NONE;
            } else
            {
                transformAxis = axis;
            }
            if (transforming)
            {
                UpdateTransform(endPos.x, endPos.y);
            }
        }

        public void ToggleSpace()
        {
            transformSpace = transformSpace == TransformSpace.WORLD ? TransformSpace.LOCAL : TransformSpace.WORLD;
            if (transforming)
            {
                UpdateTransform(endPos.x, endPos.y);
            }
        }

        private mat4 startTransform;
        private vec2 startPos;
        private vec2 endPos;
        private bool transforming = false;

        //TODO: must affect InstanceData too
        private void Translate(vec3 offset)
        {
            if (transformSpace == TransformSpace.LOCAL)
            {
                selectedInstance.GetRenderable().LocalTransform = startTransform * mat4.Translate(offset);
            } 
            else
            {
                selectedInstance.GetRenderable().LocalTransform = mat4.Translate(offset) * startTransform;
            }
        }

        private void RotateX(float value)
        {
            if (transformSpace == TransformSpace.LOCAL)
            {
                selectedInstance.GetRenderable().LocalTransform = startTransform * mat4.RotateX(value * 3.14f / 180.0f);
            }
            else
            {
                selectedInstance.GetRenderable().LocalTransform = startTransform;
                var localRotation = startTransform.ToQuaternion.ToMat4;
                selectedInstance.GetRenderable().LocalTransform *= localRotation.Inverse;
                selectedInstance.GetRenderable().LocalTransform *= mat4.RotateX(value * 3.14f / 180.0f);
                selectedInstance.GetRenderable().LocalTransform *= localRotation;
            }
        }

        private void RotateY(float value)
        {
            if (transformSpace == TransformSpace.LOCAL)
            {
                selectedInstance.GetRenderable().LocalTransform = startTransform * mat4.RotateY(value * 3.14f / 180.0f);
            }
            else
            {
                selectedInstance.GetRenderable().LocalTransform = startTransform;
                var localRotation = startTransform.ToQuaternion.ToMat4;
                selectedInstance.GetRenderable().LocalTransform *= localRotation.Inverse;
                selectedInstance.GetRenderable().LocalTransform *= mat4.RotateY(value * 3.14f / 180.0f);
                selectedInstance.GetRenderable().LocalTransform *= localRotation;
            }
        }

        private void RotateZ(float value)
        {
            if (transformSpace == TransformSpace.LOCAL)
            {
                selectedInstance.GetRenderable().LocalTransform = startTransform * mat4.RotateZ(value * 3.14f / 180.0f);
            }
            else
            {
                selectedInstance.GetRenderable().LocalTransform = startTransform;
                var localRotation = startTransform.ToQuaternion.ToMat4;
                selectedInstance.GetRenderable().LocalTransform *= localRotation.Inverse;
                selectedInstance.GetRenderable().LocalTransform *= mat4.RotateZ(value * 3.14f / 180.0f);
                selectedInstance.GetRenderable().LocalTransform *= localRotation;
            }
        }
    }

    public enum TransformSpace
    {
        WORLD,
        LOCAL
    }

    public enum TransformMode
    {
        SELECTION,
        TRANSLATE,
        ROTATE,
        SCALE
    }

    public enum TransformAxis
    {
        NONE,
        X,
        Y,
        Z,
        XZ,
        XY,
        ZY
    }
}
