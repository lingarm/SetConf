using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IWshRuntimeLibrary;
using System.IO;

namespace SetConf
{
    class ASOPD : Profile
    {
        public ASOPD()
        {
            if (CheckName())
            {
                int id = GetID();

                if (!Work(id) || !Config(id) || !Shortcut(id))
                    Console.ReadKey(true);
            }
            else
                Message(false, "Имя пользователя дожно состоять из слова \"User\" и цифр");
        }

        public override bool Config(int id)
        {
            string path = @"D:\asopdsoc\soc.cfg";

            if(System.IO.File.Exists(path))
            {
                string str = "";
                using (System.IO.StreamReader reader = System.IO.File.OpenText(path))
                {
                    str = reader.ReadToEnd();
                }
                if (str.Contains("ctlg work     D:\\WORK"))
                {
                    str = str.Replace("ctlg work     D:\\WORK", "ctlg work     D:\\WORK" + id);
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\asopdsoc\soc" + id + ".cfg"))
                    {
                        file.Write(str);
                    }
                    Message(true, "Конфигуратор soc" + id + ".cfg");
                    return true;
                }
                else
                {
                    Message(false, "В конфигураторе \"D:\\asopdsoc\\soc.cfg\" отсутствует строка \"D:\\asopdsoc\\soc.cfg\"");
                    return false;
                }
            }
            else
            {
                Message(false, "Файл \"D:\\asopdsoc\\soc.cfg\" не существует или находится в другом месте");
                return false;
            }
        }

        public override bool Work(int id)
        {
            string BeginDir = @"D:\WORK";

            if (System.IO.Directory.Exists(BeginDir))
            {
                string EndDir = BeginDir + id.ToString();
                Directory.CreateDirectory(EndDir);
                CopyFolder(BeginDir, EndDir);
                Message(true, "Каталог Work" + id.ToString());
                return true;
            }
            else
            {
                Message(false, "Папка \"D:\\WORK\" не существует или находится в другом месте");
                return false;
            }
        }

        public override bool Shortcut(int id)
        {
            if (System.IO.File.Exists(@"D:\asopdsoc\soc" + id.ToString() + ".cfg"))
            {
                object shortAdr = (object)"Desktop";
                WshShell shell = new WshShell();
                string link = ((string)shell.SpecialFolders.Item(ref shortAdr)) + @"\ASOPD.lnk";
                IWshShortcut shorcut = (IWshShortcut)shell.CreateShortcut(link);
                shorcut.TargetPath = @"D:\asopdsoc\run_soc.bat";
                shorcut.Arguments = @"/cfg:asopd" + id + ".cfg";
                shorcut.WorkingDirectory = @"D:\asopdsoc";
                shorcut.Save();
                Message(true, "Ярлык ASOPDsoc");
                return true;
            }
            else
            {
                Message(false, "Ярлык не создан, так как не создан конфигуратор");
                return false;
            }
        }

        protected override void Message(bool status, string message = "")
        {
            if (status)
                Console.WriteLine("Готово!!!");
            else
                Console.WriteLine("Ошибка!!!");

            Console.WriteLine(message + "\n");
        }
    }
}
