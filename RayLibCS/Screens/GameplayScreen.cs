using Raylib_cs;
using static Raylib_cs.Raylib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace RayLibCS.Screens
{
    internal class GameplayScreen : GenericScreen
    {
        static int framesCounter = 0;
        static int finishScreen = 0;
        static Model mBox;
        static Vector3 position;
        static Camera3D camera = new Camera3D(
            new Vector3(0,10,10),
            new Vector3(0,0,0),
            new Vector3(0,1,0),
            45f,
            CameraProjection.CAMERA_PERSPECTIVE);


        public override void DrawScreen()
        {
            // TODO: Draw GAMEPLAY screen here!
            DrawRectangle(0, 0, GetScreenWidth(), GetScreenHeight(), Color.PURPLE);
            Vector2 pos = new Vector2(20, 10);
            BeginMode3D(camera);
            DrawModel(mBox, position, 1.0f, Color.WHITE);
            DrawGrid(10, 1.0f);
            EndMode3D();
            DrawTextEx(font, "GAMEPLAY SCREEN", pos, font.BaseSize * 3.0f, 4, Color.MAROON);
            DrawText("PRESS ENTER or TAP to JUMP to ENDING SCREEN", 130, 220, 20, Color.MAROON);
        }

        public override int FinishScreen()
        {
            return finishScreen;
        }

        public override void InitScreen()
        {
            framesCounter = 0;
            finishScreen = 0;

            mBox = LoadModel("resources/helloBlockBench.gltf");
            position = Vector3.Zero;
        }

        public override void UnloadScreen()
        {
            UnloadModel(mBox);
        }

        public override void UpdateScreen()
        {
            if (IsKeyPressed(KeyboardKey.KEY_ENTER) || IsGestureDetected(Gesture.GESTURE_TAP))
            {
                finishScreen = 2;
                PlaySound(fxCoin);
            }

            if (IsKeyDown(KeyboardKey.KEY_RIGHT ))
            {
                camera.Position.X += 1;
            }
            else if (IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                camera.Position.X -= 1;
            }
        }
    }
}
