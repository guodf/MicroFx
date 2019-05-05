using System;
using System.Collections.Generic;
using System.Text;

namespace MicroFx.Logging
{
    public interface ILogger
    {
        void Log<T>(T message);

       // void Log<T>(LogType type, T message);
    }
}
