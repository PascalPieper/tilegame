﻿using System.Collections.Generic;
using SFML.Audio;
using SFML.Graphics;

namespace TileGame.Game
{
    public class ResourceManager
    {
        private static ResourceManager _instance = null;
        readonly Dictionary<string, Texture> _textures = new Dictionary<string, Texture>();
        readonly Dictionary<string, Sound> _sounds = new Dictionary<string, Sound>();
        readonly Dictionary<string, Font> _fonts = new Dictionary<string, Font>();

        public static ResourceManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ResourceManager();
                }
                return _instance;
            }
        }

        public void LoadTextureFromFile(string name, string path)
        {
            Texture texture = new Texture(path);

            _textures.Add(name, texture);
        }

        public Texture GetTexture(string name)
        {
            return _textures[name];
        }
        

        public bool LoadSoundFromFile(string name, string path)
        {
            SoundBuffer soundBuffer = new SoundBuffer(path);
            Sound s = new Sound(soundBuffer);
            _sounds.Add(name, s);

            return true;

        }

        public bool LoadFontFromFile(string name, string path)
        {
            Font font = new Font(path);
            _fonts.Add(name, font);

            return true;

        }


        public Sound GetSound(string name)
        {
            return _sounds[name];
        }

        public Font GetFont(string name)
        {
            return _fonts[name];
        }


    }
}
