using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Omicron
{
    public static class StateManager
    {
        static Dictionary<string, IMenu> menus = new Dictionary<string,IMenu>();
        static string currentMenu;
        static string nextMenu;

        static IScreen _loadScreen;

        enum GameState
        {
            InMenu,
            InGame,
            TransOn,
            TransOff,
            BackOn,
            BackOff,
            Paused,
            ActivePaused,
            LoadMenus,
            LoadGame
        }
        static GameState gameState = GameState.LoadMenus;

        static Dictionary<string, ILevel> levels = new Dictionary<string, ILevel>();
        static string currentLevel;

        static Thread load;
        static ContentManager menuContent;
        static ContentManager gameContent;

        static Game game;

        public static void AddMenu(string Key, IMenu Menu)
        {
            if (Menu != null)
                menus.Add(Key, Menu);   
        }

        public static void AddLevel(string Key, ILevel Level)
        {
            if (Level != null && Level.PauseScreen != null && Level.LoadScreen != null)
                levels.Add(Key, Level);
        }

        public static void Load(Game game, string startMenu, IScreen loadScreen)
        {
            if (loadScreen != null && menus.ContainsKey(startMenu))
            {
                StateManager.game = game;
                menuContent = new ContentManager(game.Services, game.Content.RootDirectory);
                gameContent = new ContentManager(game.Services, game.Content.RootDirectory);
                _loadScreen = loadScreen;
                _loadScreen.Load(menuContent);
                load = new Thread(() =>
                {
                    foreach (IMenu menu in menus.Values)
                    {
                        menu.Load(menuContent);
                    }
                    currentMenu = startMenu;
                    gameState = GameState.TransOn;
                });
                load.Start();
            }
        }

        public static void ChangeMenu(string MenuKey)
        {
            if (menus.ContainsKey(MenuKey) && gameState == GameState.InMenu)
            {
                nextMenu = MenuKey;
                gameState = GameState.TransOff;
            }
        }
        public static void ReturnToMenu(string MenuKey)
        {
            if (menus.ContainsKey(MenuKey) && gameState == GameState.InMenu)
            {
                nextMenu = MenuKey;
                gameState = GameState.BackOff;
            }
        }

        public static void Pause(bool PauseGame)
        {
            if (gameState == GameState.InGame)
            {
                if (PauseGame)
                    gameState = GameState.Paused;
                else gameState = GameState.ActivePaused;
            }
        }
        public static void Unpause()
        {
            if (gameState == GameState.Paused || gameState == GameState.ActivePaused)
                gameState = GameState.InGame;
        }

        public static void OpenLevel(string LevelKey)
        {
            if (levels.ContainsKey(LevelKey) && gameState == GameState.InMenu)
            {
                currentLevel = LevelKey;
                gameState = GameState.LoadGame;
                levels[currentLevel].LoadScreen.Load(gameContent);
                load = new Thread(() => 
                {
                    foreach (IMenu menu in menus.Values)
                        menu.Unload();
                    menuContent.Unload();
                    levels[currentLevel].Load(gameContent);
                    levels[currentLevel].PauseScreen.Load(gameContent);
                    gameState = GameState.InGame;
                });
                load.Start();
            }
        }
        public static void CloseLevel(string MenuKey)
        {
            Unpause();
            if (menus.ContainsKey(MenuKey) && gameState == GameState.InGame)
            {
                currentMenu = MenuKey;
                gameState = GameState.LoadMenus;
                _loadScreen.Load(menuContent);
                load = new Thread(() =>
                {
                    levels[currentLevel].LoadScreen.Unload();
                    levels[currentLevel].PauseScreen.Unload();
                    levels[currentLevel].Unload();
                    gameContent.Unload();
                    foreach (IMenu menu in menus.Values)
                        menu.Load(menuContent);
                    gameState = GameState.TransOn;
                });
                load.Start();
            }
        }

        public static void EndTransition()
        {
            switch (gameState)
            {
                case GameState.TransOn:
                    {
                        gameState = GameState.InMenu;
                        break;
                    }
                case GameState.TransOff:
                    {
                        currentMenu = nextMenu;
                        gameState = GameState.TransOn;
                        break;
                    }
                case GameState.BackOn:
                    {
                        gameState = GameState.InMenu;
                        break;
                    }
                case GameState.BackOff:
                    {
                        currentMenu = nextMenu;
                        gameState = GameState.BackOn;
                        break;
                    }
            }
        }

        public static void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.InMenu:
                    {
                        menus[currentMenu].HandleInput(gameTime);
                        menus[currentMenu].Update(gameTime);
                        break;
                    }
                case GameState.TransOn:
                    {
                        menus[currentMenu].TransOn(gameTime, false);
                        break;
                    }
                case GameState.TransOff:
                    {
                        menus[currentMenu].TransOff(gameTime, false);
                        break; 
                    }
                case GameState.BackOn:
                    {
                        menus[currentMenu].TransOn(gameTime, true);
                        break;
                    }
                case GameState.BackOff:
                    {
                        menus[currentMenu].TransOff(gameTime, true);
                        break;
                    }
                case GameState.LoadGame:
                    {
                        levels[currentLevel].LoadScreen.Update(gameTime);
                        break;
                    }
                case GameState.InGame:
                    {
                        levels[currentLevel].HandleInput(gameTime);
                        levels[currentLevel].Update(gameTime);
                        break;
                    }
                case GameState.Paused:
                    {
                        levels[currentLevel].PauseScreen.Update(gameTime);
                        break;
                    }
                case GameState.ActivePaused:
                    {
                        levels[currentLevel].Update(gameTime);
                        levels[currentLevel].PauseScreen.Update(gameTime);
                        break;
                    }
                case GameState.LoadMenus:
                    {
                        _loadScreen.Update(gameTime);
                        break;
                    }
            }
        }
        public static void Draw(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.InMenu:
                    {
                        menus[currentMenu].Draw(gameTime);
                        break;
                    }
                case GameState.TransOn:
                    {
                        menus[currentMenu].Draw(gameTime);
                        break;
                    }
                case GameState.TransOff:
                    {
                        menus[currentMenu].Draw(gameTime);
                        break;
                    }
                case GameState.BackOn:
                    {
                        menus[currentMenu].Draw(gameTime);
                        break;
                    }
                case GameState.BackOff:
                    {
                        menus[currentMenu].Draw(gameTime);
                        break;
                    }
                case GameState.LoadGame:
                    {
                        levels[currentLevel].LoadScreen.Draw(gameTime);
                        break;
                    }
                case GameState.InGame:
                    {
                        levels[currentLevel].Draw(gameTime);
                        break;
                    }
                case GameState.Paused:
                    {
                        levels[currentLevel].Draw(gameTime);
                        levels[currentLevel].PauseScreen.Draw(gameTime);
                        break;
                    }
                case GameState.ActivePaused:
                    {
                        levels[currentLevel].Draw(gameTime);
                        levels[currentLevel].PauseScreen.Draw(gameTime);
                        break;
                    }
                case GameState.LoadMenus:
                    {
                        _loadScreen.Draw(gameTime);
                        break;
                    }
            }
        }

        public static void ExitGame()
        {
            foreach (IMenu menu in menus.Values)
                menu.Unload();
            menuContent.Unload();
            if (currentLevel != null)
            {
                levels[currentLevel].LoadScreen.Unload();
                levels[currentLevel].PauseScreen.Unload();
                levels[currentLevel].Unload();
                gameContent.Unload();
            }
            game.Exit();
        }
    }
}