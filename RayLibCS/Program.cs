
using RayLibCS.Screens;
using Raylib_cs;
using System.Text;
using static Raylib_cs.Color;
using static Raylib_cs.Raylib;

namespace RayLibCS
{
    class Program
    {
        static Screen currentScreen = new LogoScreen();
        static Font font = new Font();
        static Music music = new Music();
        static Sound fxCoin = new Sound();

        //----------------------------------------------------------------------------------
        // Local Variables Definition (local to this module)
        //----------------------------------------------------------------------------------
        const int screenWidth = 800;
        const int screenHeight = 450;

        // Required variables to manage screen transitions (fade-in, fade-out)
        static float transAlpha = 0.0f;
        static bool onTransition = false;
        static bool transFadeOut = false;
        static Screen transFromScreen = null;
        static Screen transToScreen = null;

        public static void Main()
        {
            InitWindow(screenWidth, screenHeight, "Hello World");

            SetTargetFPS(60);

            InitAudioDevice();

            font = LoadFont("resources\\mecha.png");
            music = LoadMusicStream("resources\\ambient.ogg");
            fxCoin = LoadSound("resources\\coin.wav");

            SetMusicVolume(music, 1.0f);
            PlayMusicStream(music);

            currentScreen = new LogoScreen();
            currentScreen.InitScreen();

            while (!WindowShouldClose())
            {
                UpdateDrawFrame();
            }

            // Unload GameScreen

            UnloadFont(font);
            UnloadMusicStream(music);
            UnloadSound(fxCoin);

            CloseAudioDevice();

            CloseWindow();
        }

        static void ChangeToScreen(Screen screen)
        {
            currentScreen.UnloadScreen();

            screen.InitScreen();

            currentScreen = screen;
        }

        static void TransitionToScreen(Screen screen)
        {
            onTransition = true;
            transFadeOut = false;
            transFromScreen = currentScreen;
            transToScreen = screen;
            transAlpha = 0.0f;
        }

        static void UpdateTransition()
        {
            if (!transFadeOut)
            {
                transAlpha += 0.05f;

                if (transAlpha > 1.0f)
                {
                    transAlpha = 1.0f;

                    currentScreen.UnloadScreen();

                    transToScreen.InitScreen();

                    currentScreen = transToScreen;

                    transFadeOut = true;
                }
            }
            else
            {
                transAlpha -= 0.02f;

                if (transAlpha < -0.01f)
                {
                    transAlpha = 0.0f;
                    transFadeOut = false;
                    onTransition = false;
                    transFromScreen = null;
                    transToScreen = null;
                }
            }
        }

        // Draw transition effect (full-screen rectangle)
        static void DrawTransition()
        {
            DrawRectangle(0, 0, GetScreenWidth(), GetScreenHeight(), Fade(BLACK, transAlpha));
        }

        // Update and draw game frame
        static void UpdateDrawFrame()
        {
            // Update
            //----------------------------------------------------------------------------------
            UpdateMusicStream(music);       // NOTE: Music keeps playing between screens

            if (!onTransition)
            {

                currentScreen.UpdateScreen();
                switch (currentScreen)
                {
                    case GameplayScreen:
                        {
                            if (currentScreen.FinishScreen() == 1)
                            { TransitionToScreen(new LogoScreen()); }
                            else if (currentScreen.FinishScreen() == 2)
                            { TransitionToScreen(new EndingScreen()); }

                        }
                        break;
                    case LogoScreen:
                        {
                            if (currentScreen.FinishScreen() == 1)
                            { TransitionToScreen(new TitleScreen()); }
                        }
                        break;
                    case TitleScreen:
                        {
                            if (currentScreen.FinishScreen() == 2)
                            { TransitionToScreen(new GameplayScreen()); }
                        }
                        break;
                    case OptionScreen:
                        {
                            if (currentScreen.FinishScreen() == 1) 
                            { TransitionToScreen(new TitleScreen()); }
                        }
                        break;
                    case EndingScreen:
                        {
                            if (currentScreen.FinishScreen() == 1)
                            { TransitionToScreen(new TitleScreen()); }
                        }
                        break;
                    default:
                        break;
                }
            }
            else UpdateTransition();    // Update transition (fade-in, fade-out)
                                        //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();

            ClearBackground(RAYWHITE);

            currentScreen.DrawScreen();

            // Draw full screen rectangle in front of everything
            if (onTransition) DrawTransition();

            //DrawFPS(10, 10);

            EndDrawing();
            //----------------------------------------------------------------------------------
        }

    }
}