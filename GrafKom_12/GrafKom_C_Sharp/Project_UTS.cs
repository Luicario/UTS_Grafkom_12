using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using LearnOpenTK.Common;
using OpenTK.Mathematics;
using System.Threading;
using System.IO;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GrafKom_C_Sharp
{

    class Project_UTS : GameWindow
    {
        Background bg = new Background();
        Mesh[] Object3D = new Mesh[3];
        int max_count = 15;
        int counter = 0;
        bool slide_right = false;
        bool slide_left = false;
        bool play = true;
        int curr = 1;

        // Body, warna hitam.
        string path_body = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/asset/body_laptop.obj";
        string path_monitor = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/asset/monitor_laptop.obj";
        string vert_body = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/Shaders/shader_body.vert";
        string frag_body = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/Shaders/shader_body.frag";
        // Cable, warna abu.
        string path_cable = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/asset/cable_laptop.obj";
        string vert_cable = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/Shaders/shader_cable.vert";
        string frag_cable = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/Shaders/shader_cable.frag";
        // Inside, warna putih.
        string path_inside = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/asset/inside_laptop.obj";
        string vert_inside = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/Shaders/shader_inside.vert";
        string frag_inside = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/Shaders/shader_inside.frag";

        // =================================================

        string base_ps5 = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/asset/base_ps5.obj";
        string text_ps5 = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/asset/logo_ps5.obj";
        string isi_ps5 = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/asset/isi_ps5.obj";
        string cover_ps5 = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/asset/cover_ps5.obj";

        // =================================================

        // Body monitor + Keyboard, warna Hitam
        string path_badan = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/asset/BodyMonitor+Keyboard.obj";
        string vert_badan = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/Shaders/shader.vert";
        string frag_badan = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/Shaders/shader.frag";

        // layar
        string path_layar = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/asset/Layar.obj";
        string vert_layar = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/Shaders/shader_layar.vert";
        string frag_layar = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/Shaders/shader_layar.frag";

        //PC
        string path_PC = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/asset/PC.obj";
        string vert_PC = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/Shaders/shader.vert";
        string frag_PC = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/Shaders/shader.frag";

        //Atribut PC
        string path_Atribut_PC = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/asset/AtributePC.obj";
        string vert_Atribut_PC = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/Shaders/shader_AtPC.vert";
        string frag_Atribut_PC = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/Shaders/shader_AtPC.frag";

        //Atribut Keyboard
        string path_Atribut_Key = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/asset/AtributeKey.obj";
        string vert_Atribut_Key = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/Shaders/shader_AtKEY.vert";
        string frag_Atribut_Key = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/Shaders/shader_AtKEY.frag";

        // =================================================

        Mesh[] Obj = new Mesh[3];
        string lp = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/asset/laptoptext.obj";
        string ps = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/asset/ps5text.obj";
        string pc = "C:/Users/Yunat/source/repos/GrafKom_C_Sharp/GrafKom_C_Sharp/asset/pctext.obj";

        public Project_UTS(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        private void SlideShowAnimation() {

            // this whole part is transition animation
            if (KeyboardState.IsKeyReleased(Keys.A) && slide_right == false)
            {
                if (curr <= 1) return;
                Console.WriteLine(Keys.A);
                slide_left = true;
                --curr;
            }


            if (KeyboardState.IsKeyReleased(Keys.D) && slide_left == false)
            {
                if (curr >= 3) return;
                Console.WriteLine(Keys.D);
                slide_right = true;
                ++curr;
            }

            if (!slide_left && !slide_right) return;

            // transition slide right
            if (slide_right == true)
            {
                foreach (var i in Object3D)
                    i.Translate(-0.1f, 0.0f, 0.0f);

                for (int i = 0; i < 3; ++i)
                {
                    Obj[i].Translate(-0.2f, 0.0f, 0.0f);
                }
                ++counter;
            }

            // transition slide left
            if (slide_left == true)
            {
                foreach (var i in Object3D)
                    i.Translate(0.1f, 0.0f, 0.0f);

                for (int i = 0; i < 3; ++i)
                {
                    Obj[i].Translate(0.2f, 0.0f, 0.0f);
                }
                ++counter;
            }

            // reset function for animation
            if (counter >= max_count)
            {
                counter = 0;
                slide_left = false;
                slide_right = false;
            }
        }

        protected override void OnLoad()
        {
            // clear background
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            // Setup Background
            bg.SetupBackground();

            // Setup First Object, Laptop
            Mesh cable = new Mesh();
            Mesh inside = new Mesh();
            Mesh monitor = new Mesh();
            Object3D[0] = new Mesh();
            Object3D[0].LoadObjectFile(path_body);
            Object3D[0].SetupObject(vert_body, frag_body);
            monitor.LoadObjectFile(path_monitor);
            monitor.SetupObject(vert_body, frag_body);
            cable.LoadObjectFile(path_cable);
            cable.SetupObject(vert_cable, frag_cable);
            inside.LoadObjectFile(path_inside);
            inside.SetupObject(vert_inside, frag_inside);
            Object3D[0].AddChild(cable);
            Object3D[0].AddChild(monitor);
            Object3D[0].AddChild(inside);
            Object3D[0].Translate(0.0f, -0.15f, 0.0f);
            Object3D[0].Rotate(0.0f, 0.0f, -15.0f);

            // Setup Second Object
            Mesh cover = new Mesh();
            Mesh isi = new Mesh();
            Mesh text = new Mesh();
            Object3D[1] = new Mesh();
            Object3D[1].LoadObjectFile(base_ps5);
            Object3D[1].SetupObject(vert_body, frag_body);
            cover.LoadObjectFile(cover_ps5);
            cover.SetupObject(vert_inside, frag_inside);
            text.LoadObjectFile(text_ps5);
            text.SetupObject(vert_body, frag_body);
            isi.LoadObjectFile(isi_ps5);
            isi.SetupObject(vert_body, frag_body);
            Object3D[1].AddChild(isi);
            Object3D[1].AddChild(cover);
            Object3D[1].AddChild(text);
            Object3D[1].Scale(1.6f);
            Object3D[1].Rotate(0.0f, 90.0f, 0.0f);

            // Setup Third Object
            Mesh body = new Mesh();
            Mesh layar = new Mesh();
            Mesh PC = new Mesh();
            Mesh Atribut_PC = new Mesh();
            Mesh Atribut_Key = new Mesh();
            Object3D[2] = new Mesh();
            Object3D[2].LoadObjectFile(path_badan);
            Object3D[2].SetupObject(vert_badan, frag_badan);
            Atribut_PC.LoadObjectFile(path_Atribut_PC);
            Atribut_PC.SetupObject(vert_Atribut_PC, frag_Atribut_PC);
            PC.LoadObjectFile(path_PC);
            PC.SetupObject(vert_PC, frag_PC);
            Atribut_Key.LoadObjectFile(path_Atribut_Key);
            Atribut_Key.SetupObject(vert_Atribut_Key, frag_Atribut_Key);
            layar.LoadObjectFile(path_layar);
            layar.SetupObject(vert_layar, frag_layar);
            Object3D[2].AddChild(PC);
            Object3D[2].AddChild(Atribut_PC);
            Object3D[2].AddChild(Atribut_Key);
            Object3D[2].AddChild(layar);
            Object3D[2].Translate(0.0f, -0.2f, 0.0f);
            Object3D[2].Rotate(15.0f, 0.0f, 0.0f);
            Object3D[2].Scale(0.8f);
            Object3D[2].Rotate(0.0f, 90.0f, 0.0f);

            Obj[0] = new Mesh();
            Obj[0].LoadObjectFile(lp);
            Obj[0].SetupObject(vert_PC, frag_PC);
            Obj[1] = new Mesh();
            Obj[1].LoadObjectFile(ps);
            Obj[1].SetupObject(vert_PC, frag_PC);
            Obj[2] = new Mesh();
            Obj[2].LoadObjectFile(pc);
            Obj[2].SetupObject(vert_PC, frag_PC);

            for (int i = 0; i < 3; ++i)
            {
                Obj[i].Translate(6.0f * i, -1.0f, 0.0f);
                Obj[i].Scale(0.5f);
            }

            // setting for animation
            for (int i = 0; i < 3; ++i)
                Object3D[i].Translate(1.5f * i, 0.0f, 0.0f);

            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            bg.Render();

            foreach (var i in Object3D)
            {
                i.Render();
            }

            foreach (var j in Obj)
            {
                j.Render();
            }

            Thread.Sleep(10);
            SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            if (play)
                Object3D[curr - 1].Rotate(0.0f, -0.1f, 0.0f);

            if (KeyboardState.IsKeyReleased(Keys.S))
                play = !play;
            
            SlideShowAnimation();
            base.OnUpdateFrame(args);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, Size.X, Size.Y);
            base.OnResize(e);
        }
    }
}