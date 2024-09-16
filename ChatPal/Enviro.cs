using DotNetEnv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatPal
{
    internal static class Enviro
    {
        public static string CONNECT()
        {
            string projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string envFilePath = Path.Combine(projectDir, ".env");
            Env.Load(envFilePath);
            return Env.GetString("CONNECTION_STRING");
        }
    }
}
