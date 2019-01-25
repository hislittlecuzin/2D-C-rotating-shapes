using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace TKMapTool
{
    public class Shader
    {
        int handle;

        public Shader(string vertexPath, string fragmentPath) {
            int vertexShader, fragmentShader;
            try
            {
                string vertexShaderSource;
                using (StreamReader reader = new StreamReader(vertexPath, Encoding.UTF8))
                {
                    vertexShaderSource = reader.ReadToEnd();
                }

                string fragmentShaderSource;
                using (StreamReader reader = new StreamReader(fragmentPath, Encoding.UTF8))
                {
                    fragmentShaderSource = reader.ReadToEnd();
                }

                vertexShader = GL.CreateShader(ShaderType.VertexShader);
                GL.ShaderSource(vertexShader, vertexShaderSource);

                fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
                GL.ShaderSource(fragmentShader, fragmentShaderSource);

                GL.CompileShader(vertexShader);

                string infoLogVert = GL.GetShaderInfoLog(vertexShader);
                if (infoLogVert != System.String.Empty)
                {
                    System.Console.WriteLine(infoLogVert);
                }

                GL.CompileShader(fragmentShader);

                string infoLogFrag = GL.GetShaderInfoLog(fragmentShader);
                if (infoLogFrag != System.String.Empty)
                    System.Console.WriteLine(infoLogFrag);

                handle = GL.CreateProgram();
                GL.AttachShader(handle, vertexShader);
                GL.AttachShader(handle, fragmentShader);

                GL.LinkProgram(handle);

                GL.DetachShader(handle, vertexShader);
                GL.DetachShader(handle, fragmentShader);
                GL.DeleteShader(fragmentShader);
                GL.DeleteShader(vertexShader);
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }

        }

        public void Use() {
            GL.UseProgram(handle);
        }

        bool disposedValue = false;

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                GL.DeleteProgram(handle);
                disposedValue = true;
            }
        }

        ~Shader() {
            GL.DeleteProgram(handle);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
