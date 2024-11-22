using GameDevExperience.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text.Json;
using System.IO;
using Microsoft.Xna.Framework.Media;
using static System.Net.Mime.MediaTypeNames;
namespace GameDevExperience
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ScreenManager _screenManager;
        private BinaryBeats _binaryBeats;
        private int _screenWidth = 960;
        private int _screenHeight = 540;
        private Song _song;
        private Beatmap _beatmap;
        #region luke stuff

        #endregion

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.Title = "The Game Dev Experience";
            /* commented for luke testing
            _screenManager = new ScreenManager(this);
            Components.Add(_screenManager);

            _screenManager.AddScreen(new MainMenu());
            */
            _graphics.PreferredBackBufferWidth = _screenWidth;
            _graphics.PreferredBackBufferHeight = _screenHeight;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _beatmap = new Beatmap();
            _beatmap = LoadBeatMap("test.json");
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _song = Content.Load<Song>("a-video-game");
            _binaryBeats = new BinaryBeats(_song, _beatmap);
            _binaryBeats.LoadContent(Content);
            FontText.AddFont("PublicPixel", Content.Load<SpriteFont>("PublicPixel"));
        }
        /// <summary>
        /// loads in the actions from a json
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Beatmap LoadBeatMap(string path)
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Beatmap>(json);
        }
        protected override void Update(GameTime gameTime)
        {
            _binaryBeats.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _binaryBeats.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}