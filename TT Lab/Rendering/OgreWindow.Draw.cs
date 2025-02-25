namespace TT_Lab.Rendering
{
    public partial class OgreWindow
    {
        //public void DrawBox(vec3 position)
        //{
        //    DrawBox(position, vec3.Zero, vec3.Ones);
        //}

        //public void DrawBox(vec3 position, vec3 rotation, vec3 scale)
        //{
        //    DrawBox(position, rotation, scale, vec4.Ones);
        //}

        //public void DrawBox(vec3 position, vec3 rotation, vec3 scale, vec4 color)
        //{
        //    if (scene == null)
        //    {
        //        return;
        //    }

        //    rotation = rotation * 3.14f / 180.0f;
        //    mat4 matrixPosition = mat4.Translate(position.x, position.y, position.z);
        //    mat4 matrixRotationX, matrixRotationY, matrixRotationZ;
        //    matrixRotationX = mat4.RotateX(rotation.x);
        //    matrixRotationY = mat4.RotateY(rotation.y);
        //    matrixRotationZ = mat4.RotateZ(rotation.z);
        //    mat4 matrixScale = mat4.Scale(scale);
        //    mat4 transform = scene.WorldTransform;

        //    transform *= matrixPosition;
        //    transform *= matrixRotationZ * matrixRotationY * matrixRotationX;
        //    transform *= matrixScale;
        //    DrawBox(transform, color);
        //}

        //public void DrawBox(mat4 transform, vec4 color)
        //{
        //    primitiveRenderer.DrawBox(transform, color);
        //}

        //public void DrawCircle(vec3 position)
        //{
        //    DrawCircle(position, vec3.Zero, vec3.Ones);
        //}

        //public void DrawCircle(vec3 position, vec3 rotation, vec3 scale)
        //{
        //    DrawCircle(position, rotation, scale, vec4.Ones);
        //}

        //public void DrawCircle(vec3 position, vec3 rotation, vec3 scale, vec4 color)
        //{
        //    if (scene == null)
        //    {
        //        return;
        //    }

        //    rotation = rotation * 3.14f / 180.0f;
        //    mat4 matrixPosition = mat4.Translate(position.x, position.y, position.z);
        //    mat4 matrixRotationX, matrixRotationY, matrixRotationZ;
        //    matrixRotationX = mat4.RotateX(rotation.x);
        //    matrixRotationY = mat4.RotateY(rotation.y);
        //    matrixRotationZ = mat4.RotateZ(rotation.z);
        //    mat4 matrixScale = mat4.Scale(scale);
        //    mat4 transform = scene.WorldTransform;

        //    transform *= matrixPosition;
        //    transform *= matrixRotationZ * matrixRotationY * matrixRotationX;
        //    transform *= matrixScale;
        //    DrawCircle(transform, color);
        //}

        //public void DrawCircle(mat4 transform, vec4 color)
        //{
        //    primitiveRenderer.DrawCircle(transform, color);
        //}

        //public void DrawLine(vec3 point1, vec3 point2, vec4 color)
        //{
        //    if (scene == null)
        //    {
        //        return;
        //    }

        //    DrawLine(point1, point2, color, scene.WorldTransform);
        //}

        //public void DrawLine(vec3 point1, vec3 point2, vec4 color, mat4 parent)
        //{
        //    var scaleX = (point2 - point1).Length;
        //    vec3 direction = (point2 - point1).Normalized;
        //    vec3 scale = new vec3(scaleX, 1.0f, 1.0f);
        //    mat4 matrixPosition = mat4.Translate(point1.x, point1.y, point1.z);
        //    mat4 matrixRotationX, matrixRotationY, matrixRotationZ;
        //    var angleXZ = (float)glm.Angle(new vec2(direction.x, direction.z));
        //    var angleY = glm.Acos(glm.Dot(direction, new vec3(direction.x, 0, direction.z)));
        //    matrixRotationX = mat4.RotateX(0);
        //    matrixRotationY = mat4.RotateY(angleXZ);
        //    matrixRotationZ = mat4.RotateZ(-angleY);

        //    mat4 transform = parent;
        //    transform *= matrixPosition;
        //    transform *= matrixRotationZ * matrixRotationY * matrixRotationX;
        //    transform *= mat4.Scale(scale);
        //    primitiveRenderer.DrawLine(transform, color);
        //}

        //public void DrawSimpleAxis(vec3 position)
        //{
        //    DrawSimpleAxis(position, vec3.Zero);
        //}

        //public void DrawSimpleAxis(vec3 position, vec3 rotation)
        //{
        //    DrawSimpleAxis(position, rotation, vec3.Ones);
        //}

        //public void DrawSimpleAxis(vec3 position, vec3 rotation, vec3 scale)
        //{
        //    rotation = rotation * 3.14f / 180.0f;
        //    mat4 matrixPosition = mat4.Translate(position.x, position.y, position.z);
        //    mat4 matrixRotationX, matrixRotationY, matrixRotationZ;
        //    matrixRotationX = mat4.RotateX(rotation.x);
        //    matrixRotationY = mat4.RotateY(rotation.y);
        //    matrixRotationZ = mat4.RotateZ(rotation.z);
        //    mat4 matrixScale = mat4.Scale(scale);
        //    mat4 transform = mat4.Identity;

        //    transform *= matrixPosition;
        //    transform *= matrixRotationZ * matrixRotationY * matrixRotationX;
        //    transform *= matrixScale;
        //    DrawSimpleAxis(transform);
        //}

        //public void DrawSimpleAxis(mat4 transform)
        //{
        //    primitiveRenderer.DrawSimpleAxis(transform);
        //}
    }
}
