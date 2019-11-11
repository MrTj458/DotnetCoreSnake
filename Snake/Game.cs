using System;
using System.Threading;

namespace Snake
{
    public class Game
    {
        // Color fields
        ConsoleColor defaultBg;
        ConsoleColor borderColor;
        ConsoleColor snakeColor;
        ConsoleColor snakeHeadColor;
        ConsoleColor obstacleColor;

        // Playfield fields
        int offsetLeft;
        int offsetTop;
        int width;
        int height;

        // Snake fields
        int xDir;
        int yDir;
        int headX;
        int headY;
        int snakeLength;
        CircularQueue snake;

        // Obstacle fields
        int numOfObstacles;
        Position[] obstacles;

        // Game fields
        bool gameOver;
        Random rand;
        UInt64 timer;
        UInt64 obstacleResetTimer;
        int updateSpeed;
        bool debug;

        public Game(bool debug)
        {
            // Colors
            defaultBg = Console.BackgroundColor;
            borderColor = ConsoleColor.Blue;
            snakeColor = ConsoleColor.Green;
            snakeHeadColor = ConsoleColor.White;
            obstacleColor = ConsoleColor.Gray;


            // Playfield settings
            offsetLeft = 10;
            offsetTop = 19;
            width = 120; // Must be an even number!!!
            height = 20;

            // Snake settings
            xDir = 2;
            yDir = 0;
            snakeLength = 30;
            snake = new CircularQueue(snakeLength);

            // Obstacle settings
            numOfObstacles = 10;

            // Game settings
            rand = new Random();
            gameOver = false;
            timer = 0;
            obstacleResetTimer = 0;
            updateSpeed = 75;
            this.debug = debug;

            Program.DrawTitle();
            SetupBorders();
            SetupSnake();
            SetupObstacles();
        }

        public UInt64 Start()
        {
            // Update while the game is still playing
            while (!gameOver)
            {
                CheckInput();
                MoveSnake();
                CheckCollisions();
                ResetObstacles();
                DrawUI();

                if (debug) DebugUI();
                // Wait for next update
                Thread.Sleep(updateSpeed);
                timer += (UInt64)updateSpeed;
            }
            // Game is over
            return timer;
        }

        private void DrawUI()
        {
            Console.SetCursorPosition(offsetLeft, offsetTop - 1);
            Console.BackgroundColor = defaultBg;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"  Snake! {timer / 1000} Seconds");
        }

        private void DebugUI()
        {
            Console.SetCursorPosition(offsetLeft, height + offsetTop + 1);
            Console.BackgroundColor = borderColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"  x: {headX} Y: {headY} Timer: {timer} Obstacle Timer: {obstacleResetTimer}     ");
        }

        private void CheckCollisions()
        {
            // X direction collision
            if (headX > width + offsetLeft + 1 || headX <= offsetLeft)
            {
                gameOver = true;
            }

            // Y direction collision
            if (headY > height + offsetTop || headY <= offsetTop)
            {
                gameOver = true;
            }

            // Check snake collision with itself
            var tails = snake.Items;
            for (var i = 0; i < snakeLength; i++)
            {
                for (var j = 0; j < snakeLength; j++)
                {
                    // Skip checking the same position against itself
                    if (i == j)
                    {
                        continue;
                    }

                    if (tails[i].Left == tails[j].Left && tails[i].Top == tails[j].Top)
                    {
                        gameOver = true;
                    }
                }
            }

            // Check collision with obstacles
            foreach (Position p in obstacles)
            {
                if (p.Left == headX && p.Top == headY)
                {
                    gameOver = true;
                }
            }
        }

        private void MoveSnake()
        {
            // Erase end of tail
            Console.BackgroundColor = defaultBg;
            Position endTail = snake.Dequeue();
            Console.SetCursorPosition(endTail.Left, endTail.Top);
            Console.Write("  ");

            // Change current head to body color
            Console.BackgroundColor = snakeColor;
            Console.SetCursorPosition(headX, headY);
            Console.Write("  ");

            // Make & draw new head with head color
            Console.BackgroundColor = snakeHeadColor;
            Position newHead = new Position(headX + xDir, headY + yDir);
            headX = newHead.Left;
            headY = newHead.Top;
            snake.Enqueue(newHead);
            Console.SetCursorPosition(newHead.Left, newHead.Top);
            Console.Write("  ");
        }

        private void CheckInput()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);

                // Move up
                if (key.Key == ConsoleKey.UpArrow)
                {
                    xDir = 0;
                    yDir = -1;
                }

                // Move down
                if (key.Key == ConsoleKey.DownArrow)
                {
                    xDir = 0;
                    yDir = 1;
                }

                // Move left
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    xDir = -2;
                    yDir = 0;
                }

                // Move right
                if (key.Key == ConsoleKey.RightArrow)
                {
                    xDir = 2;
                    yDir = 0;
                }

                // Exit on escape key press
                if (key.Key == ConsoleKey.Escape)
                {
                    gameOver = true;
                }
            }
        }

        private void SetupSnake()
        {
            Console.BackgroundColor = snakeColor;
            for (var i = 0; i < snakeLength * 2; i += 2)
            {
                // Create snake tail piece position
                Position pos = new Position(i + offsetLeft + 2, height / 2 + offsetTop);

                // Add it to the snake and draw it
                snake.Enqueue(pos);
                Console.SetCursorPosition(pos.Left, pos.Top);
                Console.Write("  ");

                // Set head position to keep track of collisions
                if (i + 2 == snakeLength * 2)
                {
                    headX = pos.Left;
                    headY = pos.Top;
                }
            }
        }

        private void SetupBorders()
        {
            Console.BackgroundColor = borderColor;

            // Draw top border
            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.WriteLine($"{{0,{width + 4}}}", " ");

            // Draw left / right walls
            for (var i = 0; i < height; i++)
            {
                // Draw left wall
                Console.SetCursorPosition(offsetLeft, offsetTop + i + 1);
                Console.Write("  ");

                // Draw right wall
                Console.SetCursorPosition(offsetLeft + width + 2, offsetTop + i + 1);
                Console.Write("  ");
            }

            // Draw bottom border
            Console.SetCursorPosition(offsetLeft, offsetTop + height + 1);
            Console.WriteLine($"{{0,{width + 4}}}", " ");
        }

        private void SetupObstacles()
        {
            obstacles = new Position[numOfObstacles];

            // Fill obstacles array
            Console.BackgroundColor = obstacleColor;
            for (var i = 0; i < obstacles.Length; i++)
            {
                Position newPos;
                bool onTail = true;
                do
                {
                    // Generate random position in the playfield
                    newPos = new Position(2 * rand.Next((offsetLeft + 2) / 2, (width + offsetLeft + 2) / 2), rand.Next(offsetTop + 1, height + offsetTop + 1));
                    // Make sure it it not on top of a tail piece
                    foreach (Position p in snake.Items)
                    {
                        onTail = p.Left == newPos.Left && p.Top == newPos.Top;

                        if (onTail) break;
                    }
                } while (onTail);

                obstacles[i] = newPos;

                // Draw obstacle
                Console.SetCursorPosition(obstacles[i].Left, obstacles[i].Top);
                Console.Write("  ");
            }
        }

        private void ResetObstacles()
        {
            // Update timer
            obstacleResetTimer += (UInt64)updateSpeed;

            // Only reset every X seconds
            if (obstacleResetTimer / 1000 < 5)
            {
                return;
            }

            // Erase old obstacles
            Console.BackgroundColor = defaultBg;
            foreach (Position p in obstacles)
            {
                Console.SetCursorPosition(p.Left, p.Top);
                Console.Write("  ");
            }

            // Create new obstacles and reset timer
            SetupObstacles();
            obstacleResetTimer = 0;
        }
    }
}
