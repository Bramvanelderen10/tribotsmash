using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tribot
{
    public static class TriLog
    {
        public static void Log(string message)
        {
            LoggerTon.Instance.Log(message);
        }

        public static void Log(string id, int index, string message)
        {
            string log = id + index + ": " + message;
            LoggerTon.Instance.Log(log);
        }
    }

    public class LoggerTon : Singleton<LoggerTon>
    {
        private bool _enabled = true;

        public void Log(string message)
        {
            if (!_enabled)
                return;

            Debug.Log(message);
        }
    }
}
