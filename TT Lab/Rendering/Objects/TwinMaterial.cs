using System.Drawing;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Shaders;
using TT_Lab.ViewModels.Editors.Graphics;

namespace TT_Lab.Rendering.Objects
{
    public class TwinMaterial : IGLObject
    {
        private TextureBuffer _texture;
        private string _texName;
        private ShaderProgram _shader;
        private int SpecColA = 0;
        private int SpecColB = 0;
        private int SpecAlphaC = 0;
        private int SpecColD = 0;
        private int FIX = 0;
        private int texUnitPos = 0;
        private int AlphaBlending = 0;
        private int uniIndex = -1;

        public TwinMaterial(ShaderProgram program, string textureUniformName, Bitmap bitmap, ShaderViewModel viewModel, int texUnitPos, int uniIndex = -1)
        {
            this.uniIndex = uniIndex;
            _shader = program;
            _texName = textureUniformName;
            this.texUnitPos = texUnitPos;
            _texture = new TextureBuffer(bitmap.Width, bitmap.Height, bitmap);
            AlphaBlending = viewModel.AlphaBlending ? 1 : 0;
            FIX = viewModel.FixedAlphaValue;
            SpecColA = (int)viewModel.SpecOfColA;
            SpecColB = (int)viewModel.SpecOfColB;
            SpecAlphaC = (int)viewModel.SpecOfAlphaC;
            SpecColD = (int)viewModel.SpecOfColD;
        }

        public void Bind()
        {
            if (uniIndex != -1)
            {
                _shader.SetTextureUniform($"{_texName}[{uniIndex}]", OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, _texture.Buffer, (uint)texUnitPos);
                _shader.SetUniform1($"AlphaBlending[{uniIndex}]", AlphaBlending);
                _shader.SetUniform1($"FIX[{uniIndex}]", FIX / 255f);
                _shader.SetUniform1($"SpecColA[{uniIndex}]", SpecColA);
                _shader.SetUniform1($"SpecColB[{uniIndex}]", SpecColB);
                _shader.SetUniform1($"SpecAlphaC[{uniIndex}]", SpecAlphaC);
                _shader.SetUniform1($"SpecColD[{uniIndex}]", SpecColD);
            }
            else
            {
                _shader.SetTextureUniform($"{_texName}", OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, _texture.Buffer, (uint)texUnitPos);
                _shader.SetUniform1($"AlphaBlending", AlphaBlending);
                _shader.SetUniform1($"FIX", FIX / 255f);
                _shader.SetUniform1($"SpecColA", SpecColA);
                _shader.SetUniform1($"SpecColB", SpecColB);
                _shader.SetUniform1($"SpecAlphaC", SpecAlphaC);
                _shader.SetUniform1($"SpecColD", SpecColD);
            }
        }

        public void Delete()
        {
            _texture.Delete();
        }

        public void Unbind()
        {
            _texture.Unbind();
        }
    }
}
