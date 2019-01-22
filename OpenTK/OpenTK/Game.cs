using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
namespace OpenTK
{
    public class Game
    {
        
        GameWindow window;
        double theta = 0.0;
        bool away = true;
        double offset = -45;
        double incriment = 1.0;

        bool lightE = true;

        int texture;

        public Game (GameWindow window)
        {
            this.window = window;
            
            Start();
        }

        void Start() {
            window.Load += loaded;
            window.Resize += resize;
            window.UpdateFrame += updateFrame;
            window.RenderFrame += renderF;
            window.KeyPress += keyPress;
            window.KeyDown += keyDown;
            window.KeyUp += keyUp;
            window.Run(1.0 / 60.0);
            
        }

        void resize(object o, EventArgs e) {
            GL.Viewport(0,0, window.Width, window.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Matrix4 matrix = Matrix4.Perspective(45f, window.Width/window.Height, 1.0f, 100.0f);
            GL.LoadMatrix(ref matrix);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        void renderF(object o, EventArgs e) {
            GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.PushMatrix();
            GL.Translate(0.0,0.0,offset);
            GL.Rotate(theta, 1.0, 1.0, 0.0);


            DrawWhiteCube();
            //DrawCube();

            GL.PopMatrix();

            GL.Translate(0,0,-25);
            draw_quad();


            window.SwapBuffers();

            

        }

        void loaded(object o, EventArgs e) {
            GL.ClearColor(0, 0, 0, 255);
            GL.Enable(EnableCap.DepthTest);

            //lighting
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.ColorMaterial);

            float[] light_position = { 20, 20.0f, 80.0f };
            float[] light_diffuse = { 1.0f, 1.0f, 1.0f };
            //float[] light_ambient = { 0.5f, 0.0f, 0.0f };
            GL.Light(LightName.Light0, LightParameter.Position, light_position);
            GL.Light(LightName.Light0, LightParameter.Diffuse, light_diffuse);
            //GL.Light(LightName.Light0, LightParameter.Ambient, light_ambient);

            GL.Enable(EnableCap.Light0);

            GL.Enable(EnableCap.Texture2D);
            GL.GenTextures(1, out texture);
            GL.BindTexture(TextureTarget.Texture2D, texture);

            System.Drawing.Imaging.BitmapData texData = loadimage("G:/Thumbnails/1.1Tree-Fitty.bmp");
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, texData.Width, texData.Height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, texData.Scan0);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            //blending
            GL.BlendFunc(BlendingFactorSrc.Src1Alpha,BlendingFactorDest.OneMinusSrcAlpha);


        }

        void updateFrame(object o, EventArgs e) {

            KeyboardState k = Keyboard.GetState();

            if (k.IsKeyDown(Key.R)) {
                theta += 1.0;
                if (theta > 360)
                {
                    theta -= 360;
                }
            }

            
        }

        void keyPress(object  o, KeyPressEventArgs e) {
            Console.WriteLine(e.KeyChar);
        }

        void keyDown(object o, KeyboardKeyEventArgs e) {
            if (e.Key == Key.L)
                if (GL.IsEnabled(EnableCap.Lighting))
                    lightE = false;//GL.Disable(EnableCap.Lighting);
                else
                    lightE = true; // GL.Enable(EnableCap.Lighting);
        }

        void keyUp(object o, KeyboardKeyEventArgs e)
        {
            
        }

        void draw_quad() {

            GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.Lighting);
            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);

            GL.Begin(BeginMode.Quads);

            GL.Color4(0.0, 0.5, 1.0, .5);

            GL.Vertex3(0, 0, 0);
            GL.Vertex3(5, 0, 0);
            GL.Vertex3(5, 5, 0);
            GL.Vertex3(0, 5, 0);


            GL.End();

            GL.Enable(EnableCap.Texture2D);
            if (lightE)
                GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.DepthTest);
            GL.Disable(EnableCap.Blend);
        }

        void DrawWhiteCube() {
            GL.Begin(BeginMode.Quads);

            GL.Color3(1.0, 1.0, 1.0);

            //front
            GL.Normal3(0.0, 0.0, 1.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-10.0, -10.0, 10);
            GL.TexCoord2(1, 1);
            GL.Vertex3(10.0, -10.0, 10);
            GL.TexCoord2(1, 0);
            GL.Vertex3(10.0, 10.0, 10);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-10.0, 10.0, 10);

            //back
            GL.Normal3(0.0, 0.0, -1.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-10.0, -10.0, -10);
            GL.TexCoord2(1, 1);
            GL.Vertex3(10.0, -10.0, -10);
            GL.Vertex3(10.0, 10.0, -10);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-10.0, 10.0, -10);

            //top
            GL.Normal3(0.0, 1.0, 0.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(10.0, 10.0, 10);
            GL.TexCoord2(1, 1);
            GL.Vertex3(10.0, 10.0, -10);
            GL.TexCoord2(1, 0);
            GL.Vertex3(-10.0, 10.0, -10);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-10.0, 10.0, 10);

            //bottom
            GL.Normal3(0.0, -1.0, 0.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-10.0, -10.0, 10);
            GL.TexCoord2(1, 1);
            GL.Vertex3(10.0, -10.0, 10);
            GL.TexCoord2(1, 0);
            GL.Vertex3(10.0, -10.0, -10);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-10.0, -10.0, -10);

            //right
            GL.Normal3(1.0, 0.0, 0.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(10.0, -10.0, 10);
            GL.TexCoord2(1, 1);
            GL.Vertex3(10.0, -10.0, -10);
            GL.TexCoord2(1, 0);
            GL.Vertex3(10.0, 10.0, -10);
            GL.TexCoord2(0, 0);
            GL.Vertex3(10.0, 10.0, 10);

            //left
            GL.Normal3(-1.0, 0.0, 0.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-10.0, -10.0, 10);
            GL.TexCoord2(1, 1);
            GL.Vertex3(-10.0, -10.0, -10);
            GL.TexCoord2(1, 0);
            GL.Vertex3(-10.0, 10.0, -10);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-10.0, 10.0, 10);

            GL.End();
        }

        void DrawCube() {
            GL.Begin(BeginMode.Quads);

            GL.Color3(1.0, 0.0, 0.0);
            GL.Vertex3(-10.0, -10.0, 10);
            GL.Vertex3(10.0, -10.0, 10);
            GL.Vertex3(10.0, 10.0, 10);
            GL.Vertex3(-10.0, 10.0, 10);

            GL.Color3(0.0, 1.0, 0.0);
            GL.Vertex3(-10.0, -10.0, -10);
            GL.Vertex3(10.0, -10.0, -10);
            GL.Vertex3(10.0, 10.0, -10);
            GL.Vertex3(-10.0, 10.0, -10);

            GL.Color3(1.0, 0.0, 1.0);
            GL.Vertex3(10.0, 10.0, 10);
            GL.Vertex3(10.0, 10.0, -10);
            GL.Vertex3(-10.0, 10.0, -10);
            GL.Vertex3(-10.0, 10.0, 10);

            GL.Color3(1.0, 1.0, 0.0);
            GL.Vertex3(-10.0, -10.0, 10);
            GL.Vertex3(10.0, -10.0, 10);
            GL.Vertex3(10.0, -10.0, -10);
            GL.Vertex3(-10.0, -10.0, -10);


            GL.Color3(1.0, 0.0, 1.0);
            GL.Vertex3(10.0, -10.0, 10);
            GL.Vertex3(10.0, -10.0, -10);
            GL.Vertex3(10.0, 10.0, -10);
            GL.Vertex3(10.0, 10.0, 10);

            GL.Color3(0.0, 1.0, 1.0);
            GL.Vertex3(-10.0, -10.0, 10);
            GL.Vertex3(-10.0, -10.0, -10);
            GL.Vertex3(-10.0, 10.0, -10);
            GL.Vertex3(-10.0, 10.0, 10);

            GL.End();
        }

        System.Drawing.Imaging.BitmapData loadimage(string filePath) {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(filePath);
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0,0,bmp.Width,bmp.Height);
            System.Drawing.Imaging.BitmapData bmpdata = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            bmp.UnlockBits(bmpdata);
            return bmpdata;
        }
    }
}
