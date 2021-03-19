<Query Kind="Program" />

//https://adventofcode.com/2020/day/8
void Main()
{
	string[] input = File.ReadAllLines(@"C:\aoc_2020\day8\input.txt");
	List<Instruction> instructions = input.Select(i => new Instruction() 
	  {type = i.Split(' ').First(), val = Int32.Parse(i.Split(' ').Last()), run = false})
	  .ToList();
	
	Part1(instructions);
	Part2(instructions);
}

void Part1(List<Instruction> instructions)
{
	var running = true;
	int acc = 0;
	int index = 0;

	while (running)
	{
		if (instructions[index].run)
		{
			running = false;
			break;
		}
		switch (instructions[index].type)
		{
			case "nop":
				instructions[index].run = true;
				index++;
				break;
			case "acc":
				acc += instructions[index].val;
				instructions[index].run = true;
				index++;
				break;
			case "jmp":
				instructions[index].run = true;
				index += instructions[index].val;
				break;
		}
	}
	acc.Dump("Part 1");
}

void Part2(List<Instruction> instructions)
{
	
	bool terminated = false;
	int acc = 0;
	
	for(int i = 0; i < instructions.Count(); i++)
	{
		var running = true;
		acc = 0;
		int index = 0;
		
		if(instructions[i].type == "nop")
			instructions[i].type = "jmp";
		else if(instructions[i].type == "jmp")
			instructions[i].type = "nop";	
			

		instructions = instructions.Select(i => { i.run = false; return i; }).ToList();
		while (running)
		{			
			if (instructions[index].run)
			{
				running = false;
				break;
			}
			switch (instructions[index].type)
			{
				case "nop":
					instructions[index].run = true;
					index++;
					break;
				case "acc":
					acc += instructions[index].val;
					instructions[index].run = true;
					index++;
					break;
				case "jmp":
					instructions[index].run = true;
					index += instructions[index].val;
					break;
			}
			if (index >= instructions.Count())
			{
				terminated = true;
				running = false;
			}
		}

		if (instructions[i].type == "nop")
			instructions[i].type = "jmp";
		else if (instructions[i].type == "jmp")
			instructions[i].type = "nop";

		if (terminated)
			break;
	}
	
	acc.Dump("Part 2");
}

class Instruction
{
	public string type { get; set; }
	public int val { get; set; }
	public bool run { get; set; }
}