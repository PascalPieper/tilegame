using ImGuiNET;
using Project.Level;
using Project.Tiles;
using Saffron2D.GuiCollection;
using SFML.Graphics;
using SFML.System;
using TileGame.Game;
using TileGame.Tiles;

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

            
            deltaTimeClock.Restart();
            var mode = new SFML.Window.VideoMode(1920, 1080);
            View view1 = new View(new FloatRect(0, 0, 256, 144));
            var window = new SFML.Graphics.RenderWindow(mode, "TileGame Portfolio");
            window.SetFramerateLimit(60);
            window.KeyPressed += this.Window_KeyPressed;
            GuiImpl.Init(window);
            // Start the game loop
            while (window.IsOpen)
            {
                GuiImpl.Update(window, this.DeltaTime);
                DeltaTime = this.deltaTimeClock.Restart();
                if (ImGui.Begin("Stats"))
                {
                    if (ImGui.Button("Load Exercise 1"))
                    {
                        string[] allowedTiles = new[] { "Grass" };
                        string[] allowedBlockers = new[] { "Mountains" };
                        TileAssembly tileAssembly = new TileAssembly(allowedTiles, allowedBlockers);
                        LevelTemplate levelTemplate = new LevelTemplate(tileAssembly, new Vector2u(25, 25),
                            new Vector2f(4, 4));
                        Level.LevelGenerator generator = new Level.LevelGenerator(gm);
                        generator.Generate(levelTemplate);
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
                if (ImGui.Begin("Camera"))
                {
                    if (ImGui.Button("Increase Zoom"))
                    {
                        view1.Size = new Vector2f(view1.Size.X + 55,view1.Size.Y + 55);
                    }
                    if (ImGui.Button("Decrease Zoom"))
                    {
                        view1.Size = new Vector2f(view1.Size.X - 55,view1.Size.Y - 55);
                    }
                    if (ImGui.Button("Center Camera"))
                    {
                        view1.Size = new Vector2f(1000,1000);
                    }
                    if (ImGui.Button("Load Exercise 4"))
                    {
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