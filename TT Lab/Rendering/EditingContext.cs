using System;
using System.Windows;
using GlmSharp;
using org.ogre;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Extensions;
using TT_Lab.Rendering.Objects;
using TT_Lab.Rendering.Objects.SceneInstances;
using TT_Lab.Util;
using TT_Lab.ViewModels.Editors;
using TT_Lab.ViewModels.Editors.Instance;

namespace TT_Lab.Rendering
{
    public class EditingContext
    {
        public SceneInstance? SelectedInstance;
        public MovableObject? SelectedRenderable;
        public TransformMode TransformMode = TransformMode.SELECTION;
        public TransformAxis TransformAxis = TransformAxis.NONE;
        
        private readonly EditorCursor _cursor;
        private readonly SceneInstance?[] _palette = new SceneInstance[9];
        private readonly BillboardSet _positionsBillboards;
        private readonly BillboardSet _triggersBillboards;
        private readonly BillboardSet _camerasBillboards;
        private readonly BillboardSet _instancesBillboards;
        private readonly BillboardSet _aiPositionsBillboards;
        private int _currentPaletteIndex = 0;
        private readonly ChunkEditorViewModel _editor;
        private readonly SceneNode _editCtxNode;
        private readonly Gizmo _gizmo;
        private vec3 _gridStep;
        private mat4 _gridRotation;
        private readonly SceneManager _sceneManager;
        private readonly OgreWindow _renderWindow;

        public EditingContext(OgreWindow renderWindow, SceneManager sceneManager, ChunkEditorViewModel editor)
        {
            _editor = editor;
            _renderWindow = renderWindow;
            _sceneManager = sceneManager;
            _editCtxNode = sceneManager.getRootSceneNode().createChildSceneNode("EditorContextNode");
            _cursor = new EditorCursor(this);
            _gizmo = new Gizmo(sceneManager, this);
            _positionsBillboards = CreateBillboardSet(sceneManager, "PositionsBillboards", "BillboardPositions");
            _triggersBillboards = CreateBillboardSet(sceneManager, "TriggersBillboards", "BillboardTriggers");
            _camerasBillboards = CreateBillboardSet(sceneManager, "CamerasBillboards", "BillboardCameras");
            _instancesBillboards = CreateBillboardSet(sceneManager, "InstancesBillboards", "BillboardInstances");
            _aiPositionsBillboards = CreateBillboardSet(sceneManager, "AiPositionsBillboards", "BillboardAiPositions");

            _renderWindow.OnRender += (sender, args) =>
            {
                if (SelectedInstance == null)
                {
                    return;
                }

                SelectedInstance.GetEditableObject().DrawImGui();
            };
        }

        public SceneNode GetEditorNode()
        {
            return _editCtxNode;
        }

        public Entity CreateEntity(MeshPtr mesh)
        {
            return _sceneManager.createEntity(mesh);
        }

        public MovableObject GetPositionBillboards()
        {
            return _positionsBillboards;
        }

        public MovableObject GetInstancesBillboards()
        {
            return _instancesBillboards;
        }

        public MovableObject GetTriggersBillboards()
        {
            return _triggersBillboards;
        }

        public MovableObject GetCamerasBillboards()
        {
            return _camerasBillboards;
        }

        public MovableObject GetAiPositionsBillboards()
        {
            return _aiPositionsBillboards;
        }

        public Billboard CreatePositionBillboard()
        {
            return _positionsBillboards.createBillboard(0, 0, 0);
        }

        public Billboard CreateTriggerBillboard()
        {
            return _triggersBillboards.createBillboard(0, 0, 0);
        }

        public Billboard CreateInstanceBillboard()
        {
            return _instancesBillboards.createBillboard(0, 0, 0);
        }

        public Billboard CreateCameraBillboard()
        {
            return _camerasBillboards.createBillboard(0, 0, 0);
        }

        public Billboard CreateAiPositionBillboard()
        {
            return _aiPositionsBillboards.createBillboard(0, 0, 0);
        }

        public void Deselect()
        {
            SelectedInstance?.UnlinkChangesToViewModel((ViewportEditableInstanceViewModel)_editor.CurrentInstanceEditor!);
            _editor.InstanceEditorChanged(new RoutedPropertyChangedEventArgs<Object>(null, null));
            _renderWindow.SetCameraStyle(CameraStyle.CS_FREELOOK);
            SelectedInstance?.Deselect();
            SelectedInstance = null;
            SelectedRenderable = null;
            _gizmo.HideGizmo();
        }

        public void Select(SceneInstance instance)
        {
            Deselect();
            SelectedInstance = instance;
            SelectedInstance?.Select();
            SelectedRenderable = SelectedInstance?.GetEditableObject();
            
            if (SelectedInstance != null)
            {
                _renderWindow.SetCameraTarget(SelectedInstance.GetEditableObject().GetSceneNode());
                _renderWindow.SetCameraStyle(CameraStyle.CS_ORBIT);
                
                _editor.InstanceEditorChanged(new RoutedPropertyChangedEventArgs<Object>(null, SelectedInstance.GetViewModel()));
                SelectedInstance.LinkChangesToViewModel((ViewportEditableInstanceViewModel)_editor.CurrentInstanceEditor!);
                _gizmo.DetachFromCurrentObject();
                _gizmo.SwitchGizmo((Gizmo.GizmoType)(int)TransformMode);
                _gizmo.AttachToObject(SelectedInstance.GetEditableObject().GetSceneNode());
            }
        }

        public InstanceSectionResourceEditorViewModel? GetCurrentEditor()
        {
            return _editor.CurrentInstanceEditor;
        }

        public void SetGrid()
        {
            if (SelectedInstance == null)
            {
                return;
            }
            
            _gridStep.x = SelectedInstance.GetSize().x;
            _gridStep.y = SelectedInstance.GetSize().y;
            _gridStep.z = SelectedInstance.GetSize().z;
            _gridRotation = (new quat(SelectedInstance.GetRotation())).ToMat4;
            SetCursorCoordinates(SelectedInstance.GetPosition());
        }

        public void MoveCursorGrid(vec3 offset)
        {
            var cursorPos = _cursor.GetPosition();
            cursorPos += (_gridRotation * new vec4(offset * _gridStep, 1.0f)).xyz;
            SetCursorCoordinates(cursorPos);
        }

        public bool IsInstanceSelected()
        {
            return SelectedInstance != null;
        }

        public void SetCursorCoordinates(vec3 pos)
        {
            _cursor.SetPosition(pos);
        }

        public void SetPalette(SceneInstance instance)
        {
            _palette[_currentPaletteIndex] = instance;
        }

        public void SpawnAtCursor()
        {
            if (_palette[_currentPaletteIndex] == null)
            {
                return;
            }

            var cursorPosition = _cursor.GetPosition();
            var newInstance = _editor.NewSceneInstance(_palette[_currentPaletteIndex]!.GetType(), _palette[_currentPaletteIndex]!.GetViewModel());
            newInstance.SetPositionRotation(cursorPosition, _palette[_currentPaletteIndex]!.GetRotation());
            Select(newInstance);
            TransformMode = TransformMode.ROTATE;
            TransformAxis = TransformAxis.NONE;
        }

        public void StartTransform(float x, float y)
        {
            if (SelectedInstance == null)
            {
                transforming = false;
                return;
            }
            if (transforming)
            {
                return;
            }
            startPos = new vec2(x, y);
            transforming = true;
        }

        public void EndTransform(float x, float y)
        {
            if (SelectedInstance == null)
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
            var pos = SelectedRenderable!.getParentSceneNode().getPosition();
            var renderQuat = SelectedRenderable.getParentSceneNode().getOrientation();
            var glmQuat = new quat(renderQuat.x, renderQuat.y, renderQuat.z, renderQuat.w);
            var rot = glmQuat.EulerAngles * 180.0f / 3.14f;
            SelectedInstance.SetPositionRotation(new vec3(pos.x, pos.y, pos.z), rot);
        }

        public void UpdateTransform(float x, float y)
        {
            if (SelectedInstance == null || !transforming)
            {
                return;
            }
            
            endPos = new vec2(x, y);
            var delta = (endPos.x - startPos.x) + (startPos.y - endPos.y);

            if (TransformMode == TransformMode.TRANSLATE)
            {
                var k = 0.05f;
                var axis = new vec3();
                if (TransformAxis == TransformAxis.X)
                {
                    axis.x = 1.0f;
                }
                else if (TransformAxis == TransformAxis.Y)
                {
                    axis.y = 1.0f;
                }
                else if (TransformAxis == TransformAxis.Z)
                {
                    axis.z = 1.0f;
                }
                Translate(axis * k * delta);
            }
            if (TransformMode == TransformMode.ROTATE)
            {
                var k = 0.2f;
                if (TransformAxis == TransformAxis.X)
                {
                    RotateX(k * delta);
                }
                else if (TransformAxis == TransformAxis.Y)
                {
                    RotateY(k * delta);
                }
                else if (TransformAxis == TransformAxis.Z)
                {
                    RotateZ(k * delta);
                }
            }
        }

        public void ToggleTranslate()
        {
            if (transforming)
            {
                return;
            }
            
            if (TransformMode != TransformMode.TRANSLATE)
            {
                TransformMode = TransformMode.TRANSLATE;
            }
            else
            {
                TransformMode = TransformMode.SELECTION;
            }
            TransformAxis = TransformAxis.NONE;
            _gizmo.SwitchGizmo((Gizmo.GizmoType)(int)TransformMode);
        }

        public void ToggleRotate()
        {
            if (transforming)
            {
                return;
            }
            if (TransformMode != TransformMode.ROTATE)
            {
                TransformMode = TransformMode.ROTATE;
            }
            else
            {
                TransformMode = TransformMode.SELECTION;
            }
            TransformAxis = TransformAxis.NONE;
            _gizmo.SwitchGizmo((Gizmo.GizmoType)(int)TransformMode);
        }

        public void SetTransformAxis(TransformAxis axis)
        {
            if (TransformAxis == axis)
            {
                TransformAxis = TransformAxis.NONE;
            }
            else
            {
                TransformAxis = axis;
            }
            _gizmo.HighlightAxis(TransformAxis);
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
            SelectedRenderable.getParentSceneNode().translate(offset.x, offset.y, offset.z);
        }

        private void RotateX(float value)
        {
            SelectedRenderable.getParentSceneNode().pitch(new Radian(new Degree(value)));
        }

        private void RotateY(float value)
        {
            SelectedRenderable.getParentSceneNode().yaw(new Radian(new Degree(value)));
        }

        private void RotateZ(float value)
        {
            SelectedRenderable.getParentSceneNode().roll(new Radian(new Degree(value)));
        }

        private BillboardSet CreateBillboardSet(SceneManager sceneManager, string billboardName, string billboardMaterial)
        {
            var billboardSet = sceneManager.createBillboardSet(billboardName);
            billboardSet.setMaterial(MaterialManager.GetMaterial(billboardMaterial));
            billboardSet.setDefaultDimensions(2, 2);
            billboardSet.setCullIndividually(true);
            // Set the bounding box to be huge, because otherwise the node itself gets culled out and all billboards stop rendering
            billboardSet.setBounds(new AxisAlignedBox(-1000, -1000, -1000, 1000, 1000, 1000), 1000);
            billboardSet.setRenderQueueGroup((byte)RenderQueueGroupID.RENDER_QUEUE_OVERLAY);

            return billboardSet;
        }
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
