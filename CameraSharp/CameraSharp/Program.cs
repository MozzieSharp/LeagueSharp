using System;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace CameraSharp
{
    internal class Program
    {
        private static Menu menu;

        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(System.EventArgs args)
        {
            menu = new Menu("Camera#", "camera", true);

            menu.AddItem(new MenuItem("extendedzoom", "Zoomhack").SetValue(false)).ValueChanged += delegate(object sender, OnValueChangeEventArgs e)
            {
                Camera.ExtendedZoom = e.GetNewValue<bool>();
            };

            menu.AddItem(new MenuItem("movesmooth", "Smooth ").SetValue(false));
            
            menu.AddToMainMenu();

            Game.OnUpdate += delegate
            {
                if (menu.Item("movesmooth").GetValue<bool>())
                    MoveSmooth(ObjectManager.Player.Position);
            };
        }

        private static void MoveSmooth(Vector3 position)
        {
            var distance = Camera.Position.Distance(position);


            if (distance <= 1)
            {
                return;
            }

            var speed = Math.Max(0.2f, Math.Min(20, distance * 0.0007f * 20));
            var direction = (position - Camera.Position).Normalized() * (speed);

            Camera.Position = direction + Camera.Position;
        }
    }
}
