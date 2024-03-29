﻿using DataAccessLayer.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dal
{
    public class FileRepo : IRepo
    {
        private const string CONFIG_DIR = @"..\..\..\Config";
        private const string CONFIG_PATH = CONFIG_DIR + @"\config.txt";

        private const string IMAGES_DIR = @"..\..\..\Images";


        private const string CONFIG_FAVORITE_PLAYERS = CONFIG_DIR + @"\favPlayers.txt";

        public FileRepo() => CreateFileIfNotExists();

        private void CreateFileIfNotExists()
        {
            Directory.CreateDirectory(CONFIG_DIR);
            if (!File.Exists(CONFIG_PATH) || !File.Exists(CONFIG_FAVORITE_PLAYERS))
            {
                File.Create(CONFIG_PATH).Close();
                File.Create(CONFIG_FAVORITE_PLAYERS).Close();
            }
        }

        public Config GetConfig()
        {
            try
            {
                string line = File.ReadAllText(CONFIG_PATH);
                return Config.ParseFromFile(line);
            }
            catch (Exception)
            {
                return new Config();
            }
        }

        public static List<Player> LoadFavoritePlayersFromFile()
        {
            try
            {
                List<Player> players = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(CONFIG_FAVORITE_PLAYERS));
                if (players == null)
                {
                    return new List<Player>();
                }
                return players;
            }
            catch (Exception)
            {
                return new List<Player>();
            }
        } 

        public void SaveConfig(Config config)
        {
            try
            {
                File.WriteAllText(CONFIG_PATH, config.PrepareConfigForFile());
            }
            catch (Exception)
            {
                return;                
            }
        }

        public void SaveFavoritePlayers(List<Player> players)
        {
            try
            {
                File.WriteAllText(CONFIG_FAVORITE_PLAYERS, JsonConvert.SerializeObject(players));
            }
            catch (Exception)
            {
                throw new Exception("Error saving favorite players");
            }
        }
       
        public void SavePicturesFromPlayers(List<Player> players, string fifaCode)
        {
            if (players == null)
            {
                return;
            }
            string specificPath = IMAGES_DIR + $@"\{fifaCode}";
            Directory.CreateDirectory(specificPath);
            foreach (var player in players)
            {
                // Problem with bitmap. Bitmap is locked for entire lifetime of object and the file or picture is locked
                // So first kill the object, set it to null, and before that save the old bitmap in freshly new bitmap
                // Then you can save

                Bitmap newBitmap = new Bitmap(player.Picture);
                player.Picture.Dispose();
                player.Picture = null;
                newBitmap.Save(specificPath + $"\\{player.name}.png");
            }
        }

        public List<Player> LoadPicutres(List<Player> players, string fifaCode)
        {
            string picFolder = IMAGES_DIR + $@"\{fifaCode}";
            foreach (var player in players)
            {
                if (File.Exists(picFolder + $@"\{player.name}.png"))
                {
                    using (FileStream fs = new FileStream(picFolder + $@"\{player.name}.png", FileMode.Open))
                    {
                        player.Picture = (Bitmap)Image.FromStream(fs);
                        fs.Close();
                    }                    
                }                
            }

            return players;
        }
    }
}
