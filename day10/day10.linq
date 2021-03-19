<Query Kind="Program" />

//https://adventofcode.com/2020/day/10
void Main()
{
	string[] input = File.ReadAllLines(@"C:\aoc_2020\day10\input.txt");
	List<int> adaptors = input.Select(i => Int32.Parse(i)).ToList();
	
	Part1(adaptors);
	Part2(adaptors);
}

public void Part1(List<int> input)
{
	var adaptors = input.OrderBy(a => a).ToList();
	var currentOutlet = 0;
	var diff1 = 0;
	//we always have +3 at the end so start at 1.
	var diff3 = 1;

	while (adaptors.Count() > 0)
	{
		if (adaptors[0] == (currentOutlet + 1))
			diff1++;
		if (adaptors[0] == (currentOutlet + 3))
			diff3++;

		currentOutlet = adaptors[0];
		adaptors.RemoveAt(0);
	}

	(diff1 * diff3).Dump("Part 1");
}

public void Part2(List<int> input)
{
	var adaptors = input.OrderBy(a => a).ToList();	
	adaptors.Insert(0, 0);
	adaptors.Add(adaptors.Max() + 3);	
	AC(adaptors, 0).Dump("Part 2");
}

public Dictionary<int, long> cache = new Dictionary<int, long>();

public long AC(List<int> list, int n)
{
	if (list.Count() - 1 == n)
		return 1;
	if (cache.ContainsKey(n))
		return cache[n];
	long ans = 0;
	for (int i = n + 1; i < list.Count(); i++)
	{
		if (list[i] - list[n] <= 3)
		{
			ans += AC(list, i);
		}
	}
	cache.Add(n, ans);
	return ans;
}

