using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using LearnOpenTK.Common;

namespace GrafAsset3d
{
    internal class Window3d : GameWindow
    {
        Asset3d[] asset = new Asset3d[30];
        Asset3d[] terrain = new Asset3d[35];
        Asset3d[] langit = new Asset3d[2];
        Asset3d[] laut = new Asset3d[1];
        Asset3d[] rumah = new Asset3d[20];
        Asset3d[] objectBez = new Asset3d[4];
        Asset3d[] Poliwag = new Asset3d[20];
        Asset3d[] poliwag2 = new Asset3d[2];
        Asset3d[] snorlax = new Asset3d[31];
        /*Asset2d[] asset2 = new Asset2d[6];*/
        float deg = 0;
        double _time = 0;
        float _time2 = 0.0f;
        float _time3 = 0.0f;
        int _time4 = 0;
        float _time5 = 0;
        float _time6 = 0;
        float transZs = 0;
        float transYs = 0;
        float _timeTemp = 0.0f;
        bool change = true;
        Camera camera;
        bool _firstMove = true;
        Vector2 _lastPos;
        Vector3 _objecPost = new Vector3(0.0f, 0.0f, 0.0f);
        float _rotationSpeed = 1f;
        float global_i = 0.0f;
        float timeX = 0.0f;
        float timeY = 0.0f;
        float timeZ = 0.0f;
        float walkTime = 0.0f;
        float swimTime = 0.0f;
        bool changeDir = false;
        bool sleep = true;
        bool changeDirxz = false;
        bool day = true;
        bool awal = true;
        bool swim = true;
       /* float r = 0.0f;*/
        float g = 0.7f;
        float b = 1.0f;
        public Window3d(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            for (int i = 0; i < asset.Length; i++)
            {
                asset[i] = new Asset3d();
            }
            for (int i = 0; i < terrain.Length; i++)
            {
                terrain[i] = new Asset3d();
            }
            for (int i = 0; i < rumah.Length; i++)
            {
                rumah[i] = new Asset3d();
            }
            for (int i = 0; i < langit.Length; i++)
            {
                langit[i] = new Asset3d();
            }
            for (int i = 0; i < Poliwag.Length; i++)
            {
                Poliwag[i] = new Asset3d();
            }
            for (int i = 0; i < snorlax.Length; i++)
            {
                snorlax[i] = new Asset3d();
            }
            laut[0] = new Asset3d();
           
            /*for (int i = 0; i < asset2.Length; i++)
            {
                asset2[i] = new Asset2d(new float[] { }, new uint[] { });
            }*/
            /*asset[0].createElipParaboloid();*/
            /* asset[1] = new Asset3d();*/
            /*asset[0].CreateHyperboloid2SheetsVertices_A();*/
            /*asset[1].CreateHyperboloid2SheetsVertices_B();*/

            /*asset[0].tabung(0.5f, 0.0f, 0.0f, 0.037f, 0.037f, 0.037f);*/
            /*asset[0].createHyperParaboloid();*/
            /*asset[0].createHyperboloid(0.1f, 0.1f, -0.1f, 0.01f);*/
            /*asset[0].createBoxVertices(0.0f, 0.5f, 0.2f, 1.0f);*/
            /*asset[0].createEllipsoid2(0.2f, 0.2f, 0.3f, 0.0f, 0.0f, 0.0f, 30, 30);*/


            //Leher
            asset[0].tabung(-3.0f, -0.2f, 0.8f, 0.0f, 0.056f, 0.056f, 0.027f, 115, 0, 0, 0.0f, 1.2f, -0.55f);
            asset[0].setColor(1.0f, 0.7f, 0.9f, 1.0f);

            //Perut Mew
            asset[1].createBall(-0.2f, 0.8f, 0.15f, 0.08f, 0, 0, 0.0f, 0.0f, 0.0f);
            asset[1].setColor(1.0f, 0.7f, 0.9f, 1.0f);

            //tangan
            asset[2].createElipParaboloid(0.0f, 0.0f, -1.0f, 0.0f, 0.06f, 0.06f, 0.03f, -110, 0, -0.16f, 0.4f, -0.7f);
            asset[2].setColor(1.0f, 0.7f, 0.9f, 1.0f);
            asset[3].createElipParaboloid(0.0f, -0.3f, -1.0f, 0.0f, 0.06f, 0.06f, 0.03f, -110, 0, 0.06f, 0.4f, -0.7f);
            asset[3].setColor(1.0f, 0.7f, 0.9f, 1.0f);

            //Kepala Mew
            asset[4].createBall(-0.2f, 1.0f, 0.15f, 0.07f, 0, 0, 0.0f, -0.077f, 0.05f);
            asset[4].setColor(1.0f, 0.7f, 0.9f, 1.0f);

            //Kuping Mew
            asset[5].createElipCone(-0.48f, 0.2f, -0.614f, 0.1f, 90, -15, 0.15f, 0.3f, 0.0f);
            asset[5].setColor(1.0f, 0.7f, 0.9f, 1.0f);
            asset[6].createElipCone(0.1f, 0.2f, -0.724f, 0.1f, 90, 15, -0.15f, 0.3f, 0.0f);
            asset[6].setColor(1.0f, 0.7f, 0.9f, 1.0f);

            //Ujung Ekor Mew
            asset[7].createElipParaboloid(0.0f, -0.2f, 0.8f, -0.4f, 0.05f, 0.05f, 0.3f, 0, 0, 0.0f, 0.0f, 0.0f);
            asset[7].setColor(1.0f, 0.7f, 0.9f, 1.0f);

            //tulang ekor
            asset[8].tabung(-10.0f, -0.2f, 0.8f, 0.1f, 0.017f, 0.017f, 0.037f, 0, 0, 0, 0.0f, 0.0f, 0.0f);
            asset[8].setColor(1.0f, 0.7f, 0.9f, 1.0f);

            //Paha
            asset[9].createElipParaboloid(0.0f, -0.17f, -0.1f, 0.65f, 0.06f, 0.06f, 0.03f, -90, 0, 0.0f, 0.0f, 0.0f);
            asset[9].setColor(1.0f, 0.7f, 0.9f, 1.0f);
            asset[10].createElipParaboloid(0.0f, -0.23f, -0.1f, 0.65f, 0.06f, 0.06f, 0.03f, -90, 0, 0.0f, 0.0f, 0.0f);
            asset[10].setColor(1.0f, 0.7f, 0.9f, 1.0f);

            //Mata
            asset[11].createEllipsoid(0.02f, 0.03f, 0.03f, -0.17f, 0.95f, 0.23f, 0, 0, 0, 0.0f, 0.0f, 0.0f);
            asset[11].setColor(0.0f, 0.7f, 0.9f, 1.0f);
            asset[12].createEllipsoid(0.02f, 0.03f, 0.03f, -0.23f, 0.95f, 0.23f, 0, 0, 0, 0.0f, 0.0f, 0.0f);
            asset[12].setColor(0.0f, 0.7f, 0.9f, 1.0f);

            //Mata Dalam
            asset[13].createEllipsoid(0.01f, 0.02f, 0.033f, -0.17f, 0.95f, 0.23f, 0, 0, 0, 0.0f, 0.01f, 0.0f);
            asset[13].setColor(1.0f, 1.0f, 1.0f, 1.0f);
            asset[14].createEllipsoid(0.01f, 0.02f, 0.033f, -0.23f, 0.95f, 0.23f, 0, 0, 0, 0.0f, 0.01f, 0.0f);
            asset[14].setColor(1.0f, 1.0f, 1.0f, 1.0f);
            
            //Kaki kiri
            asset[15].createEllipsoid(0.020f, 0.05f, 0.010f, -0.17f, 0.37f, 0.53f, -45, 0, 0, 0.0f, 0.0f, 0.0f);
            asset[15].setColor(1.0f, 0.7f, 0.9f, 1.0f);
            //Kaki kanan
            asset[16].createEllipsoid(0.020f, 0.05f, 0.010f, -0.23f, 0.37f, 0.53f, -45, 0, 0, 0.0f, 0.0f, 0.0f);
            asset[16].setColor(1.0f, 0.7f, 0.9f, 1.0f);

            asset[17].createEllipsoid(0.01f, 0.01f, 0.01f, -0.17f, 0.37f, 0.52f, -45, 0, 0, 0.0f, 0.0f, 0.0f);
            asset[17].setColor(0.8f, 0.6f, 0.0f, 1.0f);

            asset[18].createEllipsoid(0.01f, 0.01f, 0.01f, -0.23f, 0.37f, 0.52f, -45, 0, 0, 0.0f, 0.0f, 0.0f);
            asset[18].setColor(0.8f, 0.6f, 0.0f, 1.0f);

            //Gedung 
            rumah[0].createBoxVertices(0.0f, -0.29f, -1.89f, 1.14f, 0.6f, 1.14f, 15, 0, 0.0f, 0.0f, 0.0f);
            rumah[0].setColor(1.0f, 1.0f, 1.0f, 1.0f);

            //Kanan Atap
            rumah[1].tabung(-3.8f, 0.5f, 0.2f, -1.7f, 0.2f, 0.2f, 0.2f, 15, 0, 0, 0.0f, 0.0f, 0.0f);
            rumah[1].setColor(1.0f, 0.0f, 0.0f, 1.0f);

            //Kiri Atap
            rumah[2].tabung(-3.8f, -0.5f, 0.2f, -1.7f, 0.2f, 0.2f, 0.2f, 15, 0, 0, 0.0f, 0.0f, 0.0f);
            rumah[2].setColor(1.0f, 0.0f, 0.0f, 1.0f);

            //Atap Depan
            rumah[3].tabung(-3.8f, 1.3f, 0.56f, 0.2f, 0.2f, 0.2f, 0.2f, 0, 90, 0, 0.0f, 0.0f, 0.0f);
            rumah[3].setColor(1.0f, 0.0f, 0.0f, 1.0f);

            //Atap Belakang
            rumah[4].tabung(-3.8f, 2.3f, 0.81f, 0.2f, 0.2f, 0.2f, 0.2f, 0, 90, 0, 0.0f, 0.0f, 0.0f);
            rumah[4].setColor(1.0f, 0.0f, 0.0f, 1.0f);

            //Bola D Kanan
            rumah[5].createBall(0.5f, 0.2f, -1.32f, 0.205f, 15, 0, 0.0f, 0.0f, 0.0f);
            rumah[5].setColor(1.0f, 0.0f, 0.0f, 1.0f);
            //Bola D Kiri
            rumah[6].createBall(-0.5f, 0.2f, -1.32f, 0.205f, 15, 0, 0.0f, 0.0f, 0.0f);
            rumah[6].setColor(1.0f, 0.0f, 0.0f, 1.0f);
            //Bola B Kanan
            rumah[7].createBall(0.5f, 0.2f, -2.42f, 0.205f, 15, 0, 0.0f, 0.0f, 0.0f);
            rumah[7].setColor(1.0f, 0.0f, 0.0f, 1.0f);
            //Bola B Kiri
            rumah[8].createBall(-0.5f, 0.2f, -2.42f, 0.205f, 15, 0, 0.0f, 0.0f, 0.0f);
            rumah[8].setColor(1.0f, 0.0f, 0.0f, 1.0f);

            //Pilar D Kiri
            rumah[9].tabung(-3.8f, -0.5f, -1.4f, 0.7f, 0.2f, 0.2f, 0.2f, 105, 0, 0, 0.0f, 0.0f, 0.0f);
            rumah[9].setColor(1.0f, 0.0f, 0.0f, 1.0f);
            //Pilar D Kanan
            rumah[10].tabung(-3.8f, 0.5f, -1.4f, 0.7f, 0.2f, 0.2f, 0.2f, 105, 0, 0, 0.0f, 0.0f, 0.0f);
            rumah[10].setColor(1.0f, 0.0f, 0.0f, 1.0f);
            //Pilar B Kanan
            rumah[11].tabung(-3.8f, 0.5f, -2.4f, 0.7f, 0.2f, 0.2f, 0.2f, 105, 0, 0, 0.0f, 0.0f, 0.0f);
            rumah[11].setColor(1.0f, 0.0f, 0.0f, 1.0f);
            //Pilar B Kiri
            rumah[12].tabung(-3.8f, -0.5f, -2.4f, 0.7f, 0.2f, 0.2f, 0.2f, 105, 0, 0, 0.0f, 0.0f, 0.0f);
            rumah[12].setColor(1.0f, 0.0f, 0.0f, 1.0f);
            //Atap 
            rumah[13].tabung(0.0f, 0.0f, 0.3f, -2.3f, 0.5f, 0.5f, 0.5f, 15, 0, 0, 0.0f, 0.0f, 0.0f);
            rumah[13].setColor(1.0f, 0.0f, 0.0f, 1.0f);

            //Pokeball
            rumah[14].createHalfBall(0.1f, 0.1f, 0.1f, 0.0f, 0.6f, -1.5f, 15, 0, 0.0f, 0.0f, 0.0f);
            rumah[14].setColor(1.0f, 1.0f, 1.0f, 1.0f);
            rumah[15].createHalfBall(0.1f, 0.1f, 0.1f, 0.0f, -0.6f, 1.5f, 195, 0, 0.0f, 0.0f, 0.0f);
            rumah[15].setColor(0.6f, 0.0f, 0.0f, 1.0f);

            //Ball Button
            rumah[16].createEllipsoid(0.03f, 0.03f, 0.03f, 0.0f, 0.6f, -1.4f, 15, 0, 0, 0.0f, 0.0f, 0.0f);
            rumah[16].setColor(0.0f, 0.0f, 0.0f, 1.0f);
            rumah[17].createEllipsoid(0.01f, 0.01f, 0.01f, 0.0f, 0.6f, -1.37f, 15, 0, 0, 0.0f, 0.0f, 0.0f);
            rumah[17].setColor(1.0f, 1.0f, 1.0f, 1.0f);

            //Pintu
            rumah[18].createBoxVertices(-0.2f, -0.4f, -1.3f, 0.4f, 0.5f, 0.03f, 15, 0, 0.0f, 0.0f, 0.0f);
            rumah[18].setColor(0.0f, 0.7f, 1.0f, 1.0f);
            rumah[19].createBoxVertices(0.2f, -0.4f, -1.3f, 0.4f, 0.5f, 0.03f, 15, 0, 0.0f, 0.0f, 0.0f);
            rumah[19].setColor(0.0f, 0.7f, 1.0f, 1.0f);

            /*//Garis Hitam Tengah
            rumah[17].createEllipsoid(0.03f, 0.11f, 0.03f, 0.88f, 0.0f, -1.15f, 0, 90, 0.0f, 0.0f, 0.0f);*/

            //Laut
            laut[0].createBoxVertices(0.0f, -1.0f, 0.0f, 20.0f, 0.4f, 20.0f, 15, 0, 0.0f, 0.0f, 0.0f);
            laut[0].setColor(0.0f, 0.0f, 0.5f, 1.0f);

            //Tanah
            terrain[0].createBoxVertices(-1.5f, -0.8f, -1.8f, 5.0f, 0.4f, 5.0f, 15, 0, 0.0f, 0.0f, 0.0f);
            terrain[0].setColor(0.7f, 0.6f, 0.0f, 1.0f);

            //rumput
            terrain[1].createBoxVertices(-1.5f, -0.79f, -1.8f, 4.99f, 0.4f, 4.99f, 15, 0, 0.0f, 0.0f, 0.0f);
            terrain[1].setColor(0.0f, 0.8f, 0.0f, 1.0f);

            //Gunung
            terrain[2].createElipParaboloid(-0.7f, 1.0f, 1.1f, 0.3f, 0.5f, 0.7f, 0.5f, 105, 0, 0.0f, 0.0f, 0.0f);
            terrain[2].setColor(0.6f, 0.6f, 0.6f, 1.0f);

            terrain[3].createElipParaboloid(-0.7f, -4.0f, 1.1f, 0.3f, 0.5f, 0.7f, 0.5f, 105, 0, 0.0f, 0.0f, 0.0f);
            terrain[3].setColor(0.6f, 0.6f, 0.6f, 1.0f);

            terrain[4].createElipParaboloid(-0.7f, 1.0f, -4.9f, 0.3f, 0.5f, 0.7f, 0.5f, 105, 0, 0.0f, 0.0f, 0.0f);
            terrain[4].setColor(0.6f, 0.6f, 0.6f, 1.0f);

            terrain[5].createElipParaboloid(-0.7f, -4.0f, -4.9f, 0.3f, 0.5f, 0.7f, 0.5f, 105, 0, 0.0f, 0.0f, 0.0f);
            terrain[5].setColor(0.6f, 0.6f, 0.6f, 1.0f);

            //Batang Pohon
            terrain[6].tabung(-3.8f, -2.5f, -1.4f, 0.3f, 0.2f, 0.2f, 0.2f, 105, 0, 0, 0.0f, 0.0f, 0.0f);
            terrain[6].setColor(0.4f, 0.2f, 0.0f, 1.0f);

            //Daun
            terrain[7].createElipParaboloid(-0.9f, -2.5f, -1.5f, -1.2f, 0.5f, 0.7f, 0.5f, 105, 0, 0.0f, 0.0f, 0.0f);
            terrain[7].setColor(0.3f, 1.0f, 0.3f, 1.0f);

            terrain[8].createElipParaboloid(-0.9f, -2.5f, -1.5f, -1.8f, 0.5f, 0.7f, 0.5f, 105, 0, 0.0f, 0.0f, 0.0f);
            terrain[8].setColor(0.3f, 1.0f, 0.3f, 1.0f);

            terrain[9].createElipParaboloid(-0.9f, -2.5f, -1.5f, -2.4f, 0.5f, 0.7f, 0.5f, 105, 0, 0.0f, 0.0f, 0.0f);
            terrain[9].setColor(0.3f, 1.0f, 0.3f, 1.0f);

            terrain[10].tabung(-3f, -3.525f, 1.265f, -0.2f, 0.075f, 0.075f, 0.2f, -75, 0, 0, 0f, 0f, 0f);
            terrain[10].setColor(0.2196f, 0.1294f, 0f, 1f);

            //dahan
            terrain[11].tabung(-1.5f, 2.1f, -2.6f, -1.95f, 0.025f, 0.025f, 0.1f, 60, 105, 0, 0f, 0f, 0f);
            terrain[11].setColor(0.2196f, 0.1294f, 0f, 1f);

            //Daun
            //float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, int degreesx, int degreesy, int degreesz, float transx, float transy, float transz
            terrain[12].createEllipsoid(0.5f, 0.2f, 0.35f, -3.7f, 0.28f, -1.4f, 15, 0, 0, 0f, 0f, 0f);
            terrain[12].setColor(0.3f, 1.0f, 0.3f, 1.0f);

            terrain[12].createEllipsoid(0.3f, 0.1f, 0.15f, -3.65f, 0.5f, -1.4f, 15, 0, 0, 0f, 0f, 0f);
            terrain[12].setColor(0.3f, 1.0f, 0.3f, 1.0f);

            //Ayunan
            terrain[13].createBoxVertices(-3.15f, -0.38f, -1.5f, 0.3f, 0.1f, 0.3f, 15, 0, 0.0f, 0.0f, 0.0f);
            terrain[13].setColor(0.6f, 0.3f, 0.0f, 1.0f);

            //Awan
            terrain[14].createBall(-0.05f, 0.0f, 0.0f, 0.2f, 15, 0, 1.5f, 2.5f, 0.0f);
            terrain[14].setColor(0.96f, 0.95f, 0.95f, 1.0f);
            terrain[15].createEllipsoid(0.13f, 0.15f, 0.2f, -0.2f, -0.1f, 0.0f, 15, 0, 0, 1.5f, 2.5f, 0.0f);
            terrain[15].setColor(0.96f, 0.95f, 0.95f, 1.0f);
            terrain[16].createEllipsoid(0.14f, 0.09f, 0.18f, -0.05f, -0.2f, -0.05f, 15, 0, 0, 1.5f, 2.5f, 0.0f);
            terrain[16].setColor(0.96f, 0.95f, 0.95f, 1.0f);
            terrain[17].createEllipsoid(0.16f, 0.09f, 0.18f, 0.21f, -0.19f, -0.0f, 15, 0, 0, 1.5f, 2.5f, 0.0f);
            terrain[17].setColor(0.96f, 0.95f, 0.95f, 1.0f);
            terrain[18].createBall(0.355f, -0.15f, 0.12f, 0.05f, 15, 0, 1.5f, 2.5f, 0.0f);
            terrain[18].setColor(0.96f, 0.95f, 0.95f, 1.0f);
            terrain[19].createBall(0.355f, -0.15f, -0.12f, 0.05f, 15, 0, 1.5f, 2.5f, 0.0f);
            terrain[19].setColor(0.96f, 0.95f, 0.95f, 1.0f);
            terrain[20].createEllipsoid(0.11f, 0.075f, 0.18f, 0.45f, -0.1f, -0.0f, 15, 0, 0, 1.5f, 2.5f, 0.0f);
            terrain[20].setColor(0.96f, 0.95f, 0.95f, 1.0f);
            terrain[21].createEllipsoid(0.2f, 0.15f, 0.19f, 0.2f, -0.08f, -0.0f, 15, 0, 0, 1.5f, 2.5f, 0.0f);
            terrain[21].setColor(0.96f, 0.95f, 0.95f, 1.0f);

            terrain[22].createBall(-0.05f, 0.0f, 0.0f, 0.2f, 15, 0, -3.0f, 3.5f, 0.0f);
            terrain[22].setColor(0.96f, 0.95f, 0.95f, 1.0f);
            terrain[23].createEllipsoid(0.13f, 0.15f, 0.2f, -0.2f, -0.1f, 0.0f, 15, 0, 0, -3.0f, 3.5f, 0.0f);
            terrain[23].setColor(0.96f, 0.95f, 0.95f, 1.0f);
            terrain[24].createEllipsoid(0.14f, 0.09f, 0.18f, -0.05f, -0.2f, -0.05f, 15, 0, 0, -3.0f, 3.5f, 0.0f);
            terrain[24].setColor(0.96f, 0.95f, 0.95f, 1.0f);
            terrain[25].createEllipsoid(0.16f, 0.09f, 0.18f, 0.21f, -0.19f, -0.0f, 15, 0, 0, -3.0f, 3.5f, 0.0f);
            terrain[25].setColor(0.96f, 0.95f, 0.95f, 1.0f);
            terrain[26].createBall(0.355f, -0.15f, 0.12f, 0.05f, 15, 0, -3.0f, 3.5f, 0.0f);
            terrain[26].setColor(0.96f, 0.95f, 0.95f, 1.0f);
            terrain[27].createBall(0.355f, -0.15f, -0.12f, 0.05f, 15, 0, -3.0f, 3.5f, 0.0f);
            terrain[27].setColor(0.96f, 0.95f, 0.95f, 1.0f);
            terrain[28].createEllipsoid(0.11f, 0.075f, 0.18f, 0.45f, -0.1f, -0.0f, 15, 0, 0, -3.0f, 3.5f, 0.0f);
            terrain[28].setColor(0.96f, 0.95f, 0.95f, 1.0f);
            terrain[29].createEllipsoid(0.2f, 0.15f, 0.19f, 0.2f, -0.08f, -0.0f, 15, 0, 0, -3.0f, 3.5f, 0.0f);
            terrain[29].setColor(0.96f, 0.95f, 0.95f, 1.0f);

            //Matahari
            langit[0].createBall(0.0f, 10.5f, 0.0f, 1.3f, 0, 0, 0.0f, 0.0f, 0.0f);
            langit[0].setColor(1.0f, 1.0f, 0.0f, 1.0f);

            //Bulan
            langit[1].createBall(0.0f, -10.5f, 0.0f, 1.3f, 0, 0, 0.0f, 0.0f, 0.0f);
            langit[1].setColor(0.7f, 0.7f, 0.7f, 1.0f);

            //<< ------------------Poliwag------------------------->

            //float _positionX, float _positionY, float _positionZ, float _radius, int degreesx, int degreesz, float transx, float transy, float transz)
            //<  badan  >
            Poliwag[0].createBall(2.0f, -0.6050f, -0.15f, 0.1f, 0, 0, 0.0f, 0.0f, 0.0f);
            Poliwag[0].setColor(0.0f, 0.3f, 0.8f, 1.0f);

            //< Perut >
            //float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, int degreesx, int degreesz, float transx, float transy, float transz
            Poliwag[1].createHalfEllipsoid(0.075f, 0.075f, 0.075f, 2.0f, 0.6175f, -0.050f, 195, 0, 0.0f, 0.0f, 0.0f);
            Poliwag[1].setColor(1.0f, 1.0f, 1.0f, 1.0f);

            //< Paha >            
            //float panjang, float _positionX, float _positionY, float _positionZ, float _radiusx, float _radiusy, float _radiusz, int degreesx, int degressy, int degreesz, float transx, float transy, float transz
            //Paha Kanan
            Poliwag[2].tabung(-3.0f, 1.950f, 0.0f, 0.7f, 0.015f, 0.015f, 0.015f, 105, 0, 0, 0.0f, 0f, 0f);
            Poliwag[2].setColor(0.0f, 0.3f, 0.8f, 1.0f);
            //Paha Kiri
            Poliwag[3].tabung(-3.0f, 2.060f, 0.0f, 0.7f, 0.015f, 0.015f, 0.015f, 105, 0, 0, 0.0f, 0f, 0f);
            Poliwag[3].setColor(0.0f, 0.3f, 0.8f, 1.0f);


            //< Kaki >
            //float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, int degreesx, int degreesz, float transx, float transy, float transz
            //Kaki kiri
            Poliwag[4].createEllipsoid(0.020f, 0.010f, 0.05f, 1.950f, -0.725f, 0.035f, 15, 0, 0, 0.0f, 0.0f, 0.0f);
            Poliwag[4].setColor(0.0f, 0.3f, 0.8f, 1.0f);
            //Kaki kanan
            Poliwag[5].createEllipsoid(0.020f, 0.010f, 0.05f, 2.060f, -0.725f, 0.035f, 15, 0, 0, 0.0f, 0.0f, 0.0f);
            Poliwag[5].setColor(0.0f, 0.3f, 0.8f, 1.0f);

            //< Tulang Ekor >
            //float panjang, float _positionX, float _positionY, float _positionZ, float _radiusx, float _radiusy, float _radiusz, int degreesx, int degressy, int degreesz, float transx, float transy, float transz
            Poliwag[6].tabung(-2.5f, 2.0f, -0.680f, -0.250f, 0.005f, 0.005f, 0.037f, 0, 0, 0, 0.0f, 0.0f, 0.0f);
            Poliwag[6].setColor(0.0f, 0.3f, 0.8f, 1.0f); //0.0f, 0.3f, 0.8f, 1.0f

            //< Ekor >
            //float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, int degreesx, int degreesz, float transx, float transy, float transz
            Poliwag[7].createEllipsoid(0.04f, 0.0020f, 0.1f, 2.0f, -0.68f, -0.3f, 0, 0, 0, 0.0f, 0.0f, 0.0f);
            Poliwag[7].setColor(1f, 1f, 1f, 1f);

            //< Mata >
            //float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, int degreesx, int degreesz, float transx, float transy, float transz
            //Mata Kiri
            Poliwag[8].createHalfEllipsoid(0.02f, 0.02f, 0.02f, 1.975f, 0.180f, 0.515f, 120, 0, 0.0f, 0.0f, 0.0f); //175 \ 510
            Poliwag[8].setColor(0.54f, 0.73f, 1f, 1.0f);
            //Mata Kanan
            Poliwag[9].createHalfEllipsoid(0.02f, 0.02f, 0.02f, 2.025f, 0.180f, 0.515f, 120, 0, 0.0f, 0.0f, 0.0f);
            Poliwag[9].setColor(0.54f, 0.73f, 1f, 1.0f);

            //< Kornea >
            //float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, int degreesx, int degreesz, float transx, float transy, float transz
            //Mata Kiri
            Poliwag[10].createHalfEllipsoid(0.015f, 0.015f, 0.02f, 1.975f, 0.182f, 0.512f, 120, 0, 0.0f, 0.0f, 0.0f); // 075/510
            Poliwag[10].setColor(0f, 0f, 0f, 1.0f);
            //Mata Kanan
            Poliwag[11].createHalfEllipsoid(0.015f, 0.015f, 0.02f, 2.025f, 0.182f, 0.512f, 120, 0, 0.0f, 0.0f, 0.0f);
            Poliwag[11].setColor(0f, 0f, 0f, 1.0f);

            //< Mulut >
            //float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, int degreesx, int degreesz, float transx, float transy, float transz
            Poliwag[12].createEllipsoid(0.020f, 0.010f, 0.010f, 2f, -0.55f, -0.065f, 0, 0, 0, 0.0f, 0.0f, 0.0f);
            Poliwag[12].setColor(1f, 0f, 0.35f, 1.0f);

            /*---------------------------- Snorlax ----------------------------*/

            //Badan(luar) 
            snorlax[0].createEllipsoid(0.4f, 0.47f, 0.2f, -1.2f, -0.37f, -0.4f, 15, 0, 0, 0.0f, 0.0f, 0.0f);
            snorlax[0].setColor(0.1f, 0.27f, 0.22f, 1.0f);

            //perut(dalam)
            snorlax[1].createHalfEllipsoid(0.3f, 0.34f, 0.28f, -1.2f, 0.3f, 0.38f, 195, 0, 0.0f, 0.0f, 0.0f);
            snorlax[1].setColor(0.98f, 0.88f, 0.73f, 1.0f);

            //kepala(luar)
            snorlax[2].createEllipsoid(0.17f, 0.18f, 0.1f, -1.2f, 0.05f, -0.4f, 15, 0, 0, 0.0f, 0.0f, 0.0f);
            snorlax[2].setColor(0.1f, 0.27f, 0.22f, 1.0f);

            //muka(dalam)
            snorlax[3].createHalfEllipsoid(0.13f, 0.13f, 0.11f, -1.195f, -0.025f, 0.35f, 195, 0, 0.0f, 0.0f, 0.0f);
            snorlax[3].setColor(0.98f, 0.88f, 0.73f, 1.0f);

            //mata dan mulut
            /*snorlax[4].createBoxVertices(-1.24f, 0.11f, -0.285f, 0.04f, 0.005f, 0.03f, 15, 0, 0.0f, 0.0f, 0.0f);
            snorlax[4].setColor(0f, 0f, 0f, 1.0f);
            snorlax[5].createBoxVertices(-1.15f, 0.11f, -0.285f, 0.04f, 0.005f, 0.03f, 15, 0, 0.0f, 0.0f, 0.0f);
            snorlax[5].setColor(0f, 0f, 0f, 1.0f);*/

            //smiley mouth
            //snorlax[6].createquarterEllipsoid(0.04f, 0.015f, 0.04f, -1.2f, 0.13f, -0.22f, 0, 0, 0, 0.0f, 0.0f, 0.0f);

            //plain mouth
            snorlax[6].createBoxVertices(-1.19f, 0.06f, -0.257f, 0.06f, 0.005f, 0.03f, 15, 0, 0.0f, 0.0f, 0.0f);
            snorlax[6].setColor(0f, 0f, 0f, 1.0f);

            //gigi
            snorlax[7].createElipCone(-1.214f, -0.25f, -0.08f, 0.04f, 105, 0, 0.0f, 0.0f, 0.0f);
            snorlax[7].setColor(0.9f, 0.89f, 0.88f, 1.0f);
            snorlax[8].createElipCone(-1.167f, -0.25f, -0.08f, 0.04f, 105, 0, 0.0f, 0.0f, 0.0f);
            snorlax[8].setColor(0.9f, 0.89f, 0.88f, 1.0f);

            //segitiga muka
            snorlax[9].createElipCone2(-1.16f, -0.345f, -0.213f, 0.12f, 105, 1, 0.0f, 0.0f, 0.0f);
            snorlax[9].setColor(0.98f, 0.88f, 0.73f, 1.0f);
            snorlax[10].createElipCone2(-1.225f, -0.345f, -0.213f, 0.12f, 105, 1, 0.0f, 0.0f, 0.0f);
            snorlax[10].setColor(0.98f, 0.88f, 0.73f, 1.0f);

            //telinga
            snorlax[11].createElipParaboloid(0.0f, -0.7f, -0.61f, -1.06f, 0.22f, 0.2f, 0.2f, 105, 45, 0.0f, 0.0f, 0.0f);
            snorlax[11].setColor(0.1f, 0.27f, 0.22f, 1.0f);
            snorlax[12].createElipParaboloid(0.0f, -1.0f, -0.17f, 0.583f, 0.22f, 0.2f, 0.2f, 105, -45, 0.0f, 0.0f, 0.0f);
            snorlax[12].setColor(0.1f, 0.27f, 0.22f, 1.0f);

            //tangan
            snorlax[13].createElipParaboloid(-0.8f, -0.6f, -4.4f, 0.2f, -0.25f, 0.2f, 2.0f, 105, 45, -0.5f, 0.0f, 4.0f);
            snorlax[13].setColor(0.1f, 0.27f, 0.22f, 1.0f);
            snorlax[14].createElipParaboloid(-0.8f, -0.4f, -4.17f, 1.141f, -0.25f, 0.2f, 2.0f, 105, -45, -0.5f, 0.0f, 4.0f);
            snorlax[14].setColor(0.1f, 0.27f, 0.22f, 1.0f);

            //kuku tangan
            snorlax[15].createElipCone(-0.95f, -0.627f, -1.19f, 0.03f, 105, 45, 0.0f, 0.0f, 0.0f);
            snorlax[15].setColor(0.9f, 0.89f, 0.88f, 1.0f);
            snorlax[16].createElipCone(-0.931f, -0.627f, -1.178f, 0.03f, 105, 45, 0.0f, 0.0f, 0.0f);
            snorlax[16].setColor(0.9f, 0.89f, 0.88f, 1.0f);
            snorlax[17].createElipCone(-0.97f, -0.627f, -1.181f, 0.03f, 105, 45, 0.0f, 0.0f, 0.0f);
            snorlax[17].setColor(0.9f, 0.89f, 0.88f, 1.0f);

            //kuku tangan kanan
            snorlax[18].createElipCone(-0.778f, -0.215f, 0.445f, 0.05f, 105, -45, 0.0f, 0.0f, 0.0f);
            snorlax[18].setColor(0.9f, 0.89f, 0.88f, 1.0f);
            snorlax[19].createElipCone(-0.758f, -0.215f, 0.432f, 0.05f, 105, -45, 0.0f, 0.0f, 0.0f);
            snorlax[19].setColor(0.9f, 0.89f, 0.88f, 1.0f);
            snorlax[20].createElipCone(-0.739f, -0.215f, 0.439f, 0.05f, 105, -45, 0.0f, 0.0f, 0.0f);
            snorlax[20].setColor(0.9f, 0.89f, 0.88f, 1.0f);

            //kaki
            snorlax[21].createEllipsoid(0.11f, 0.11f, 0.06f, -0.9f, -0.5f, -0.2f, 15, 0, 0, 0.0f, 0.0f, 0.0f);
            snorlax[21].setColor(0.87f, 0.77f, 0.5f, 1.0f);
            snorlax[22].createHalfEllipsoid(0.09f, 0.08f, 0.06f, -0.9f, 0.5f, 0.19f, 195, 0, 0.0f, 0.0f, 0.0f);
            snorlax[22].setColor(0.33f, 0.26f, 0.15f, 1.0f);
            snorlax[23].createEllipsoid(0.11f, 0.11f, 0.06f, -1.5f, -0.5f, -0.2f, 15, 0, 0, 0.0f, 0.0f, 0.0f);
            snorlax[23].setColor(0.87f, 0.77f, 0.5f, 1.0f);
            snorlax[24].createHalfEllipsoid(0.09f, 0.08f, 0.06f, -1.5f, 0.5f, 0.19f, 195, 0, 0.0f, 0.0f, 0.0f);
            snorlax[24].setColor(0.33f, 0.26f, 0.15f, 1.0f);

            //kuku kaki
            snorlax[25].createElipCone(-0.9f, -0.19f, 0.355f, 0.08f, 105, 0, 0.0f, 0.0f, 0.0f);
            snorlax[25].setColor(0.9f, 0.89f, 0.88f, 1.0f);
            snorlax[26].createElipCone(-0.83f, -0.19f, 0.385f, 0.08f, 105, 0, 0.0f, 0.0f, 0.0f);
            snorlax[26].setColor(0.9f, 0.89f, 0.88f, 1.0f);
            snorlax[27].createElipCone(-0.97f, -0.19f, 0.385f, 0.08f, 105, 0, 0.0f, 0.0f, 0.0f);
            snorlax[27].setColor(0.9f, 0.89f, 0.88f, 1.0f);

            snorlax[28].createElipCone(-1.5f, -0.19f, 0.355f, 0.08f, 105, 0, 0.0f, 0.0f, 0.0f);
            snorlax[28].setColor(0.9f, 0.89f, 0.88f, 1.0f);
            snorlax[29].createElipCone(-1.43f, -0.19f, 0.385f, 0.08f, 105, 0, 0.0f, 0.0f, 0.0f);
            snorlax[29].setColor(0.9f, 0.89f, 0.88f, 1.0f);
            snorlax[30].createElipCone(-1.57f, -0.19f, 0.385f, 0.08f, 105, 0, 0.0f, 0.0f, 0.0f);
            snorlax[30].setColor(0.9f, 0.89f, 0.88f, 1.0f);





            //< Mulut >
            //float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, int degreesx, int degreesz, float transx, float transy, float transz
            //jari tangan
            /*asset[0].createElipCone(0.2f, 0.2f, 0.2f, 0.2f, 90, -15, 0.15f, 0.3f, 0.0f);
            asset[0].setColor(1.0f, 0.7f, 0.9f, 1.0f);
            asset[1].createElipCone(-0.2f, 0.2f, 0.2f, 0.2f, 90, 15, -0.15f, 0.3f, 0.0f);
            asset[1].setColor(1.0f, 0.7f, 0.9f, 1.0f);*/

            //Ujung kaki
            /*asset[0].createElipCone(0.2f, 0.2f, 0.2f, 0.2f, 90, -15, 0.15f, 0.3f, 0.0f);
            asset[0].setColor(1.0f, 0.7f, 0.9f, 1.0f);
            asset[1].createElipCone(-0.2f, 0.2f, 0.2f, 0.2f, 90, 15, -0.15f, 0.3f, 0.0f);
            asset[1].setColor(1.0f, 0.7f, 0.9f, 1.0f);*/

            /*asset2[0].createElips(0.0f, 0.5f, 0.25f, 0.5f);*/

            //Bezier
            objectBez[0] = new Asset3d(
                new float[1080],
                new uint[]
                {

                }
            );
            objectBez[1] = new Asset3d(
                new float[1080],
                new uint[]
                {

                }
            );
            objectBez[2] = new Asset3d(
                new float[1080],
                new uint[]
                {

                }
            );
            poliwag2[0] = new Asset3d(
               new float[1080],
               new uint[]
               {

               }
               );
        }

        public Matrix4 generateArbRotationMatrix(Vector3 axis, Vector3 center, float degree)
        {
            var rads = MathHelper.DegreesToRadians(degree);

            var secretFormula = new float[4, 4] {
                { (float)Math.Cos(rads) + (float)Math.Pow(axis.X, 2) * (1 - (float)Math.Cos(rads)), axis.X* axis.Y * (1 - (float)Math.Cos(rads)) - axis.Z * (float)Math.Sin(rads),    axis.X * axis.Z * (1 - (float)Math.Cos(rads)) + axis.Y * (float)Math.Sin(rads),   0 },
                { axis.Y * axis.X * (1 - (float)Math.Cos(rads)) + axis.Z * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Y, 2) * (1 - (float)Math.Cos(rads)), axis.Y * axis.Z * (1 - (float)Math.Cos(rads)) - axis.X * (float)Math.Sin(rads),   0 },
                { axis.Z * axis.X * (1 - (float)Math.Cos(rads)) - axis.Y * (float)Math.Sin(rads),   axis.Z * axis.Y * (1 - (float)Math.Cos(rads)) + axis.X * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Z, 2) * (1 - (float)Math.Cos(rads)), 0 },
                { 0, 0, 0, 1}
            };
            var secretFormulaMatix = new Matrix4
            (
                new Vector4(secretFormula[0, 0], secretFormula[0, 1], secretFormula[0, 2], secretFormula[0, 3]),
                new Vector4(secretFormula[1, 0], secretFormula[1, 1], secretFormula[1, 2], secretFormula[1, 3]),
                new Vector4(secretFormula[2, 0], secretFormula[2, 1], secretFormula[2, 2], secretFormula[2, 3]),
                new Vector4(secretFormula[3, 0], secretFormula[3, 1], secretFormula[3, 2], secretFormula[3, 3])
            );

            return secretFormulaMatix;
        }
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            camera.Fov = camera.Fov - e.OffsetY;
        }
        protected override void OnLoad()
        {

            base.OnLoad();
            
            //background color
            GL.ClearColor(0.0f, 0.07f, 1.0f, 1.0f);
            //tali ayunan
            objectBez[0].setTitikBezier(-2.7f, 1.0f, -1.5f);
            objectBez[0].setTitikBezier(-2.7f, 0.5f, -1.5f);
            objectBez[0].setTitikBezier(-2.7f, 0.1f, -1.5f);
            objectBez[0].setTitikBezier(-3.0f, 0.0f, -1.5f);
            objectBez[0].setTitikBezier(-3.2f, -0.2f, -1.5f);
            objectBez[0].setTitikBezier(-3.4f, 0.0f, -1.5f);
            objectBez[0].setTitikBezier(-3.4f, 0.0f, -1.5f);
            objectBez[0].setTitikBezier(-3.5f, 0.7f, -1.5f);

            //mata snorlax
            objectBez[1].setTitikBezier(-1.255f, 0.18f, -0.23f);
            objectBez[1].setTitikBezier(-1.215f, 0.18f, -0.23f);
            objectBez[1].setTitikBezier(-1.215f, 0.17f, -0.23f);

            objectBez[2].setTitikBezier(-1.165f, 0.17f, -0.23f);
            objectBez[2].setTitikBezier(-1.165f, 0.18f, -0.23f);
            objectBez[2].setTitikBezier(-1.125f, 0.18f, -0.23f);

            //<---------------------------<Pusaran Poliwag>-------------------------------
            //poliwag2[0].setTitikBezier(2.0f, -0.620f, -0.035f); --> Titik Pusat
            poliwag2[0].setTitikBezier(2.000f, -0.670f, -0.0675f);//1
            poliwag2[0].setTitikBezier(2.030f, -0.670f, -0.0600f); //2
            poliwag2[0].setTitikBezier(2.050f, -0.650f, -0.0525f);//3
            poliwag2[0].setTitikBezier(2.050f, -0.600f, -0.0360f);//4
            poliwag2[0].setTitikBezier(2.030f, -0.570f, -0.0360f);//5
            poliwag2[0].setTitikBezier(2.000f, -0.570f, -0.035f);//6
            poliwag2[0].setTitikBezier(1.970f, -0.570f, -0.0375f);//7
            poliwag2[0].setTitikBezier(1.950f, -0.600f, -0.0400f);//8
            poliwag2[0].setTitikBezier(1.950f, -0.635f, -0.0475f);//9
            poliwag2[0].setTitikBezier(1.970f, -0.650f, -0.0375f);//10
            poliwag2[0].setTitikBezier(2.005f, -0.650f, -0.035f);//11
            //poliwag2[0].setTitikBezier(2.025f, -0.635f, -0.035f);//12
            poliwag2[0].setTitikBezier(2.025f, -0.605f, -0.035f);//13
            //poliwag2[0].setTitikBezier(2.005f, -0.580f, -0.035f);//14
            poliwag2[0].setTitikBezier(1.990f, -0.580f, -0.035f);//15
            //poliwag2[0].setTitikBezier(1.980f, -0.600f, -0.035f);//16
            //poliwag2[0].setTitikBezier(1.980f, -0.620f, -0.035f);//17
            poliwag2[0].setTitikBezier(1.990f, -0.620f, -0.035f);//18
            //poliwag2[0].setTitikBezier(2.005f, -0.635f, -0.035f);//19
            //poliwag2[0].setTitikBezier(2.015f, -0.635f, -0.035f);//20
            //poliwag2[0].setTitikBezier(2.000f, -0.610f, -0.035f);//21
            //poliwag2[0].setTitikBezier(1.995f, -0.615f, -0.035f);//22
            // poliwag2[0].setTitikBezier(1.9975f, -0.625f, -0.035f);//23
            //poliwag2[0].setTitikBezier(2.0000f, -0.620f, -0.035f);//24

            List<Vector3> _verticesTemp = objectBez[0].CreateCurveBezier();
            objectBez[0].setVertices(_verticesTemp);
            objectBez[0].setColor(0.0f, 0.0f, 0.0f, 1.0f);
            objectBez[0].load("../../../Shader/shader.vert", "../../../Shader/shader.frag", Size.X, Size.Y);
            List<Vector3> _verticesTemp2 = objectBez[1].CreateCurveBezier();
            objectBez[1].setVertices(_verticesTemp2);
            objectBez[1].setColor(0.0f, 0.0f, 0.0f, 1.0f);
            objectBez[1].load("../../../Shader/shader.vert", "../../../Shader/shader.frag", Size.X, Size.Y);
            List<Vector3> _verticesTemp3 = objectBez[2].CreateCurveBezier();
            objectBez[2].setVertices(_verticesTemp3);
            objectBez[2].setColor(0.0f, 0.0f, 0.0f, 1.0f);
            objectBez[2].load("../../../Shader/shader.vert", "../../../Shader/shader.frag", Size.X, Size.Y);
            List<Vector3> _politemp = poliwag2[0].CreateCurveBezier();
            poliwag2[0].setVertices(_politemp);
            poliwag2[0].setColor(0f, 0f, 0.0f, 1.0f);
            poliwag2[0].load("../../../Shader/shader.vert", "../../../Shader/shader.frag", Size.X, Size.Y);
            Console.WriteLine("1. Camera Movement: (FGHT)");
            Console.WriteLine("2. Camera Rotation Y axis: (NM,K)");
            Console.WriteLine("3. Move Mew: Space");
            Console.WriteLine("4.  Close : Esc");

            camera = new Camera(new Vector3(0, 0, 1), Size.X / Size.Y);
            for (int i = 0; i < asset.Length; i++)
            {
                asset[i].load("../../../Shader/shader.vert", "../../../Shader/shader.frag", Size.X, Size.Y);
            }
            for (int i = 0; i < terrain.Length; i++)
            {
                terrain[i].load("../../../Shader/shader.vert", "../../../Shader/shader.frag", Size.X, Size.Y);
            }
            for (int i = 0; i < rumah.Length; i++)
            {
                rumah[i].load("../../../Shader/shader.vert", "../../../Shader/shader.frag", Size.X, Size.Y);
            }
            for (int i = 0; i < langit.Length; i++)
            {
                langit[i].load("../../../Shader/shader.vert", "../../../Shader/shader.frag", Size.X, Size.Y);
            }
            for (int i = 0; i < Poliwag.Length; i++)
            {
                Poliwag[i].load("../../../Shader/shader.vert", "../../../Shader/shader.frag", Size.X, Size.Y);
            }
            for (int i = 0; i < snorlax.Length; i++)
            {
                snorlax[i].load("../../../Shader/shader.vert", "../../../Shader/shader.frag", Size.X, Size.Y);
            }
            laut[0].load("../../../Shader/shader.vert", "../../../Shader/shader.frag", Size.X, Size.Y);
            objectBez[0].load("../../../Shader/shader.vert", "../../../Shader/shader.frag", Size.X, Size.Y);
            objectBez[1].load("../../../Shader/shader.vert", "../../../Shader/shader.frag", Size.X, Size.Y);
            objectBez[2].load("../../../Shader/shader.vert", "../../../Shader/shader.frag", Size.X, Size.Y);
            poliwag2[0].load("../../../Shader/shader.vert", "../../../Shader/shader.frag", Size.X, Size.Y);
            GL.Enable(EnableCap.DepthTest);
           /* for (int i = 0; i < asset2.Length; i++)
            {
                asset2[i].load("../../../Shader/shader.vert", "../../../Shader/shader.frag", Size.X, Size.Y);
            }*/
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            //Background color UNTUK BENCMARK
            GL.ClearColor(0.0f, g, b, 1.0f);
            /*Console.WriteLine("waktu : " + global_i);*/
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
     
            Matrix4 temp = Matrix4.Identity;
            _time = -3.3 * args.Time;

            if (day)
            {
                _time5 = -(1 / 10.0f);
                transZs = -0.001f;
                transYs = 0.001f;
                g -= (0.7f / 360.0f);
                b -= (1.0f / 360.0f);
                global_i += 0.1f;
                if (global_i >= 60)
                {
                    day = false;
                    sleep = true;
                }
            }
            else
            {
                _time5 = (1 / 10.0f);
                transZs = 0.001f;
                transYs = -0.001f;
                g += (0.7f / 360.0f);
                b += (1.0f / 360.0f);
                global_i -= 0.1f;
                if (global_i <= 0)
                {
                    day = true;
                    sleep = false;
                }
            }

            if (change)
            {
                _time2 = 0.001f;
                _time3 = -0.001f;
                _timeTemp += 0.001f;
                if (_timeTemp >= 0.2f)
                {
                    change = false;
                }
            }
            else 
            { 
                _time2 = -0.001f;
                _time3 = 0.001f;
                _timeTemp -= 0.001f;
                if (_timeTemp <= 0.0f)
                {
                    change = true;
                }
            }
            
            if(!changeDir)
            {
                _time4 = 0;
                timeY = 0.1f;
                _time4 = 6;
                walkTime += 0.1f;
                if (walkTime > 3.0f)
                {
                    changeDir = true;
                }
            }
            else if (changeDir)
            {
                _time4 = 0;
                timeY = -0.1f;
                _time4 = 6;
                walkTime -= 0.1f;
                if (walkTime < 0.0f)
                {
                    changeDir = false;
                }
            }

            if (swim)
            {
                _time6 = 0.1f;
                swimTime += 0.1f;
                if (swimTime > 2)
                {
                    swim = false;
                }
            }
            else
            {
                _time6 = -0.1f;
                swimTime -= 0.1f;
                if (swimTime < 0)
                {
                    swim = true;
                }
            }


            objectBez[0].render(3, _time, temp, 5, 0.0f, 0.0f, 0.0f, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0);
            objectBez[1].render(3, _time5, temp, 3, 0.0f, transYs, transZs, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0);
            objectBez[2].render(3, _time5, temp, 3, 0.0f, transYs, transZs, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0);
            poliwag2[0].render(3, _time6, temp, 2, 0.0f, 0.0f, 0.0f, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0);

            deg += MathHelper.DegreesToRadians(20f); /*coba ini di command nanti*/
            for (int i = 0; i < asset.Length; i++)
            {   
                asset[i].render(1, _time, temp, 4, timeX, timeY, timeZ, camera.GetViewMatrix(), camera.GetProjectionMatrix(), _time4);
            }
            for (int i = 0; i < 14; i++)
            {
                terrain[i].render(1, _time, temp, 5, 0.0f, 0.0f, 0.0f, camera.GetViewMatrix(), camera.GetProjectionMatrix(),0);
            }            
            for (int i = 14; i < terrain.Length; i++)
            {
                //terrain[i].render(1, _time, temp, 4, 0.0f, 0.5f, 0.0f, camera.GetViewMatrix(), camera.GetProjectionMatrix(),60);
                terrain[i].render(1, _time, temp, 0, 0.0f, 0.0f, 0.0f, camera.GetViewMatrix(), camera.GetProjectionMatrix(),0);
            }
            for (int i = 0; i < 18; i++)
            {
                rumah[i].render(1, _time, temp, 1, 0.0f, 0.0f, 0.0f, camera.GetViewMatrix(), camera.GetProjectionMatrix(),0);
            }
            for (int i = 0; i < langit.Length; i++)
            {
                langit[i].render(1, _time, temp, 2, 0.0f, 0.0f, 0.0f, camera.GetViewMatrix(), camera.GetProjectionMatrix(),0);
            }
            for (int i = 0; i < Poliwag.Length; i++)
            {
                Poliwag[i].render(1, _time6, temp, 2, 0.0f, 0.0f, 0.0f, camera.GetViewMatrix(), camera.GetProjectionMatrix(),0);
            }
            for (int i = 0; i < snorlax.Length; i++)
            {
                snorlax[i].render(1, _time5, temp, 3, 0.0f, transYs, transZs, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0);
            }
            /*for (int i = 21; i < snorlax.Length; i++)
            {
                snorlax[i].render(1, _time, temp, 1, 0.0f, 0.0f, 0.0f, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0);
            }*/

            laut[0].render(1, _time2, temp, 1, 0.0f, _time2, 0.0f, camera.GetViewMatrix(), camera.GetProjectionMatrix(),0);
            rumah[18].render(1, _time2, temp, 1, _time3, 0.0f, 0.0f, camera.GetViewMatrix(), camera.GetProjectionMatrix(),0);
            rumah[19].render(1, _time2, temp, 1, _time2, 0.0f, 0.0f, camera.GetViewMatrix(), camera.GetProjectionMatrix(),0);
            /*  for (int i = 0; i < asset2.Length; i++)
              {
                  asset2[i].render(1, _time);
              }*/
            SwapBuffers();
        }

        //ini tiap perubahan frame
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
            camera.AspectRatio = Size.X / (float)Size.Y;
        }

        //Jalan berdasarkan FPS setting
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            Matrix4 temp = Matrix4.Identity;
            _time = 20.0 * args.Time;
            var input = KeyboardState; //Var ini menyimpan status keyboard ketika window dinyalakan
            var mouse_input = MouseState; //Var ini menyimpan kondisi mouse
            float cameraSpeed = 0.8f;
            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            //mew
            if (input.IsKeyDown(Keys.Space))
            {
                for (int i = 0; i < asset.Length; i++)
                {
                    asset[i].render(1, _time, temp, 0, 0.0f, 0.0f, 0.0f, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0);
                }
            }
            
            //Camera movement
            if (input.IsKeyDown(Keys.G))
            {
                camera.Position -= camera.Front * cameraSpeed * (float)args.Time;
            }
            if (input.IsKeyDown(Keys.T))
            {
                camera.Position += camera.Front * cameraSpeed * (float)args.Time;
            }
            if (input.IsKeyDown(Keys.F))
            {
                camera.Position -= camera.Right * cameraSpeed * (float)args.Time;
            }
            if (input.IsKeyDown(Keys.H))
            {
                camera.Position += camera.Right * cameraSpeed * (float)args.Time;
            }
            var mouse = MouseState;
            var sensitivity = 0.2f;

            if (_firstMove)
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);
                camera.Yaw += deltaX * sensitivity;   //kekanan atau kekiri
                camera.Pitch -= deltaY * sensitivity; //keatas kebawah ada lagi Roll (ke sumbu X)
            }

            //Camera RotateY
            if (KeyboardState.IsKeyDown(Keys.N))
            {
                var axis = new Vector3(0, 1, 0);
                camera.Position -= _objecPost;
                camera.Yaw += _rotationSpeed;
                camera.Position = Vector3.Transform(camera.Position,
                    generateArbRotationMatrix(axis, _objecPost, _rotationSpeed).ExtractRotation());
                camera.Position += _objecPost;

                camera._front = -Vector3.Normalize(camera.Position - _objecPost);
            }
            if (KeyboardState.IsKeyDown(Keys.Comma)) 
            {
                var axis = new Vector3(0, 1, 0);
                camera.Position -= _objecPost;
                camera.Yaw -= _rotationSpeed;
                camera.Position = Vector3.Transform(camera.Position,
                    generateArbRotationMatrix(axis, _objecPost, -_rotationSpeed).ExtractRotation());
                camera.Position += _objecPost;

                camera._front = -Vector3.Normalize(camera.Position - _objecPost);
            }
            if (KeyboardState.IsKeyDown(Keys.K))
            {
                var axis = new Vector3(1, 0, 0);
                camera.Position -= _objecPost;
                camera.Pitch -= _rotationSpeed;
                camera.Position = Vector3.Transform(camera.Position,
                    generateArbRotationMatrix(axis, _objecPost, _rotationSpeed).ExtractRotation());
                camera.Position += _objecPost;
                camera._front = -Vector3.Normalize(camera.Position - _objecPost);
            }
            if (KeyboardState.IsKeyDown(Keys.M))
            {
                var axis = new Vector3(1, 0, 0);
                camera.Position -= _objecPost;
                camera.Pitch += _rotationSpeed;
                camera.Position = Vector3.Transform(camera.Position,
                    generateArbRotationMatrix(axis, _objecPost, -_rotationSpeed).ExtractRotation());
                camera.Position += _objecPost;
                camera._front = -Vector3.Normalize(camera.Position - _objecPost);
            }
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
            Matrix4 temp = Matrix4.Identity;
            /*Console.WriteLine("Time : " + _timeTemp);*/
            deg += MathHelper.DegreesToRadians(20f); /*coba ini di command nanti*/
            if (e.Key == Keys.Right)
            {
                for (int i = 0; i < asset.Length; i++)
                {
                    asset[i].render(1, _time, temp, 1, 0.01f, 0.0f, 0.0f, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0);
                }
            }
            if (e.Key == Keys.Down)
            {
                for (int i = 0; i < asset.Length; i++)
                {
                    asset[i].render(1, _time, temp, 1, 0.0f, -0.01f, 0.0f, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0);
                }
            }
            if (e.Key == Keys.Up)
            {
                for (int i = 0; i < asset.Length; i++)
                {
                    asset[i].render(1, _time, temp, 1, 0.0f, 0.01f, 0.0f, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0);
                }
            }
            if (e.Key == Keys.Left)
            {
                for (int i = 0; i < asset.Length; i++)
                {
                    asset[i].render(1, _time, temp, 1, -0.01f, 0.0f, 0.0f, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0);
                }
            }
            if (e.Key == Keys.W)
            {
                for (int i = 0; i < asset.Length; i++)
                {
                    asset[i].render(1, _time, temp, 1, 0.0f, 0.0f, 0.01f, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0);
                }
            }
            if (e.Key == Keys.S)
            {
                for (int i = 0; i < asset.Length; i++)
                {
                    asset[i].render(1, _time, temp, 1, 0.0f, 0.0f, -0.01f, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0);
                }
            }
        }
    }
}
