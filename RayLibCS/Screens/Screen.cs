using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayLibCS.Screens
{
    enum GameScreen { UNKNOWN = -1, LOGO = 0, TITLE, OPTIONS, GAMEPLAY, ENDING };

    internal interface Screen
    {
        void InitScreen();
        void UpdateScreen();
        void DrawScreen();
        void UnloadScreen();
        int FinishScreen();
    }
}
