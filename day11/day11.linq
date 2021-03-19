<Query Kind="Program" />

//https://adventofcode.com/2020/day/11
int lineLen = 0;
int rowLen = 0;
string seats = "";
string seatsp2 = "";

bool debug = false;

void Main()
{
	string[] input = File.ReadAllLines(@"C:\aoc_2020\day11\input.txt");
	
	seats = string.Join("", input);
	seatsp2 = seats;
	
	lineLen = input[0].Length;
	rowLen = seats.Length / lineLen;
	
	//Part1();
	Part2();
}

void Part2()
{
	debug = false;
	if(debug)
	{
		seatsp2.Length.Dump("seats lenght");
		GetVisibleSeats(8372, 'L').Dump();
		SeatPrint(seatsp2);
		GetVisibleSeats(8372-lineLen+1, 'L').Dump();
	}
	
	var running = !debug;
	while (running)
	{
		var newSeats = "";
		for (int i = 0; i < seatsp2.Length; i++)
		{
			var visibleOccupied = GetVisibleSeats(i + 1, '#');

			if (seatsp2[i] == '.')
				newSeats += '.';
			else if (seatsp2[i] == 'L' && visibleOccupied == 0)
				newSeats += '#';
			else if (seatsp2[i] == '#' && visibleOccupied >= 5)
				newSeats += 'L';
			else
				newSeats += seatsp2[i];
		}

		if (seatsp2 == newSeats)
		{
			running = false;
			break;
		}
		
		seatsp2 = newSeats;		
	}
	
	seatsp2.Count(x => x == '#').Dump("Part 2");	
}

List<int> GetValidVisibleSeats(int n)
{
	var indexes = new List<int>();
	//west/east
	indexes.Add(GetVisibleInDirection(n, -1));
	indexes.Add(GetVisibleInDirection(n, +1));
	//north/south
	indexes.Add(GetVisibleInDirection(n, -lineLen));
	indexes.Add(GetVisibleInDirection(n, +lineLen));
	//north-west/north-east
	indexes.Add(GetVisibleInDirection(n, -(lineLen + 1)));
	indexes.Add(GetVisibleInDirection(n, -(lineLen - 1)));
	//south-west/south-east
	indexes.Add(GetVisibleInDirection(n, +(lineLen - 1)));
	indexes.Add(GetVisibleInDirection(n, +(lineLen + 1)));

	return indexes.Where(x => x != -1).ToList(); ;
}

int GetVisibleInDirection(int n, int direction)
{
	var finished = false;
	var x = n + direction;

	while (!finished)
	{
		if (x > seatsp2.Length || x <= 0)
			return -1;
		if(direction == 1 && (IsRightEdge(n)))
			return -1;
		if (direction == -1 && (IsLeftEdge(n)))
			return -1;
		if(direction == -(lineLen - 1) && (IsRightEdge(n)))
			return -1;
		if (direction == -(lineLen + 1) && (IsLeftEdge(n)))
			return -1;
		if (direction == +(lineLen - 1) && ( IsLeftEdge(n)))
			return -1;
		if (direction == +(lineLen + 1) && ( IsRightEdge(n)))
			return -1;
		if (seatsp2[x - 1] != '.')
			return x;
		x += direction;
	}

	return -1;
}

void Part1()
{
	var running = true;
	while (running)
	{
		var newSeats = "";
		for (int i = 0; i < seats.Length; i++)
		{
			var adjacentOccupied = GetAdjacentCount(i + 1, '#');
			
			if (seats[i] == '.')
				newSeats += '.';
			else if (seats[i] == 'L' && adjacentOccupied == 0)
				newSeats += '#';
			else if (seats[i] == '#' && adjacentOccupied >= 4)
				newSeats += 'L';
			else
				newSeats += seats[i];
		}
		
		if (seats == newSeats)
		{
			running = false;
			break;
		}
		
		seats = newSeats;
	}
	
	seats.Count(x => x == '#').Dump("Part 1");
}

int GetAdjacentCount(int n, char c)
{
	int count = 0;
	var indexes = GetValidAdjacentindexes(n);
	
	foreach (var i in indexes)
	{
		if(seats[i - 1] == c)
			count++;
	}
	
	return count;
}

int GetVisibleSeats(int n, char c)
{
	int count = 0;	
	var indexes = GetValidVisibleSeats(n);
	
	if(debug)
		indexes.Dump();
		
	foreach (var i in indexes)
	{
		if (seatsp2[i - 1] == c)
			count++;
	}

	return count;
}

List<int> GetValidAdjacentindexes(int n)
{
	var indexes = new List<int>();
	
	var isLeftEdge = IsLeftEdge(n);
	var isRightEdge = IsRightEdge(n);
	
	if(!isLeftEdge)
		indexes.Add(n - 1);
	if(!isRightEdge)
		indexes.Add(n + 1);
	indexes.Add(n + lineLen);
	if(!isRightEdge)
		indexes.Add(n + lineLen + 1);
	if(!isLeftEdge)
		indexes.Add(n + lineLen - 1);
	indexes.Add(n - lineLen);
	if(!isRightEdge)
		indexes.Add(n - lineLen + 1);
	if(!isLeftEdge)
		indexes.Add(n - lineLen - 1);
	
	indexes = indexes.Where(x => x <= seats.Length && x >= 1).ToList();

	return indexes;
}

bool IsLeftEdge(int n)
{
	return (n % lineLen == 1);
}

bool IsRightEdge(int n)
{
	return (n % lineLen == 0);
}

void SeatPrint(string toPrint)
{
	Console.WriteLine();
	for (int row = 0; row < rowLen; row++)
	{
		for (int i = 0; i < lineLen; i++)
		{
			var index = i + (row * lineLen);
			Console.Write(toPrint[index]);
		}
		Console.WriteLine();
	}	
}
