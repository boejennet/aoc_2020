<Query Kind="Program" />

void Main()
{
	string[] input = File.ReadAllLines(@"C:\aoc_2020\day5\input.txt");
	List<int> seatNums = new List<int>();
	
	foreach(var line in input)
	{
		int[] rows = GetFilledArray(128);
		for(int i = 0; i < 7; i++)
		{
			if(line[i] == 'F')
				rows = Lower(rows);
			else
				rows = Upper(rows);
		}
		
		int[] columns = GetFilledArray(8);
		for(int i = 0; i < 3; i++)
		{
			if (line[i + 7] == 'R')
				columns = Upper(columns);
			else
				columns = Lower(columns);
		}
		seatNums.Add((rows[0] * 8) + columns[0]);
	}
	
	//part 1
	seatNums.Max().Dump("Part 1");
	
	//part 2
	seatNums = seatNums.OrderBy(n => n).ToList();
	for(int i = 0; i < seatNums.Count() - 1; i++ )
	{
		if(i > 0)
		{
			if(seatNums[i + 1] - seatNums[i] != 1)
			{
				(seatNums[i + 1] - 1).Dump("Part 2");
			}
		}
	}
}

public int[] Lower(int[] input)
{
	var lenHalf = input.Length/2;
	return input.Take(lenHalf).ToArray();
}

public int[] Upper(int[] input)
{
	var lenHalf = input.Length/2;
	return input[lenHalf..]; 
}

public int[] GetFilledArray(int len)
{
	int[] ret = new int[len];
	for (int i = 0; i < ret.Length; i++)
	{
		ret[i] = i;
	}
	return ret;
}

// You can define other methods, fields, classes and namespaces here
