using System;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            var defaultBg = Console.BackgroundColor;

            // Draw title screen
            Console.ForegroundColor = ConsoleColor.Green;
            DrawTitle();
            Console.WriteLine("\n\n");
            Console.WriteLine("          Press any key to start!");
            var key = Console.ReadKey();

            Console.Clear();

            // Play game
            Game game = key.Key == ConsoleKey.Escape ? new Game(true) : new Game(false);
            UInt64 time = game.Start();

            // Draw game over screen
            Console.BackgroundColor = defaultBg;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Clear();
            DrawGameOver();
            Console.WriteLine("\n\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"          You survived for {time / 1000} seconds!");
            Console.WriteLine();

            // Exit on escape
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("          Press escape to exit");
            ConsoleKeyInfo quitKey = new ConsoleKeyInfo();
            while (quitKey.Key != ConsoleKey.Escape)
            {
                quitKey = Console.ReadKey(true);
            }

            // Exit nicely
            Console.BackgroundColor = defaultBg;
            Console.Clear();
        }

        public static void DrawTitle()
        {
            Console.WriteLine("                                                                           ,---,  ");
            Console.WriteLine("                                                                        ,`--.' |  ");
            Console.WriteLine("            .--.--.                                     ,-.             |   :  :  ");
            Console.WriteLine("           /  /    '.                               ,--/ /|             '   '  ;  ");
            Console.WriteLine("          |  :  /`. /        ,---,                ,--. :/ |             |   |  |  ");
            Console.WriteLine("          ;  |  |--`     ,-+-. /  |               :  : ' /              '   :  ;  ");
            Console.WriteLine("          |  :  ;_      ,--.'|'   |    ,--.--.    |  '  /       ,---.   |   |  '  ");
            Console.WriteLine("           \\  \\    `.  |   |  ,\"' |   /       \\   '  |  :      /     \\  '   :  |  ");
            Console.WriteLine("            `----.   \\ |   | /  | |  .--.  .-. |  |  |   \\    /    /  | ;   |  ;  ");
            Console.WriteLine("            __ \\  \\  | |   | |  | |   \\__\\/: . .  '  : |. \\  .    ' / | `---'. |  ");
            Console.WriteLine("           /  /`--'  / |   | |  |/    ,\" .--.; |  |  | ' \\ \\ '   ;   /|  `--..`;  ");
            Console.WriteLine("          '--'.     /  |   | |--'    /  /  ,.  |  '  : |--'  '   |  / | .--,_     ");
            Console.WriteLine("            `--'---'   |   |/       ;  :   .'   \\ ;  |,'     |   :    | |    |`.  ");
            Console.WriteLine("                       '---'        |  ,     .-./ '--'        \\   \\  /  `-- -`, ; ");
            Console.WriteLine("                                     `--`---'                  `----'     '---`\"  ");
        }

        public static void DrawGameOver()
        {
            Console.WriteLine("                                                                                                                           ,---,  ");
            Console.WriteLine("                                                                             ,----..                                    ,`--.' |  ");
            Console.WriteLine("            ,----..                            ____                         /   /   \\                                   |   :  :  ");
            Console.WriteLine("           /   /   \\                         ,'  , `.                      /   .     :                                  '   '  ;  ");
            Console.WriteLine("          |   :     :                     ,-+-,.' _ |                     .   /   ;.  \\                         __  ,-. |   |  |  ");
            Console.WriteLine("          .   |  ;. /                  ,-+-. ;   , ||                    .   ;   /  ` ;      .---.            ,' ,'/ /| '   :  ;  ");
            Console.WriteLine("          .   ; /--`      ,--.--.     ,--.'|'   |  ||    ,---.           ;   |  ; \\ ; |    /.  ./|    ,---.   '  | |' | |   |  '  ");
            Console.WriteLine("          ;   | ;  __    /       \\   |   |  ,', |  |,   /     \\          |   :  | ; | '  .-' . ' |   /     \\  |  |   ,' '   :  |  ");
            Console.WriteLine("          |   : |.' .'  .--.  .-. |  |   | /  | |--'   /    /  |         .   |  ' ' ' : /___/ \\: |  /    /  | '  :  /   ;   |  ;  ");
            Console.WriteLine("          .   | '_.' :   \\__\\/: . .  |   : |  | ,     .    ' / |         '   ;  \\; /  | .   \\  ' . .    ' / | |  | '    `---'. |  ");
            Console.WriteLine("          '   ; : \\  |   ,\" .--.; |  |   : |  |/      '   ;   /|          \\   \\  ',  /   \\   \\   ' '   ;   /| ;  : |     `--..`;  ");
            Console.WriteLine("          '   | '/  .'  /  /  ,.  |  |   | |`-'       '   |  / |           ;   :    /     \\   \\    '   |  / | |  , ;    .--,_     ");
            Console.WriteLine("          |   :    /   ;  :   .'   \\ |   ;/           |   :    |            \\   \\ .'       \\   \\ | |   :    |  ---'     |    |`.  ");
            Console.WriteLine("           \\   \\ .'    |  ,     .-./ '---'             \\   \\  /              `---`          '---\"   \\   \\  /            `-- -`, ; ");
            Console.WriteLine("            `---`       `--`---'                        `----'                                       `----'               '---`\"  ");
        }
    }
}
