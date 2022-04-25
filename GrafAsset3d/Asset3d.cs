using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafAsset3d
{
    internal class Asset3d
    {
        List<Vector3> _vertices = new List<Vector3>();
        List<uint> _indices = new List<uint>();
        float[] _verticesColor = new float[]
        {

        };
        float[] _verticesBez =
        {

        };
        uint[] _indikasi =
        {

        };
        int index;
        int[] _pascal = { };
        int _vertexBufferObject;
        int _vertexArrayObject;
        int _elementBufferObject;
        Shader _shader;
        Matrix4 _view;
        Matrix4 _projection;
        Matrix4 _model;
        public Vector3 _centerPosition;
        public List<Vector3> _euler;
        public List<Asset3d> Child;

        public Asset3d(List<Vector3> vertices, List<uint> indices)
        {
            _vertices = vertices;
            _indices = indices;
            setdefault();
        }
        public Asset3d(float[] _verticeses, uint[] indikasi)
        {
            _verticesBez = _verticeses;
            _indikasi = indikasi;
            index = 0;
            _vertices = new List<Vector3>();
            setdefault();
        }
        public Asset3d()
        {
            _vertices = new List<Vector3>();
            setdefault();
        }
        public void setdefault()
        {
            _euler = new List<Vector3>();
            //sumbu X
            _euler.Add(new Vector3(1, 0, 0));
            //sumbu y
            _euler.Add(new Vector3(0, 1, 0));
            //sumbu z
            _euler.Add(new Vector3(0, 0, 1));
            _model = Matrix4.Identity;
            _centerPosition = new Vector3(0.0f, 0.5f, 0.0f);
            _verticesColor = new float[] { 0.3f, 1.0f, 0.5f, 1.0f };
            Child = new List<Asset3d>();

        }

        public void setColor(float r, float g, float b, float o)
        {
            _verticesColor = new float[] { r, g, b, o };
        }

        public void load(string shadervert, string shaderfrag, float Size_x, float Size_y)
        {
            //Buffer
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count
                * Vector3.SizeInBytes, _vertices.ToArray(), BufferUsageHint.StaticDraw);

            //VAO
            //parameter 1 --> variable _vertices nya itu disimpan di shader index
            //keberapa?
            //parameter 2 --> didalam variable _vertices, ada berapa vertex?
            //paramter 3  --> jenis vertex yang dikirim typenya apa?
            //parameter 4 --> datanya perlu dinormalisasi ndak?
            //parameter 5 --> dalam 1 vertex/1 baris itu mengandung berapa banyak
            //titik?
            //parameter 6 --> data yang mau diolah mulai dari vertex ke berapa
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            //kalau mau bikin object settingannya beda dikasih if
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float,
                false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //ada data yang disimpan di _indices
            if (_indices.Count != 0)
            {
                _elementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Count
                    * sizeof(uint), _indices.ToArray(), BufferUsageHint.StaticDraw);
            }
            _shader = new Shader(shadervert, shaderfrag);
            _shader.Use();

            _view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);

            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Size_x / (float)Size_y, 0.1f, 100.0f);
            foreach (var item in Child)
            {
                item.load(shadervert, shaderfrag, Size_x, Size_y);
            }
        }
        public void render(int _lines, double time, Matrix4 temp, int _rot, float transx, float transy, float transz, Matrix4 camera_view, Matrix4 camera_projection, int timeBaru)
        {
            _shader.Use();
            GL.BindVertexArray(_vertexArrayObject);
            bool change = true;
            if (_rot == 0)
            {
                _model = _model * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(time));
            }
            else if (_rot == 1)
            {
                _model = _model * Matrix4.CreateTranslation(transx, transy, transz);
            }
            else if (_rot == 2)
            {
                _model = _model * Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(time));
            }
            else if (_rot == 3)
            {
                _model = _model * Matrix4.CreateTranslation(transx, transy, transz);
                _model = _model * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(time));
            }
            else if (_rot == 4)
            {
                _model = _model * Matrix4.CreateTranslation(transx, transy, transz);
                _model = _model * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(timeBaru));
            }
            /*_model = _model * Matrix4.CreateTranslation(0.0f, 0.001f, 0.0f);*/
            //_model = temp;

            _shader.SetMatrix4("model", _model);
            _shader.SetMatrix4("view", camera_view);
            _shader.SetMatrix4("projection", camera_projection);

            if (_indices.Count != 0)
            {
                GL.DrawElements(PrimitiveType.Triangles, _indices.Count, DrawElementsType.UnsignedInt, 0);
            }
            else
            {

                if (_lines == 0)
                {
                    GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Count);
                }
                else if (_lines == 1)
                {
                    GL.DrawArrays(PrimitiveType.TriangleFan, 0, _vertices.Count);
                }
                else if (_lines == 2)
                {

                }
                else if (_lines == 3)
                {
                    GL.DrawArrays(PrimitiveType.LineStrip, 0, _vertices.Count);
                }
                
            }
            foreach (var item in Child)
            {
                item.render(_lines, time, temp, _rot, transx, transy, transz, camera_view, camera_projection, timeBaru);
            }

            //get uniform from shader
            int vertexColorLocation = GL.GetUniformLocation(_shader.Handle, "ourColor");
            GL.Uniform4(vertexColorLocation, _verticesColor[0], _verticesColor[1], _verticesColor[2], _verticesColor[3]);
        }
        /*public void createBox()
        {
            //FACE
            //SEGITIGA BACK 1
            _vertices.Add(new Vector3(-0.5f, -0.5f, 0.5f));
            _vertices.Add(new Vector3(0.5f, -0.5f, 0.5f));
            _vertices.Add(new Vector3(0.5f, 0.5f, 0.5f));
            //SEGITIGA BACK 2
            _vertices.Add(new Vector3(0.5f, 0.5f, 0.5f));
            _vertices.Add(new Vector3(-0.5f, 0.5f, 0.5f));
            _vertices.Add(new Vector3(-0.5f, -0.5f, 0.5f));
            //LEFT FACE
            //SEGITIGA LEFT 1
            _vertices.Add(new Vector3(-0.5f, 0.5f, 0.5f));
            _vertices.Add(new Vector3(-0.5f, 0.5f, -0.5f));
            _vertices.Add(new Vector3(-0.5f, -0.5f, -0.5f));
            //SEGITIGA LEFT 2
            _vertices.Add(new Vector3(-0.5f, -0.5f, -0.5f));
            _vertices.Add(new Vector3(-0.5f, -0.5f, 0.5f));
            _vertices.Add(new Vector3(-0.5f, 0.5f, 0.5f));
            //RIGHT FACE
            //SEGITIGA RIGHT 1
            _vertices.Add(new Vector3(0.5f, 0.5f, 0.5f));
            _vertices.Add(new Vector3(0.5f, 0.5f, -0.5f));
            _vertices.Add(new Vector3(0.5f, -0.5f, -0.5f));
            //SEGITIGA LEFT 2
            _vertices.Add(new Vector3(0.5f, -0.5f, -0.5f));
            _vertices.Add(new Vector3(0.5f, -0.5f, 0.5f));
            _vertices.Add(new Vector3(0.5f, 0.5f, 0.5f));
            //BOTTOM FACE
            //SEGITIGA BOTTOM 1
            _vertices.Add(new Vector3(-0.5f, -0.5f, -0.5f));
            _vertices.Add(new Vector3(0.5f, -0.5f, -0.5f));
            _vertices.Add(new Vector3(0.5f, -0.5f, 0.5f));
            //SEGITIGA BOTTOM 2
            _vertices.Add(new Vector3(0.5f, -0.5f, 0.5f));
            _vertices.Add(new Vector3(-0.5f, -0.5f, 0.5f));
            _vertices.Add(new Vector3(-0.5f, -0.5f, -0.5f));
            //FRONT FACE
            //SEGITIGA BOTTOM 1
            _vertices.Add(new Vector3(-0.5f, 0.5f, -0.5f));
            _vertices.Add(new Vector3(0.5f, 0.5f, -0.5f));
            _vertices.Add(new Vector3(0.5f, 0.5f, 0.5f));
            //SEGITIGA BOTTOM 2
            _vertices.Add(new Vector3(0.5f, 0.5f, 0.5f));
            _vertices.Add(new Vector3(-0.5f, 0.5f, 0.5f));
            _vertices.Add(new Vector3(-0.5f, 0.5f, -0.5f));

        }*/
        public void createBoxVertices(float x, float y, float z, float lengthx, float lengthy, float lengthz, int degreesx, int degreesz, float transx, float transy, float transz)
        {
            _model = Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(degreesx));
            _model = _model * Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(degreesz));
            _model = _model * Matrix4.CreateTranslation(transx, transy, transz);
            _centerPosition.X = x;
            _centerPosition.Y = y;
            _centerPosition.Z = z;
            Vector3 temp_vector;

            //TITIK 1
            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 2
            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 3
            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 4
            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 5
            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 6
            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 7
            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 8
            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);

            _indices = new List<uint>
            {
                //SEGITIGA DEPAN 1
                0,1,2,
                //SEGITIGA DEPAN 2
                1,2,3,
                //SEGITIGA ATAS 1
                0,4,5,
                //SEGITIGA ATAS 2
                0,1,5,
                //SEGITIGA KANAN 1
                1,3,5,
                //SEGITIGA KANAN 2
                3,5,7,
                //SEGITIGA KIRI 1
                0,2,4,
                //SEGITIGA KIRI 2
                2,4,6,
                //SEGITIGA BELAKANG 1
                4,5,6,
                //SEGITIGA BELAKANG 2
                5,6,7,
                //SEGITIGA BAWAH 1
                2,3,6,
                //SEGITIGA BAWAH 2
                3,6,7
            };
        }
        public void createHalfEllipsoid(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, int degreesx, int degreesz, float transx, float transy, float transz)
        {
            _model = Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(degreesx));
            _model = _model * Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(degreesz));
            _model = _model * Matrix4.CreateTranslation(transx, transy, transz);
            _centerPosition.X = _x;
            _centerPosition.Y = _y;
            _centerPosition.Z = _z;
            float pi = (float)Math.PI;
            Vector3 temp_vector;
            for (float u = -pi; u <= pi; u += pi / 380) // u <= 1/2 * pi --> param buat setengah ellipsoid horizontal
            {
                for (float v = -pi / 2; v <= 1 / 2 * pi / 2; v += pi / 380) //v <= 1 / 2 * pi / 2 --> param buat setengah ellipsoid vertical
                {
                    temp_vector.X = _x + (float)Math.Cos(v) * (float)Math.Cos(u) * radiusX;
                    temp_vector.Y = _y + (float)Math.Cos(v) * (float)Math.Sin(u) * radiusY;
                    temp_vector.Z = _z + (float)Math.Sin(v) * radiusZ;
                    _vertices.Add(temp_vector);
                }
            }
        }
        public void createTorus(float center_x, float center_y, float center_z, float r1, float r2)
        {
            _centerPosition.X = center_x;
            _centerPosition.Y = center_y;
            _centerPosition.Z = center_z;

            float pi = (float)Math.PI;
            Vector3 temp_vector;

            for (float u = 0; u <= 2 * pi; u += pi / 380)
            {
                for (float v = 0; v <= 2 * pi; v += pi / 380)
                {
                    temp_vector.X = center_x + (r1 + r2 * (float)Math.Cos(v)) * (float)Math.Cos(u);
                    temp_vector.Y = center_y + (r1 + r2 * (float)Math.Cos(v)) * (float)Math.Sin(u);
                    temp_vector.Z = center_z + r2 * (float)Math.Sin(v);
                    _vertices.Add(temp_vector);

                }
            }
        }
        public void createEllipsoid(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, int degreesx, int degreesy, int degreesz, float transx, float transy, float transz)
        {
            _model = Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(degreesx));
            _model = _model * Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(degreesz));
            _model = _model * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(degreesy));
            _model = _model * Matrix4.CreateTranslation(transx, transy, transz);
            _centerPosition.X = _x;
            _centerPosition.Y = _y;
            _centerPosition.Z = _z;
            float pi = (float)Math.PI;
            Vector3 temp_vector;
            for (float u = -pi; u <= pi; u += pi / 300)
            {
                for (float v = -pi / 2; v <= pi / 2; v += pi / 300)
                {
                    temp_vector.X = _x + (float)Math.Cos(v) * (float)Math.Cos(u) * radiusX;
                    temp_vector.Y = _y + (float)Math.Cos(v) * (float)Math.Sin(u) * radiusY;
                    temp_vector.Z = _z + (float)Math.Sin(v) * radiusZ;
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void createHalfBall(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, int degreesx, int degreesz, float transx, float transy, float transz)
        {
            _model = Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(degreesx));
            _model = _model * Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(degreesz));
            _model = _model * Matrix4.CreateTranslation(transx, transy, transz);
            _centerPosition.X = _x;
            _centerPosition.Y = _y;
            _centerPosition.Z = _z;
            float pi = (float)Math.PI;
            Vector3 temp_vector;
            for (float u = -pi; u <= 1 / 2 * pi; u += pi / 380)
            {
                for (float v = -pi / 2; v <= pi / 2; v += pi / 380)
                {
                    temp_vector.X = _x + (float)Math.Cos(v) * (float)Math.Cos(u) * radiusX;
                    temp_vector.Y = _y + (float)Math.Cos(v) * (float)Math.Sin(u) * radiusY;
                    temp_vector.Z = _z + (float)Math.Sin(v) * radiusZ;
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void createEllipsoid2(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, int sectorCount, int stackCount)
        {
            _centerPosition.X = _x;
            _centerPosition.Y = _y;
            _centerPosition.Z = _z;
            float pi = (float)Math.PI;
            Vector3 temp_vector;
            float sectorStep = 2 * (float)Math.PI / sectorCount;
            float stackStep = (float)Math.PI / stackCount;
            float sectorAngle, StackAngle, x, y, z;

            for (int i = 0; i <= stackCount; ++i)
            {
                StackAngle = pi / 2 - i * stackStep;
                x = radiusX * (float)Math.Cos(StackAngle);
                y = radiusY * (float)Math.Cos(StackAngle);
                z = radiusZ * (float)Math.Sin(StackAngle);

                for (int j = 0; j <= sectorCount; ++j)
                {
                    sectorAngle = j * sectorStep;

                    temp_vector.X = x * (float)Math.Cos(sectorAngle);
                    temp_vector.Y = y * (float)Math.Sin(sectorAngle);
                    temp_vector.Z = z;
                    _vertices.Add(temp_vector);
                }
            }

            uint k1, k2;
            for (int i = 0; i < stackCount; ++i)
            {
                k1 = (uint)(i * (sectorCount + 1));
                k2 = (uint)(k1 + sectorCount + 1);
                for (int j = 0; j < sectorCount; ++j, ++k1, ++k2)
                {
                    if (i != 0)
                    {
                        _indices.Add(k1);
                        _indices.Add(k2);
                        _indices.Add(k1 + 1);
                    }
                    if (i != (stackCount - 1))
                    {
                        _indices.Add(k1 + 1);
                        _indices.Add(k2);
                        _indices.Add(k2 + 1);
                    }
                }
            }
        }

        public void createHyperboloid(float _positionX, float _positionY, float _positionZ, float _radius)
        {
            Vector3 temp_vector; float _pi = (float)Math.PI; 
            for (float v = -_pi / 2; v <= _pi / 2; v += 0.01f)
            {
                for (float u = -_pi; u <= _pi; u += (_pi / 30))
                {
                    temp_vector.X = _positionX + _radius * (1 / (float)Math.Cos(v)) * (float)Math.Cos(u); 
                    temp_vector.Y = _positionY + _radius * (1 / (float)Math.Cos(v)) * (float)Math.Sin(u); 
                    temp_vector.Z = _positionZ + _radius * (float)Math.Tan(v);
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void tabung(float panjang, float _positionX, float _positionY, float _positionZ, float _radiusx, float _radiusy, float _radiusz, int degreesx, int degressy, int degreesz, float transx, float transy, float transz)
        {
            _model = Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(degreesx));
            _model = _model * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(degressy));
            _model = _model * Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(degreesz));
            _model = _model * Matrix4.CreateTranslation(transx, transy, transz);
            Vector3 temp_vector; float _pi = (float)Math.PI;
            for (float v = panjang; v <= _pi / 2; v += 0.01f)
            {
                for (float u = panjang; u <= _pi; u += (_pi / 30))
                {
                    temp_vector.X = _positionX + _radiusx * (float)Math.Cos(u); //ganti ke v jadi kumis kucing
                    temp_vector.Y = _positionY + _radiusy * (float)Math.Sin(u); //ganti ke v jadi kumis kucing
                    temp_vector.Z = _positionZ + _radiusz * v;
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void createElipCone(float _positionX, float _positionY, float _positionZ, float _radius, int degreesx, int degreesz, float transx, float transy, float transz)
        {
            _model = Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(degreesx));
            _model = _model * Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(degreesz));
            _model = _model * Matrix4.CreateTranslation(transx, transy, transz);
            Vector3 temp_vector; float _pi = (float)Math.PI;
            for (float v = 0.0f; v <= 0.5f; v += 0.1f)
            {
                for (float u = -_pi; u <= _pi; u += (_pi / 300))
                {
                    temp_vector.X = _positionX + _radius * -0.4f *  (float)Math.Cos(u) * v;
                    temp_vector.Y = _positionY + _radius * -0.4f * (float)Math.Sin(u) * v;
                    temp_vector.Z = _positionZ + _radius * v;
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void createBall(float _positionX, float _positionY, float _positionZ, float radius, int degreesx, int degreesz, float transx, float transy, float transz)
        {
            _model = Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(degreesx));
            _model = _model * Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(degreesz));
            _model = _model * Matrix4.CreateTranslation(transx, transy, transz);
            _centerPosition.X = _positionX;
            _centerPosition.Y = _positionY;
            _centerPosition.Z = _positionZ;
            float pi = (float)Math.PI;
            Vector3 temp_vector;
            for (float u = -pi; u <= pi; u += pi / 300)
            {
                for (float v = -pi; v <= pi; v += pi / 300)
                {
                    temp_vector.X = _positionX + (float)Math.Cos(v) * (float)Math.Cos(u) * radius;
                    temp_vector.Y = _positionY + (float)Math.Cos(v) * (float)Math.Sin(u) * radius;
                    temp_vector.Z = _positionZ + (float)Math.Sin(v) * radius;
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void createElipParaboloid(float panjang, float _positionX, float _positionY, float _positionZ, float _radiusX, float _radiusY, float _radiusZ, int degreesx, int degreesz, float transx, float transy, float transz)
        {
            _model = Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(degreesx));
            _model = _model * Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(degreesz));
            _model = _model * Matrix4.CreateTranslation(transx, transy, transz);

            float _pi = (float)Math.PI;

            for (float v = panjang; v <= 0.4f; v += _pi / 500)
            {
                for (float u = -_pi; u <= _pi; u += _pi / 300)
                {
                
                    Vector3 vec;
                    vec.X = (float)Math.Cos(u) * v * _radiusX + _positionX;
                    vec.Y = (float)Math.Sin(u) * v * _radiusY + _positionY;
                    vec.Z = v * v + _positionZ;
                    _vertices.Add(vec);
                }
            }
        }

        public void createHyperParaboloid()
        {
            float _positionX = -0.5f;
            float _positionY = 0.0f;
            float _positionZ = 0.0f;

            float _radiusX = 0.3f;
            float _radiusY = 0.3f;
            float _radiusZ = 0.3f;
            float _pi = 3.14159f;

            for (float u = -_pi; u < _pi; u += _pi / 30)
            {
                for (float v = 0; v < _pi; v += _pi / 30)
                {
                    Vector3 vec;
                    vec.X = (float)Math.Tan(u) * v * _radiusX + _positionX;
                    vec.Y = (1.0f / (float)Math.Cos(u)) * v * _radiusY + _positionY;
                    vec.Z = v * v + _positionZ;
                    _vertices.Add(vec);
                }
            }
        }
        public void createquarterEllipsoid(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, int degreesx, int degreesy, int degreesz, float transx, float transy, float transz)
        {
            _model = Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(degreesx));
            _model = _model * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(degreesy));
            _model = _model * Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(degreesz));
            _model = _model * Matrix4.CreateTranslation(transx, transy, transz);
            _centerPosition.X = _x;
            _centerPosition.Y = _y;
            _centerPosition.Z = _z;
            float pi = (float)Math.PI;
            Vector3 temp_vector;
            for (float u = -pi; u <= 1 / 4 * pi; u += pi / 380)
            {
                for (float v = -pi / 2; v <= 1 / 2 * pi / 2; v += pi / 380)
                {
                    temp_vector.X = _x + (float)Math.Cos(v) * (float)Math.Cos(u) * radiusX;
                    temp_vector.Y = _y + (float)Math.Cos(v) * (float)Math.Sin(u) * radiusY;
                    temp_vector.Z = _z + (float)Math.Sin(v) * radiusZ;
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void createElipCone2(float _positionX, float _positionY, float _positionZ, float _radius, int degreesx, int degreesz, float transx, float transy, float transz)
        {
            _model = Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(degreesx));
            _model = _model * Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(degreesz));
            _model = _model * Matrix4.CreateTranslation(transx, transy, transz);
            Vector3 temp_vector; float _pi = (float)Math.PI;
            for (float v = 0.0f; v <= 0.5f; v += 0.1f)
            {
                for (float u = -_pi; u <= _pi; u += (_pi / 300))
                {
                    temp_vector.X = _positionX + _radius * 1.1f * (float)Math.Cos(u) * v;
                    temp_vector.Y = _positionY + _radius * (float)Math.Sin(u) * v;
                    temp_vector.Z = _positionZ + _radius * 1.4f * v;
                    _vertices.Add(temp_vector);
                }
            }
        }
        public void CreateHyperboloid2SheetsVertices_A()
        {
            float _positionX = -0.5f;
            float _positionY = 0.0f;
            float _positionZ = 0.0f;

            float _radiusX = 0.3f;
            float _radiusY = 0.3f;
            float _radiusZ = 0.3f;
            float _pi = 3.14159f;

            //sisi A
            for (float u = -_pi / 2; u < _pi / 2; u += _pi / 30)
            {
                for (float v = -_pi / 2; v < _pi / 2; v += _pi / 30)
                {
                    Vector3 vec;
                    vec.X = (float)Math.Tan(v) * (float)Math.Cos(u) * _radiusX + _positionX;
                    vec.Y = (float)Math.Tan(v) * (float)Math.Sin(u) * _radiusY + _positionY;
                    vec.Z = (1.0f / (float)Math.Cos(v)) * _radiusZ + _positionZ;
                    _vertices.Add(vec);
                }
            }

        }
        public void CreateHyperboloid2SheetsVertices_B()
        {
            float _positionX = -0.5f;
            float _positionY = 0.0f;
            float _positionZ = 0.0f;

            float _radiusX = 0.3f;
            float _radiusY = 0.3f;
            float _radiusZ = 0.3f;
            float _pi = 3.14159f;

            //sisi B
            for (float u = -_pi / 2; u < _pi / 2; u += _pi / 30)
            {
                for (float v = _pi / 2; v < 3 * _pi / 2; v += _pi / 30)
                {
                    Vector3 vec;
                    vec.X = (float)Math.Tan(v) * (float)Math.Cos(u) * _radiusX + _positionX;
                    vec.Y = (float)Math.Tan(v) * (float)Math.Sin(u) * _radiusY + _positionY;
                    vec.Z = (1.0f / (float)Math.Cos(v)) * _radiusZ + _positionZ;
                    _vertices.Add(vec);
                }
            }


        }
        //Bezier
        public void setTitikBezier(float _x, float _y, float _z)
        {
            //x
            _verticesBez[index * 3] = _x;
            //y
            _verticesBez[index * 3 + 1] = _y;
            //z
            _verticesBez[index * 3 + 2] = _z;
            index++;

            GL.BufferData(BufferTarget.ArrayBuffer, _verticesBez.Length * sizeof(float), _verticesBez, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        }

        public List<int> getRow(int rowIndex)
        {
            List<int> currow = new List<int>();
            //------
            currow.Add(1);
            if (rowIndex == 0)
            {
                return currow;
            }
            //-----
            List<int> prev = getRow(rowIndex - 1);
            for (int i = 1; i < prev.Count; i++)
            {
                int curr = prev[i - 1] + prev[i];
                currow.Add(curr);
            }
            currow.Add(1);
            return currow;
        }

        public List<Vector3> CreateCurveBezier()
        {
            List<Vector3> _vertices_bezier = new List<Vector3>();
            List<int> pascal = getRow(index - 1); //Berdasarkan jumlah titik yang di klik
            _pascal = pascal.ToArray();
            for (float t = 0; t <= 1.0f; t += 0.01f)
            {
                Vector3 p = getP(index, t);
                _vertices_bezier.Add(p);
            }
            return _vertices_bezier;

        }

        public Vector3 getP(int n, float t)
        {
            Vector3 p = new Vector3(0, 0, 0); //0,0,0 ini start
            float k;
            for (int i = 0; i < n; i++)
            {
                k = (float)Math.Pow((1 - t), n - 1 - i) * (float)Math.Pow(t, i) * _pascal[i];
                p.X += k * _verticesBez[i * 3];
                p.Y += k * _verticesBez[i * 3 + 1];
                p.Z += k * _verticesBez[i * 3 + 2];
            }
            return p;
        }

        public bool getVerticesLength()
        {
            if (_verticesBez[0] == 0)
            {
                return false;
            }
            if ((_verticesBez.Length + 1) / 3 > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void setVertices(List<Vector3> vertices)
        {
            _vertices = vertices;
        }

        public void addChild(float x, float y, float z, float lengthx, float lengthy, float lengthz, int degreesx, int degreesz, float transx, float transy, float transz)
        {
            Asset3d newChild = new Asset3d();
            newChild.createBoxVertices(x, y, z, lengthx, lengthy, lengthz, degreesx, degreesz, transx, transy, transz);
            Child.Add(newChild);
        }
    }
}
