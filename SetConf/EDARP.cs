using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IWshRuntimeLibrary;
using System.IO;

namespace SetConf
{
    class EDARP : Profile
    {
        public override bool Config(int id)
        {
            return true;
        }

        public override bool Work(int id)
        {
            return true;
        }

        public override bool Shortcut(int id)
        {
            return true;
        }

        protected override void Message(bool status, string message = "")
        {

        }
    }
}
