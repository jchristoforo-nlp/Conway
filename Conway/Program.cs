

namespace MyApp 
{
    internal class Program
    {
        // global variable (on/off switch)
        public static bool alive = true;
        public static ConsoleColor live = ConsoleColor.White;
        public static ConsoleColor dead = ConsoleColor.Black;

        static void Main(string[] args)
        {
            var speed = 0;
            var speedMessage = "set to normal.";
            var message = string.Empty;

            foreach (var arg in args)
            {
                // check if speed value was provided
                var refresh = arg.Contains("refresh-rate");
                if (refresh)
                {
                    // find if refresh rate was specified and separate values
                    var cycleRate = Array.FindIndex(args, row => row.Contains("refresh-rate"));
                    var num = String.Join("", args[cycleRate + 1].Where(char.IsDigit));
                    var str = String.Join("", args[cycleRate + 1].Where(char.IsLetter));

                    // did user provide a valid number?
                    int isNumber;
                    bool isNumeric = int.TryParse(num, out isNumber);

                    if (isNumeric)
                    {
                        // thanks for the number but not valid and you get no sleep 
                        if (isNumber < 0)
                        {
                            speed = 0;
                            num = "0";
                            Console.WriteLine("Thanks for the number but not valid and you get no sleep. :(");
                        }

                        speed = Int32.Parse(num);
                        if (str == "s")
                        {
                            speed *= 1000;
                        }
                        else if (str == "m")
                        {
                            speed *= 60000;
                        }
                        else
                        {
                            str = "ms";
                        }
                        speedMessage = string.Format("running at speed {0}{1}", num, str);
                    }
                    else
                    {
                        Console.WriteLine("Speed value is not an integer, will not be accepted in running app. Please provide value number to function without issue.");
                    }
                }

                // check if live color value was provided
                var liveColor = arg.Contains("live-color");
                if (liveColor)
                {
                    var lvColor = Array.FindIndex(args, row => row.Contains("live-color"));
                    var lvColorValue = args[lvColor + 1];

                    if (lvColorValue != null || lvColorValue != string.Empty)
                    {
                        message = SetLvColor(lvColorValue);
                        Console.WriteLine(message);
                    }
                    else
                    {
                        Console.WriteLine("Live cells will default to white.");
                    }
                }

                // check if dead color value was provided
                var deadColor = arg.Contains("dead-color");
                if (deadColor)
                {
                    var ddColor = Array.FindIndex(args, row => row.Contains("dead-color"));
                    var ddColorValue = args[ddColor + 1];

                    if (ddColorValue != null || ddColorValue != string.Empty)
                    {
                        message = SetDdColor(ddColorValue);
                        Console.WriteLine(message);
                    }
                    else
                    {
                        Console.WriteLine("Dead cells will default to black.");
                    }
                }

                // check which animal was chosen
                var speak = arg.Contains("draw-a");
                if (speak)
                {
                    var spLike = Array.FindIndex(args, row => row.Contains("draw-a"));
                    var sound = args[spLike + 1];

                    if (sound.ToLower() == "dog")
                    {
                        Draw("dog");
                    }
                    else if (sound.ToLower() == "bunny")
                    {
                        Draw("bunny");
                    }
                    else if (sound.ToLower() == "owl")
                    {
                        Draw("owl");
                    }
                    else
                    {
                        Console.WriteLine("Animal not recognized");
                    }
                }

                if (arg.Contains("funny"))
                {
                    Console.WriteLine("The other day I told my friend 10 jokes about binary");
                    Console.WriteLine("\n");
                    Console.WriteLine("........wait for it...Unfortuntaly, he didn't get either of them");
                    System.Threading.Thread.Sleep(1500);
                    Console.WriteLine("\n");
                    Console.WriteLine("  bah hah haha               :P");
                    Console.WriteLine("\n");
                }
            }

            Console.WriteLine("Speed will be " + speedMessage);

            var w = Console.WindowWidth;
            var h = Console.WindowHeight;

            Console.WriteLine("Screen width = " + w.ToString() + " height = " + h.ToString());
            Thread.Sleep(5000);
            Console.WriteLine("...and GO!");
            Thread.Sleep(1500);

            var arrConway = new int[h, w];
            arrConway = Randomize(arrConway);

            Initialize(arrConway);

            while (alive)
            {
                Thread.Sleep(speed);
                Console.Clear();
                // set to stop unless a live cell is found in the next round
                alive = false;
                arrConway = Conwayed(arrConway);
            }
        }

        private static int[,] Conwayed(int[,] lastArray)
        {
            //bool alive = false;
            var newArray = new int[lastArray.GetLength(0), lastArray.GetLength(1)];

            for (int z = 0; z < lastArray.GetLength(0); z++)
            {
                // go thru array
                for (int l = 0; l < lastArray.GetLength(1); l++)
                {
                    int upperleft = 0;
                    int left = 0;
                    int lowerleft = 0;
                    int upper = 0;
                    int lower = 0;
                    int upperright = 0;
                    int right = 0;
                    int lowerright = 0;

                    // test for prior row
                    if (z > 0)
                    {
                        upper = lastArray[z - 1, l];

                        // we have prior row, test for cell to left 
                        if (l - 1 >= 0)
                        {
                            upperleft = lastArray[z - 1, l - 1];
                        }

                        // we have prior row, test for cell to right
                        if (l + 1 < lastArray.GetLength(1))
                        {
                            upperright = lastArray[z - 1, l + 1];
                        }
                    }

                    // test for left cell
                    if (l - 1 >= 0)
                    {
                        left = lastArray[z, l - 1];
                    }

                    // test for right cell
                    if (l + 1 < lastArray.GetLength(1))
                    {
                        right = lastArray[z, l + 1];
                    }

                    // test for next row
                    if (z + 1 < lastArray.GetLength(0))
                    {
                        lower = lastArray[z + 1, l];

                        // we have next row, test for cell to left
                        if (l - 1 >= 0)
                        {
                            lowerleft = lastArray[z + 1, l - 1];
                        }

                        // we have next row, test for cell to right
                        if (l + 1 < lastArray.GetLength(1))
                        {
                            lowerright = lastArray[z + 1, l + 1];
                        }
                    }

                    // count up cells and determine next state
                    var cnt = upperleft + left + lowerleft + upper + lower + upperright + right + lowerright;

                    if ((cnt < 4 && cnt >= 2) && lastArray[z, l] == 1)
                    {
                        Console.ForegroundColor = live;
                        Console.BackgroundColor = live;
                        newArray[z, l] = 1;
                        alive = true;
                    }
                    else if (cnt == 3 && lastArray[z, l] == 0)
                    {
                        Console.ForegroundColor = live;
                        Console.BackgroundColor = live;
                        newArray[z, l] = 1;
                        alive = true;
                    }
                    else
                    {
                        Console.ForegroundColor = dead;
                        Console.BackgroundColor = dead;
                        newArray[z, l] = 0;
                    }

                    Console.Write(newArray[z, l]); //<--- this will be the new cell value
                }
            }

            return newArray;
        }

        private static void Initialize(int[,] arrConway)
        {
            // go thru rows
            for (int z = 0; z < arrConway.GetLength(0); z++)
            {
                // go thru columns
                for (int l = 0; l < arrConway.GetLength(1); l++)
                {
                    Console.BackgroundColor = dead;
                    Console.ForegroundColor = dead;
                    if (arrConway[z, l] == 1)
                    {
                        Console.ForegroundColor = live;
                        Console.BackgroundColor = live;
                    }
                    Console.Write(arrConway[z, l]); //<--- this will be value
                }
            }
        }

        private static int[,] Randomize(int[,] array)
        {
            var rowLen = array.GetLength(0);
            var widthLen = array.GetLength(1);

            for (int r = 0; r < rowLen; r++)
            {
                for (int c = 0; c < widthLen; c++)
                {
                    var rand = new Random();
                    int rand_num = rand.Next(0, 2);
                    array[r, c] = rand_num;
                }
            }

            return array;
        }

        private static string SetLvColor(string color)
        {
            live = GetColor(color);
            return "Livecolore cells are presented in " + color;
        }
        
        private static string SetDdColor(string color)
        {
            dead = GetColor(color);
            return "Dead colore cells are presented in " + color;
        }

        private static ConsoleColor GetColor(string color)
        {
            var thisColor = new ConsoleColor();
            switch (color.ToLower())
            {
                case "red":
                    thisColor = ConsoleColor.Red;
                    break;
                case "yellow":
                    thisColor = ConsoleColor.Yellow;
                    break;
                case "blue":
                    thisColor = ConsoleColor.Blue;
                    break;
                default:
                    break;
            }
            return thisColor;
        }

        private static void Draw(string animal)
        {
            switch (animal)
            {
                case "dog":
                    Console.WriteLine("          ___");
                    Console.WriteLine("( ___ ( ) '  ` ;");
                    Console.WriteLine("/  ,     /  `");
                    Console.WriteLine("\\ \\ ''- -\\ \\  ");
                    break;

                case "bunny":
                    Console.WriteLine(" (\\___/)");
                    Console.WriteLine(" (='.'=)");
                    Console.WriteLine(" (')_(')");
                    break;

                case "owl":
                    Console.WriteLine(" ,____,");
                    Console.WriteLine(" ( 0,0)");
                    Console.WriteLine("  /)_)");
                    Console.WriteLine("   ````");
                    break;

                default:
                    break;
            }
            Thread.Sleep(1500);
            Console.WriteLine("ta da!");
            Thread.Sleep(5000);
        }
    }
}
