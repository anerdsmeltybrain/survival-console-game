using System;
using System.Linq;

public class Player(string name) {
	public string name { get; set;} = name; 
	public int health = 3;
	public int position { get; set;}  = 0;

	public void updatePos() {
		this.position += 1;
	}

	public void subHealth() {
		this.health -= 1;
	}

	public void addHealth() {
		this.health += 1;
	}
}

public class LevelHandler(Player player, int [,] level) {

	Player player {get; set;} = player;
	int [,] level {get; set;} = level;
	int streak = 0;
	string title_msg = """
Welcome to the Game :D, you have 3 choices use (1 2 3)
each lane has a bomb it's your job to choose the right one!
""";
			// ConsoleColor colors =  ConsoleColor {
	// 	ConsoleColor.DarkBlue,
	// 	ConsoleColor.Blue,
	// 	ConsoleColor.Black
	// };

	public void displayTitle() {
		Console.WriteLine(title_msg);
		Console.WriteLine($"Health: {player.health} Pos: {player.position}");
	}
	
	private void checkPos() {
		if (player.position == 6) {
			Console.WriteLine("You Win!");
		}
	}

	public void readInput() {
		string keyInput = Console.ReadLine();
		int intkeyinput = Convert.ToInt32(keyInput);
		switch(intkeyinput) {
			case 1: case 2: case 3:
					if(level[player.position, intkeyinput - 1] == 1) {
						player.subHealth();
						Console.WriteLine("you hit a bomb!");
						player.updatePos();
						streak = 0;
					} else {
						player.updatePos();
						streak += 1;
						if (streak == 3) {
							player.addHealth();
							streak = 0;
						}
					}
				break;
			default:
				Console.WriteLine("you pressed not a valid key");
				readInput();
				break;
		}
	}

	public void monitorHealth(Player player) {
		switch(player.health) {
			case >= 3:
				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
				break;
			case 2:
				Console.BackgroundColor = ConsoleColor.DarkBlue;
				Console.ForegroundColor = ConsoleColor.White;
				break;
			case 1:
				Console.BackgroundColor = ConsoleColor.Blue;
				Console.ForegroundColor = ConsoleColor.White;
				break;
			default:
				Console.WriteLine("You Lose");
				Environment.Exit(0);
				break;
		}
	}

	public void winCondition() {
		if (player.health > 0 && player.position > 6)
			Console.WriteLine("You Win!!!");
	}}

namespace survival {
	
	class Program {
		//the level
		static int[,] level = new int[7, 3];

        static void Main() {

		Player godfree = new Player("godfree");
		LevelHandler levelhandle = new LevelHandler(godfree, level);

		//generate the level
			for (int i = 0; i < 7; i++) {
						
				for (int j = 0; j < 3; j++) {
				if(((j == 2) == (((level[i, 0] == 0) && (level[i,1] ==  0))))) {
						level[i, 2] = 1;
						break;
				}
					Random randone = new Random();
					Random randtwo = new Random();
					int rand_num_one = randone.Next(2);
					int rand_num_two = randtwo.Next(99);
					// Console.WriteLine($"RN2: {rand_num_two} [{i}] [{j}] = {rand_num_one}");
						switch(rand_num_two) {
								case > 99: break;
								case > 66: break;
								case > 33:
									rand_num_one = (rand_num_one == 1) ? level[i, j] = 1 : level[i, j] = 0;
									level[i,j] = rand_num_one;

									if (rand_num_one == 1) i++;
									//player read inputs here
									continue;
								default:
									break;
						}
					}
				}
				
			Console.Clear();
			// levelhandle.displayTitle();
			for (int i = 0; i < 7; i++) {
				levelhandle.monitorHealth(godfree);
				levelhandle.winCondition();
				// Console.Clear();
				levelhandle.displayTitle();
				levelhandle.readInput();
				// levelhandle.displayTitle();
				// Console.WriteLine($"{level[i, 0]}{level[i, 1]}{level[i, 2]}");
			for (int j = 0; j < i + 1; j++) 
					Console.WriteLine($"{level[j, 0]}{level[j, 1]}{level[j, 2]}");

			}
				
		}
	}
}
