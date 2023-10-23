using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayLibCS.Screens
{
    internal class OptionScreen : GenericScreen
    {
        static int framesCounter = 0;
        static int finishScreen = 0;

        public override void DrawScreen()
        {

        }

        public override int FinishScreen()
        {
            return finishScreen;
        }

        public override void InitScreen()
        {
            // TODO: Initialize OPTIONS screen variables here!
            framesCounter = 0;
            finishScreen = 0;
        }

        public override void UnloadScreen()
        {
           
        }

        public override void UpdateScreen()
        {
            throw new NotImplementedException();
        }
    }
}
