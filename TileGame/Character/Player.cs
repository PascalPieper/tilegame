using ImGuiNET;
using SFML.Graphics;
using SFML.System;
using TileGame.Game;
using TileGame.Interfaces;
using TileGame.Level;

namespace TileGame.Character
{
    public class Player : Char, IMove, IHealth
    {
        private int _health = 100;
        public bool CanMove { get; set; } = true;

        public int Health
        {
            get => _health;
            set
            {
                if (value > _health)
                {
                    _health = 0;
                }
                else
                {
                    _health = value;
                }
            }
        }

        private void ResetStats()
        {
            Health = StartHealth;
            Strength = StartStrength;
            MaxWeight = StartMaxWeight;
            CurrentWeight = 0;
        }

        public void Validate()
        {
            ResetStats();
            foreach (var item in ItemInventory.Items)
            {
            }
        }

        public bool MoveUp()
        {
            this.Sprite.Position += new Vector2f(0, -8);
            return true;
        }

        public bool MoveDown()
        {
            this.Sprite.Position += new Vector2f(0, 8);
            return true;
        }

        public bool MoveLeft()
        {
            this.Sprite.Position += new Vector2f(-8, 0);
            return true;
        }

        public bool MoveRight()
        {
            this.Sprite.Position += new Vector2f(8, 0);
            return true;
        }

        public Player(ItemInventory itemInventory) : base(itemInventory)
        {
            this.Sprite = new Sprite();
            this.Sprite.Texture = ResourceManager.Instance.LoadTexture("resources/player.png");
            Sprite.Scale = new Vector2f(0.65f, 0.65f);
        }

        public override void Update()
        {
            base.Update();
            ImGui.Begin("Player Stats");
            ImGui.SetWindowFontScale(2);

            ImGui.Text("Current Health: " + this.Health);
            ImGui.Text("Strength: " + this.Strength);
            ImGui.Text("Current Weight: " + this.CurrentWeight);
            ImGui.Text("Max Weight: " + this.MaxWeight);

            ImGui.End();
        }


        public void TakeDamage(int amount)
        {
            this.Health -= amount;
        }

        public void OnDeath()
        {
        }
    }
}