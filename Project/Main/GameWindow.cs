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
            //Window Settings
            var mode = new SFML.Window.VideoMode(1920, 1080);
            View view1 = new View(new FloatRect(0, 0, 256, 144));
            var window = new SFML.Graphics.RenderWindow(mode, "TileGame Portfolio");
            window.SetFramerateLimit(60);
            
            //Setup for Input
            window.KeyPressed += this.Window_KeyPressed;
            GuiImpl.Init(window);
            
            //Instantiation of crucial Managers
            var gameManager = new GameManager();
            var activeLevel = new Level.Level(gameManager);
            
            // Start the game loop
            while (window.IsOpen)
            {
                DeltaTime = this.deltaTimeClock.Restart();
                GuiImpl.Update(window, this.DeltaTime);
                
                #region ImGui Interface

                if (ImGui.Begin("Level Selection"))
                {
                    if (ImGui.Button("Load Exercise 1"))
                    {
                        activeLevel.UnloadLevel();
                        LevelGenerator generator = new LevelGenerator(gameManager);
                        
                        string[] allowedTiles = new[] { "Grass" };
                        string[] allowedBlockers = new[] { "Mountains" };
                        TileAssembly tileAssembly = new TileAssembly(allowedTiles, allowedBlockers);
                        LevelTemplate levelTemplate = new LevelTemplate(tileAssembly, new Vector2u(25, 25),
                            new Vector2f(4, 4));
                        activeLevel = generator.Generate(levelTemplate);
                    }

                    if (ImGui.Button("Load Exercise 2"))
                    {
                        activeLevel.UnloadLevel();
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
                        view1.Size = new Vector2f(view1.Size.X + 55, view1.Size.Y + 55);
                    }

                    if (ImGui.Button("Decrease Zoom"))
                    {
                        view1.Size = new Vector2f(view1.Size.X - 55, view1.Size.Y - 55);
                    }

                    if (ImGui.Button("Center Camera"))
                    {
                        view1.Size = new Vector2f(1000, 1000);
                    }

                    if (ImGui.Button("Load Exercise 4"))
                    {
                    }

                    ImGui.End();
                }

                #endregion

                // Process events
                window.SetView(view1);
                window.DispatchEvents();
                gameManager.Draw(window);
                GuiImpl.Render();
                window.Display();
                window.Clear();

                deltaTimeClock.Restart();
            }
        }
        
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