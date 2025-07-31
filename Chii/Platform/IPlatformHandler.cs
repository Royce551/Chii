using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chii.Platform
{
    public interface IPlatformHandler
    {
        event EventHandler<EventArgs> ClipboardChanged;

        void Initialize();

        void SetKeyboardHooks();

        void UnsetKeyboardHooks();
    }
}
