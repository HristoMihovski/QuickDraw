using System;
using System.Diagnostics;

namespace QuickDrawGame
{
    class QuickDraw
    {
        const string Menu = @"
Quick Draw
Face your opponent and wait for the signal. Once the
signal is given, shoot your opponent by pressing [space]
before they shoot you. It's all about your reaction time.
Choose Your Opponent:
[1] Easy....1000 milliseconds
[2] Medium...500 milliseconds
[3] Hard.....250 milliseconds
[4] Harder...125 milliseconds";
        
        const string Wait = @"
 _O O_
|/|_ wait _|\|
 / \   / \
/ | | \
------------------------------------------------------";
        
        const string Fire = @"
 ********
*  FIRE  *
_O ******** O_
|/|_    _|\|
 / \  spacebar  / \
/ | | \
------------------------------------------------------";
        
        const string LoseTooSlow = @"
   > ╗__O
  //    Too Slow   / \
O/__/\  You Lose   /\
    \ | \
------------------------------------------------------";
        
        const string LoseTooFast = @"
 Too Fast  > ╗__O
  //  You Missed   / \
O/__/\  You Lose   /\
    \ | \
------------------------------------------------------";
        
        const string Win = @"
  O__╔ <
  / \  \\
 /\  You Win  /\__\O
/ |  /
------------------------------------------------------";

        static void Main(string[] args)
        {
            Random random = new Random();
            bool playAgain = true;

            while (playAgain)
            {
                Console.Clear();
                Console.WriteLine(Menu);
                int choice = int.Parse(Console.ReadLine());

                TimeSpan requiredReactionTime = choice switch
                {
                    1 => TimeSpan.FromMilliseconds(1000),
                    2 => TimeSpan.FromMilliseconds(500),
                    3 => TimeSpan.FromMilliseconds(250),
                    4 => TimeSpan.FromMilliseconds(125),
                    _ => TimeSpan.FromMilliseconds(1000)
                };

                Console.Clear();
                Console.WriteLine(Wait);

                TimeSpan signal = TimeSpan.FromMilliseconds(random.Next(2000, 5000));
                Stopwatch stopwatch = new Stopwatch();
                bool isTooFast = false;
                stopwatch.Start();

                while (stopwatch.Elapsed < signal && !isTooFast)
                {
                    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                    {
                        isTooFast = true;
                    }
                }

                Console.Clear();

                if (isTooFast)
                {
                    Console.WriteLine(LoseTooFast);
                }
                else
                {
                    Console.WriteLine(Fire);
                    stopwatch.Restart();
                    bool isTooSlow = true;
                    TimeSpan reactionTime = default;

                    while (stopwatch.Elapsed < requiredReactionTime && isTooSlow)
                    {
                        if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                        {
                            isTooSlow = false;
                            reactionTime = stopwatch.Elapsed;
                        }
                    }

                    Console.Clear();

                    if (isTooSlow)
                    {
                        Console.WriteLine(LoseTooSlow);
                    }
                    else
                    {
                        Console.WriteLine(Win);
                        Console.WriteLine($"Your reaction time: {reactionTime.TotalMilliseconds} ms");
                    }
                }

                Console.WriteLine("Do you want to play again? (y/n)");
                playAgain = Console.ReadKey(true).Key == ConsoleKey.Y;
            }
        }
    }
}
