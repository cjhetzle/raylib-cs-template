using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayLibCS.Screens
{
    internal abstract class GenericScreen : Screen
    {
        protected GameScreen currentScreen;
        protected Font font;
        protected Music music;
        protected Sound fxCoin;

        public abstract void InitScreen();

        public abstract void UnloadScreen();

        public abstract void UpdateScreen();

        public abstract void DrawScreen();

        public abstract int FinishScreen();
    }
}
