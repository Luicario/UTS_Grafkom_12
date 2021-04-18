using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OpenTK.Graphics.OpenGL4;
using LearnOpenTK.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System.Globalization;

namespace GrafKom_C_Sharp
{
    class Mesh
    {
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> textureVertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<uint> vertexIndices = new List<uint>();
        int _vbo;
        int _ebo;
        int _vao;
        Shader _shader;
        Matrix4 transform;
        List<Mesh> child = new List<Mesh>();
        // Universal Function
        public Mesh()
        {

        }

        public void AddChild(Mesh i) {
            child.Add(i);
        }

        public Mesh GetChild(int index) {
            return child[index];
        }

        public List<Vector3> GetVertices()
        {
            return vertices;
        }
        public List<uint> GetVertexIndices()
        {
            return vertexIndices;
        }

        public void SetVertexIndices(List<uint> temp)
        {
            vertexIndices = temp;
        }

        public int GetVertexBufferObject()
        {
            return _vbo;
        }

        public int GetElementBufferObject()
        {
            return _ebo;
        }

        public int GetVertexArrayObject()
        {
            return _vao;
        }

        public Shader GetShader()
        {
            return _shader;
        }

        public Matrix4 GetTransform()
        {
            return transform;
        }

        public void Rotate(float rx, float ry, float rz)
        {
            transform = transform * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rx));
            transform = transform * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(ry));
            transform = transform * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rz));

            foreach (var meshobj in child)
            {
                meshobj.Rotate(rx, ry, rz);
            }
        }
        public void Scale(float scale)
        {
            transform = transform * Matrix4.CreateScale(scale);

            foreach (var meshobj in child)
            {
                meshobj.Scale(scale);
            }
        }

        public void Translate(float tx, float ty, float tz)
        {
            transform = transform * Matrix4.CreateTranslation(tx, ty, tz);

            foreach (var meshobj in child)
            {
                meshobj.Translate(tx, ty, tz);
            }
        }

        public void LoadObjectFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Unable to open \"" + path + "\", does not exist.");
            }

            using (StreamReader streamReader = new StreamReader(path))
            {
                while (!streamReader.EndOfStream)
                {
                    List<string> words = new List<string>(streamReader.ReadLine().ToLower().Split(' '));
                    words.RemoveAll(s => s == string.Empty);

                    if (words.Count == 0)
                        continue;

                    string type = words[0];
                    words.RemoveAt(0);


                    switch (type)
                    {
                        case "v":
                            // https://stackoverflow.com/questions/11202673/converting-string-to-float-in-c-sharp/11203062
                            // https://stackoverflow.com/questions/16657090/why-does-float-parse-return-wrong-value
                            vertices.Add(new Vector3((float.Parse(words[0], CultureInfo.InvariantCulture.NumberFormat) / 5),
                                (float.Parse(words[1], CultureInfo.InvariantCulture.NumberFormat) / 5),
                                (float.Parse(words[2], CultureInfo.InvariantCulture.NumberFormat) / 5)));
                            break;

                        case "vt":
                            textureVertices.Add(new Vector3(float.Parse(words[0]), float.Parse(words[1]),
                                                            words.Count < 3 ? 0 : float.Parse(words[2])));
                            break;

                        case "vn":
                            normals.Add(new Vector3(float.Parse(words[0]), float.Parse(words[1]), float.Parse(words[2])));
                            break;

                        case "f":
                            foreach (string w in words)
                            {
                                if (w.Length == 0)
                                    continue;

                                string[] comps = w.Split('/');

                                vertexIndices.Add(uint.Parse(comps[0]) - 1);
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        public void SetupObject(string vert, string frag)
        {
            transform = Matrix4.Identity;

            // box
            _vbo = GL.GenBuffer(); // vertex buffer object
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer,
                vertices.Count * Vector3.SizeInBytes,
                vertices.ToArray(),
                BufferUsageHint.StaticDraw);

            _vao = GL.GenVertexArray(); // vertex array object
            GL.BindVertexArray(_vao);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float,
                false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            _ebo = GL.GenBuffer(); // element buffer object
            GL.BindBuffer(BufferTarget.ElementArrayBuffer,
                _ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer,
                vertexIndices.Count * sizeof(uint),
                vertexIndices.ToArray(), BufferUsageHint.StaticDraw);

            _shader = new Shader(vert, frag);
            _shader.Use();

            //PrintVertexIndeces();
        }

        public void Render()
        {
            _shader.Use();
            _shader.SetMatrix4("transform", transform);
            GL.BindVertexArray(_vao);
            GL.DrawElements(PrimitiveType.Triangles,
                vertexIndices.Count,
                DrawElementsType.UnsignedInt, 0);

            foreach (var meshobj in child)
            {
                meshobj.Render();
            }
        }

        public void PrintVertexIndeces()
        {
            Console.Write("Vertex Indices: ");
            foreach (uint i in vertexIndices)
            {
                Console.Write(i + " ");
            }
            Console.Write('\n');
        }

        public void PrintVertices()
        {
            Console.Write("\nVertices: \n");
            foreach (Vector3 i in vertices)
            {
                Console.Write(i.X + " ");
            }
            Console.Write('\n');
            foreach (Vector3 i in vertices)
            {
                Console.Write(i.Y + " ");
            }
            Console.Write('\n');
            foreach (Vector3 i in vertices)
            {
                Console.Write(i.Z + " ");
            }
            Console.Write('\n');
        }

        // Shape Specific Function
        // Box
        public void CreateBoxVertices(float x, float y, float z, float len)
        {
            float _midX = x;
            float _midY = y;
            float _midZ = z;
            float _boxLength = len;

            Vector3 temp;

            // point 1
            temp.X = _midX - _boxLength / 2.0f;
            temp.Y = _midY + _boxLength / 2.0f;
            temp.Z = _midZ - _boxLength / 2.0f;
            vertices.Add(temp);

            // point 2
            temp.X = _midX + _boxLength / 2.0f;
            temp.Y = _midY + _boxLength / 2.0f;
            temp.Z = _midZ - _boxLength / 2.0f;
            vertices.Add(temp);

            // point 3
            temp.X = _midX - _boxLength / 2.0f;
            temp.Y = _midY - _boxLength / 2.0f;
            temp.Z = _midZ - _boxLength / 2.0f;
            vertices.Add(temp);

            // point 4
            temp.X = _midX + _boxLength / 2.0f;
            temp.Y = _midY - _boxLength / 2.0f;
            temp.Z = _midZ - _boxLength / 2.0f;
            vertices.Add(temp);

            // point 5
            temp.X = _midX - _boxLength / 2.0f;
            temp.Y = _midY + _boxLength / 2.0f;
            temp.Z = _midZ + _boxLength / 2.0f;
            vertices.Add(temp);

            // point 6
            temp.X = _midX + _boxLength / 2.0f;
            temp.Y = _midY + _boxLength / 2.0f;
            temp.Z = _midZ + _boxLength / 2.0f;
            vertices.Add(temp);

            // point 7
            temp.X = _midX - _boxLength / 2.0f;
            temp.Y = _midY - _boxLength / 2.0f;
            temp.Z = _midZ + _boxLength / 2.0f;
            vertices.Add(temp);

            // point 8
            temp.X = _midX + _boxLength / 2.0f;
            temp.Y = _midY - _boxLength / 2.0f;
            temp.Z = _midZ + _boxLength / 2.0f;
            vertices.Add(temp);

            vertexIndices = new List<uint>{
                // Segitiga Depan 1
                0, 1, 2,
                // Segitiga Depan 2
                1, 2, 3,
                // Segitiga Atas 1
                0, 4, 5,
                // Segitiga Atas 2
                0, 1, 5,
                // Segitiga Kanan 1
                1, 3, 5,
                // Segitiga Kanan 2
                3, 5, 7,
                // Segitiga Kiri 1
                0, 2, 4,
                // Segitiga Kiri 2
                2, 4, 6,
                // Segitiga Belakang 1
                4, 5, 6,
                // Segitiga Belakang 2
                5, 6, 7,
                // Segitiga Bawah 1
                2, 3, 6,
                // Segitiga Bawah 2
                3, 6, 7
            };
        }

    }
}