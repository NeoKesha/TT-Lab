using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TT_Lab.Rendering.Shaders
{
    public static class ShaderStorage
    {
        private static readonly Dictionary<StoredVertexShaders, Shader> compiledVertexShaders = new();
        private static readonly Dictionary<StoredFragmentShaders, Shader> compiledFragmentShaders = new();
        private static readonly Dictionary<LibraryVertexShaders, Shader> compiledLibraryVertexShaders = new();
        private static readonly Dictionary<LibraryFragmentShaders, Shader> compiledLibraryFragmentShaders = new();
        private static readonly Dictionary<String, ShaderProgram> compiledPrograms = new();
        private static bool cacheBuilt = false;

        public static void BuildShaderCache()
        {
            if (cacheBuilt) return;

            Log.WriteLine("Compiling shaders...");

            var sPath = "Shaders\\";
            var mPath = $"{sPath}Materials\\";
            var textLoader = Util.ManifestResourceLoader.LoadTextFile;

            // Library fragment shaders
            var texScrollShader = new Shader(ShaderType.FragmentShader, textLoader($"{mPath}TexScroll.frag"));
            compiledLibraryFragmentShaders.Add(LibraryFragmentShaders.TexScroll, texScrollShader);
            var lightShader = new Shader(ShaderType.FragmentShader, textLoader($"{sPath}Light.frag"));
            compiledLibraryFragmentShaders.Add(LibraryFragmentShaders.Light, lightShader);
            var passShader = new Shader(ShaderType.FragmentShader, textLoader($"{sPath}Pass.frag"));
            compiledLibraryFragmentShaders.Add(LibraryFragmentShaders.Pass, passShader);
            var texturePassShader = new Shader(ShaderType.FragmentShader, textLoader($"{sPath}TexturePass.Frag"));
            compiledLibraryFragmentShaders.Add(LibraryFragmentShaders.TexturePass, texturePassShader);
            var twinmaterialPassShader = new Shader(ShaderType.FragmentShader, textLoader($"{sPath}TwinmaterialPass.frag"));
            compiledLibraryFragmentShaders.Add(LibraryFragmentShaders.TwinmaterialPass, twinmaterialPassShader);

            // Library vertex shaders
            var vertexShadingShader = new Shader(ShaderType.VertexShader, textLoader($"{sPath}VertexShading.vert"));
            compiledLibraryVertexShaders.Add(LibraryVertexShaders.VertexShading, vertexShadingShader);
            var texScrollVertShader = new Shader(ShaderType.VertexShader, textLoader($"{mPath}TexScroll.vert"));
            compiledLibraryVertexShaders.Add(LibraryVertexShaders.TexScroll, texScrollVertShader);

            // Fragment shaders
            var modelTexturedShader = new Shader(ShaderType.FragmentShader, textLoader($"{sPath}ModelTextured.frag"));
            compiledFragmentShaders.Add(StoredFragmentShaders.ModelTextured, modelTexturedShader);
            var wboitBlendShader = new Shader(ShaderType.FragmentShader, textLoader($"{sPath}WBOIT_blend.frag"));
            compiledFragmentShaders.Add(StoredFragmentShaders.WboitBlend, wboitBlendShader);
            var wboitScreenResultShader = new Shader(ShaderType.FragmentShader, textLoader($"{sPath}WBOIT_ScreenResult.frag"));
            compiledFragmentShaders.Add(StoredFragmentShaders.WboitScreenResult, wboitScreenResultShader);

            // Vertex shaders
            var modelRenderShader = new Shader(ShaderType.VertexShader, textLoader($"{sPath}ModelRender.vert"));
            compiledVertexShaders.Add(StoredVertexShaders.ModelRender, modelRenderShader);
            var wboitBlendVertShader = new Shader(ShaderType.VertexShader, textLoader($"{sPath}WBOIT_blend.vert"));
            compiledVertexShaders.Add(StoredVertexShaders.WboitBlend, wboitBlendVertShader);
            var wboitScreenResultVertShader = new Shader(ShaderType.VertexShader, textLoader($"{sPath}WBOIT_ScreenResult.vert"));
            compiledVertexShaders.Add(StoredVertexShaders.WboitScreenResult, wboitScreenResultVertShader);

            cacheBuilt = true;

            Log.WriteLine("Compiled shaders...");
        }

        public static void ClearShaderCache()
        {
            compiledPrograms.ToList().ForEach(kv => kv.Value.Delete());
            compiledPrograms.Clear();
            compiledVertexShaders.Clear();
            compiledFragmentShaders.Clear();
            compiledLibraryFragmentShaders.Clear();
            compiledLibraryVertexShaders.Clear();
        }

        public static ShaderProgram BuildShaderProgram(StoredVertexShaders vertex, StoredFragmentShaders fragment, LibraryVertexShaders libraryVertex = LibraryVertexShaders.VertexShading, LibraryFragmentShaders libraryFragment = LibraryFragmentShaders.Pass)
        {
            var resultProgramName = $"{vertex}{fragment}{libraryVertex}{libraryFragment}";
            if (compiledPrograms.TryGetValue(resultProgramName, out ShaderProgram? shaderProgram))
            {
                return shaderProgram;
            }

            var program = new ShaderProgram(compiledVertexShaders[vertex], compiledFragmentShaders[fragment], compiledLibraryVertexShaders[libraryVertex], compiledLibraryFragmentShaders[libraryFragment]);
            compiledPrograms.Add(resultProgramName, program);

            return program;
        }

        public enum StoredVertexShaders
        {
            ModelRender,
            WboitBlend,
            WboitScreenResult
        }

        public enum StoredFragmentShaders
        {
            ModelTextured,
            WboitBlend,
            WboitScreenResult
        }

        public enum LibraryVertexShaders
        {
            VertexShading,
            TexScroll
        }

        public enum LibraryFragmentShaders
        {
            Pass,
            TexturePass,
            TexScroll,
            Light,
            TwinmaterialPass
        }
    }
}
