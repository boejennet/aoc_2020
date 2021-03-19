<Query Kind="Program" />


void Main()
{
	string[] input = File.ReadAllLines(@"C:\aoc_2020\day2\input.txt");
	var rules = GetRules(input);

	rules.Where(r => IsValidTask1(r)).Count().Dump("Part 1");
	rules.Where(r => IsValidTask2(r)).Count().Dump("Part 2");

	bool IsValidTask1(PasswordRule rule)
	{
		var charCount = rule.password.Where(p => p == rule.pChar).Count();
		return charCount >= rule.min && charCount <= rule.max;
	}

	bool IsValidTask2(PasswordRule rule)
	{
		var firstCharMatch = rule.password[rule.min - 1] == rule.pChar;
		var secondCharMatch = rule.password[rule.max - 1] == rule.pChar;
		return (firstCharMatch || secondCharMatch) && !(firstCharMatch && secondCharMatch);
	}

	List<PasswordRule> GetRules(string[] input)
	{
		var rules = new List<PasswordRule>();
		foreach (var line in input)
		{
			var rule = new PasswordRule();

			var ruleNumbers = new Regex(@"[0-9]+");
			var numbers = ruleNumbers.Matches(line);
			rule.min = Int32.Parse(numbers[0].Value);
			rule.max = Int32.Parse(numbers[1].Value);

			var rulePChar = new Regex(@"[a-z]:");
			var pChar = rulePChar.Match(line);
			rule.pChar = pChar.Value.First();

			var rulePassword = new Regex(@": [a-z]+");
			var password = rulePassword.Match(line);
			rule.password = password.Value.Replace(": ", "");

			rules.Add(rule);
		}
		return rules;
	}
}


struct PasswordRule
{
	public int min;
	public int max;
	public char pChar;
	public string password;
}
