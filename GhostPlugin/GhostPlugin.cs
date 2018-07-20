using System;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.Localization;
using TerrariaApi.Server;
using TShockAPI;

namespace Ghost
{
    [ApiVersion(2, 1)]
    public class Ghost : TerrariaPlugin
    {
        public override string Name => "Ghost";

        public override string Author => "SirApples";

        public override string Description => "A plugin that allows admins to become completely invisible to players.";

        public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        public override void Initialize()
        {
            Commands.ChatCommands.Add(new Command("ghost.ghost", OnGhost, "ghost"));
        }
        void OnGhost(CommandArgs args)
        {
            int i = Projectile.NewProjectile(args.Player.LastNetPosition, Microsoft.Xna.Framework.Vector2.Zero, 170, 0, 0);
            Main.projectile[i].timeLeft = 0;
            NetMessage.SendData(27, -1, -1, null, i);
            args.TPlayer.active = !args.TPlayer.active;
            NetMessage.SendData(14, -1, args.Player.Index, null, args.Player.Index, args.TPlayer.active.GetHashCode());
            if (args.TPlayer.active)
            {
                NetMessage.SendData(4, -1, args.Player.Index, null, args.Player.Index);
                NetMessage.SendData(13, -1, args.Player.Index, null, args.Player.Index);
            }
            args.Player.SendSuccessMessage($"{(args.TPlayer.active ? "Dis" : "En")}abled Ghost.");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            base.Dispose(disposing);
        }

        public Ghost(Main game)
            : base(game)
        {
            Order = 10;
        }
    }
}
