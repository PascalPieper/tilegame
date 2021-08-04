using ImGuiNET;
using Saffron2D.GuiCollection;
using SFML.Graphics;
using SFML.System;
using TileGame.Game;

namespace Project.Main
{
    class GameWindow
    {
        private Clock deltaTimeClock;
        private Time DeltaTime;

        public GameWindow()
        {
            this.deltaTimeClock = new Clock();
            this.DeltaTime = new Time();
        }
        public void Run()
        {
            var gm = new GameManager();
            LevelGenerator.LevelGenerator generator = new LevelGenerator.LevelGenerator(gm, new Vector2u(128,55), new Vector2f(1,1));
            generator.Generate();
            deltaTimeClock.Restart();
            var mode = new SFML.Window.VideoMode(1920, 1080);
            View view1 = new View(new FloatRect(0, 0, 32, 18));
            var window = new SFML.Graphics.RenderWindow(mode, "TileGame Portfolio");
            window.KeyPressed += this.Window_KeyPressed;
            GuiImpl.Init(window);
            var circle = new SFML.Graphics.CircleShape(100f)
            {
                FillColor = SFML.Graphics.Color.Blue
            };

            // Start the game loop
            while (window.IsOpen)
            {
                GuiImpl.Update(window, this.DeltaTime);
                DeltaTime = this.deltaTimeClock.Restart();
                if (ImGui.Begin("Stats"))
                {
                    if (ImGui.Button("Load Exercise 1"))
                    {
                        
                    }
                    if (ImGui.Button("Load Exercise 2"))
                    {
                    }
                    if (ImGui.Button("Load Exercise 3"))
                    {
                    }
                    if (ImGui.Button("Load Exercise 4"))
                    {
                    }
                    if (ImGui.Button("Load Exercise 5"))
                    {
                    }
                    if (ImGui.Button("Load Exercise 5"))
                    {
                    }

                    if (ImGui.Button("Close Game"))
                    {
                        window.Close();
                    }
                    ImGui.End();
                }
                
                
                // Process events
                window.SetView(view1);
                window.DispatchEvents();
                gm.Draw(window);
                GuiImpl.Render();
                
                
                window.Display();
                window.Clear();
            }
        }

        /// <summary>
        /// Function called when a key is pressed
        /// </summary>
        private void Window_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            var window = (SFML.Window.Window)sender;
            if (e.Code == SFML.Window.Keyboard.Key.Escape)
            {
                window.Close();
            }
        }
    }
}