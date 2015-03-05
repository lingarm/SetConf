using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IWshRuntimeLibrary;
using System.IO;

namespace SetConf
{
    abstract class Profile
    {
        public abstract bool Shortcut(int id);

        public abstract bool Config(int id);

        public abstract bool Work(int id);

        protected abstract void Message(bool satus, string message = "");

        protected bool CheckName()
        {
            string tmp = "";
            char[] username = Environment.UserName.ToCharArray();
            //char[] username = "User15".ToCharArray();/*-----TEMP-----*/
            foreach (char item in username)
            {
                if (Char.IsNumber(item))
                {
                    tmp += item;
                }
            }
            if (tmp.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected int GetID()
        {
            string tmp = "";
            char[] username = Environment.UserName.ToCharArray();
            //char[] username = "user14".ToCharArray();/*-----TEMP-----*/
            foreach (char item in username)
            {
                if (Char.IsNumber(item))
                {
                    tmp += item;
                }
            }
            Console.WriteLine("Ваш ID = " + Int32.Parse(tmp));
            return Int32.Parse(tmp);
        }

        protected void CopyFolder(string BeginDir, string EndDir)
        {
            DirectoryInfo dir_inf = new DirectoryInfo(BeginDir);
            foreach (DirectoryInfo dir in dir_inf.GetDirectories())
            {
                if (Directory.Exists(EndDir + "\\" + dir.Name) != true)
                {
                    Directory.CreateDirectory(EndDir + "\\" + dir.Name);
                }
                CopyFolder(dir.FullName, EndDir + "\\" + dir.Name);
            }
            foreach (string file in Directory.GetFiles(BeginDir))
            {
                string filik = file.Substring(file.LastIndexOf('\\'), file.Length - file.LastIndexOf("\\"));
                System.IO.File.Copy(file, EndDir + "\\" + filik, true);
            }
        }
    }
}
