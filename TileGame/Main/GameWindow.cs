using System;
using ImGuiNET;
using Saffron2D.GuiCollection;
using SFML.Graphics;
using SFML.System;
using TileGame.Game;
using TileGame.Items;
using TileGame.Level;
using TileGame.Tiles;

namespace TileGame.Main
{
    class GameWindow
    {
        private Clock deltaTimeClock;
        private Time DeltaTime;
        private int _generationSpeed = 15;
        private int _mapsizeX = 32;
        private int _mapsizeY = 32;
        private View _activeview;
        Level.Level activeLevel = null;

        public GameWindow()
        {
            this.deltaTimeClock = new Clock();
            this.DeltaTime = new Time();
        }

        public void Run()
        {
            //Window Settings
            var mode = new SFML.Window.VideoMode(1920, 1080);
            View view1 = new View(new FloatRect(-120, -15, 512, 288));
            _activeview = view1;
            //view1.Zoom(2);
            var window = new SFML.Graphics.RenderWindow(mode, "TileGame Portfolio");
            window.SetFramerateLimit(60);
            window.SetKeyRepeatEnabled(true);

            //Setup for Input
            window.KeyPressed += this.Window_KeyPressed;
            window.MouseWheelScrolled += this.Window_MousePressed;
            GuiImpl.Init(window);
            ImGui.LoadIniSettingsFromDisk("imgui.ini");

            //Instantiation of crucial Managers
            var gameManager = new GameManager();

            //var generator = new LevelGenerator(gameManager, new TileFactory(gameManager));

            // Start the game loop
            while (window.IsOpen)
            {
                DeltaTime = this.deltaTimeClock.Restart();
                GuiImpl.Update(window, this.DeltaTime);

                #region ImGui Interface

                if (ImGui.Begin("Level Selection"))
                {
                    ImGui.Columns(2);
                    ImGui.SetColumnWidth(0, 270);
                    ImGui.SetColumnWidth(1, 100);
                    if (ImGui.SliderInt("Spawn Speed", ref _generationSpeed, 1, 15))
                    {
                    }

                    ImGui.SliderInt("Map Size X", ref _mapsizeX, 2, 5000);
                    ImGui.SliderInt("Map Size Y", ref _mapsizeY, 2, 5000);
                    ImGui.NextColumn();

                    if (ImGui.Button("Load 1a & 1b"))
                    {
                        string[] allowedTiles = new[] { nameof(Grass) };
                        string[] allowedBlockers = new[] { nameof(Mountains) };
                        TileAssembly tileAssembly = new TileAssembly(allowedTiles, allowedBlockers);
                        string[] spawnableItems = new[] { nameof(Weapon), nameof(Armor), nameof(Ring) };
                        var itemAssembly = new ItemAssembly(spawnableItems);
                        LevelTemplate levelTemplate = new LevelTemplate(tileAssembly,
                            new Vector2i(_mapsizeX, _mapsizeY),
                            new Vector2f(16, 16), itemAssembly);
                        LoadDefaultLevel(gameManager, ref activeLevel, gameManager, _generationSpeed, levelTemplate);
                    }

                    if (ImGui.Button("Load 1c"))
                    {
                        string[] allowedTiles = new[] { nameof(Grass), nameof(PoisonSwamp) };
                        string[] allowedBlockers = new[] { nameof(Mountains) };
                        TileAssembly tileAssembly = new TileAssembly(allowedTiles, allowedBlockers);
                        string[] spawnableItems = new[] { nameof(Weapon), nameof(Armor), nameof(Ring) };
                        var itemAssembly = new ItemAssembly(spawnableItems);
                        LevelTemplate levelTemplate = new LevelTemplate(tileAssembly,
                            new Vector2i(_mapsizeX, _mapsizeY),
                            new Vector2f(16, 16), itemAssembly);
                        LoadDefaultLevel(gameManager, ref activeLevel, gameManager, _generationSpeed, levelTemplate);
                    }


                    if (ImGui.Button("Load 3"))
                    {
                    }

                    if (ImGui.Button("Load 4"))
                    {
                    }

                    if (ImGui.Button("Load 5"))
                    {
                    }

                    if (ImGui.Button("Unload Current Level"))
                    {
                        UnloadLevel(gameManager, ref activeLevel);
                    }

                    if (ImGui.Button("Tick"))
                    {
                        activeLevel?.Tick();
                    }

                    if (ImGui.Button("Close Game"))
                    {
                        ImGui.SaveIniSettingsToDisk("imgui.ini");
                        window.Close();
                    }

                    ImGui.End();
                }

                if (ImGui.Begin("Camera"))
                {
                    ImGui.Columns(2);
                    if (ImGui.Button("Increase Zoom"))
                    {
                        view1.Zoom(0.95f);
                    }

                    if (ImGui.Button("Decrease Zoom"))
                    {
                        view1.Zoom(1.05f);
                    }

                    ImGui.NextColumn();
                    if (ImGui.Button("Move Camera Right"))
                    {
                        view1.Center = new Vector2f(view1.Center.X - 25, view1.Center.Y);
                    }

                    if (ImGui.Button("Move Camera Left"))
                    {
                        view1.Center = new Vector2f(view1.Center.X + 25, view1.Center.Y);
                    }

                    if (ImGui.Button("Move Camera Up"))
                    {
                        view1.Center = new Vector2f(view1.Center.X, view1.Center.Y - 5);
                    }

                    if (ImGui.Button("Move Camera Down"))
                    {
                        view1.Center = new Vector2f(view1.Center.X, view1.Center.Y + 5);
                    }

                    ImGui.End();
                }

                #endregion

                activeLevel?.Update();
                gameManager.Update();

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

            if (activeLevel != null && activeLevel.LevelGenerationQueue.Count == 0)
            {
                if (e.Code is SFML.Window.Keyboard.Key.Left or SFML.Window.Keyboard.Key.A)
                {
                    Console.WriteLine("Left");
                    activeLevel.MovePlayerLeft();
                }

                if (e.Code is SFML.Window.Keyboard.Key.Right or SFML.Window.Keyboard.Key.D)
                {
                    Console.WriteLine("Right");
                    activeLevel.MovePlayerRight();
                }

                if (e.Code is SFML.Window.Keyboard.Key.Up or SFML.Window.Keyboard.Key.W)
                {
                    Console.WriteLine("Up");
                    activeLevel.MovePlayerUp();
                }

                if (e.Code is SFML.Window.Keyboard.Key.Down or SFML.Window.Keyboard.Key.S)
                {
                    Console.WriteLine("Down");
                    activeLevel.MovePlayerDown();
                }
            }
        }

        private void Window_MousePressed(object sender, SFML.Window.MouseWheelScrollEventArgs s)
        {
            var window = (SFML.Window.Window)sender;
            if (s.Wheel == SFML.Window.Mouse.Wheel.VerticalWheel)
            {
                if (s.Delta > 0)
                {
                    _activeview.Zoom(0.93f);
                }
                else
                {
                    _activeview.Zoom(1.08f);
                }
            }
        }

        private void UnloadLevel(GameManager gm, ref Level.Level activeLevel)
        {
            gm.UnloadAllGameObjects();
            activeLevel?.DestroyAllTiles();
        }

        private void LoadDefaultLevel(GameManager gm, ref Level.Level activeLevel, GameManager gameManager,
            int generationSpeed,
            LevelTemplate template)
        {
            gm.UnloadAllGameObjects();
            activeLevel?.DestroyAllTiles();
            var generator = new LevelGenerator(gameManager, new TileFactory(gameManager));
            activeLevel = generator.GenerateLevel(template, _generationSpeed);
        }
    }
}