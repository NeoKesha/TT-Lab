using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;
using GlmSharp;
using org.ogre;
using TT_Lab.AssetData.Graphics;
using TT_Lab.AssetData.Graphics.Shaders;
using TT_Lab.Assets;
using TT_Lab.Util;
using TT_Lab.ViewModels.Editors.Graphics;
using Twinsanity.TwinsanityInterchange.Common;
using Math = System.Math;
using Matrix4 = org.ogre.Matrix4;

namespace TT_Lab.Rendering;

public static class TwinMaterialGenerator
{
    private static Dictionary<string, GeneratedMaterial> materialToRenderPriorityMap = new();

    public struct GeneratedMaterial
    {
        public MaterialPtr Material;
        public ushort RenderPriority;
    }
    
    public struct ShaderSettings
    {
        public bool MirrorX { get; set; }
        public bool UseSkinning { get; set; }
    }

    public static GeneratedMaterial GenerateMaterialFromViewModel(MaterialViewModel material, ShaderSettings shaderSettings = default, bool forceRebuild = false)
    {
        var matData = new MaterialData();
        material.Copy(ref matData);
        var originalHashCode = AssetManager.Get().GetAssetData<MaterialData>(material.EditableResource).GetHashCode();
        return GenerateMaterialFromTwinMaterial(matData, shaderSettings, forceRebuild, originalHashCode);
    }
    
    [SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize")]
    public static GeneratedMaterial GenerateMaterialFromTwinMaterial(MaterialData twinMaterial, ShaderSettings shaderSettings = default, bool forceRebuild = false, int hash = -1)
    {
        var materialText = new StringBuilder();
        var ogreMaterialManager = org.ogre.MaterialManager.getSingleton();
        var formattedName = twinMaterial.Name.Replace(" ", "_");
        formattedName = formattedName.Remove(formattedName.Length - 1, 1);
        var hashCode = hash == -1 ? twinMaterial.GetHashCode() : hash;
        var materialName = $"{formattedName}_{hashCode}";
        if (ogreMaterialManager.resourceExists(materialName, GlobalConsts.OgreGroup))
        {
            if (!forceRebuild)
            {
                return materialToRenderPriorityMap[materialName];
            }
            
            materialToRenderPriorityMap.Remove(materialName);
            ogreMaterialManager.remove(materialName, GlobalConsts.OgreGroup);
        }

        ushort renderPriority = 0;
        
        materialText.AppendLine($"material {materialName}");
        StartBlock(materialText);
        materialText.AppendLine("technique");
        StartBlock(materialText);
        for (var i = 0; i < twinMaterial.Shaders.Count; ++i)
        {
            var shader = twinMaterial.Shaders[i];
            var shaderPriority = (int)shader.UnkVector2.W;
            if (shaderPriority > renderPriority)
            {
                renderPriority = (ushort)shaderPriority;
            }
            
            materialText.AppendLine("pass");
            StartBlock(materialText);
            // TODO: Write shader params
            materialText.AppendLine("cull_hardware none");
            if (shader.ABlending == TwinShader.AlphaBlending.ON)
            {
                materialText.AppendLine("scene_blend alpha_blend");

                var operation = "add";
                switch (shader.AlphaRegSettingsIndex)
                {
                    case TwinShader.AlphaBlendPresets.Mix:
                        // TODO: Figure it out???
                        break;
                    case TwinShader.AlphaBlendPresets.Sub:
                        operation = "subtract";
                        break;
                    case TwinShader.AlphaBlendPresets.Add:
                    case TwinShader.AlphaBlendPresets.Alpha:
                    case TwinShader.AlphaBlendPresets.Zero:
                    case TwinShader.AlphaBlendPresets.Destination:
                    case TwinShader.AlphaBlendPresets.Source:
                    default:
                        break;
                }
                materialText.AppendLine($"scene_blend_op {operation}");
                // materialText.AppendLine("depth_write off");
            }
            else
            {
                materialText.AppendLine("scene_blend one zero");
                //materialText.AppendLine("depth_write on");
            }

            if (shader.ZValueDrawingMask == TwinShader.ZValueDrawMask.UPDATE)
            {
                materialText.AppendLine("depth_write on");
            }
            else
            {
                materialText.AppendLine("depth_write off");
            }

            materialText.AppendLine("depth_check on");
            switch (shader.DepthTest)
            {
                case TwinShader.DepthTestMethod.NEVER:
                    materialText.AppendLine("depth_func always_fail");
                    break;
                case TwinShader.DepthTestMethod.ALWAYS:
                    materialText.AppendLine("depth_func always_pass");
                    break;
                case TwinShader.DepthTestMethod.GEQUAL:
                    materialText.AppendLine("depth_func less_equal");
                    break;
                case TwinShader.DepthTestMethod.GREATER:
                    materialText.AppendLine("depth_func less");
                    break;
            }

            switch (shader.ShaderType)
            {
                case TwinShader.Type.StandardUnlit:
                    break;
                case TwinShader.Type.StandardLit:
                    break;
                case TwinShader.Type.LitSkinnedModel:
                    break;
                case TwinShader.Type.UnlitSkydome:
                    break;
                case TwinShader.Type.ColorOnly:
                    break;
                case TwinShader.Type.LitEnvironmentMap:
                case TwinShader.Type.UnlitEnvironmentMap:
                    break;
                case TwinShader.Type.UiShader:
                    break;
                case TwinShader.Type.LitMetallic:
                    break;
                case TwinShader.Type.LitReflectionSurface:
                    break;
                case TwinShader.Type.Particle:
                    break;
                case TwinShader.Type.Decal:
                    break;
                case TwinShader.Type.UnlitGlossy:
                    break;
                case TwinShader.Type.UnlitClothDeformation:
                    break;
                case TwinShader.Type.UnlitClothDeformation2:
                    break;
                case TwinShader.Type.UnlitBillboard:
                    break;
                // Unused shaders in Twinsanity, currently functionality and purpose unknown
                case TwinShader.Type.SHADER_17:
                case TwinShader.Type.SHADER_20:
                case TwinShader.Type.SHADER_25:
                case TwinShader.Type.SHADER_30:
                case TwinShader.Type.SHADER_31:
                case TwinShader.Type.SHADER_32:
                    break;
            }
            
            // Setup texture
            materialText.AppendLine("texture_unit");
            StartBlock(materialText);
            // materialText.AppendLine("texture \"boat_guy.png\"");
            if (shader.TextureFilterWhenTextureIsExpanded == TwinShader.TextureFilter.LINEAR)
            {
                materialText.AppendLine("filtering bilinear");
            }
            else
            {
                materialText.AppendLine("filtering none");
            }
            switch (shader.ShaderType)
            {
                case TwinShader.Type.StandardUnlit:
                    break;
                case TwinShader.Type.StandardLit:
                    break;
                case TwinShader.Type.LitSkinnedModel:
                    break;
                case TwinShader.Type.UnlitSkydome:
                    break;
                case TwinShader.Type.ColorOnly:
                    break;
                case TwinShader.Type.LitEnvironmentMap:
                case TwinShader.Type.UnlitEnvironmentMap:
                    materialText.AppendLine("env_map planar");
                    break;
                case TwinShader.Type.UiShader:
                    break;
                case TwinShader.Type.LitMetallic:
                    break;
                case TwinShader.Type.LitReflectionSurface:
                    break;
                case TwinShader.Type.Particle:
                    break;
                case TwinShader.Type.Decal:
                    break;
                case TwinShader.Type.UnlitGlossy:
                    break;
                case TwinShader.Type.UnlitClothDeformation:
                case TwinShader.Type.UnlitClothDeformation2:
                    break;
                case TwinShader.Type.UnlitBillboard:
                    break;
                // Unused shaders in Twinsanity, currently functionality and purpose unknown
                case TwinShader.Type.SHADER_17:
                case TwinShader.Type.SHADER_20:
                case TwinShader.Type.SHADER_25:
                case TwinShader.Type.SHADER_30:
                case TwinShader.Type.SHADER_31:
                case TwinShader.Type.SHADER_32:
                    break;
            }
            EndBlock(materialText);
            
            // Setup shader programs
            materialText.AppendLine("fragment_program_ref DiffuseTextureFragShader");
            StartBlock(materialText);
            materialText.AppendLine("param_named_auto uDiffuseColor custom 0");
            materialText.AppendLine("param_named_auto elapsedTime time");
            var scrollX = shader.UvScrollSpeed.Z;
            var scrollY = shader.UvScrollSpeed.W;
            if (shader.XScrollSettings == TwinShader.XScrollFormula.Disabled)
            {
                scrollX = 0f;
            }
            if (shader.YScrollSettings == TwinShader.YScrollFormula.Disabled)
            {
                scrollY = 0f;
            }
            
            var passValue = 0;
            var alphaTestFunc = 0;
            if (shader.ATest == TwinShader.AlphaTest.ON)
            {
                passValue = shader.AlphaValueToBeComparedTo;
                /*
                 *  #define CMPF_ALWAYS_FAIL 0
                    #define CMPF_ALWAYS_PASS 1
                    #define CMPF_LESS 2
                    #define CMPF_LESS_EQUAL 3
                    #define CMPF_EQUAL 4
                    #define CMPF_NOT_EQUAL 5
                    #define CMPF_GREATER_EQUAL 6
                    #define CMPF_GREATER 7
                 */
                alphaTestFunc = shader.ATestMethod switch
                {
                    TwinShader.AlphaTestMethod.NEVER => 0,
                    TwinShader.AlphaTestMethod.ALWAYS => 1,
                    TwinShader.AlphaTestMethod.LESS => 2,
                    TwinShader.AlphaTestMethod.LEQUAL => 3,
                    TwinShader.AlphaTestMethod.EQUAL => 4,
                    TwinShader.AlphaTestMethod.GEQUAL => 6,
                    TwinShader.AlphaTestMethod.GREATER => 7,
                    TwinShader.AlphaTestMethod.NOTEQUAL => 5,
                    _ => alphaTestFunc
                };
            }
            materialText.AppendLine($"param_named uScrollSpeedAndAlphaTest float4 {scrollX.ToString(CultureInfo.InvariantCulture)} {scrollY.ToString(CultureInfo.InvariantCulture)} {(passValue / 255.0f).ToString(CultureInfo.InvariantCulture)} {alphaTestFunc}");
            EndBlock(materialText);
            var vertexShaderName = shaderSettings.UseSkinning ? "SkinningVertShader" : "DiffuseTextureVertShader";
            materialText.AppendLine($"vertex_program_ref {vertexShaderName}");
            StartBlock(materialText);
            materialText.AppendLine("param_named_auto elapsedTime time");
            if (shader.ShaderType is TwinShader.Type.UnlitClothDeformation or TwinShader.Type.UnlitClothDeformation2)
            {
                materialText.AppendLine($"param_named deformSpeed float4 {shader.FloatParam[0].ToString(CultureInfo.InvariantCulture)} {shader.FloatParam[1].ToString(CultureInfo.InvariantCulture)} 1 0");
            }

            var shaderSettingsVec = new vec4();
            if (shaderSettings.MirrorX)
            {
                shaderSettingsVec.x = 1;
            }
            materialText.AppendLine($"param_named shaderSettings float4 {shaderSettingsVec.x.ToString(CultureInfo.InvariantCulture)} {shaderSettingsVec.y.ToString(CultureInfo.InvariantCulture)} {shaderSettingsVec.z.ToString(CultureInfo.InvariantCulture)} {shaderSettingsVec.w.ToString(CultureInfo.InvariantCulture)}");
            EndBlock(materialText);
            
            // End pass block
            EndBlock(materialText);
        }
        // End technique block
        EndBlock(materialText);
        // End material block
        EndBlock(materialText);

        var memStream = new MemoryStream(Encoding.Default.GetBytes(materialText.ToString()));
        unsafe
        {
            fixed (byte* ptr = memStream.ToArray())
            {
                var dataStream = new MemoryDataStream(new IntPtr(ptr), (uint)memStream.Length, false, true);
                var dataPtr = new DataStreamPtr(dataStream);
                ogreMaterialManager.parseScript(dataPtr, GlobalConsts.OgreGroup);
                dataPtr.Dispose();
                // HACK: Because of garbage collector the MemoryDataStream would be deleted a 2nd time,
                // but technically it was deleted from disposing DataStreamPtr earlier
                // thus we ban GC from attempting to collect dataStream object ever
                GC.SuppressFinalize(dataStream);
            }
        }

        var resultMaterial = ogreMaterialManager.getByName(materialName);
        resultMaterial.compile();

        var storedMaterial = new GeneratedMaterial
        {
            Material = resultMaterial,
            RenderPriority = renderPriority
        };
        materialToRenderPriorityMap.Add(materialName, storedMaterial);
        
        for (var i = 0; i < twinMaterial.Shaders.Count; i++)
        {
            MaterialManager.SetupMaterialPlainTexture(resultMaterial, twinMaterial.Shaders[i].TextureId, (uint)i);
        }
        
        return storedMaterial;
    }

    private static void StartBlock(StringBuilder materialText)
    {
        materialText.AppendLine("{");
    }

    private static void EndBlock(StringBuilder materialText)
    {
        materialText.AppendLine("}");
    }
}